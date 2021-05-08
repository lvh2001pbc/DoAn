using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_KhachHang
    {
        private static BLL_KhachHang _Instance;
        public static BLL_KhachHang Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_KhachHang();
                }
                return _Instance;
            }
        }
        public List<Khach> getAllKhach()
        {
            var db = new DoAnEntities();
            return db.Khaches.Select(s => s).ToList();
        }

        public Khach getKhachByID(int ID)
        {
            List<Khach> all = getAllKhach();
            foreach( Khach i in all)
            {
                if (i.MaKhachHang == ID)
                {
                    return i;
                }
            }
            return null;
        }
    }
}
