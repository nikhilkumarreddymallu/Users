using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
            
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
