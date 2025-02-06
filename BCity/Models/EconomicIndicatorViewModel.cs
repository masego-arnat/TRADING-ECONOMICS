namespace C_City.Models
{
    public class EconomicIndicatorViewModel
    {
        public string Country { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public double LatestValue { get; set; }
        //public double q1 { get; set; }
        //public double q2 { get; set; }
        //public double q3 { get; set; }
        //public double q4 { get; set; }
        public double? q1 { get; set; } // Nullable double
        public double? q2 { get; set; } // Nullable double
        public double? q3 { get; set; } // Nullable double
        public double? q4 { get; set; } // Nullable double
        public string Frequency { get; set; }
        public string Unit { get; set; }
    }
}
