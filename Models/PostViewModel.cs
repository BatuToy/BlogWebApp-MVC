using AppBlog.Entity;

namespace AppBlog.Models;

public class PostViewModel{
    public List<Post>? Posts { get; set; } = new();
    public List<Tag>? Tags { get; set; } = new();
}