using AutoMapper;
using Imobi.Domain.Enum;
using Imobi.Domain.Models;
using Imobi.MVC.ViewModels;

namespace Imobi.MVC.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Empreendimento, EmpreendimentoViewModel>().ReverseMap();

            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();

            CreateMap<Arquivo, ArquivoViewModel>().ReverseMap();

            CreateMap<Unidade, UnidadeViewModel>().ReverseMap();
            CreateMap<Unidade, UnidadeViewModel>()
                    .ForMember(dest => dest.Empreendimento, opt => opt.MapFrom(src => src.Empreendimento))
                    .ReverseMap();


            CreateMap<ImagemViewModel, Imagem>();
            CreateMap<Imagem, ImagemViewModel>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Tipo) || src.Tipo == "Galeria"
                        ? TipoImagemEmpreendimentoEnum.Fachada
                        : Enum.Parse<TipoImagemEmpreendimentoEnum>(src.Tipo)))
                .ReverseMap()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));
        }
    }
}
