using DOAN.View;
using DoAnDatHang;
using DoAnDatHang.BLL;
using DoAnDatHang.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DOAN
{
    public partial class Staff : Form
    {
        int Id;
        Thread a;
        public Staff(int id = 0)
        {
            InitializeComponent();
            Id = id;
            LoadThongTin();
            ShowDon();
            LoadDay();
            LoadCBB();
            CheckNewDon();
        }
        public void LoadDtgvDon()
        {
            dtgvDon.DataSource = BLL_HoaDon.Instance.getAllHoaDonView(true);
            dtgvDon.Columns["MaHoaDon"].HeaderText = "Mã hoá đơn";
            dtgvDon.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
            dtgvDon.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dtgvDon.Columns["ThoiGian"].HeaderText = "Thời gian";
        }
        public void LoadDtgvDonxacnhan()
        {
            dtgvDonxacnhan.DataSource = BLL_HoaDon.Instance.getAllHoaDonView(false);
            dtgvDonxacnhan.Columns["MaHoaDon"].HeaderText = "Mã hoá đơn";
            dtgvDonxacnhan.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
            dtgvDonxacnhan.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dtgvDonxacnhan.Columns["ThoiGian"].HeaderText = "Thời gian";
        }
        public void CheckNewDon()
        {
            a = new Thread(() => {
                int prev = BLL_HoaDon.Instance.getAllHoaDonView(false).Count;
                while (true)
                {
                    Thread.Sleep(5000);
                    int now = BLL_HoaDon.Instance.getAllHoaDonView(false).Count;
                    if (now > prev)
                    {
                        ShowDon();
                    }
                }
            });
            a.Name = "Check";
            a.IsBackground = true;
            a.Start();
        }


        public void LoadKhach()
        {
            dtgv_KH.DataSource = BLL_KhachHang.Instance.getAllKhach();
            dtgv_KH.Columns["MaKhachHang"].HeaderText = "Mã khách hàng";
            dtgv_KH.Columns["HoTen"].HeaderText = "Họ tên";
            dtgv_KH.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dtgv_KH.Columns["SDT"].HeaderText = "Số điện thoại";
        }
        public void LoadCBB()
        {
            comboBox2.Items.AddRange(new string[] { "Theo ngày", "Theo tổng tiền" });
            comboBox1.Items.AddRange(new string[] { "Theo mã khách hàng", "Theo tên"});
        }
        public void LoadDay()
        {
            monthFrom.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            monthTo.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                yearFrom.Items.Add(i);
                yearTo.Items.Add(i);
            }
            comboDoanhthu.SelectedIndex = 0;
        }
        public void ShowDon()
        {

            if (dtgvDon.InvokeRequired)
            {
                dtgvDon.Invoke(new MethodInvoker(LoadDtgvDon));
            }
            else LoadDtgvDon();
            if (dtgvDonxacnhan.InvokeRequired)
            {
                dtgvDonxacnhan.Invoke(new MethodInvoker(LoadDtgvDonxacnhan));
            }
            else LoadDtgvDonxacnhan();
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
            LoadKhach();
        }
        public void ShowKhach(string Name, int Id)
        {
            List<KhachView> khaches = BLL_KhachHang.Instance.getAllKhach().Where(s => s.HoTen.Contains(Name) && (s.MaKhachHang == Id || Id == 0)).ToList();
            dtgv_KH.DataSource = khaches;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            AddCustomer ad = new AddCustomer(false);
            ad.reset += ShowKhach;
            ad.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AddCustomer edit = new AddCustomer(true);
            edit.reset += ShowKhach;
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
                int id = Convert.ToInt32(dtgvDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                OrderDetail od = new OrderDetail(id);
                od.ShowDialog();
            }
        }

        private void btnUpdatdonhang_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_HoaDon.Instance.getHoaDonViewByTime(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.Columns["MaHoaDon"].HeaderText = "Mã hoá đơn";
            dataGridView1.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
            dataGridView1.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dataGridView1.Columns["ThoiGian"].HeaderText = "Thời gian";
            txtTongdon.Text = dataGridView1.Rows.Count.ToString();
            int sum = 0;
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                sum += Convert.ToInt32(i.Cells["ThanhTien"].Value);
            }
            txtThanhtienDon.Text = sum.ToString();
        }

        private void btnAdđon_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.a += ShowDon;
            a.ShowDialog();
        }

        private void btnUpdateDT_Click(object sender, EventArgs e)
        {
            List<DoanhThu> doanhthu = new List<DoanhThu>();
            if (comboDoanhthu.SelectedIndex == 0)
            {
                doanhthu = BLL_HoaDon.Instance.getAllDoanhThuByDay(dateFromDoanhThu.Value, dateToDoanhThu.Value);
                if (doanhthu.Count == 0) MessageBox.Show("Không có đơn hàng nào");
                chart1.Titles.Clear();
                chart1.Titles.Add("Thành tiền theo ngày");
            }
            else if (comboDoanhthu.SelectedIndex == 1)
            {
                DateTime dateFrom = new DateTime(Convert.ToInt32(yearFrom.Text), Convert.ToInt32(monthFrom.Text), 1);
                DateTime dateTo = new DateTime(Convert.ToInt32(yearTo.Text), Convert.ToInt32(monthTo.Text), 1);
                doanhthu = BLL_HoaDon.Instance.getAllDoanhThuByMonth(dateFrom.Date,dateTo.Date);
                chart1.Titles.Clear();
                chart1.Titles.Add("Thành tiền theo tháng");
            }
            else
            {
                DateTime dateFrom = new DateTime(Convert.ToInt32(yearFrom.Text), Convert.ToInt32(monthFrom.Text), 1);
                DateTime dateTo = new DateTime(Convert.ToInt32(yearTo.Text), Convert.ToInt32(monthTo.Text), 1);
                doanhthu = BLL_HoaDon.Instance.getAllDoanhThuByYear(dateFrom.Date, dateTo.Date);
                if (doanhthu.Count == 0) MessageBox.Show("Không có đơn hàng nào");
                chart1.Titles.Clear();
                chart1.Titles.Add("Thành tiền theo năm");
            }
            chart1.DataSource = doanhthu;
            chart1.Series["Series1"].YValueMembers = "ThanhTien";
            chart1.Series["Series1"].XValueMember = "Ngay";
            txtDoanhthu.Text = doanhthu.Sum(s => s.ThanhTien).ToString();
        }

        private void comboDoanhthu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 2)
            {
                monthFrom.Enabled = false;
                monthFrom.Text = "1";
                monthTo.Enabled = false;
                monthTo.Text = "1";
                yearFrom.Enabled = true;
                yearTo.Enabled = true;
                dateFromDoanhThu.Enabled = !true;
                dateToDoanhThu.Enabled = !true;
            }
            else if ((sender as ComboBox).SelectedIndex == 0)
            {
                monthFrom.Enabled = false;
                monthTo.Enabled = false;
                yearFrom.Enabled = false;
                yearTo.Enabled = false;
                dateFromDoanhThu.Enabled = true;
                dateToDoanhThu.Enabled = true;
            }
            else if ((sender as ComboBox).SelectedIndex == 1)
            {
                monthFrom.Enabled = !false;
                monthTo.Enabled = !false;
                yearFrom.Enabled = !false;
                yearTo.Enabled = !false;
                dateFromDoanhThu.Enabled = !true;
                dateToDoanhThu.Enabled = !true;
            }
        }

        private void btnEditDon_Click(object sender, EventArgs e)
        {
            if (dtgvDon.SelectedRows.Count > 0)
            {
                int mahoadon = Convert.ToInt32(dtgvDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                AddOrder a = new AddOrder(mahoadon);
                a.ShowDialog();
            }
        }

        private void btnDelDon_Click(object sender, EventArgs e)
        {
            if (dtgvDon.SelectedRows.Count > 0)
            {
                int mahoadon = Convert.ToInt32(dtgvDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                BLL_MonAn.Instance.removeMonAnHDDatHangbyID(mahoadon);
                BLL_HoaDon.Instance.removeHoaDon(mahoadon);
                MessageBox.Show("Oke");
            }
        }

        private void btnUpdateDon_Click(object sender, EventArgs e)
        {
            ShowDon();
            dtgvDon.DataSource = BLL_HoaDon.Instance.getHoaDonViewByTime(dateTimePicker4.Value, dateTimePicker3.Value);
        }

        private void btnSortdon_Click(object sender, EventArgs e)
        {
            List<HoaDonView> a;
            if (txtDon.TextLength > 0)
            {
                int Id = Convert.ToInt32(txtDon.Text);
                a = BLL_HoaDon.Instance.GetHoaDonViewContainsID(Id, true);
            }
            else
            {
                a = BLL_HoaDon.Instance.getAllHoaDonView(true);
            }
            if (comboBox1.SelectedIndex == 1) a.Sort(HoaDonView.CompareByNgay);
            else a.Sort(HoaDonView.CompareByTongTien);
            DateTime to = dateTimePicker2.Value.AddDays(1);
            dtgvDon.DataSource = a.Select(s => s.ThoiGian.CompareTo(dateTimePicker1.Value) >= 0 && s.ThoiGian.CompareTo(to) <= 0);
        }

        private void btnSuaDonXacNhan_Click(object sender, EventArgs e)
        {
            if (dtgvDon.SelectedRows.Count > 0)
            {
                int mahoadon = Convert.ToInt32(dtgvDonxacnhan.SelectedRows[0].Cells["MaHoaDon"].Value);
                AddOrder a = new AddOrder(mahoadon);
                a.ShowDialog();
            }
        }

        private void btnSearchKH_Click(object sender, EventArgs e)
        {
            if (txt_SearchKH.Text.Length > 0)
            {
                dtgv_KH.DataSource = BLL_KhachHang.Instance.getKhach(Convert.ToInt32(txt_SearchKH.Text), txtSearchName.Text);
            }
            else
                dtgv_KH.DataSource = BLL_KhachHang.Instance.getAllKhach().Where(s => s.HoTen.Contains(txtSearchName.Text)).ToList();
        }

        private void btnSearchHD_Click(object sender, EventArgs e)
        {
            if (txtDon.TextLength > 0)
            {
                int Id = Convert.ToInt32(txtDon.Text);
                dtgvDon.DataSource = BLL_HoaDon.Instance.getHoaDonViewByTime(dateTimePicker4.Value, dateTimePicker3.Value).Where(s => s.MaHoaDon.ToString().Contains(Id.ToString()));
            }
            else
            {
                ShowDon();
            }
        }

        private void btnSortCustomer_Click(object sender, EventArgs e)
        {
            List<KhachView> a;
            if (txt_SearchKH.Text.Length > 0)
            {
                a = BLL_KhachHang.Instance.getKhach(Convert.ToInt32(txt_SearchKH.Text), txtSearchName.Text);
            }
            else
                a = BLL_KhachHang.Instance.getAllKhach();
            if (comboBox1.SelectedIndex == 1) a.Sort(KhachView.CompareByMa);
            else a.Sort(KhachView.CompareByTen);
            dtgv_KH.DataSource = a;
        }
    }
}
