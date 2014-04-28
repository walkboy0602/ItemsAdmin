using App.Core.Data;
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
    public interface IUserService
    {
        // UserProfiles 
        //UserProfile GetUserProfile(string userName);
        //UserProfile GetUserProfile(int userId);
        void Save(UserProfile userProfile);

        // Helper to do 
        string GetHash(string text);

    }

    public class UserService : IUserService
    {
        //private readonly IConfigService configService;
        //private readonly IEmailService emailService;
        private ShopDBEntities db;

        //public UserService(IConfigService configService, IEmailService emailService)
        //{
        //    db = new ShopDBEntities();
        //    this.configService = configService;
        //    this.emailService = emailService;
        //}

        public UserService()
        {
            db = new ShopDBEntities();
        }

        void IUserService.Save(UserProfile userProfile)
        {
            if (userProfile.UserId == 0)
            {
                this.db.UserProfiles.Add(userProfile);
            }

            this.db.SaveChanges();
        }

        private string salt = "HJIO6589";
        string IUserService.GetHash(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(String.Concat(text, salt));
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

    }
}
