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
            dataGridView1.Columns["NhanVien"].Visible = false;
            dataGridView1.Columns["MaNhanVien"].Visible = false;
            dataGridView1.Columns["Khach"].Visible = false;
            dataGridView1.Columns["MonAn_HDDatHang"].Visible = false;
            dataGridView1.Columns["NuocUong_HDDatHang"].Visible = false;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = BLL_MonAn.Instance.getAllMonAn();
            dataGridView2.Columns["MonAn_HDDatHang"].Visible = false;
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Text = "Thêm";
            buttonColumn.HeaderText = "Thêm";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(buttonColumn);
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
                        return;
                    }
                }
                dataGridView3.Rows.Add(mon.MaMonAn,mon.TenMonAn,mon.Gia,1,mon.Gia);
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView a = (sender as DataGridView);
            if (a.Columns[e.ColumnIndex].Name.Equals("SoLuong") && e.RowIndex >= 0)
            {
                a.Rows[e.RowIndex].Cells["ThanhTien"].Value = Convert.ToDouble(a.Rows[e.RowIndex].Cells["Gia"].Value) * Convert.ToInt32(a.Rows[e.RowIndex].Cells["SoLuong"].Value);
            }
        }
    }
}
