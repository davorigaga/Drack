using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class DriverLicense
    {
        [Display(Name = "DriverLicense")]
        public int DriverLicenseId { get; set; }
        [Display(Name = "Driver")]
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        [Display(Name = "Image")]
        public string DriverLicenseImagePath { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
    }
}