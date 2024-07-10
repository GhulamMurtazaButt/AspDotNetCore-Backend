using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.User
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string username { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }
        public string password { get; set; }

    }
}
