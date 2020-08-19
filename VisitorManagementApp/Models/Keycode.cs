using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagementApp.Models
{
    public class Keycode
    {
        [Key]
        public int KeycodeId { get; set; }

        [Required(ErrorMessage = "Key is required")]
        public string Key { get; set; }

        [DisplayName("Appointment Day")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string AppointmentDay { get; set; }

        [DisplayName("Appointment Time")]
        [Required(ErrorMessage = "Time is required")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public string AppointmentTime { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }

        [ForeignKey("Staff")]
        public int StaffId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Staff Staff { get; set; }
    }
}