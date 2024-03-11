namespace FinancyControl.API.Resources
{
    public record DespesaQueryResource : QueryResource
    {
        public int? TipoDespesaId { get; init; }
    }
}
