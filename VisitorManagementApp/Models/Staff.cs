using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VisitorManagementApp.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be longer than 8 characters")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please confirm your Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = " Home Address is required")]
        public string Address { get; set; }

        [DisplayName("Office Location")]
        [Required(ErrorMessage = "Office/Meeting Loction is required")]
        public string OfficeLocation { get; set; }

        public virtual ICollection<Keycode> Keycodes { get; set; }
    }
}