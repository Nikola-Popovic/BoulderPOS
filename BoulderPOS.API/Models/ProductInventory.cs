using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderPOS.API.Models
{
    public class ProductInventory
    {
        [Key] 
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int InStoreQuantity { get; set; }

        public int OrderedQuantity { get; set; }

        public int SuretyQuantity { get; set; }

        public virtual Product Product { get; set; }
    }
}