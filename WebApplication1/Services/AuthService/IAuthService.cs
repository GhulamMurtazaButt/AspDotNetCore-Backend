using WebApplication1.Dtos.User;
using WebApplication1.Models;
using DataLibrary.Models;
namespace WebApplication1.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(Users user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<string>> ConfirmEmail(string id, string token);
       
    }
}

