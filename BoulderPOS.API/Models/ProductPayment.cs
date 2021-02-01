using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderPOS.API.Models
{
    public class ProductPayment
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public bool IsRefunded { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal SellingPrice { get; set; }

        public virtual Customer User { get; set; }

        public virtual Product Product { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
    }
}
