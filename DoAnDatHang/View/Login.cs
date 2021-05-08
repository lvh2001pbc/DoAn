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
using DOAN.View;

namespace DOAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BLL_Login.Instance.checkUsernameAndPassword(textBox1.Text, textBox2.Text) == 1)
            {
                var user = BLL_Login.Instance.getLoginByUsername(textBox1.Text);
                var khach = BLL_Login.Instance.getKhach(user);
                Customer form = new Customer(khach.MaKhachHang);
                form.Show();
                this.Hide();
            }
            else if (BLL_Login.Instance.checkUsernameAndPassword(textBox1.Text, textBox2.Text) == 2)
            {
                Staff form = new Staff();
                form.Show();
                this.Hide();
            }
            else MessageBox.Show("Sai tai khoan hoac mat khau");
        }
    }
}
