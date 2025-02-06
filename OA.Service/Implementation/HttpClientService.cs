using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace OA.Service.Implementation
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientKey;
        private readonly ILogger<HttpClientService> _logger;
        public HttpClientService(
       HttpClient httpClient,
       IConfiguration configuration,
       ILogger<HttpClientService> logger)
        {
            _httpClient = httpClient;
            _clientKey = configuration["ApiKeys:TradingEconomics"];
            _logger = logger;

            // Configure base settings
            _httpClient.BaseAddress = new Uri("https://api.tradingeconomics.com/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client", _clientKey);
        }

        public async Task<string> SendGetRequestAsync(string url, string baseURL = "https://api.tradingeconomics.com/")
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error making HTTP request to {url}");
                throw; // or handle as needed
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error in HTTP request to {url}");
                throw;
            }
        }


        public async Task<JObject> GetStockData(string country)
        {
            string url = $"https://api.tradingeconomics.com/markets/stocks/country/{country}?client=guest:guest";
            var response = await _httpClient.GetStringAsync(url);
            return JObject.Parse(response);
        }


        public async Task CompareStockData(string country1, string country2)
        {
            var data1 = await GetStockData(country1);
            var data2 = await GetStockData(country2);

            // Add your comparison logic here
            // For example, you can print the data or perform some analysis
            Console.WriteLine($"{country1} Stock Data:");
            Console.WriteLine(data1);

            Console.WriteLine($"\n{country2} Stock Data:");
            Console.WriteLine(data2);

            //return new StockComparisonViewModel
            //{
            //    Country1 = country1,
            //    Country2 = country2,
            //    Data1 = data1,
            //    Data2 = data2
            //};
        }
    }
}
