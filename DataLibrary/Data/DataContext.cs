using DataLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataLibrary
{
    public class DataContext : IdentityDbContext<Users>
    {
        //DbContextOptions caries the database string
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Users> AspNetUsers { get; set; }
        public DbSet<Products> Products { get; set; }   
    }
}
