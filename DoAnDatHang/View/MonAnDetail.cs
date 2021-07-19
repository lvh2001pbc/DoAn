using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnDatHang.BLL;

namespace DoAnDatHang.View
{
    public partial class MonAnDetail : Form
    {
        int ID;
        bool isMonAn;
        public MonAnDetail(int id = 0, bool MonAn = true)
        {
            InitializeComponent();
            ID = id;
            isMonAn = MonAn;
            if (isMonAn) textBox3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isMonAn)
            {
                MonAn mon = new MonAn
                {
                    Gia = Convert.ToDecimal(textBox2.Text),
                    TenMonAn = textBox1.Text
                };
                if (ID == 0)
                {
                    BLL_MonAn.Instance.addMonAn(mon);
                }
                else
                {
                    BLL_MonAn.Instance.suaMonAn(ID, mon);
                }
            }
            else
            {
                NuocUong nuoc = new NuocUong
                {
                    MaNuocUong = ID,
                    Gia = Convert.ToDecimal(textBox2.Text),
                    TenNuocUong = textBox1.Text,
                    SoLuongCon = Convert.ToInt32(textBox3.Text)
                };
                BLL_NuocUong.Instance.addFixNuocUong(nuoc);
            }
        }

        private void MonAnDetail_Load(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (isMonAn)
                {
                    MonAn a = BLL_MonAn.Instance.getMonAnByID(ID);
                    textBox1.Text = a.Gia.ToString();
                    textBox2.Text = a.TenMonAn;
                }
                else
                {
                    NuocUong a = BLL_NuocUong.Instance.getNuocByID(ID);
                    textBox2.Text = a.Gia.ToString();
                    textBox1.Text = a.TenNuocUong;
                    textBox3.Text = a.SoLuongCon.ToString();
                }
            }
        }
    }
}
