using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Services.Email;
using WebApplication1.Strings;
using WebApplication1.Services.TokenService;
using DataLibrary.Models;
using WebApplication1.Utilities.Base64;
namespace WebApplication1.Services.AuthService.NewFolder
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<Users> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IBase64 _base64;

        public AuthService(IEmailService emailService, UserManager<Users> userManager, ITokenService tokenService, IBase64 base64)
        {
            _emailService = emailService;
            _userManager = userManager;
            _tokenService = tokenService;
            _base64 = base64;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Users user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                response.success = false;
                response.message = ErrorStrings.UserNotFound;

            }
            else if (user.EmailConfirmed == true)
            {

                if (!await _userManager.CheckPasswordAsync(user, password))
                {
                    response.success = false;
                    response.message = ErrorStrings.IncorrectPassword;

                }
                else
                {
                    response.success = true;
                    response.data = _tokenService.generateToken(user);
                }
            }
            else
            {
                response.success = false;
                response.message = ErrorStrings.EmailConfirmationMsg_LoginBefore;
            }

            return response;

        }

        public async Task<ServiceResponse<string>> Register(Users user, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Users finduser = await _userManager.FindByEmailAsync(user.Email);
            Users findusername = await _userManager.FindByNameAsync(user.UserName);

            if (finduser != null)
            {
                response.success = false;
                response.message = ErrorStrings.UserAlreadyExist;
                return response;
            }
            if (findusername != null)
            {
                response.success = false;
                response.message = ErrorStrings.UserAlreadyExist;
                return response;
            }
             IdentityResult result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    response.success = false;
                    response.message = ErrorStrings.InvalidPassword;
                    return response;
                }

                else
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string tokenEncode = _base64.Base64Encode(token);
                    string confirmationLink = $"https://localhost:7124/api/Auth/confirm-email?id={user.Id}&token={tokenEncode}";
                    await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>;.", true);
                }

            response.message = ErrorStrings.EmailConfirmation;
            response.data = user.Id;
            return response;

        }

        public async Task<ServiceResponse<string>> ConfirmEmail(string id, string token)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            string id_ = _base64.Base64Encode(id);
            string token_ = _base64.Base64Encode(token);

            Users user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                response.success = false;
                response.message = ErrorStrings.UserNotFound;
                return response;
            }

            try
            {
                string tokenDecode = _base64.Base64Decode(token);
                IdentityResult tokenValid = await _userManager.ConfirmEmailAsync(user, tokenDecode);
                if (tokenValid.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    response.success = true;
                    response.message = ErrorStrings.EmailSuccess;
                }
                else
                {
                    response.success = false;
                    response.message = ErrorStrings.InvalidToken;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }



            return response;
        }

    }
}
