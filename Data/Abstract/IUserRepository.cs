using AppBlog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog.Data.Abstract;

public interface IUserRepository
{
    IQueryable<User> Users { get; }

    Task<User> CreateUser([FromBody] User user);
}
