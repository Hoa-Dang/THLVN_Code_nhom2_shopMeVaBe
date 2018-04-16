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
using System.Data.Sql;

namespace WindowsFormsApplication1
{
    
    public partial class MatHang : Form
    {
        SqlConnection conect = new SqlConnection("Data Source=DESKTOP-31FIH19\\HOADANG;Initial Catalog=shopMeVaBe;Integrated Security=True");
        String type, maHangCu, ncc;
        Load load;
        public MatHang(String t, Load l)
        {
            InitializeComponent();
            type = t;
            button1.Text = type;
            this.Load += MatHang_Load;
            button1.Click +=button1_Click;
            load = l;
        }
        public MatHang(String t, String ma, String ten, int giaB, int giaV, int sl, string n, Load l)
        {
            InitializeComponent();
            type = t;
            load = l;
            button1.Text = type;
            maHangCu = ma;
            textBox1.Text = ma;
            textBox2.Text = ten;
            textBox3.Text = giaV + "";
            textBox4.Text = giaB + "";
            textBox5.Text = sl + "";
            ncc= n;
            this.Load += MatHang_Load;
            button1.Click += button1_Click;
        }
        void MatHang_Load(object sender, EventArgs e)
        {

            conect.Open();
            string sql = "select tenNhaCungCap from NhaCungCap";
            SqlCommand cmd = new SqlCommand(sql, conect);
            SqlDataAdapter dt = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            dt.Fill(tb);
            comboBox1.Items.Clear();
            foreach (DataRow item in tb.Rows)
            {
                comboBox1.Items.Add(item[0].ToString());
            }
            comboBox1.SelectedItem = ncc;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(type.Equals("Cập nhật")){
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Mã Hàng là bắt buộc vui lòng nhập đầy đủ !!!", "Có lỗi xảy ra !!!");
                }
                else
                {
                    string mh = textBox1.Text;
                    string query = "select maHang from MatHang where maHang = '" + mh + "'";
                    SqlCommand cmd = new SqlCommand(query, conect);
                    SqlDataReader maH = cmd.ExecuteReader();
                    if (maH.Read() && !mh.Equals(maHangCu))
                    {
                        maH.Close();
                        MessageBox.Show("Mã hàng đã tồn tại");
                    }
                    else
                    {
                        maH.Close();
                        string th = textBox2.Text;
                        float gv = float.Parse(textBox3.Text);
                        float gb = float.Parse(textBox4.Text);
                        int sl = Int32.Parse(textBox5.Text);
                        string item = (string)this.comboBox1.SelectedItem;
                        string getNCC = "select maNhaCungCap from NhaCungCap where tenNhaCungCap ='" + item + "'";
                        cmd = new SqlCommand(getNCC, conect);
                        SqlDataReader dtr = cmd.ExecuteReader();
                        if (dtr.Read())
                        {
                            String maNCC = (string)dtr[0];
                            dtr.Close();
                            string update = "update MatHang set maHang='" + mh + "',tenHang= N'" + th + "', giaVon=" + gv + ", giaBan=" + gb + ",soLuong=" + sl + ",maNhaCungCap = N'" + maNCC + "' where maHang='" + maHangCu + "'";
                            cmd = new SqlCommand(update, conect);
                            try
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Update dữ liệu thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                load();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Update dữ liệu không thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            conect.Close();
                        }
                        else
                        {
                            MessageBox.Show("Tên nhà cung cấp không đúng");
                        }

                    }
                }
            }
            else if (type.Equals("Tạo mới"))
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Mã Hàng là bắt buộc vui lòng nhập đầy đủ !!!", "Có lỗi xảy ra !!!");
                }
                else
                {
                    string mh = textBox1.Text;
                    string query = "select maHang from MatHang where maHang = '"+mh+"'";
                    SqlCommand cmd = new SqlCommand(query, conect);
                    SqlDataReader maH = cmd.ExecuteReader();
                    if (maH.Read())
                    {
                        maH.Close();
                        MessageBox.Show("Mã hàng đã tồn tại");
                    }
                    else
                    {
                        maH.Close();
                        string th = textBox2.Text;
                        float gv = float.Parse(textBox3.Text);
                        float gb = float.Parse(textBox4.Text);
                        int sl = Int32.Parse(textBox5.Text);
                        string item = (string)this.comboBox1.SelectedItem;
                        string getNCC = "select maNhaCungCap from NhaCungCap where tenNhaCungCap ='" + item + "'";
                        cmd = new SqlCommand(getNCC, conect);
                        SqlDataReader dtr = cmd.ExecuteReader();
                        if (dtr.Read())
                        {
                            String maNCC = (string)dtr[0];
                            dtr.Close();
                            string update = "insert into MatHang values ('" + mh + "', N'" + th + "', " + gv + ", " + gb + "," + sl + ",'" + maNCC + "')";
                            cmd = new SqlCommand(update, conect);
                            try
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Tạo sản phẩm thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                load();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Không thể tạo sản phẩm !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            dtr.Close();
                            MessageBox.Show("Tên nhà cung cấp không đúng");
                        }
                    }
                    

                }
            }

            

        }
    }
}
