namespace FinancyControl.API.Resources
{
    public class DespesaResource
    {
        public required int Id { get; init; }
        public required string Descricao { get; init; }
        public required decimal Valor { get; init; }
        public TipoDespesaResource? TipoDespesa { get; init; }
    }
}
