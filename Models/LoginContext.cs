using Microsoft.EntityFrameworkCore;

namespace LoginRegistration.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
 