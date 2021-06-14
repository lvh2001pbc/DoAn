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
using System.Security.Cryptography;

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
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BLL_Login.Instance.checkUsernameAndPassword(textBox1.Text, MD5Hash(textBox2.Text)) == 1)
            {
                var user = BLL_Login.Instance.getLoginByUsername(textBox1.Text);
                var khach = BLL_Login.Instance.getKhach(user);
                Customer form = new Customer(khach.MaKhachHang);
                form.Show();
                form.FormClosing += CloseForm;
                this.Hide();
            }
            else if (BLL_Login.Instance.checkUsernameAndPassword(textBox1.Text, MD5Hash(textBox2.Text)) == 2)
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
