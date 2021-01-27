using BoulderPOS.API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderPOS.Model
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Column(TypeName = "bytea")]
        public byte[] CategoryIcon { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
