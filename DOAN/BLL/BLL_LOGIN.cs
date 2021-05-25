using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_LOGIN
    {
        private static BLL_LOGIN _Instance;
        public static BLL_LOGIN Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_LOGIN();
                return _Instance;
            }
            set
            {

            }
        }
        public Khach getKhach(Login login)
        {
           using (var db = new QLKTX())
            {
                return db.Khaches.Where(s => s.Username.Equals(login.Username)).Select(s => s).ToList()[0];

            }
            return null;
        }
        public Login getLoginByUserName(string username)
        {
            using(var db= new QLKTX())
            {
                var user = db.Logins.Where(s => s.Username.Equals(username)).Select(s => s).ToList();
                if (user.Count > 0)
                {
                    return user[0];
                }
                return null;
            }
        }
        public int CheckUserNameByPassword(string username,string password)
        {
            Login user = getLoginByUserName(username);
            if(user!=null && user.Password == password)
            {
                return user.ID;
            }
            return -1;
        }
    }
}
