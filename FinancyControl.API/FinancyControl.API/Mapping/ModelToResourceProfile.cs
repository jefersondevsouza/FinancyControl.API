using AutoMapper;
using FinancyControl.API.Domain.Models;
using FinancyControl.API.Domain.Models.Queries;
using FinancyControl.API.Resources;
using FinancyControl.API.Extension;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancyControl.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<TipoDespesa, TipoDespesaResource>().
                ForMember(src => src.ClassificacaoDespesa,
                           opt => opt.MapFrom(src => src.ClassificacaoDespesa.ToDescriptionString()));

            CreateMap<Despesa, DespesaResource>();

            CreateMap<QueryResult<Despesa>, QueryResultResource<DespesaResource>>();
        }
    }
}
