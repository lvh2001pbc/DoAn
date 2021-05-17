using DOAN.View;
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

namespace DOAN
{
    public partial class Staff : Form
    {
        int Id;
        public Staff(int id = 0)
        {
            InitializeComponent();
            Id = id;
            LoadThongTin();
            ShowDon();
        }
        public void ShowDon()
        {
            dtgvDon.DataSource = BLL_HoaDon.Instance.getAllHoaDonView();
        }
        public void LoadThongTin()
        {
            NhanVien staff = BLL_NhanVien.Instance.getNhanVienByID(Id);
            txtName.Text = staff.HoTen;
            txtEmail.Text = staff.Email;
            txtAddress.Text = staff.DiaChi;
            txtSDT.Text = staff.SDT;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void Staff_Load(object sender, EventArgs e)
        {
            dtgv_KH.DataSource = BLL_KhachHang.Instance.getAllKhach();
            dtgv_KH.Columns["MaKhachHang"].HeaderText = "Mã khách hàng";
            dtgv_KH.Columns["HoTen"].HeaderText = "Họ tên";
            dtgv_KH.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dtgv_KH.Columns["SDT"].HeaderText = "Số điện thoại";
            dtgv_KH.Columns["SoDu"].HeaderText = "Số dư";
            dtgv_KH.Columns["HDDatHangs"].Visible = false;
            dtgv_KH.Columns["Login"].Visible = false;
        }
        public void Show(string Name, int Id)
        {
            List<Khach> khaches = BLL_KhachHang.Instance.getAllKhach().Where(s => s.HoTen.Contains(Name) && (s.MaKhachHang == Id || Id == 0)).ToList();
            dtgv_KH.DataSource = khaches;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            AddCustomer ad = new AddCustomer(false);
            ad.reset += Show;
            ad.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AddCustomer edit = new AddCustomer(true);
            edit.reset += Show;
            edit.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow i in dtgv_KH.SelectedRows)
            {
                Khach khach = BLL_KhachHang.Instance.getKhachByID(Convert.ToInt32(i.Cells["MaKhachHang"].Value));
                if (khach != null)
                {
                    BLL_KhachHang.Instance.removeKhachHang(khach);
                }
            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dtgvDon.SelectedRows.Count > 0)
            {
                OrderDetail od = new OrderDetail(Convert.ToInt32(dtgvDon.SelectedRows[0].Cells["MaHoaDon"].Value));
                od.ShowDialog();
            }
        }

        private void btnUpdatdonhang_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_HoaDon.Instance.getHoaDonViewByTime(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void btnAdđon_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
        }
    }
}
