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
        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BLL_Login.Instance.checkUsernameAndPassword(textBox1.Text, textBox2.Text) == 1)
            {
                var user = BLL_Login.Instance.getLoginByUsername(textBox1.Text);
                var khach = BLL_Login.Instance.getKhach(user);
                Customer form = new Customer(khach.MaKhachHang);
                form.Show();
                form.FormClosing += CloseForm;
                this.Hide();
            }
            else if (BLL_Login.Instance.checkUsernameAndPassword(textBox1.Text, textBox2.Text) == 2)
            {
                var user = BLL_Login.Instance.getLoginByUsername(textBox1.Text);
                var nvien = BLL_Login.Instance.getNhanVien(user);
                Staff form = new Staff(nvien.MaNhanVien);
                form.Show();
                form.FormClosing += CloseForm;
                this.Hide();
            }
            else MessageBox.Show("Sai tai khoan hoac mat khau");
        }
    }
}
