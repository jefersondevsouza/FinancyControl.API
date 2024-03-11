using FinancyControl.API.Domain.Models;
using System;

namespace FinancyControl.API.Persistence.Contexts
{
    public class SeedData
    {
        public static async Task Seed(AppDbContext context)
        {
            var tiposDespesas = new List<TipoDespesa>
            {
                new() { Id = 100, Name = "Financiamentos", ClassificacaoDespesa = ClassificacaoDespesaEnum.GastoFixo }, // Id set manually due to in-memory provider
                new() { Id = 101, Name = "Lazer", ClassificacaoDespesa = ClassificacaoDespesaEnum.Lazer }
            };

            var despesas = new List<Despesa>
            {
                new() {
                    Id = 100,
                    Descricao = "Financiamento carro",
                    Valor = 1500,
                    TipoDespesaId = 100
                },
                new() {
                    Id = 101,
                    Descricao = "Mensalidade Clube",
                    Valor = 190,
                    TipoDespesaId = 101
                }
            };

            context.TiposDespesas.AddRange(tiposDespesas);
            context.Despesas.AddRange(despesas);

            await context.SaveChangesAsync();
        }
    }
}
