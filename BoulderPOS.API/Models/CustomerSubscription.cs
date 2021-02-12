﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoulderPOS.API.Models
{
    public class CustomerSubscription
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public bool AutoRenewal { get; set; }

        public virtual Customer Customer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "timestamp")]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "timestamp")]
        public DateTime Updated { get; set; }

        public CustomerSubscription(int customerId)
        {
            CustomerId = customerId;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public CustomerSubscription(int customerId, DateTime startDate, DateTime endDate)
        {
            CustomerId = customerId;
            StartDate = startDate;
            EndDate = endDate;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}
