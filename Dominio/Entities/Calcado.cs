using System.ComponentModel.DataAnnotations;

namespace Minimal.Dominio.Entities;

public class Calcado
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Marca { get; set; } = default!;

    [Required]
    [StringLength(20)]
    public string Modelo { get; set; } = default!;

    [Required]
    public decimal Preco { get; set; } = default!;

    [Required]
    public int Tamanho { get; set; } = default!;
}