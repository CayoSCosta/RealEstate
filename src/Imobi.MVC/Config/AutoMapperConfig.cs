using AutoMapper;
using Imobi.Domain.Models;
using Imobi.MVC.Models.ViewModels;

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

        }
    }
}
