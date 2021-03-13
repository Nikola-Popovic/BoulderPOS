using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string PicturePath { get; set; }

        [Column(TypeName = "bytea")]
        public string PicturePreviewPath { get; set; }

        public virtual ICollection<ProductPayment> Orders { get; set; }
        public virtual CustomerSubscription Subscription { get; set; }
        public virtual CustomerEntries Entries { get; set; }
    }
}
