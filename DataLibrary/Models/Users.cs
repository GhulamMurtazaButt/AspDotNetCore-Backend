using Microsoft.AspNetCore.Identity;

namespace DataLibrary.Models
{
    public class Users : IdentityUser
    {
        public string Name { get; set; }

    }
}
