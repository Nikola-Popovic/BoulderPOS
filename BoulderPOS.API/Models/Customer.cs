using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BoulderPOS.API.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        
        public bool NewsletterSubscription { get; set; }

        [Column(TypeName = "varchar")]
        public string Picture { get; set; }

        [Column(TypeName = "bytea")]
        public string PicturePreviewPath { get; set; }

        [JsonIgnore]
        public virtual ICollection<BillProduct> BillProducts { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual CustomerSubscription Subscription { get; set; }
        public virtual CustomerEntries Entries { get; set; }

        public Customer()
        {
        }
    }
}
