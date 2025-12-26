using System.ComponentModel.DataAnnotations;

namespace Imobi.Domain.Enum
{
    public enum EstagioObraEnum
    {
        [Display(Name = "Breve Lançamento")]
        BreveLancamento = 1,

        [Display(Name = "Lançamento")]
        Lancamento = 2,

        [Display(Name = "Em Obras")]
        EmObras = 3,

        [Display(Name = "Pronto para Morar")]
        Pronto = 4
    }
}
