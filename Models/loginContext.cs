using Microsoft.EntityFrameworkCore;
using login.Models;

namespace login.Models
{
  public class loginContext : DbContext
  {
    // base() calls the parent class' constructor passing the "options" parameter along
    public loginContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users {get;set;}
  }
}
