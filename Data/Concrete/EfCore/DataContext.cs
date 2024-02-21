using AppBlog.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Data.Concrete.EfCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }






    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments  => Set<Comment>();
    public DbSet<Tag> Tags => Set<Tag>();


}
