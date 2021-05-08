using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_Login
    {
        private static BLL_Login _Instance;
        public static BLL_Login Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Login();
                }
                return _Instance;
            }
        }
        public Khach getKhach(Login login)
        {
            using (var db = new DoAnEntities())
            {
                return db.Khaches.Where(s => s.Username.Equals(login.Username)).Select(s => s).ToList()[0];
            }
            return null;
        }
        public Login getLoginByUsername(string username)
        {
            using (var db = new DoAnEntities())
            {
                var user = db.Logins.Where(s => s.Username.Equals(username)).Select(s => s).ToList();
                if (user.Count > 0)
                {
                    return user[0];
                }
                return null;
            }
        }
        public int checkUsernameAndPassword(string username, string password)
        {
            Login user = getLoginByUsername(username);
            if (user != null && user.Password.Equals(password))
            {
                return user.ID;
            }
            else return -1;
        }
    }
}
