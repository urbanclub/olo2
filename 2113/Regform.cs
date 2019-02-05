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

namespace _2113
{
    public partial class Regform : Form
    {

        private Authform dddd;
        public Regform(Authform authform)
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int errors = 0;
            string outMessage = "";
            if(textBox1.Text == "")
            {
                errors = errors + 1;
                outMessage = outMessage + errors.ToString() + "Введите номер телефона\n";
            }
            
            if (textBox4.Text == "" || textBox5.Text == "" || textBox5.Text != textBox4.Text)
            { 
                 errors = errors + 1;
            outMessage = outMessage + errors.ToString() + "Введите корректный пароль\n";
        }
            errors = 0;
            if(errors == 0)
            {
                var connection = new SqlConnection(Properties.Settings.Default.dbConnectionString);
                connection.Open();
                dddd = new Authform();     
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO users(login,name,address,pass) VALUES(@login,@name,@address,@pass)", connection);
                    cmd.Parameters.AddWithValue("@login", textBox1.Text);
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@address", textBox3.Text);                  
                    cmd.Parameters.AddWithValue("@pass",dddd.md5(textBox4.Text));
                    int regged = Convert.ToInt32(cmd.ExecuteNonQuery());
                    connection.Close();
                    MessageBox.Show("Вы успешно зарегистрированы!");
                }catch
                {
                    MessageBox.Show("Пользователь существует!");

                }
            }
            else
            {
                MessageBox.Show("Есть ошибки!" + outMessage);
              
            }
            }
         
    }
}
