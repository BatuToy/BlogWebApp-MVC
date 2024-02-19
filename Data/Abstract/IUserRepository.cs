using AppBlog.Entity;

namespace AppBlog.Data.Abstract;

public interface IUserRepository
{
    IQueryable<User> Users { get; }

    void GetUsers();
}
