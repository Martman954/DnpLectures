using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app_db.db");
    }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<User>().HasData(
    //         new User { Id = 0, Username = "admin", Password = "admin123" }
    //     );        
    //     modelBuilder.Entity<Post>().HasData(
    //         new Post { Id = 0, Title = "SampleText", Body = "Lorum ipsum ipusm", UserId = 0}
    //     );        
    //     modelBuilder.Entity<Comment>().HasData(
    //         new Comment { Id = 0, Body = "admin",PostId = 0, UserId = 0}
    //     );
    // }

}