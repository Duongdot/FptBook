namespace FptBookNew1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class account
    {
        public account()
        {
            feedbacks = new HashSet<feedback>();
            orders = new HashSet<order>();
        }
        [Key]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string fullname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation new password do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[z0-9.-]+\.[A-Za-z]{2,4}")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(13)]
        public string phone { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        public int state { get; set; }

        public  ICollection<feedback> feedbacks { get; set; }


        public  ICollection<order> orders { get; set; }
    }
}
