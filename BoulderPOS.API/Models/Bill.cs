using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BoulderPOS.API.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime ProcessedDateTime { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdatedDateTime { get; set; }
        
        public virtual ICollection<BillProduct> Products { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public Bill()
        {
        }
    }
}
