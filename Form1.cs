using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class FormLogin : Form
    {
        BanHang banHang;
        string ten = "";
        string tenDn = "";
        bool type = false;
        SqlConnection cnn;
        public FormLogin()
        {
            InitializeComponent();
            ReadSettings();
            string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
        }
        private void ReadSettings()
        {
            if (Properties.Settings.Default.RememberMe == "true")
            {
                textBoxUsername.Text = Properties.Settings.Default.Username;
                textBoxPassword.Text = Properties.Settings.Default.Password;
                remember.Checked = true;
            }
            else
            {
                textBoxUsername.Text = "";
                textBoxPassword.Text = "";
                remember.Checked = false;
            }
        }
        private void SaveSettings()
        {
            if (remember.Checked)
            {
                Properties.Settings.Default.Username = textBoxUsername.Text;
                Properties.Settings.Default.Password = textBoxPassword.Text;
                Properties.Settings.Default.RememberMe = "true";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = textBoxUsername.Text;
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.RememberMe = "false";
                Properties.Settings.Default.Save();
            }
        }
        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.SystemColors.Highlight;
            label4.Visible = false;
            if (textBoxPassword.Text != "")
                buttonLogin.Enabled = true;
            else
                buttonLogin.Enabled = false;
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            panel2.BackColor = System.Drawing.SystemColors.Highlight;
            label5.Visible = false;
            if (textBoxUsername.Text != "")
                buttonLogin.Enabled = true;
            else
                buttonLogin.Enabled = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            int tk=0, mk=0;
            String sql = "SELECT * FROM Users";
            SqlCommand cm = new SqlCommand(sql, cnn);
            SqlDataReader rd = cm.ExecuteReader();
            while (rd.Read())
            {
                if (textBoxUsername.Text != rd["tenDangNhap"].ToString())
                {
                    tk = 0;
                }
                if (textBoxPassword.Text != rd["matKhau"].ToString())
                    mk = 0;
                if(textBoxUsername.Text==rd["tenDangNhap"].ToString() && textBoxPassword.Text!=rd["matKhau"].ToString())
                {
                    tk = 1;
                    mk = 0;
                }
                if (textBoxUsername.Text != rd["tenDangNhap"].ToString() && textBoxPassword.Text == rd["matKhau"].ToString())
                {
                    tk = 0;
                    mk = 1;
                }
                if (textBoxUsername.Text==rd["tenDangNhap"].ToString() && textBoxPassword.Text == rd["matKhau"].ToString())
                {
                    tk = 1;
                    mk = 1;
                    if (rd["typeAccount"].ToString() == "True")
                        type = true;
                    else
                        type = false;
                    ten = rd["hoTen"].ToString();
                    tenDn = rd["tenDangNhap"].ToString();
                    break;
                }
            }
            if(tk==0)
            {
                panel1.BackColor = Color.Red;
                label4.Visible = true;
                label4.Text="Tên đăng nhập không đúng";
                cnn.Close();
            } 
            if (mk == 0)
            {
                panel2.BackColor = Color.Red;
                label5.Visible = true;
                label5.Text = "Mật khẩu không đúng";
                cnn.Close();
            }
            if(tk==1 && mk==1)
            {
                SaveSettings();
                cnn.Close();
                banHang = new BanHang(cnn, ten,tenDn, type);
                banHang.Show();
                this.Hide();
                banHang.FormClosed += banHang_FormClosed;
                banHang._btn_Huy.Click += _btn_Huy_Click;
            }

           
        }

        void _btn_Huy_Click(object sender, EventArgs e)
        {
            banHang.Huy();
            this.Show();
        }

        void banHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        
    }

}
