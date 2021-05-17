using DoAnDatHang;
using DoAnDatHang.BLL;
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
    public partial class AddCustomer : Form
    {
        public delegate void ResetDGV(string name, int ID);
        public bool Edit;
        public ResetDGV reset { get; set; }
        public AddCustomer(bool edit)
        {
            InitializeComponent();
            Edit = edit;
            if (Edit)
            {
                
                txt_User.ReadOnly = true;
                txtPass.ReadOnly = true;
                //textCMND.ReadOnly = true;
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Edit)
            {
                Login login = new Login()
                {
                    Username = txt_User.Text,
                    Password = txtPass.Text,
                    ID_Role = 1
                };
                if (!BLL_Login.Instance.addLogin(login))
                {
                    MessageBox.Show("Không thể thêm, xin hãy thử lại sau");
                    return;
                }
                Khach khach = new Khach()
                {
                    HoTen = txtName.Text,
                    CMND = Convert.ToInt32(textCMND.Text),
                    DiaChi = txtAddress.Text,
                    SDT = txtSDT.Text,
                    Email = txtEmail.Text,
                    Username = txt_User.Text
                };
                if (!BLL_KhachHang.Instance.addKhachHang(khach))
                {
                    MessageBox.Show("Không thể thêm, xin hãy thử lại sau");
                    BLL_Login.Instance.removeLogin(login);
                    return;
                }
            }
            else
            {
                Login login = new Login()
                {
                    Username = txt_User.Text,
                    Password = txtPass.Text,
                    ID_Role = 1
                };
                if (!BLL_Login.Instance.editLogin(login))
                {
                    MessageBox.Show("Không thể sửa, xin hãy thử lại sau");
                    return;
                }
                Khach khach = new Khach()
                {
                    HoTen = txtName.Text,
                    CMND = Convert.ToInt32(textCMND.Text),
                    DiaChi = txtAddress.Text,
                    SDT = txtSDT.Text,
                    Email = txtEmail.Text,
                    Username = txt_User.Text
                };
                if (!BLL_KhachHang.Instance.editKhachHang(khach))
                {
                    MessageBox.Show("Không thể sửa, xin hãy thử lại sau");
                    return;
                }
            }
            this.reset("",0);
            this.Dispose();
        }
    }
}
