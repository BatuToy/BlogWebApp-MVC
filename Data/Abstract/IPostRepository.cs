using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;

namespace AppBlog.Data.Abstract;

public interface IPostRepository 
{
    IQueryable<Post> Posts { get; }

    void CreatePost(Post post);


}
