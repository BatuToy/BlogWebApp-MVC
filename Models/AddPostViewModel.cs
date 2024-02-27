using System.ComponentModel.DataAnnotations;

namespace AppBlog.Models;

public class AddPostViewModel 
{
    public int PostId { get; set; }
    public bool isActive { get; set; }
    [Required]
    [Display(Name = "Başlık")]
    public string? Title { get; set; }

    [Required]
    [Display(Name = "İçerik")]
    public string?  Content { get; set; }

    [Required]
    [Display(Name = "Açıklama")]
    public string?  Description { get; set; }

    [Required]
    [Display(Name = "Url")]
    public string?  Url { get; set; }

}