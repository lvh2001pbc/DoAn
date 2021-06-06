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
                db.Logins.Attach(login);
                return login.Khaches.First();
            }
        }
        public NhanVien getNhanVien(Login login)
        {
            using (var db = new DoAnEntities())
            {
                Login log = db.Logins.Find(login.Username);
                return log.NhanViens.Single();
            }
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
                return user.ID_Role;
            }
            else return -1;
        }

        public bool addLogin(Login login)
        {
            try
            {
                using( var db = new DoAnEntities())
                {
                    db.Logins.Add(login);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool removeLogin(Login login)
        {
            try
            {
                using(var db = new DoAnEntities())
                {
                    db.Logins.Remove(login);
                    db.SaveChanges();
                }
            }
            catch{
                return false;
            }
            return true;
        }
        public bool editLogin(Login login)
        {
            using(var db = new DoAnEntities())
            {
                var result = db.Logins.SingleOrDefault(s => s.Username.Equals(login.Username));
                if (result != null)
                {
                    result.Password = login.Password;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
