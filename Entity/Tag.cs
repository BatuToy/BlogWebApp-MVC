using System.ComponentModel.DataAnnotations;

namespace AppBlog.Entity;

public class Tag
{
    
    public int TagId { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }

    public List<Post> Posts { get; set; } = new List<Post>();
}
