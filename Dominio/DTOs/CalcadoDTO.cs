namespace Minimal.Dominio.DTOs;

public record CalcadoDTO
{

    public string Marca { get; set; } = default!;

    public string Modelo { get; set; } = default!;

    public decimal Preco { get; set; } = default!;

    public int Tamanho { get; set; } = default!;
}