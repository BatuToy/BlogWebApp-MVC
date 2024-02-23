using System.ComponentModel.DataAnnotations;

namespace AppBlog.Entity;

public enum TagColors {
    primary ,
    secondary ,
    danger ,
    success ,
    warning 
}

public class Tag
{
    
    public int TagId { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }
    public TagColors Color { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}
