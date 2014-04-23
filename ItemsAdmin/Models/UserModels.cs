using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MonsterAdmin.Models
{
    [MetadataType(typeof(UserModels))]
    public partial class User 
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string cwdPassword { get; set; }
    }

    public class UserModels
    {
        //[Key]
        //public int ID { get; set; }

        //[Required]
        //public string FirstName { get; set; }

        //[Required]
        //public string LastName { get; set; }

        [Required]
        public string Email { get; set; }


        //[Required]
        //public string Mobile { get; set; }

        //[Required]
        //public string Identification { get; set; }

        //public string IdentificationFile { get; set; }

        //public System.DateTime CreateDate { get; set; }

        //public int LoginCounter { get; set; }
        //public string LastIP { get; set; }
        //public Nullable<System.DateTime> LastLoginDate { get; set; }
    }

    public class RegisterUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}