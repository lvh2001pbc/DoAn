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
using DoAnDatHang;

namespace DOAN.View
{
    public partial class Customer : Form
    {
        int Id;
        public Customer(int ID)
        {
            InitializeComponent();
            Id = ID;
            SetTaiKhoan();
        }
        public void SetTaiKhoan()
        {
            Khach khach = BLL_KhachHang.Instance.getKhachByID(Id);
            textBox2.Text = khach.MaKhachHang.ToString();
            txtName.Text = khach.HoTen;
            txtEmail.Text = khach.Email;
            txtAddress.Text = khach.DiaChi;
            txtSDT.Text = khach.SDT;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Khach khach = BLL_KhachHang.Instance.getKhachByID(Id);
            dataGridView1.DataSource = khach.HDDatHangs.Where(s => s.ThoiGian.Date.CompareTo(dateTimePicker1.Value.Date) >= 0 && s.ThoiGian.Date.CompareTo(dateTimePicker2.Value.Date) <= 0).ToList();
            dataGridView1.Columns["MaKhachHang"].Visible = false;
            dataGridView1.Columns["Khach"].Visible = false;
            dataGridView1.Columns["MonAn_HDDatHang"].Visible = false;
            dataGridView1.Columns["NuocUong_HDDatHang"].Visible = false;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = BLL_MonAn.Instance.getAllMonAnView();
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Text = "Thêm";
            buttonColumn.HeaderText = "Thêm";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(buttonColumn);
            dataGridView5.DataSource = BLL_NuocUong.Instance.getAllNuocView();
            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            buttonColumn1.Text = "Thêm";
            buttonColumn1.HeaderText = "Thêm";
            buttonColumn1.UseColumnTextForButtonValue = true;
            dataGridView5.Columns.Add(buttonColumn1);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView a = (sender as DataGridView);
            if (a.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                MonAn mon = BLL_MonAn.Instance.getMonAnByID(Convert.ToInt32(a.Rows[e.RowIndex].Cells["MaMonAn"].Value));
                foreach (DataGridViewRow i in dataGridView3.Rows)
                {
                    if (Convert.ToInt32(i.Cells["MaMonAn"].Value) == Convert.ToInt32(a.Rows[e.RowIndex].Cells["MaMonAn"].Value))
                    {
                        i.Cells["SoLuong"].Value = Convert.ToInt32(i.Cells["SoLuong"].Value) + 1;
                        SetThanhTien();
                        return;
                    }
                }
                dataGridView3.Rows.Add(mon.MaMonAn,mon.TenMonAn,mon.Gia,1,mon.Gia);
                SetThanhTien();
            }
        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView a = (sender as DataGridView);
            if (a.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                NuocUong mon = BLL_NuocUong.Instance.getNuocByID(Convert.ToInt32(a.Rows[e.RowIndex].Cells["MaNuocUong"].Value));
                foreach (DataGridViewRow i in dataGridView4.Rows)
                {
                    if (Convert.ToInt32(i.Cells["MaNuocUong"].Value) == Convert.ToInt32(a.Rows[e.RowIndex].Cells["MaNuocUong"].Value))
                    {
                        i.Cells["SoLuongNuoc"].Value = Convert.ToInt32(i.Cells["SoLuongNuoc"].Value) + 1;
                        SetThanhTien();
                        return;
                    }
                }
                dataGridView4.Rows.Add(mon.MaNuocUong, mon.TenNuocUong, mon.Gia, 1, mon.Gia);
                SetThanhTien();
            }
        }
        private void SetThanhTien()
        {
            double b = 0;
            try
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dataGridView3.Rows[i].Cells["ThanhTien"].Value) != null)
                    {
                        b += Convert.ToDouble(dataGridView3.Rows[i].Cells["ThanhTien"].Value);
                    }
                }
                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dataGridView4.Rows[i].Cells["ThanhTienNuoc"].Value) != null)
                    {
                        b += Convert.ToDouble(dataGridView4.Rows[i].Cells["ThanhTienNuoc"].Value);
                    }
                }

            }
            catch
            {

            }
            textBox3.Text = b.ToString();
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView a = (sender as DataGridView);
            if (a.Columns[e.ColumnIndex].HeaderText.Equals("Số lượng") && e.RowIndex >= 0)
            {
                a.Rows[e.RowIndex].Cells["ThanhTien"].Value = Convert.ToDouble(a.Rows[e.RowIndex].Cells["Gia"].Value) * Convert.ToInt32(a.Rows[e.RowIndex].Cells["SoLuong"].Value);
            }
            SetThanhTien();
        }
        private void dataGridView5_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView a = (sender as DataGridView);
            if (a.Columns[e.ColumnIndex].HeaderText.Equals("Số lượng") && e.RowIndex >= 0)
            {
                a.Rows[e.RowIndex].Cells["ThanhTienNuoc"].Value = Convert.ToDouble(a.Rows[e.RowIndex].Cells["GiaNuoc"].Value) * Convert.ToInt32(a.Rows[e.RowIndex].Cells["SoLuongNuoc"].Value);
            }
            SetThanhTien();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var mon = new List<MonAn_HDDatHang>();
            var nuoc = new List<NuocUong_HDDatHang>();
            HDDatHang hoadon = new HDDatHang
            {
                ThoiGian = DateTime.Now,
                //   MaNhanVien = (comboBox2.SelectedItem as NhanVien).MaNhanVien,
                TrangThai = false,
                MaKhachHang = Id
            };
            int ma = BLL_HoaDon.Instance.createHoaDon(hoadon);
            foreach (DataGridViewRow i in dataGridView3.Rows)
            {
                mon.Add(new MonAn_HDDatHang
                {
                    MaMonAn = Convert.ToInt32(i.Cells["MaMonAn"].Value),
                    Gia = Convert.ToInt32(i.Cells["Gia"].Value),
                    SoLuong = Convert.ToInt32(i.Cells["SoLuong"].Value),
                    MaHDDHang = ma
                });
            }
            foreach (DataGridViewRow i in dataGridView4.Rows)
            {
                nuoc.Add(new NuocUong_HDDatHang
                {
                    MaNuocUong = Convert.ToInt32(i.Cells["MaNuocUong"].Value),
                    Gia = Convert.ToInt32(i.Cells["GiaNuoc"].Value),
                    SoLuong = Convert.ToInt32(i.Cells["SoLuongNuoc"].Value),
                    MaHDDatHang = ma
                });
            }
            if(BLL_MonAn.Instance.addMonAnHDDatHang(mon) &&
            BLL_NuocUong.Instance.addNuocUong_HDDatHang(nuoc))
            {
                MessageBox.Show("Thanh cong");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MaHoaDon"].Value);
                OrderDetail od = new OrderDetail(id);
                od.ShowDialog();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView a = (sender as DataGridView);
            if (a.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                a.Rows.Remove(a.Rows[e.RowIndex]);
                SetThanhTien();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
