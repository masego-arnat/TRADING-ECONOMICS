namespace BCity.Models
{
    public class StockComparisonViewModel
    {
        public string Country1 { get; set; }
        public string Country2 { get; set; }
        public List<StockInfo> Data1 { get; set; }
        public List<StockInfo> Data2 { get; set; }
    }
}
