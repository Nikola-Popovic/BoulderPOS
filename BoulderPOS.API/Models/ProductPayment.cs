using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BoulderPOS.API.Models
{
    public class ProductPayment
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public bool IsRefunded { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime ProcessedDateTime { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime UpdatedDateTime { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public ProductPayment()
        {
        }
    }
}
