using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagementApp.Models
{
    public class Visitor
    {
        [Key]
        public int VisitorId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Name is required")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Purpose of Visit is required")]
        public AppointmentType Purpose { get; set; }

        public string TimeIn { get; set; }

        public string TimeOut { get; set; }


    }

    public enum AppointmentType
    {
        Delivery,
        Consulting,
        Legal,
        Interview,
        Informal,
        Other
    }
}