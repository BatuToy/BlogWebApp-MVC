using AppBlog.Data.Abstract;
using AppBlog.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog.Data.Concrete.EfCore;

public class EfUserRepository : IUserRepository
{
   private readonly DataContext _context;

    public EfUserRepository(DataContext context)
    {
        _context = context;
    }
   
   
   public IQueryable<User> Users => _context.Users;

    public async Task<User> CreateUser([FromBody] User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return (user);
    }
}
