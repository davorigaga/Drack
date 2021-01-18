using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class GarageChairman
    {
        [Display(Name = "Garage Chairman")]
        public int GarageChairmanId { get; set; }
        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Image")]
        public string GarageImagePath { get; set; }

        [Display(Name = "Garage")]
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
    }
}