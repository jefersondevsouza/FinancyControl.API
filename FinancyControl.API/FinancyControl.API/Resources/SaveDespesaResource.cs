using System.ComponentModel.DataAnnotations;

namespace FinancyControl.API.Resources
{
    public record SaveDespesaResource
    {
        [Required]
        [MaxLength(50)]
        public string? Descricao { get; init; }

        [Required]
        [Range(0, 10000)]
        public decimal Valor { get; init; }

        [Required]
        public int TipoDespesaId { get; init; }
    }
}
