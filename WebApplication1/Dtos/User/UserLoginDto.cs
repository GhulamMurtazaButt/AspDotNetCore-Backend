using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.User
{
    public class UserLoginDto
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }
        public string password { get; set; }

    }
}
