using AppBlog.Data.Abstract;
using AppBlog.Entity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AppBlog.Data.Concrete.EfCore;

public class EfUserRepository : IUserRepository
{
   private readonly DataContext _context;

    public EfUserRepository(DataContext context)
    {
        _context = context;
    }
   
   
   public IQueryable<User> Users => _context.Users;

    public void GetUsers()  {}
}
