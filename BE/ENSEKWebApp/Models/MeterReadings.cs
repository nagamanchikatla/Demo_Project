namespace ENSEKWebApp.Models
{
    public class MeterReadings
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string MeterReadingDate { get; set; }
        public int MeterReadValue { get; set; }        

    }
}
