﻿using C_City.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OA.Service.Implementation;
using OA.Service.Interface;
using System.Net.Http.Headers;

namespace C_City.Controllers
{
    public class ForecastController : Controller
    {
        private readonly IHttpClientService _stockMarketService;
        private readonly string _clientKey;
        private readonly HttpClient _httpClient;
        static string clientKeyS = "guest:guest";
        private readonly ILogger<HttpClientService> _logger;
        public ForecastController(
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
        public async Task<JsonResult> GetEconomicIndicators(string countries)
        {
            //mexico seems to be the only xountry that works for this end point 
            string url = $"https://api.tradingeconomics.com/forecast/country/{countries}?c={_clientKey}";
            //string url = $"https://api.tradingeconomics.com/markets/stocks/country/{countries}?client={_clientKey}";

            //var requestUrl = $"https://api.tradingeconomics.com/forecast/country/{countries}/indicator/gdp,population?c=guest:guest";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var indicators = JsonConvert.DeserializeObject<List<EconomicIndicatorViewModel>>(jsonResponse);
                return Json(indicators);
            }

            // Handle error response
            return Json(new { error = "Failed to fetch data from the API." });
        }

        [HttpPost]
        public async Task<IActionResult> GetEconomicData(string country)
        {
            try
            {


                //mexico seems to be the only xountry that works for this end point 
                string url = $"https://api.tradingeconomics.com/forecast/country/{country}?c={_clientKey}";
                //string url = $"https://api.tradingeconomics.com/markets/stocks/country/{countries}?client={_clientKey}";

                //var requestUrl = $"https://api.tradingeconomics.com/forecast/country/{countries}/indicator/gdp,population?c=guest:guest";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var indicators = JsonConvert.DeserializeObject<List<EconomicIndicatorViewModel>>(jsonResponse);

                    var processedIndicators = indicators.Select(indicator => new EconomicIndicatorViewModel
                    {
                        Country = indicator.Country,
                        Category = indicator.Category,
                        Title = indicator.Title,
                        LatestValue = indicator.LatestValue,
                        q1 = indicator.q1 ?? 0, // Replace null with 0
                        q2 = indicator.q2 ?? 0,
                        q3 = indicator.q3 ?? 0,
                        q4 = indicator.q4 ?? 0,
                        Frequency = indicator.Frequency,
                        Unit = indicator.Unit
                    }).ToList();

                    return Json(processedIndicators); // Return the processed list

                }

                //// Handle error response
                //return Json(new { error = "Failed to fetch data from the API." });
                //// Call your external API here using the country parameter
                //var apiUrl = $"https://example.com/api/data?country={country}"; // Replace with your API URL
                //var response = await _httpClient.GetStringAsync(apiUrl);

                //// Deserialize the response into a list of objects
                //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(response);
                // Handle error response
                //return Json(new { error = "Failed to fetch data from the API." });
                // Return an empty list if the response is not successful
                return Json(new List<EconomicIndicatorViewModel>());
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
