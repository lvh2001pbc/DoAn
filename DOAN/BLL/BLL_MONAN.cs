using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_MONAN
    {
        private static BLL_MONAN _Instance;
        public static BLL_MONAN Instance
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new BLL_MONAN();
                return _Instance;
            }
            set { }
        }
        public List<MonAn> getAllMonAn()
        {
            var db = new QLKTX();
            return db.MonAns.ToList();
        }
        public MonAn getMonAnbyID(int ID)
        {
            List<MonAn> all = getAllMonAn();
            foreach(MonAn i in all)
            {
                if (i.MaMonAn == ID)
                {
                    return i; 
                }
            }
            return null;
        }
    }
}
