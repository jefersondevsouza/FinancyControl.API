using AutoMapper;
using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;
using FinancyControl.API.Resources;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancyControl.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveTipoDespesaResource, TipoDespesa>()
                .ForMember(src => src.ClassificacaoDespesa, opt => opt.MapFrom(src => (ClassificacaoDespesaEnum)src.ClassificacaoDespesa));

            CreateMap<SaveDespesaResource, Despesa>();
            CreateMap<DespesaQueryResource, DespesaQuery>();
        }
    }
}
