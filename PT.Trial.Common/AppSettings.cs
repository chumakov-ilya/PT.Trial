namespace PT.Trial.Common
{
    public class AppSettings
    {
        public string BusConnectionString { get; set; } = "host=localhost";
        public string WebConnectionString { get; set; } = "http://localhost:42424";

        public int MaxNumberCount { get; set; } = 2000;
    }
}