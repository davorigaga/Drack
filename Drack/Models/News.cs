using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class News
    {
        [Display(Name = "News")]
        public int NewsId { get; set; }
        [Required]
        [Display(Name = "Headline")]
        public string Headline { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string NewsDescription { get; set; }
        [Display(Name = "Image")]
        public string NewsImagePath { get; set; }


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