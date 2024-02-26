using System.ComponentModel.DataAnnotations;

namespace AppBlog.Models;

public class LoginViewModel 
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-Mail")]
    public string? Email { get; set; }

    [StringLength(10 , ErrorMessage = "{0} alanı en az {2} karakter uzunluğa sahip olmalıdır." , MinimumLength = 6)]
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }
}