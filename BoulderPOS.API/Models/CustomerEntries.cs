using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public virtual Customer Customer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "timestamp")]
        public DateTime Updated { get; set; }

        public CustomerEntries(int customerId)
        {
            CustomerId = customerId;
            Quantity = 0;
            Updated = DateTime.Now;
            UnlimitedEntries = false;
        }

        public CustomerEntries(int customerId, int quantity, bool unlimitedEntries)
        {
            CustomerId = customerId;
            Quantity = quantity;
            UnlimitedEntries = unlimitedEntries;
            Updated = DateTime.Now;
        }
    }
}
