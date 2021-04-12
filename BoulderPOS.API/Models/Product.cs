using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BoulderPOS.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        // For Subscription it represents a duration
        public int Quantity { get; set; } = 1;

        public bool IsAvailable { get; set; } = true;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }

        [JsonIgnore]
        public virtual ProductInventory Inventory { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductPayment> Orders { get; set; }

        public Product()
        {
        }
    }
}
