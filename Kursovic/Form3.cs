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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        int Count, Counter,DownCounter, index;
        string SQL;
        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            Masked();
            Check();
            Clear();
            SQL = "SELECT COUNT(*) FROM protocols Where idCar = (Select idCar FROM gibddmodern.cars Where Number = '" + textBox1.Text + textBox2.Text + "' and PassportNumber = '" + maskedTextBox1.Text + "');";
            MySQL(SQL, 1);
            if(Count == 1)
            {
                SQL = "call gibddmodern.Found_Fine('" + textBox1.Text + textBox2.Text + "','" + maskedTextBox1.Text + "');";
                MySQL(SQL, index = 0);
            }
            if(Count > 1)
            {
                button2.Enabled = true;
                SQL = "call gibddmodern.FineLimit(0,1,'" + textBox1.Text + textBox2.Text + "','" + maskedTextBox1.Text + "');";
                MySQL(SQL, 0);
                Counter = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            Counter++;
            if(Counter <= Count)
            {
                Clear();
                SQL = "call gibddmodern.FineLimit(" + Counter + ",1,'" + textBox1.Text + textBox2.Text + "','" + maskedTextBox1.Text + "');";
                MySQL(SQL, 0);
                if(Counter == Count-1) button2.Enabled = false;
            }
            else button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            Counter--;
            if (Counter >= 0)
            {
                Clear();
                SQL = "call gibddmodern.FineLimit(" + Counter + ",1,'" + textBox1.Text + textBox2.Text + "','" + maskedTextBox1.Text + "');";
                MySQL(SQL,0);
                if (Counter == 0) button3.Enabled = false;
            }
            else button3.Enabled = false;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = System.Drawing.Color.Black;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = System.Drawing.Color.Black;
        }

        public void MySQL(string SQL, int index)
        {
            Connector NEW = new Connector();
            NEW.SQL(SQL);
            MySqlDataReader reader = NEW.reader1();
            switch (index)
            {
                case 0:
                    string Sur = "", Name = "", MiddleName = "";
                    while (reader.Read())
                    {
                        textBox3.Text = reader["IdProtocol"].ToString();
                        Sur = reader["Surname"].ToString();
                        Name = reader["Name"].ToString();
                        MiddleName = reader["MiddleName"].ToString();
                        textBox5.Text = reader["IdFine"].ToString();
                        textBox6.Text = reader["Description"].ToString();
                        textBox7.Text = reader["Punishment"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["Date"]);
                        textBox8.Text = reader["Place"].ToString();
                        textBox9.Text = reader["DescriptionFine"].ToString();
                    }
                    textBox4.Text = Sur + " " + Name + " " + MiddleName;
                    break;
                case 1:
                    while (reader.Read())
                    {
                        Count = Convert.ToInt32(reader["COUNT(*)"]);
                        textBox10.Text = Count.ToString();
                    }
                    break;
            }
        }

        public void Clear()
        {
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
        }

        public bool Masked()
        {
            if(maskedTextBox1.Text == "")
            {
                MessageBox.Show("Не заполнено поле СТС", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;
            }
            return true;
        }

        public bool Check()
        {
            if (textBox1.Text == "A000AA" & textBox2.Text == " 47")
            {
                if (textBox1.Text == "А000АА")
                {
                    if (textBox2.Text == " 47") 
                    { 
                        MessageBox.Show("Номерной знак не введен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        return false; 
                    }
                    else 
                    { 
                        MessageBox.Show("Не заполнена часть с номером", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        return false;
                    } 
                }
                else 
                { 
                    MessageBox.Show("Не заполнена часть с регионом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    return false; 
                }
            }
            else
            {
                Regex myReg = new Regex(@"^[АВСЕНКМОРТХУ]{1}[0-9]{3}[АВСЕНКМОРТХУ]{2}[0-9]{2,3}$");
                if (myReg.IsMatch(textBox1.Text + textBox2.Text) == false)
                {
                    MessageBox.Show("Номер не соответствует ГОСТу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
