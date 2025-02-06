using BCity.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OA.Service.Implementation;
using OA.Service.Interface;
using System.Net.Http.Headers;

namespace BCity.Controllers
{
    public class StockMarketController : Controller
    {
        private readonly IHttpClientService _stockMarketService;
        private readonly string _clientKey;
        private readonly HttpClient _httpClient;
        static string clientKeyS = "guest:guest";
        private readonly ILogger<HttpClientService> _logger;
        public StockMarketController(
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


        public IActionResult Index()
        {

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> CompareStocks(string country1, string country2)
        {
            try
            {
                // Asynchronously fetch stock data for the specified country.
                // The 'country1 & country2' variable contains the name of the country for which stock data is requested.
                // The result is stored in 'data1 & data2' for further processing.
                var data1 = await GetStockData(country1);
                var data2 = await GetStockData(country2);


                //var getforcastByCountry = GetforecastByCountry(country1).Result;
                //Console.WriteLine(getforcastByCountry);

                var model = new StockComparisonViewModel
                {
                    Country1 = country1,
                    Country2 = country2,
                    Data1 = data1,
                    Data2 = data2
                };

                return Json(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



        }


        public async Task<List<StockInfo>> GetStockData(string country)
        {
            try
            {

                string url = $"https://api.tradingeconomics.com/markets/stocks/country/{country}?client={_clientKey}";
                var response = await _httpClient.GetStringAsync(url);
                if (response is null)
                {
                    // Handle the null case, e.g., return an empty list or throw an exception
                    return new List<StockInfo>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<StockInfo>>(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
        public async Task<string> GetForecastsByCountriesIndicators(string country)
        {
            try
            {
                //https://api.tradingeconomics.com/forecast/updates?country=albania&c={_clientKey}
                string url = $"https://api.tradingeconomics.com/markets/forecast/updates/country={country}c={_clientKey}";
                var response = await _httpClient.GetStringAsync(url);
                if (response is null)
                {
                    // Handle the null case, e.g., return an empty list or throw an exception
                    return response;
                }
                else
                {
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }


        public async static Task<string> GetEarningsByCountry(string country)
        {

            return await HttpRequester($"/earnings/country/{country}");
        }


        public async static Task<string> GetforecastByCountry(string country1)
        {



            return await HttpRequester($"/forecast/country/{country1}");
        }
        public async static Task<string> GetforecastByCountryIndicator(string country1)
        {

            // Build the API URL dynamically based on the countries parameter
            //var requestUrl = $"https://api.tradingeconomics.com/forecast/country/{country1}/indicator/gdp,population?c=guest:guest";
            var requestUrl = $"https://api.tradingeconomics.com/markets/forecasts/index?c=your_api_key?c=guest:guest";
            var content = "";
            //new HttpRequestMessage(new HttpMethod("GET"), "https://api.tradingeconomics.com/forecast/country/mexico,sweden?c=guest:guest");
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), requestUrl))
                //using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.tradingeconomics.com/forecast/country/mexico,sweden/indicator/gdp,population?c=guest:guest"))

                //using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.tradingeconomics.com/forecast/country/mexico?c=guest:guest"))
                {
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);

                    }
                }
            }

            return content;
            //return await HttpRequester($"/forecast/country/{country1},{country2}/indicator/gdp,population?c=guest:guest");
        }

        public async static Task<string> GetMarketIndex()
        {

            return await HttpRequester($"/markets/index");
        }

        public async static Task<string> HttpRequester(string url, string baseURL = "https://api.tradingeconomics.com/")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Clear();

                    //ADD Acept Header to tell the server what data type you want
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                    //ADD Authorization
                    AuthenticationHeaderValue auth = new AuthenticationHeaderValue("Client", clientKeyS);
                    client.DefaultRequestHeaders.Authorization = auth;

                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                return $"Error at HttpRequester with msg: {e}";
            }
        }
        //public IActionResult OnGetPartial()
        //{
        //    return Partial("_AuthorPartialRP");
        //}



    }
}

