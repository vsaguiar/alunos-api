using System.ComponentModel.DataAnnotations;

namespace AlunosApi.ViewModels;

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }


    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    [DataType(DataType.Password)]
    [Display(Name = "Confirmar senha.")]
    [Compare("Password", ErrorMessage = "Senhas não conferem.")]
    public string ConfirmPassword { get; set; }
}
