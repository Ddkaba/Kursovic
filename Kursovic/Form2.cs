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
using System.Text.RegularExpressions;

namespace Kursovic
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            Check();
            int index;
            string SQL = "call gibddmodern.Number_Car('" + textBox1.Text + textBox2.Text +"');";
            MySQL(SQL, index = 0);
            SQL = "call gibddmodern.Wanted('" + textBox1.Text + textBox2.Text + "')";
            MySQL(SQL, index = 1);
        }

        public void Check()
        {
            if (textBox1.Text == "A000AA" & textBox2.Text == "47")
            {
                if (textBox1.Text == "A000AA")
                {
                    if (textBox2.Text == "47") MessageBox.Show("Номерной знак не введен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Не заполнена часть с номером", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Не заполнена часть с регионом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Regex myReg = new Regex(@"^[АВСЕНКМОРТХУ]{1}[0-9]{3}[АВСЕНКМОРТХУ]{2}[0-9]{2,3}$");
                if(myReg.IsMatch(textBox1.Text+textBox2.Text) == false)
                {
                    MessageBox.Show("Номер не соответствует ГОСТу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void MySQL(string SQL, int index)
        {
            string str = "server=localhost;user=root;password=2506Russia5002;database=gibddmodern;port=3306";
            MySqlConnection connection = new MySqlConnection(str);
            try
            {
                connection.Open();
                string sql = SQL;
                MySqlCommand mySql = new MySqlCommand(sql, connection);
                Reader(mySql,index);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Reader(MySqlCommand mySql, int index)
        {
            string Sur = "", Name = "", MiddleName = "";
            MySqlDataReader reader = mySql.ExecuteReader();

            if (index == 0)
            {
                while (reader.Read())
                {
                    Sur = reader["Surname"].ToString();
                    Name = reader["Name"].ToString();
                    MiddleName = reader["MiddleName"].ToString();
                    textBox4.Text = reader["PassportNumber"].ToString();
                    textBox5.Text = reader["NameBrand"].ToString();
                    textBox6.Text = reader["NameModel"].ToString();
                    textBox7.Text = reader["BodyNumber"].ToString();
                    textBox8.Text = reader["Color"].ToString();
                    textBox9.Text = reader["Category"].ToString();
                    textBox10.Text = reader["Description"].ToString();
                }
                textBox3.Text = Sur + " " + Name + " " + MiddleName; 
            }
            if(index == 1)
            {
                while (reader.Read())
                {
                    textBox12.Text = reader["DateWanted"].ToString();
                    textBox13.Text = reader["IdCar"].ToString();
                    textBox14.Text = reader["SpecialSigns"].ToString();
                    textBox15.Text = reader["Causes"].ToString();
                }
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = System.Drawing.Color.Black;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = System.Drawing.Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
