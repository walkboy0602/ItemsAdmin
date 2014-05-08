
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
    
public partial class User
{

    public int ID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public byte[] HashPassword { get; set; }

    public string Mobile { get; set; }

    public string Identification { get; set; }

    public string IdentificationFile { get; set; }

    public string PasswordQuestion { get; set; }

    public string PasswordAnswer { get; set; }

    public bool IsApproved { get; set; }

    public bool IsLockedOut { get; set; }

    public System.DateTime CreateDate { get; set; }

    public int LoginCounter { get; set; }

    public string LastIP { get; set; }

    public Nullable<System.DateTime> LastLoginDate { get; set; }

    public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }

    public Nullable<System.DateTime> LastLockoutDate { get; set; }

    public Nullable<int> FailedPasswordAttemptCount { get; set; }

    public Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }

    public string Comment { get; set; }

}

}
