
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;



namespace WindowsFormsApplication1
{
    public delegate void Load();
    public partial class BanHang : Form
    {
        Load load; 
        public Button _btn_Huy
        {
            get { return btnHuy; }
        }
        SqlConnection conn;
        string user;
        string tenDn;
        int maHoaDon;
        bool typeAccount = false;
        int tongTien = 0;
        SqlCommand cmd;
        string sql;
        SqlDataAdapter data;
        bool stateTaoMoi = false;
        bool stateCapNhat = false;
        public BanHang(SqlConnection c, String u, string tdn, bool type)
        {
            InitializeComponent();
            load = ShowData;
            conn = c;
            conn.Open();
            this.Load +=Form1_Load;
            dtgBangGia.CellClick += dtgBangGia_CellClick;
            dtgHangMua.CellValueChanged += dtgHangMua_CellValueChanged;
            txtChietKhau.Leave += txtChietKhau_Leave;
            txtTienKhachDua.TextChanged += txtTienKhachDua_Leave;
            dtgHangMua.CellClick += dtgHangMua_CellClick;
            lbTongTien.TextChanged += lbTongTien_TextChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            btnTaoMoi.Click += btnTaoMoi_Click;
            btnIn.Click += btnIn_Click;
            user = u;
            tenDn = tdn;
            typeAccount = type;
            //tab bang hang
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            btnDelete.Click += btnDelete_Click;
            buttonCreate.Click += buttonCreate_Click;
            buttonSearch.Click +=buttonSearch_Click;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            string mahang = (String) dataGridView1.CurrentRow.Cells[0].Value;
            string del = "delete  from [MatHang] where maHang='" + mahang + "'";
            cmd = new SqlCommand(del, conn);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Đã xóa!");
                ShowData();
            }
            else
                MessageBox.Show("Lỗi!");
            
        }

        void btnTaoMoi_Click(object sender, EventArgs e)
        {
            dtgHangMua.Rows.Clear();
            txtKhachHang.Text = "";
            lbTongTien.Text = "0";
            txtChietKhau.Text = "";
            txtTienKhachDua.Text = "";
            lbTienTraKhach.Text = "0";
        }
        public void Huy()
        {
            dtgHangMua.Rows.Clear();
            txtKhachHang.Text = "";
            lbTongTien.Text = "0";
            txtChietKhau.Text = "";
            txtTienKhachDua.Text = "";
            lbTienTraKhach.Text = "0";
            //
            String q = "select max(maHoaDon) from HoaDon";
            SqlCommand comm = new SqlCommand(q, conn);
            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            if (reader.IsDBNull(0))
            {
                maHoaDon = 0;
            }
            else
                maHoaDon = Int32.Parse(reader[0].ToString()) + 1;
            lbMaHoaDon.Text = maHoaDon.ToString();
            reader.Close();
            //
            Hide();
        }

        void btnTimKiem_Click(object sender, EventArgs e)
        {
            String query = "select maHang as [Mã Hàng], tenHang as [Tên Hàng], giaBan as [Giá Bán] from MatHang where maHang= '"+txtTimKiem.Text+"'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dtgBangGia.DataSource = table;
        }
       

        void lbTongTien_TextChanged(object sender, EventArgs e)
        {
            if (IsNumber(txtTienKhachDua.Text))
            {

                int kd = Convert.ToInt32(txtTienKhachDua.Text);
                lbTienTraKhach.Text = kd - Convert.ToInt32(lbTongTien.Text) + "";
            }
        }

        void dtgHangMua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                tongTien -= Convert.ToInt32(dtgHangMua.Rows[e.RowIndex].Cells[2].Value) * Convert.ToInt32(dtgHangMua.Rows[e.RowIndex].Cells[4].Value);
                lbTongTien.Text = Convert.ToInt32(lbTongTien.Text) -  Convert.ToInt32(dtgHangMua.Rows[e.RowIndex].Cells[2].Value) * Convert.ToInt32(dtgHangMua.Rows[e.RowIndex].Cells[4].Value) + "";
                dtgHangMua.Rows.RemoveAt(e.RowIndex);
            }
        }

        void txtTienKhachDua_Leave(object sender, EventArgs e)
        {
            if (txtTienKhachDua.Text.Equals(""))
                lbTienTraKhach.Text = 0 + "";
            else if (IsNumber(txtTienKhachDua.Text))
            {
                int kd = Convert.ToInt32(txtTienKhachDua.Text);
                lbTienTraKhach.Text = kd - Convert.ToInt32(lbTongTien.Text) + "";

            }
            else
            {
                MessageBox.Show("Định dạng số không đúng.");
                txtTienKhachDua.Text = "";
                lbTienTraKhach.Text = "0";
            }
        }

        void txtChietKhau_Leave(object sender, EventArgs e)
        {
            if (txtChietKhau.Text.Equals(""))
                lbTongTien.Text = tongTien + "";
            else if (IsNumber(txtChietKhau.Text))
            {
                Double ck = Convert.ToDouble(txtChietKhau.Text);
                lbTongTien.Text = (tongTien - tongTien * (ck / 100)) + "";
                
            }
            else
            {
                MessageBox.Show("Định dạng số không đúng.");
                txtChietKhau.Text = "";
                lbTongTien.Text = tongTien + "";
            }
        }


        void btnIn_Click(object sender, EventArgs e)
        {
            if (txtKhachHang.Text.Equals("") || txtTienKhachDua.Text.Equals("") || dtgHangMua.Rows.Count == 0)
                MessageBox.Show("Chưa đủ điệu kiện để in");
            else {
                try
                {
                    PrintDialog _PrintDialog = new PrintDialog();
                    PrintDocument _PrintDocument = new PrintDocument();

                    _PrintDialog.Document = _PrintDocument; //add the document to the dialog box

                    _PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(_CreateReceipt); //add an event handler that will do the printing
                    //on a till you will not want to ask the user where to print but this is fine for the test envoironment.
                    DialogResult result = _PrintDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        _PrintDocument.Print();
                        string[] s = lbNgay.Text.Split('/');
                        string ngay = s[2] + "-" + s[1] + "-" + s[0];
                        int ck = 0;
                        if (!txtChietKhau.Text.Equals(""))
                            ck = Convert.ToInt32(txtChietKhau.Text);
                        int kd = Convert.ToInt32(txtTienKhachDua.Text);
                        MessageBox.Show(ck + " " + kd);
                        String query = "insert into HoaDon values('" + lbMaHoaDon.Text + "', '" + tenDn + "',N'" + txtKhachHang.Text + "','" + ngay + "'," + ck + "," + kd + ")";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.ExecuteNonQuery();
                        foreach (DataGridViewRow row in dtgHangMua.Rows)
                        {
                            query = "insert into chiTietHoaDon values('" + lbMaHoaDon.Text + "','" + (string)row.Cells[0].Value + "'," + (int)row.Cells[2].Value + ")";
                            comm = new SqlCommand(query, conn);
                            comm.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception)
                {

                }
            }

            
        }

        //
        private void _CreateReceipt(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New", 12);
            float FontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphic.DrawString("SHOP MẸ VÀ BÉ", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);
            string top = "Tên Hàng".PadRight(24) + "Giá bán " + " Số lượng "+ " Thành Tiền";
            graphic.DrawString(top, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight; //make the spacing consistent
            graphic.DrawString("-----------------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 5; //make the spacing consistent

            foreach (DataGridViewRow row in dtgHangMua.Rows)
            {
                string tenHang = (string) row.Cells[1].Value;
                int giaBan = (int) row.Cells[3].Value;
                int SoLuong = (int) row.Cells[2].Value;
                int thanhTien = (int) row.Cells[4].Value;

                graphic.DrawString(tenHang, font, new SolidBrush(Color.Black), startX, startY + offset);
                graphic.DrawString(giaBan.ToString(), font, new SolidBrush(Color.Black), startX + 250, startY + offset);
                graphic.DrawString(SoLuong.ToString(), font, new SolidBrush(Color.Black), startX + 340, startY + offset);
                graphic.DrawString(thanhTien.ToString(), font, new SolidBrush(Color.Black), startX + 440, startY + offset);
                offset = offset + (int)FontHeight + 5; //make the spacing consistent              
            }

            offset = offset + 30; //make some room so that the total stands out.

            graphic.DrawString("TỔNG TIỀN TRẢ ", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            graphic.DrawString(lbTongTien.Text, new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 250, startY + offset);

            offset = offset + (int)FontHeight + 5; //make the spacing consistent              
            graphic.DrawString("TIỀN KHÁCH ĐƯA ", font, new SolidBrush(Color.Black), startX, startY + offset);
            graphic.DrawString(txtTienKhachDua.Text, font, new SolidBrush(Color.Black), startX + 250, startY + offset);

            offset = offset + (int)FontHeight + 5; //make the spacing consistent              
            graphic.DrawString("TIỀN TRẢ KHÁCH", font, new SolidBrush(Color.Black), startX, startY + offset);
            graphic.DrawString(lbTienTraKhach.Text, font, new SolidBrush(Color.Black), startX + 250, startY + offset);

            offset = offset + 30; //make the spacing consistent              
            graphic.DrawString(" CẢM ƠN BẠN ĐÃ GHÉ THĂM!,", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)FontHeight + 5; //make the spacing consistent              
            graphic.DrawString("HI VỌNG BẠN SẼ GHÉ THĂM LẠI!", font, new SolidBrush(Color.Black), startX, startY + offset);
        }
        //

        void dtgHangMua_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgHangMua.Rows[e.RowIndex];
            if (IsNumber(row.Cells[2].Value.ToString()))
            {
                row.Cells[4].Value = Convert.ToInt32(row.Cells[3].Value) * Convert.ToInt32(row.Cells[2].Value);
                tongTien = 0;
                foreach(DataGridViewRow r in dtgHangMua.Rows)
                    tongTien += Convert.ToInt32(r.Cells[4].Value);
                lbTongTien.Text = tongTien + "";
            }
            else
            {
                MessageBox.Show("Số lượng Không hợp lệ");
            }
        }

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        void dtgBangGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                DataGridViewCellCollection cells = dtgBangGia.Rows[e.RowIndex].Cells;
                bool kt = true;
                foreach (DataGridViewRow row in dtgHangMua.Rows)
                {
                    if (row.Cells[0].Value == cells[0].Value)
                    {
                        kt = false;
                        break;
                    }
                }
                    
                if (kt)
                {
                    dtgHangMua.Rows.Add(cells[0].Value, cells[1].Value, 1, cells[2].Value, cells[2].Value);
                    tongTien = tongTien + Convert.ToInt32(cells[2].Value);
                    lbTongTien.Text = tongTien + "";
                }
                    
            }
     
        }

        void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
            textBox1.GotFocus += new EventHandler(this.TextGotFocus);
            textBox1.LostFocus += new EventHandler(this.TextLostFocus);
            ShowData();
            loadHoaDon();
        }

        private void TextGotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Nhập tên hàng,mã hàng...")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;

            }
        }
        private void TextLostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Nhập tên hàng,mã hàng...")
            {
                tb.Text = "";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string chuoi = textBox1.Text.ToString();
            sql = "select maHang , tenHang , giaVon ,giaBan ,soLuong , tenNhaCungCap from [MatHang] , [NhaCungCap] where MatHang.maNhaCungCap = NhaCungCap.maNhaCungCap and (maHang like '%" + chuoi + "%' or tenHang like N'%" + chuoi + "%' )";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader re = cmd.ExecuteReader();
            if (re.Read() == false)
                MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            else
            {
                conn.Close();
                conn.Open();
                DataTable dt2 = new DataTable();
                data = new SqlDataAdapter(sql, conn);
                data.Fill(dt2);
                dataGridView1.DataSource = dt2;
            }
            
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (stateTaoMoi == false)
            {
                MatHang matHang = new MatHang("Tạo mới", load);
                matHang.Show();
                stateTaoMoi = true;
                matHang.FormClosed += matHang_FormClosed1;
            }
            
        }

        void matHang_FormClosed1(object sender, FormClosedEventArgs e)
        {
            stateTaoMoi = false;
        }
        private void ShowData()
        {
            String query = "select maHang as [Mã Hàng], tenHang as [Tên Hàng], giaBan as [Giá Bán] from MatHang";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dtgBangGia.DataSource = table;
            dtgBangGia.Columns[1].Width = 125;
            lbTenNV.Text = user;
            lbNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            //tab Bán Hàng
            String q = "select maHang, count(maHang) from chiTietHoaDon group by maHang";

            DataTable dt = new DataTable();
            sql = "select MatHang.maHang , tenHang  ,giaBan , giaVon, MatHang.soLuong , tenNhaCungCap  from [MatHang] , [NhaCungCap] where MatHang.maNhaCungCap=NhaCungCap.maNhaCungCap ";
            data = new SqlDataAdapter(sql, conn);
            data.Fill(dt);
            dt.Columns.Add(new DataColumn("tonkho"));
            dataGridView1.DataSource = dt;
            foreach (DataRow row in dt.Rows){
                row["tonkho"] = row[4];
            }
          
            SqlCommand com = new SqlCommand(q,conn);
            SqlDataReader reader = com.ExecuteReader();
            while(reader.Read()){
                foreach (DataRow row in dt.Rows)
                {
                    if (((string)row[0]).Equals((string)reader[0]))
                        row["tonkho"] = (int)row[4] - (int)reader[1];
                }
            }
            reader.Close();
            if (dataGridView1.Rows.Count > 0)
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
        }
        public void loadHoaDon()
        {
            string query = "select max(maHoaDon) from HoaDon";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            if (reader.IsDBNull(0))
            {
                maHoaDon = 0;
            }
            else
                maHoaDon = Int32.Parse(reader[0].ToString()) + 1;
            lbMaHoaDon.Text = maHoaDon.ToString();
            reader.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (stateCapNhat == false)
            {
                string mh = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                string th = (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                int gb = (int)dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                int gv = (int)dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                int sl = (int)dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                string ncc = (string)dataGridView1.Rows[e.RowIndex].Cells[5].Value;
                MatHang matHang = new MatHang("Cập nhật", mh, th, gb, gv, sl, ncc, load);
                matHang.Show();
                stateCapNhat = true;
                matHang.FormClosed +=matHang_FormClosed2;
            }
            

        }
        void matHang_FormClosed2(object sender, FormClosedEventArgs e)
        {
            stateCapNhat = false;
        }
        
    }
}

