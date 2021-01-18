using Drack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class DriverNextOfKin
    {
        [Display(Name = "Next of Kin")]
        public int DriverNextOfKinId { get; set; }
        [Display(Name = "Driver")]
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        //bio information
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Other Names")]
        public string OtherNames { get; set; }
        public string FullName
        {
            get { return LastName + " " + FirstName + " " + OtherNames; }
        }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public string Age
        {
            get
            {
                string output = "";
                if (DateOfBirth != null)
                {
                    // Save today's date.
                    var today = DateTime.Today;
                    DateTime temp;
                    if (DateTime.TryParse(DateOfBirth.ToString(), out temp))
                    {
                        // Calculate the age.
                        var age = today.Year - temp.Year;
                        // Do stuff with it.
                        if (temp > today.AddYears(-age))
                        {
                            output = (age--).ToString();
                        }
                        else
                        {
                            output = (age).ToString();
                        }
                    }
                    else
                    {
                        output = "Invalid Datatype";
                    }
                }
                else
                {
                    output = "";
                }
                return output;
            }
        }
        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }
        public Gender Gender { get; set; }

        //contact information
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone No.")]
        public string PhoneNumber { get; set; }

        //geolocation information
        [Display(Name = "State")]
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [Display(Name = "Passport")]
        public string ImagePath { get; set; }

        public RelationShip Relationship { get; set; }

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