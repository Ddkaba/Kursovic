using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kursovic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Human = "", Role = "";
            Check();
            string SQL = "SELECT idHuman, Role FROM gibddmodern.password WHERE Login = "+textBox1.Text+" and Password = SHA1("+textBox2.Text+");";
            MySQL(SQL, Human, Role);
        }

        public void MySQL(string SQL, string Human, string Role)
        {
            string str = "server=localhost;user=root;password=2506Russia5002;database=gibddmodern;port=3306";
            MySqlConnection connection = new MySqlConnection(str);

            try
            {
                connection.Open();
                string sql = SQL;
                MySqlCommand mySql = new MySqlCommand(sql, connection);
                MySqlDataReader reader = mySql.ExecuteReader();
                while(reader.Read())
                {
                    Human = reader["idHuman"].ToString();
                    Role = reader["Role"].ToString();
                }
                Check(Human, Role);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Check()
        {
           if(textBox1.Text == "" || textBox2.Text == "")
           {
                if(textBox1.Text == "")
                {
                    if(textBox2.Text == "") MessageBox.Show("Не заполнены оба поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Не заполнено поле Login", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Не заполнено поле Password", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }

        public void Check(string Human, string Role)
        {
            if(Human == "") MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(Human != "" & Role != "")
            {
                Form form2 = new Form2();
                form2.Show();
                this.Hide();
            }
        }
    }
}