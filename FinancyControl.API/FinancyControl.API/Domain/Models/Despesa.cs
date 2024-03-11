using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FinancyControl.API.Domain.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public int TipoDespesaId { get; set; }
        public TipoDespesa? TipoDespesa { get; set; }
    }
}
