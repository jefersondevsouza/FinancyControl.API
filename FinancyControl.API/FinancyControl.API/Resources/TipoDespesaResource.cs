namespace FinancyControl.API.Resources
{
    public record TipoDespesaResource
    {
        public required int Id { get; init; }
        public required string Name { get; init; }

        public required string ClassificacaoDespesa { get; init; }
    }
}
