using BCity.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OA.Service.Implementation;
using System.Net.Http.Headers;

namespace C_City.Controllers
{
    public class ChangesController : Controller
    {

        private readonly string _clientKey;
        private readonly HttpClient _httpClient;
        static string clientKeyS = "guest:guest";
        private readonly ILogger<HttpClientService> _logger;
        public ChangesController(
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



        [HttpGet]
        public async Task<IActionResult> GetchangesData()
        {
            try
            {



                string url = $"https://api.tradingeconomics.com/changes?c={_clientKey}";


                var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var indicators = JsonConvert.DeserializeObject<List<ComparisonResult>>(jsonResponse);


                    return Json(indicators); // Return the processed list

                }

                return Json(new List<ComparisonResult>());
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
