namespace PT.Trial.Common
{
    public class AppSettings
    {
        public string BusSubscriptionId { get; set; } = "test";
        public string BusConnectionString { get; set; } = "host=localhost";
        public string WebConnectionString { get; set; } = "http://localhost:42424";

        public int MaxNumberCount { get; set; } = 500;
        public int DefaultCalculationsCount { get; set; } = 10;

    }
}