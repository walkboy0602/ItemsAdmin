using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Framework.Model
{

    public class LoginCredential
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class Account
    {
        public int AccountID { get; set; }

        public int AccountType { get; set; }

        public DateTime ExpireDate { get; set; }
    }

    public class User: Account
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50, MinimumLength=6)]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "Email must be 50 characters or less in length")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [StringLength(25, ErrorMessage = "Last name must be 25 characters or less in length")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [StringLength(25, ErrorMessage = "First name must be 25 characters or less in length")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string Mobile { get; set; }
    }
}
