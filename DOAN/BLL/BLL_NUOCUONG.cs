using DOAN.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN.BLL
{
    class BLL_NUOCUONG
    {
        private static BLL_NUOCUONG _Instance;
        public static BLL_NUOCUONG Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_NUOCUONG();
                return _Instance;
            }
            set
            {

            }
        }
        public List<NuocUong>  getAllNuoc()
        {
            var db = new QLKTX();
            return db.NuocUongs.ToList();
        }
        public NuocUong getNuocbyID(int ID)
        {
            List<NuocUong> all = getAllNuoc();
            foreach (NuocUong i in all)
            {
                if (i.MaNuocUong == ID)
                {
                    return i;
                }
            }
            return null;
        }
    }
}
