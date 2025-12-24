using Imobi.Domain.Interfaces;
using Imobi.Domain.Models;
using Imobi.Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Imobi.Service.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepo;
        private readonly HttpClient _httpClient;

        public EnderecoService(IEnderecoRepository enderecoRepo, HttpClient httpClient)
        {
            _enderecoRepo = enderecoRepo;
            _httpClient = httpClient;
        }

        public async Task<Endereco?> BuscarEnderecoPorCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return null;

            var cepLimpo = cep.Replace("-", "").Replace(".", "").Trim();

            if (cepLimpo.Length != 8) return null;

            try
            {
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepLimpo}/json/");

                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                var viaCepDto = JsonSerializer.Deserialize<ViaCepResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (viaCepDto == null || viaCepDto.Erro) return null;

                return new Endereco
                {
                    Cep = viaCepDto.Cep ?? cep,
                    Logradouro = viaCepDto.Logradouro ?? string.Empty,
                    Bairro = viaCepDto.Bairro ?? string.Empty,
                    Cidade = viaCepDto.Localidade ?? string.Empty,
                    Uf = viaCepDto.Uf ?? string.Empty,
                    Complemento = viaCepDto.Complemento ?? string.Empty
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<Endereco?> ObterPorId(Guid id)
        {
            return await _enderecoRepo.ObterPorId(id);
        }

        public async Task Atualizar(Endereco endereco)
        {
            await _enderecoRepo.Atualizar(endereco);
        }

        public void Dispose()
        {
            _enderecoRepo?.Dispose();
            _httpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
