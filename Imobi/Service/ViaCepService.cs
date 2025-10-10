using Imobi.Models;
using System.Text.Json;

namespace Imobi.Service;

public class ViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ViaCepResponse?> BuscarEnderecoPorCep(string cep)
    {
        cep = cep.Replace("-", "").Trim();

        var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var endereco = JsonSerializer.Deserialize<ViaCepResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return endereco?.Cep == null ? null : endereco;
    }
}
