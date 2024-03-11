using System.ComponentModel.DataAnnotations;

namespace FinancyControl.API.Resources
{
    public record SaveTipoDespesaResource
    {
        [Required]
        [MaxLength(30)]
        public string? Descricao { get; init; }

        [Required]
        [Range(1, 5)]
        public int ClassificacaoDespesa { get; init; }
    }
}
