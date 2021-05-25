using DOAN.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN.View
{
    public partial class CheckOrder : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        int ID;
        public CheckOrder(int id)
        {
            InitializeComponent();
            ID = id;
            LoadHoaDon();
        }
        public void LoadHoaDon()
        {
            dataGridView1.DataSource = BLL_DatHang.Instance.HoadonChiTiet(ID);
            txtMaDon.Text = ID.ToString();
            txtTongTien.Text = BLL_DatHang.Instance.getHDbyMaHD(ID).ThanhTien.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xác nhận Thành Công");
            this.Close();
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(txtMaDon.Text);
            BLL_DatHang.Instance.DellHoaDon(ID);
            MessageBox.Show("Hủy đơn thành công");
            d();
            this.Close();
        }
    }
}
