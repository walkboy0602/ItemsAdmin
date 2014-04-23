using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using System.Data.SqlClient;

namespace Framework.DAL
{
    class UserDL
    {
        private dbConnection conn;

        //Constructor
        public UserDL()
        {
            conn = new dbConnection();
        }

        public int Register(User user)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("EXEC sp_register_user @username, @password, @email, @firstname, @lastname, @mobile, @ip, @type ");

            SqlParameter[] param = 
            {
                new SqlParameter("@username", user.Email),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@email", user.Email),
                new SqlParameter("@firstname", user.FirstName),
                new SqlParameter("@lastname", user.LastName),
                new SqlParameter("@mobile", user.Mobile),
                new SqlParameter("@ip", string.Empty),
                new SqlParameter("@type", user.AccountType)
            };

            int AccountID = conn.executeInsertQueryReturnID(sb.ToString(), param);

            return AccountID;
        }

        public User AutoLogin(int userID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT id as UserID, FirstName, LastName, Email FROM [User] WHERE id = @userID ");

            SqlParameter[] param =
            {
                new SqlParameter("@userID", userID)
            };

            List<User> users = conn.executeSelectQuery<User>(sb.ToString(), param);

            return users.Count > 0 ? users[0] : null;
        }

        public User Login(LoginCredential cred)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT u.ID as UserID, FirstName, LastName, Email FROM Account ac ");
            sb.Append("INNER JOIN [User] u ON u.AccountID = ac.ID ");
            sb.Append("WHERE Username = @username AND pwdcompare(@password, Password) = 1 ");

            SqlParameter[] param =
            {
                new SqlParameter("@username", cred.Email),
                new SqlParameter("@password", cred.Password)
            };

            List<User> users = conn.executeSelectQuery<User>(sb.ToString(), param);

            return users.Count > 0 ? users[0] : null;

        }
    }
}
