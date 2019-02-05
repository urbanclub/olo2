using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2113
{
    public partial class Authform : Form
    {
        public Authform()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Regform reg = new Regform(this);
            reg.Show();
            this.Hide();
        }

        public string md5(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void Authform_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var connection = new SqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            string login = textBox1.Text;
            string password = textBox2.Text;
            SqlCommand cmd = new SqlCommand("SELECT id,admin FROM users WHERE login ='" + login + "' AND pass = '" + md5(password) + "'", connection);
            //SqlDataReader reader = cmd.ExecuteReader();
            //int authed = Convert.ToInt32(reader[0]);
            //int authed = Convert.ToInt32(cmd.ExecuteScalard
            SqlDataReader reader = cmd.ExecuteReader();
            int id = 0;
            int admin = 0;
            while (reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
            admin= Convert.ToInt32(reader[1]);
            }
           // MessageBox.Show("User:"+ id + ";admin:"+ admin);
           if(id>0 && admin==0)
            {
                Orderlist Odform = new Orderlist(this,id);
                Odform.Show();
                this.Hide();
            }else if(id>0 && admin ==1)
            {
                AdminList adform = new AdminList();
                adform.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Иди в пень!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
