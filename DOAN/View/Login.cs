using DOAN.BLL;
using DOAN.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (BLL_LOGIN.Instance.CheckUserNameByPassword(textBox1.Text, textBox2.Text) == 1)
            {
                var user = BLL_LOGIN.Instance.getLoginByUserName(textBox1.Text);
                var khach = BLL_LOGIN.Instance.getKhach(user);
                Customer f = new Customer(khach.MaKhachHang);
                f.Show();
                this.Hide();
            }
            else if(BLL_LOGIN.Instance.CheckUserNameByPassword(textBox1.Text, textBox2.Text) == 2)
            {
                Staff f = new Staff();
                f.Show();
                this.Hide();
            }
        }
    }
}
