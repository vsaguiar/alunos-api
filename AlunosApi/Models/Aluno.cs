using System.ComponentModel.DataAnnotations;

namespace AlunosApi.Models;

public class Aluno
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Nome { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public int Idade { get; set; }
}
