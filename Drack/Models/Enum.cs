using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Drack.Models
{

    public enum Gender
    {
        [Display(Name = "Female")]
        Female = 1,
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Not disclosing")]
        Not_Disclosing
    }
    public enum MaritalStatus
    {
        [Display(Name = "Single")]
        Single = 1,
        [Display(Name = "Married")]
        Married,
        [Display(Name = "Divorced")]
        Divorced,
        [Display(Name = "Widow")]
        Widow,
        [Display(Name = "Widower")]
        Widower
    }

    public enum DriverStatus
    {
        [Display(Name = "Inactive")]
        Inactive = 1,
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Suspended")]
        Suspended
    }
    public enum RelationShip
    {
        [Display(Name = "Family")]
        Family = 1,
        [Display(Name = "Relative")]
        Relative,
        [Display(Name = "Friend")]
        Friend,
        [Display(Name = "Colleague")]
        Colleague,
        [Display(Name = "Other")]
        Other
    }

    public enum EthnicOrigin
    {
        [Display(Name = "African")]
        African = 1,
        [Display(Name = "Indian")]
        Indian,
        [Display(Name = "White")]
        White,
        [Display(Name = "Coloured")]
        Coloured
    }

    public enum PolarQuestion
    {
        [Display(Name = "No")]
        No = 1,
        [Display(Name = "Yes")]
        Yes
    }

    public enum PaymentMode
    {
        [Display(Name = "Online")]
        Online = 1,
        [Display(Name = "Pending")]
        Pending
    }

    public enum PaymentStatus
    {
        [Display(Name = "Failed")]
        Failed = 0,
        [Display(Name = "Successful")]
        Successful,
        [Display(Name = "Pending")]
        Pending
    }

    public enum Religion
    {
        [Display(Name = "Christianity")]
        Christianity = 1,
        [Display(Name = "Islam")]
        Islam,
        [Display(Name = "Others")]
        Others
    }

    public enum OrderStatus
    {
        [Display(Name = "Received")]
        Received = 1,
        [Display(Name = "Active")]
        Active,
        [Display(Name = "On Delivery")]
        On_Delivery,
        [Display(Name = "Inactive")]
        Inactive
    }

    public class Enum
    {
        public string GetDisplayValue(string enumAsString)
        {
            return enumAsString.Replace("_", " ");
        }
    }
}