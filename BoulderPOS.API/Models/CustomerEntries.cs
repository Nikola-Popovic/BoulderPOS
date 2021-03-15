using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoulderPOS.API.Models
{
    public class CustomerEntries 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int Quantity { get; set; }

        public bool UnlimitedEntries { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public CustomerEntries()
        {
        }

        public CustomerEntries(int customerId)
        {
            CustomerId = customerId;
            Quantity = 0;
            UnlimitedEntries = false;
        }

        public CustomerEntries(int customerId, int quantity, bool unlimitedEntries)
        {
            CustomerId = customerId;
            Quantity = quantity;
            UnlimitedEntries = unlimitedEntries;
        }
    }
}
