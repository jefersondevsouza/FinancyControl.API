using Microsoft.AspNetCore.Identity;

namespace FinancyControl.API.Domain.Models
{
    public class TipoDespesa
    {
        public int Id { get; set; }
        public string Name { get; set; } = default(string)!;
        public ClassificacaoDespesaEnum ClassificacaoDespesa { get; set; } = default(ClassificacaoDespesaEnum)!;
        public List<Despesa> Despesas { get; set; } = new();
    }
}
