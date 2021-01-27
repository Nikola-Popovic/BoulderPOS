using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BoulderPOS.API.Models;

namespace BoulderPOS.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "timestamp")]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "timestamp")]
        public DateTime Updated { get; set; }

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

        [Column(TypeName = "bytea")]
        public byte[] Picture { get; set; }

        public ICollection<ProductPayment> Orders { get; set; }
        public CustomerSubscription Subscription { get; set; }
        public CustomerEntries Entries { get; set; }
    }
}
