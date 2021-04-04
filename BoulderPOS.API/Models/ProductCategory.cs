using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BoulderPOS.API.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IconName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
        }
    }
}
