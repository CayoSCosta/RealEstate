using AutoMapper;
using Imobi.Domain.Enum;
using Imobi.Domain.Models;
using Imobi.Domain.Models.Util;
using Imobi.MVC.ViewModels;
using Imobi.MVC.ViewModels.Util;

namespace Imobi.MVC.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // --- ENTIDADES BÁSICAS ---
            CreateMap<Empreendimento, EmpreendimentoViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Arquivo, ArquivoViewModel>().ReverseMap();

            // --- UNIDADES ---
            CreateMap<Unidade, UnidadeViewModel>()
                .ForMember(dest => dest.Empreendimento, opt => opt.MapFrom(src => src.Empreendimento))
                .ReverseMap();

            // --- IMAGENS ---
            CreateMap<Imagem, ImagemViewModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Tipo) || src.Tipo == "Galeria"
                        ? TipoImagemEmpreendimentoEnum.Fachada
                        : Enum.Parse<TipoImagemEmpreendimentoEnum>(src.Tipo)))
                .ReverseMap()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));

            // --- FILTROS E BUSCA (Resolvendo Ambiguidades) ---
            CreateMap<Imobi.MVC.ViewModels.Util.FilterOperator, Imobi.Domain.Models.Util.FilterOperator>();

            CreateMap<FilterItem, FilterItemDomain>();

            CreateMap<QueryParameters, SearchParametersDomain>();

            // --- PAGINAÇÃO GENÉRICA ---
            CreateMap(typeof(PagedResult<>), typeof(PagedResultViewModel<>));
        }
    }
}