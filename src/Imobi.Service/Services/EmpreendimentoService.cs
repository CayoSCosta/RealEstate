using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Domain.Models.Util;

namespace Imobi.Application.Services
{
    public class EmpreendimentoService : IEmpreendimentoService
    {
        private readonly IEmpreendimentoRepository _empreendimentoRepo;
        private readonly IImagemRepository _imagemRepo;

        public EmpreendimentoService(IEmpreendimentoRepository empreendimentoRepo,
                                     IImagemRepository imagemRepo)
        {
            _empreendimentoRepo = empreendimentoRepo;
            _imagemRepo = imagemRepo;
        }

        public async Task<IEnumerable<Empreendimento>> ObterTodos() => await _empreendimentoRepo.ObterTodos();

        public async Task<Empreendimento?> ObterPorId(Guid id) => await _empreendimentoRepo.ObterPorId(id);

        public async Task<Empreendimento?> ObterComDetalhes(Guid id) => await _empreendimentoRepo.ObterComDetalhesAsync(id);

        public async Task<PagedResult<Empreendimento>> ObterPaginado(SearchParametersDomain parameters)
        {
            return await _empreendimentoRepo.ObterPaginado(parameters, x => x.Endereco);
        }
        public async Task Adicionar(Empreendimento empreendimento, List<Imagem> imagensDoForm)
        {
            if (imagensDoForm != null && imagensDoForm.Any())
            {
                foreach (var img in imagensDoForm)
                {
                    empreendimento.Imagens.Add(new Imagem
                    {
                        Tipo = img.Tipo,
                        Base64 = img.Base64,
                        Legenda = img.Legenda,
                        Ordem = img.Ordem,
                        EmpreendimentoId = empreendimento.Id
                    });
                }
            }

            await _empreendimentoRepo.Adicionar(empreendimento);
        }

        public async Task Atualizar(Empreendimento empreendimento, List<Imagem> imagensDoForm)
        {
            var atual = await _empreendimentoRepo.ObterComDetalhesAsync(empreendimento.Id);
            if (atual == null) return;

            atual.Nome = empreendimento.Nome;
            atual.Sobre = empreendimento.Sobre;
            atual.Status = empreendimento.Status;
            atual.Estagio = empreendimento.Estagio;
            atual.AreaConstruida = empreendimento.AreaConstruida;
            atual.EnderecoId = empreendimento.EnderecoId;

            await _empreendimentoRepo.Atualizar(atual);

            var idsNoForm = imagensDoForm.Select(x => x.Id).ToList();
            var imagensNoBanco = atual.Imagens.ToList();

            foreach (var imgDb in imagensNoBanco)
            {
                if (!idsNoForm.Contains(imgDb.Id))
                {
                    atual.Imagens.Remove(imgDb);
                }
            }

            foreach (var imgForm in imagensDoForm)
            {
                var imgDb = imagensNoBanco.FirstOrDefault(x => x.Id == imgForm.Id);

                if (imgDb == null || imgForm.Id == Guid.Empty)
                {
                    var novaImg = new Imagem
                    {
                        Id = Guid.Empty,
                        Base64 = imgForm.Base64,
                        Legenda = imgForm.Legenda,
                        Ordem = imgForm.Ordem,
                        Tipo = imgForm.Tipo,
                        EmpreendimentoId = atual.Id
                    };
                    atual.Imagens.Add(novaImg);
                }
                else
                {
                    imgDb.Legenda = imgForm.Legenda;
                    imgDb.Ordem = imgForm.Ordem;
                    imgDb.Tipo = imgForm.Tipo;

                    if (!string.IsNullOrEmpty(imgForm.Base64) && imgForm.Base64.StartsWith("data:image"))
                    {
                        imgDb.Base64 = imgForm.Base64;
                    }
                }
            }

            await _empreendimentoRepo.Atualizar(atual);
        }

        public async Task Remover(Guid id)
        {
            await _empreendimentoRepo.Remover(id);
        }

        public async Task RemoverImagem(Guid imagemId)
        {
            await _imagemRepo.Remover(imagemId);
        }

        private async Task AtualizarCaracteristicasEmpreendimento(Guid empreendimentoId)
        {
            var empreendimento = await _empreendimentoRepo.ObterComDetalhesAsync(empreendimentoId);
            if (empreendimento == null || !empreendimento.Unidades.Any()) return;

            empreendimento.AreaConstruida = empreendimento.Unidades.Min(u => u.AreaConstruida);

            await _empreendimentoRepo.Atualizar(empreendimento);
        }

        public void Dispose()
        {
            _empreendimentoRepo?.Dispose();
            _imagemRepo?.Dispose();
        }
    }
}