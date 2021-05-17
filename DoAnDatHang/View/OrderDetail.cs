using DoAnDatHang.BLL;
using DoAnDatHang.View;
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
    public partial class OrderDetail : Form
    {
        int Id;
        public OrderDetail(int ID)
        {
            InitializeComponent();
            Id = ID;
            Load();
        }
        public void Load()
        {
            textBox1.Text = Id.ToString();
            textBox1.ReadOnly = true;
            List<ItemView> list = BLL_Items.Instance.getAllItemViewsbyHD(Id);
            dataGridView1.DataSource = list;
            decimal sum = 0;
            foreach (ItemView i in list)
            {
                sum += i.ThanhTien;
            }
            textBox2.Text = sum.ToString();
        }
    }
}
