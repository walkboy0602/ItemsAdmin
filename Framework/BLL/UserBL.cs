using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DAL;
using Framework.Model;

namespace Framework.BLL
{
    public class UserBL
    {
        UserDL _UserDL;

        public UserBL()
        {
            _UserDL = new UserDL();
        }

        public int Register(User user)
        {
            return _UserDL.Register(user);
        }

        public User AutoLogin(int id)
        {
            return _UserDL.AutoLogin(id);
        }

        public User Login(LoginCredential cred)
        {
            return _UserDL.Login(cred);
        }
    }   
}
