using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Model
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base (options){  }
    public DbSet<ValueModel> Values { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    }
}