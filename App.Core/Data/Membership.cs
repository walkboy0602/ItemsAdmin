
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace App.Core.Data
{

using System;
    using System.Collections.Generic;
    
public partial class Membership
{

    public int UserId { get; set; }

    public Nullable<System.DateTime> CreateDate { get; set; }

    public string EmailConfirmationToken { get; set; }

    public Nullable<bool> IsEmailConfirmed { get; set; }

    public Nullable<System.DateTime> LastPasswordFailureDate { get; set; }

    public int PasswordFailuresSinceLastSuccess { get; set; }

    public Nullable<System.DateTime> PasswordChangedDate { get; set; }

    public string PasswordSalt { get; set; }

    public string PasswordVerificationToken { get; set; }

    public Nullable<System.DateTime> PasswordVerificationTokenExpirationDate { get; set; }

    public Nullable<bool> IsMobileConfirmed { get; set; }

    public Nullable<bool> IsIdentificationConfirmed { get; set; }

}

}
