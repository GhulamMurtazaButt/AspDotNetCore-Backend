using DataLibrary.Models;

namespace WebApplication1.Services.TokenService
{
    public interface ITokenService
    {
        public string generateToken(Users user);
    }
}
