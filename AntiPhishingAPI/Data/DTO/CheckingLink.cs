namespace AntiPhishingAPI.Data.DTO
{
    public class CheckingLink
    {
        public string Link { get;set; }
        public double Dangerousity { get; set; } = default(double);
    }
}
