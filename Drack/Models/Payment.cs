using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class Payment
    {
        [Display(Name = "Payment")]
        public int PaymentId { get; set; }
        [Display(Name = "Driver")]
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Payment Status")]
        public PaymentStatus PaymentStatus { get; set; }

        [Display(Name = "Date Time")]
        public DateTime? DateTime { get; set; }
    }
}