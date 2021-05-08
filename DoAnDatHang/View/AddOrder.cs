using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnDatHang;

namespace DOAN.View
{
    public partial class AddOrder : Form
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DoAnEntities a = new DoAnEntities();
            List<Khach> khach = a.Khaches.ToList();
            dataGridView1.DataSource = a.Khaches.ToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
