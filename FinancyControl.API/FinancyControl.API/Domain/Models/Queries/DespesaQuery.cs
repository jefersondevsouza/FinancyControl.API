namespace FinancyControl.API.Domain.Models.Queries
{
    public class DespesaQuery : Query
    {
        public int? TipoDespesaId { get; set; }

        public DespesaQuery(int? tipoDespesaId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            TipoDespesaId = tipoDespesaId;
        }
    }
}
