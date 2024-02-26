using System.ComponentModel.DataAnnotations;

namespace AppBlog.Models;

public class RegisterViewModel 
{

    [Required]
    [Display(Name = "Kullanıcı Adı")]
    public  string? UserName { get; set; }

    [Required]
    [Display(Name = "Ad Soyad")]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "E-Mail")]
    public string? Email { get; set; }

    [StringLength(10 , ErrorMessage = "{0} alanı en az {2} karakter uzunluğa sahip olmalıdır." , MinimumLength = 6)]
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [Required]
    [Compare(nameof(Password) , ErrorMessage = "Parola eşleşmemekte!")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? ConfirmPassword { get; set; }
}