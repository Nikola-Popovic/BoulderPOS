using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BoulderPOS.API.Models
{
    public class CustomerSubscription
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public bool AutoRenewal { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public CustomerSubscription(int customerId)
        {
            CustomerId = customerId;
        }

        public CustomerSubscription(int customerId, DateTime startDate, DateTime endDate)
        {
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
