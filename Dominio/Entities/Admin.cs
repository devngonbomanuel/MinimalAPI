using System.ComponentModel.DataAnnotations;

namespace Minimal.Dominio.Entities;

public class Admin
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(8)]
    public string Senha { get; set; } = default!;

    [Required]
    [StringLength(20)]
    public string Perfil { get; set; } = default!;
}