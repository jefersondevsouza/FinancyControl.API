using System.ComponentModel;

namespace FinancyControl.API.Domain.Models
{
    public enum ClassificacaoDespesaEnum
    {
        [Description("GF")]
        GastoFixo = 1,
        [Description("LF")]
        LiberdadeFinanceira = 2,
        [Description("SO")]
        Sonhos = 3,
        [Description("LA")]
        Lazer = 4,
        [Description("CO")]
        Conhecimento = 5
    }
}
