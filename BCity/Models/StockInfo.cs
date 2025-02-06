namespace BCity.Models
{
    public class StockInfo
    {
        public string Symbol { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public decimal Last { get; set; }
        public decimal Close { get; set; }
        public decimal DailyChange { get; set; }
        public decimal DailyPercentualChange { get; set; }
    }
}
