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
        public bool isStaff;
        public int ID;
        public ResetDGV reset { get; set; }
        public AddCustomer(bool edit, bool staff = false, int Id = 0)
        {
            InitializeComponent();
            Edit = edit;
            isStaff = staff;
            ID = Id;
            if (Edit)
            {
                if (isStaff)
                {
                    NhanVien nv = BLL_NhanVien.Instance.getNhanVienByID(ID);
                    txt_User.Text = nv.Username;
                    txtAddress.Text = nv.DiaChi;
                    txtEmail.Text = nv.Email;
                    txtName.Text = nv.HoTen;
                    textCMND.Text = nv.CMND.ToString();
                    txtSDT.Text = nv.SDT;
                }
                else
                {
                    Khach nv = BLL_KhachHang.Instance.getKhachByID(ID);
                    txt_User.Text = nv.Username;
                    txtAddress.Text = nv.DiaChi;
                    txtEmail.Text = nv.Email;
                    txtName.Text = nv.HoTen;
                    textCMND.Text = nv.CMND.ToString();
                    txtSDT.Text = nv.SDT;
                }
                txt_User.ReadOnly = true;
                txtPass.ReadOnly = true;
                textCMND.ReadOnly = true;
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
                    ID_Role = isStaff ? 2 : 1
                };
                if (!BLL_Login.Instance.addLogin(login))
                {
                    MessageBox.Show("Không thể thêm, xin hãy thử lại sau");
                    return;
                }
                if (!isStaff)
                {
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
                    NhanVien nvien = new NhanVien()
                    {
                        HoTen = txtName.Text,
                        CMND = Convert.ToInt32(textCMND.Text),
                        DiaChi = txtAddress.Text,
                        SDT = txtSDT.Text,
                        Email = txtEmail.Text,
                        Username = txt_User.Text
                    };
                    if (!BLL_NhanVien.Instance.addNhanVien(nvien))
                    {
                        MessageBox.Show("Không thể thêm, xin hãy thử lại sau");
                        BLL_Login.Instance.removeLogin(login);
                        return;
                    }
                }
            }
            else
            {
                Login login = new Login()
                {
                    Username = txt_User.Text,
                    Password = txtPass.Text,
                    ID_Role = isStaff ? 2 : 1
                };
                if (!BLL_Login.Instance.editLogin(login))
                {
                    MessageBox.Show("Không thể sửa, xin hãy thử lại sau");
                    return;
                }
                if (!isStaff)
                {
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
                else
                {
                    NhanVien nvien = new NhanVien()
                    {
                        HoTen = txtName.Text,
                        CMND = Convert.ToInt32(textCMND.Text),
                        DiaChi = txtAddress.Text,
                        SDT = txtSDT.Text,
                        Email = txtEmail.Text,
                        Username = txt_User.Text
                    };
                    if (!BLL_NhanVien.Instance.editNhanVien(nvien))
                    {
                        MessageBox.Show("Không thể thêm, xin hãy thử lại sau");
                        BLL_Login.Instance.removeLogin(login);
                        return;
                    }
                }
            }
            this.reset("",0);
            this.Dispose();
        }
    }
}
