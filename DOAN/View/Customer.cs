using DOAN.BLL;
using DOAN.DTO;
using DOAN.Entity;
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
    public partial class Customer : Form
    {
        int ID;
        static List<Mon> hdmon;
        static List<Nuoc> hdnuoc;
        public Customer(int id)
        {
            InitializeComponent();
            ID = id;
            LoadMonAn();
            LoadNuoc();
            LoadHoaDon();
            SetTaiKhoan();
        }
        public void SetTaiKhoan()
        {
            Khach khach = BLL_KH.Instance.getKhachbyID(ID);
            txtName.Text = khach.HoTen;
            txtSDT.Text = khach.SDT;
            txtAddress.Text = khach.DiaChi;
            txtEmail.Text = khach.Email;
            txtID.Text = khach.MaKhachHang.ToString();
            txtCMND.Text = khach.CMND.ToString();
        }
        private void LoadMonAn()
        {
            dgvMonAn.DataSource = BLL_MONAN.Instance.getAllMonAn();
            dgvMonAn.Columns["MonAn_HDDatHang"].Visible = false;
        }
        private void LoadNuoc()
        {
            dgvNuocUong.DataSource = BLL_NUOCUONG.Instance.getAllNuoc();
            dgvNuocUong.Columns["NuocUong_HDDatHang"].Visible = false;
        }
        private void LoadHoaDon()
        {
            dgvDonDatHang.DataSource = BLL_DatHang.Instance.GetListHDDatHangbyKhach(ID);
            dgvDonDatHang.Columns["MaKhachHang"].Visible = false;
            dgvDonDatHang.Columns["Khach"].Visible = false;
            dgvDonDatHang.Columns["MonAn_HDDatHang"].Visible = false;
            dgvDonDatHang.Columns["NuocUong_HDDatHang"].Visible = false;

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Khach khach = new Khach();
            khach.MaKhachHang = Convert.ToInt32(txtID.Text);
            khach.HoTen = txtName.Text;
            khach.DiaChi = txtAddress.Text;
            khach.Email = txtEmail.Text;
            khach.SDT = txtSDT.Text;
            if (BLL_KH.Instance.UpdateUser(khach) == true) MessageBox.Show("Cập nhật thành công");
            else MessageBox.Show("Cập nhật không thành công");
        }
        private void btnAddMon_Click(object sender, EventArgs e)
        {
            if (dgvMonAn.Rows.Count > 0)
            {
                MonAn mon = BLL_MONAN.Instance.getMonAnbyID(Convert.ToInt32(dgvMonAn.CurrentRow.Cells["MaMonAn"].Value));
                foreach (DataGridViewRow i in dgvDatMonAn.Rows)
                {
                    if (Convert.ToInt32(i.Cells["MaMonAn"].Value) == Convert.ToInt32(dgvMonAn.CurrentRow.Cells["MaMonAn"].Value))
                    {
                        i.Cells["Soluong"].Value = Convert.ToInt32(i.Cells["Soluong"].Value) + 1;
                        i.Cells["ThanhTien"].Value = Convert.ToInt32(i.Cells["Gia"].Value) * Convert.ToInt32(i.Cells["Soluong"].Value);
                        return;
                    }
                }
                dgvDatMonAn.Rows.Add(mon.MaMonAn, mon.TenMonAn, mon.Gia, 1, mon.Gia);
            }
            else
                MessageBox.Show("Vui lòng chọn Món Ăn");
        }
        private void btnDelMon_Click(object sender, EventArgs e)
        {
            if (dgvDatMonAn.Rows.Count > 0)
            {
                DataGridViewRow data = dgvDatMonAn.CurrentRow;
                if (Convert.ToInt32(data.Cells["Soluong"].Value) > 1)
                {
                    data.Cells["Soluong"].Value = Convert.ToInt32(data.Cells["Soluong"].Value) - 1;
                    data.Cells["ThanhTien"].Value = Convert.ToInt32(data.Cells["ThanhTien"].Value) - Convert.ToInt32(data.Cells["Gia"].Value);
                    return;
                }
                dgvDatMonAn.Rows.RemoveAt(data.Index);
            }
            else
                MessageBox.Show("Vui lòng chọn Món cần xóa");
        }

        private void btnXacnhanMon_Click(object sender, EventArgs e)
        {
             hdmon = new List<Mon>();
            foreach (DataGridViewRow i in dgvDatMonAn.Rows)
            {
                if (Convert.ToInt32(i.Cells["MaMonAn"].Value) == 0)
                    break;
                hdmon.Add(new Mon
                {
                    MaMonAn = Convert.ToInt32(i.Cells["MaMonAn"].Value),
                    TenMon = Convert.ToString(i.Cells["TenMonAn"].Value),
                    Gia = Convert.ToInt32(i.Cells["Gia"].Value),
                    Soluong = Convert.ToInt32(i.Cells["Soluong"].Value),
                    ThanhTien = Convert.ToInt32(i.Cells["ThanhTien"].Value)
                }) ;
            }
            dgvDatHangMon.DataSource = hdmon;
            MessageBox.Show("Đã thêm vào giỏ hàng");
        }

        private void btnAddNuoc_Click(object sender, EventArgs e)
        {
            if (dgvNuocUong.Rows.Count > 0)
            {
                NuocUong nuoc = BLL_NUOCUONG.Instance.getNuocbyID(Convert.ToInt32(dgvNuocUong.CurrentRow.Cells["MaNuocUong"].Value));
                foreach (DataGridViewRow i in dgvDatNuoc.Rows)
                {
                    if (Convert.ToInt32(i.Cells["MaNuoc"].Value) == Convert.ToInt32(dgvNuocUong.CurrentRow.Cells["MaNuocUong"].Value))
                    {
                        if (Convert.ToInt32(i.Cells["SoluongNuoc"].Value) == Convert.ToInt32(dgvNuocUong.CurrentRow.Cells["SoluongCon"].Value))
                        {
                            MessageBox.Show("Đã hết số lượng");
                            return;
                        }
                        else
                        {
                            i.Cells["SoluongNuoc"].Value = Convert.ToInt32(i.Cells["SoluongNuoc"].Value) + 1;
                            i.Cells["ThanhTienNuoc"].Value = Convert.ToInt32(i.Cells["GiaNuoc"].Value) * Convert.ToInt32(i.Cells["SoluongNuoc"].Value);
                            return;
                        }
                    }
                }
                dgvDatNuoc.Rows.Add(nuoc.MaNuocUong, nuoc.TenNuocUong, nuoc.Gia, 1, nuoc.Gia);
               
            }
            else
                MessageBox.Show("Vui lòng chọn Món Ăn");
        }

        private void btnDelNuoc_Click(object sender, EventArgs e)
        {
            if (dgvNuocUong.Rows.Count > 0)
            {
                DataGridViewRow data = dgvDatNuoc.CurrentRow;
                if (Convert.ToInt32(data.Cells["SoluongNuoc"].Value) > 1)
                {
                    data.Cells["SoluongNuoc"].Value = Convert.ToInt32(data.Cells["SoluongNuoc"].Value) - 1;
                    data.Cells["ThanhTienNuoc"].Value = Convert.ToInt32(data.Cells["ThanhTienNuoc"].Value) - Convert.ToInt32(data.Cells["GiaNuoc"].Value);
                    return;
                }
                dgvDatNuoc.Rows.RemoveAt(data.Index);
            }
            else
                MessageBox.Show("Vui lòng chọn Món cần xóa");
        }

        private void btnXacNhanNuoc_Click(object sender, EventArgs e)
        {
            hdnuoc= new List<Nuoc>();
            foreach (DataGridViewRow i in dgvDatNuoc.Rows)
            {
                if (Convert.ToInt32(i.Cells["MaNuoc"].Value) == 0)
                    break;
                hdnuoc.Add(new Nuoc
                {
                    MaNuoc = Convert.ToInt32(i.Cells["MaNuoc"].Value),
                    TenNuoc = Convert.ToString(i.Cells["TenNuoc"].Value),
                    Gia = Convert.ToInt32(i.Cells["GiaNuoc"].Value),
                    Soluong = Convert.ToInt32(i.Cells["SoluongNuoc"].Value),
                    Thanhtien = Convert.ToInt32(i.Cells["ThanhTienNuoc"].Value)
                });
            }
            dgvDatHangNuoc.DataSource = hdnuoc;
            MessageBox.Show("Đã thêm vào giỏ hàng");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HDDatHang hd = BLL_DatHang.Instance.AddDonHang(ID);
            if (hdmon != null)
            {
                BLL_DatHangMonAn.Instance.AddHoaDonMon(hdmon, hd.MaHDDatHang);
            }
            if (hdnuoc != null)
            {
                BLL_DatHangNuoc.Instance.AddHoaDonNuoc(hdnuoc, hd.MaHDDatHang);
            }
            MessageBox.Show("Đơn hàng đã được xác nhận");
            LoadHoaDon();
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dgvDonDatHang.CurrentRow.Cells["MaHDDatHang"].Value);
            BLL_DatHang.Instance.DellHoaDon(ID);
            MessageBox.Show("Hủy đơn thành công");
            LoadHoaDon();
        }

        private void btnSearchDon_Click(object sender, EventArgs e)
        {

        }

        private void btnChiTietDon_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dgvDonDatHang.CurrentRow.Cells["MaHDDatHang"].Value);
            CheckOrder f = new CheckOrder(ID);
            f.d += new CheckOrder.MyDel(LoadHoaDon);
            f.Show();
        }
    }
}
