﻿using App.Core.Data;
using App.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;

namespace App.Core.Services
{
    public interface IUsersService
    {
        // UserProfiles 
        UserProfile GetUserProfile(string userName);
        UserProfile GetUserProfile(int userId);
        void Save(UserProfile userProfile);

        // OAuth Membership
        OAuthMembership GetOAuthMembership(string provider, string providerUserId);
        void SaveOAuthMembership(string provider, string providerUserId, int userId);

        // Membership
        void Save(App.Core.Data.Membership membership, bool add);
        App.Core.Data.Membership GetMembership(int userId);
        App.Core.Data.Membership GetMembershipByConfirmToken(string token, bool withUserProfile);
        App.Core.Data.Membership GetMembershipByVerificationToken(string token, bool withUserProfile);
        void ChangePassword(Data.Membership membership, string newPassword);

        // Emails
        void SendAccountActivationMail(string email);
        void SentChangePasswordEmail(string email);

        // Roles
        string[] GetRoles(string userName);

        // Helper to do 
        string GetHash(string text);
    }

    public class UsersService : IUsersService
    {
        private readonly IDatabaseContext db;
        private readonly IConfigService configService;
        private readonly IEmailService emailService;

        public UsersService(IDatabaseContext db, IConfigService configService, IEmailService emailService)
        {
            this.db = db;
            this.configService = configService;
            this.emailService = emailService;
        }

        UserProfile IUsersService.GetUserProfile(string userName)
        {
            return this.db.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
        }

        UserProfile IUsersService.GetUserProfile(int userId)
        {
            return this.db.UserProfiles.FirstOrDefault(x => x.UserId.Equals(userId));
        }

        void IUsersService.Save(UserProfile userProfile)
        {
            if (userProfile.UserId == 0)
            {
                this.db.Add(userProfile);
            }
            this.db.SaveChanges();
        }

        OAuthMembership IUsersService.GetOAuthMembership(string provider, string providerUserId)
        {
            return this.db.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
        }

        void IUsersService.SaveOAuthMembership(string provider, string providerUserId, int userId)
        {
            var oAuthMembership = this.db.OAuthMemberships.FirstOrDefault(x => x.Provider.Equals(provider) && x.ProviderUserId.Equals(providerUserId));
            if (oAuthMembership == null)
            {
                oAuthMembership = new OAuthMembership { Provider = provider, ProviderUserId = providerUserId };
                this.db.Add(oAuthMembership);
            }
            oAuthMembership.UserId = userId;
            this.db.SaveChanges();
        }

        App.Core.Data.Membership IUsersService.GetMembership(int userId)
        {
            return this.db.Memberships.FirstOrDefault(x => x.UserId == userId);
        }

        App.Core.Data.Membership IUsersService.GetMembershipByConfirmToken(string token, bool withUserProfile)
        {
            var membership = this.db.Memberships.FirstOrDefault(x => x.ConfirmationToken.Equals(token.ToLower()));
            if (membership != null && withUserProfile)
            {
                membership.UserProfile = this.db.UserProfiles.First(x => x.UserId == membership.UserId);
            }
            return membership;
        }

        App.Core.Data.Membership IUsersService.GetMembershipByVerificationToken(string token, bool withUserProfile)
        {
            var membership = this.db.Memberships.FirstOrDefault(x => x.PasswordVerificationToken.Equals(token.ToLower()));
            if (membership != null && withUserProfile)
            {
                membership.UserProfile = this.db.UserProfiles.First(x => x.UserId == membership.UserId);
            }
            return membership;
        }

        void IUsersService.Save(App.Core.Data.Membership membership, bool add)
        {
            if (add)
            {
                this.db.Add(membership);
            }
            this.db.SaveChanges();
        }

        // to do: transfer it to service
        void IUsersService.SendAccountActivationMail(string email)
        {
            var userProfile = (this as IUsersService).GetUserProfile(email);
            if (userProfile == null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }

            var membership = (this as IUsersService).GetMembership(userProfile.UserId);
            if (membership == null)
            {
                throw new MembershipCreateUserException(MembershipCreateStatus.ProviderError);
            }

            var configValues = this.configService.GetValues(new ConfigName[] { ConfigName.WebsiteUrlName, ConfigName.WebsiteTitle, ConfigName.WebsiteUrl });
            var viewData = new ViewDataDictionary { Model = userProfile };
            viewData.Add("Membership", membership);
            this.emailService.SendEmail(
                new SendEmailModel
                {
                    EmailAddress = email,
                    Subject = configValues[ConfigName.WebsiteUrlName.ToString()] + ": Confirm your registration",
                    WebsiteUrlName =  configValues[ConfigName.WebsiteUrlName.ToString()],
                    WebsiteTitle = configValues[ConfigName.WebsiteTitle.ToString()],
                    WebsiteURL = configValues[ConfigName.WebsiteUrl.ToString()]
                },
                "ConfirmRegistration",
                viewData
            );
        }

        string[] IUsersService.GetRoles(string userName)
        {
            var userProfile = this.db.UserProfiles.FirstOrDefault(x => x.UserName.Equals(userName));
            if (userProfile != null)
            {
                return userProfile.Roles.Select(x => x.RoleName).ToArray();
            }
            return new string[] { };
        }

        void IUsersService.SentChangePasswordEmail(string email)
        {
            var userProfile = (this as IUsersService).GetUserProfile(email);
            if (userProfile == null)
            {
                throw new MembershipCreateUserException("User not found.");
            }

            var membership = (this as IUsersService).GetMembership(userProfile.UserId);
            if (membership == null)
            {
                throw new MembershipCreateUserException("User not found.");
            }

            membership.PasswordVerificationToken = Guid.NewGuid().ToString();
            this.db.SaveChanges();

            var configValues = this.configService.GetValues(new ConfigName[] { ConfigName.WebsiteUrlName, ConfigName.WebsiteTitle, ConfigName.WebsiteUrl });
            var viewData = new ViewDataDictionary { Model = membership };
            emailService.SendEmail(
                new SendEmailModel
                {
                    EmailAddress = email,
                    Subject = configValues[ConfigName.WebsiteUrlName.ToString()] + ": Change password.",
                    WebsiteUrlName =  configValues[ConfigName.WebsiteUrlName.ToString()],
                    WebsiteTitle = configValues[ConfigName.WebsiteTitle.ToString()],
                    WebsiteURL = configValues[ConfigName.WebsiteUrl.ToString()]
                },
                "ChangePassword", 
                viewData
            );
        }

        private string salt = "HJIO6589";
        string IUsersService.GetHash(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(String.Concat(text, salt));
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }


        void IUsersService.ChangePassword(Data.Membership membership, string newPassword)
        {
            if (membership == null)
            {
                throw new Exception("User not found.");
            }

            membership.PasswordVerificationToken = null;
            membership.PasswordSalt = (this as IUsersService).GetHash(newPassword);

            this.db.SaveChanges();
        }

    }
}
