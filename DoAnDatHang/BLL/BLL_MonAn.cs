using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnDatHang.BLL
{
    class BLL_MonAn
    {
        private DoAnEntities db = new DoAnEntities();
        private static BLL_MonAn _Instance;
        public static BLL_MonAn Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_MonAn();
                }
                return _Instance;
            }
        }
        public List<MonAn> getAllMonAn()
        {
            return db.MonAns.ToList();
        }
        public MonAn getMonAnByID(int Id)
        {
            return db.MonAns.Find(Id);
        }
    }
}
