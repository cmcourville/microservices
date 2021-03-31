using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace frontend
{
    public class TaxClient
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient _client;

        public TaxClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<TaxCalculationResult> GetTaxAsync()
        {
            var responseMessage = await _client.GetAsync("/taxcalculation");
            var stream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TaxCalculationResult>(stream, _options);

        }
    }
}
