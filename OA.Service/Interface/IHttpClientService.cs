namespace OA.Service.Interface
{
    public interface IHttpClientService
    {

        Task<string> SendGetRequestAsync(string url, string baseURL = "https://api.tradingeconomics.com/");

    }
}
