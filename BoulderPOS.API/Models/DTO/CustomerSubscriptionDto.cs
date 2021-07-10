namespace BoulderPOS.API.Models.DTO
{
    public class CustomerSubscriptionDto
    {
        public int CustomerId { get; set; }
        
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public bool AutoRenewal { get; set; }
        
    }
}
