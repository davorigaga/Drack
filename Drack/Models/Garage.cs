using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class Garage
    {
        [Display(Name = "Garage")]
        public int GarageId { get; set; }
        [Required]
        [Display(Name = "Garage Name")]
        public string GarageName { get; set; }
        [Required]
        [Display(Name = "Garage Number")]
        public string GarageNumber { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Driver Due")]
        public decimal DriverDue { get; set; }
        public virtual ICollection<GarageChairman> GarageChairmen { get; set; }
    }
}