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
    public partial class FineForm : Form
    {
        public FineForm()
        {
            InitializeComponent();
        }

        int Count, Counter;
        string SQL, PN;
        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            bool check = Check();
            if (check == true)
            {
                bool mask = Masked();
                if (mask == true)
                {
                    Clear();
                    SQL = "SELECT PassportNumber FROM gibddmodern.cars WHERE Number = '" + textBox1.Text + textBox2.Text + "';";
                    MySQL(SQL, 2);
                    if (PN == maskedTextBox1.Text)
                    {
                        SQL = "SELECT COUNT(*) FROM protocols Where idCar = (Select idCar FROM gibddmodern.cars Where Number = '" + textBox1.Text + textBox2.Text + "' and PassportNumber = '" + maskedTextBox1.Text + "');";
                        MySQL(SQL, 1);
                        if (textBox10.Text == "0") MessageBox.Show("Штрафы не обнаружены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            MessageBox.Show("Были обнаружены штрафы", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (Count == 1)
                            {
                                SQL = "call gibddmodern.Found_Fine('" + textBox1.Text + textBox2.Text + "','" + maskedTextBox1.Text + "');";
                                MySQL(SQL, 0);
                            }
                            if (Count > 1)
                            {
                                button2.Enabled = true;
                                SQL = "call gibddmodern.FineLimit(0,1,'" + textBox1.Text + textBox2.Text + "','" + maskedTextBox1.Text + "');";
                                MySQL(SQL, 0);
                                Counter = 0;
                            }
                        }
                    }
                    else MessageBox.Show("Машина с таким СТС и Номером не обнаружена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            form1.Show();
            this.Hide();
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

        public void MySQL(string SQL, int index) //Метод для выполнения запросов
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
                case 2:
                    while(reader.Read()) PN = reader["PassportNumber"].ToString();
                    break;
            }
        }

        public void Clear() //Очистка всех полей
        {
            TextBox[] textBoxes = new TextBox[] {textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };
            for (int i = 0; i < 8; i++) textBoxes[i].Text = string.Empty;
        }

        public bool Masked() //Проверка заполненности maskedtextbox1
        {
            if(maskedTextBox1.MaskCompleted) return true;
            else { MessageBox.Show("Не заполнено поле СТС", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
        }

        public bool Check() //Проверка заполненности Панели с номером
        {
            if (textBox1.Text == "А000АА" && textBox2.Text == "00")
            {
                if (textBox2.Text == "00")
                {
                    if (textBox1.Text == "A000AA") { MessageBox.Show("Номерной знак не введен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                    else { MessageBox.Show("Не заполнена часть с регионом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                }
                else { MessageBox.Show("Не заполнена часть с номером", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            }
            else
            {
                Regex myReg = new Regex(@"^[АВСЕНКМОРТХУ]{1}[0-9]{3}[АВСЕНКМОРТХУ]{2}[0-9]{1}[1-9]{1,2}$");
                if (myReg.IsMatch(textBox1.Text + textBox2.Text) == false) { MessageBox.Show("Номер не соответствует ГОСТу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            }
            return true;
        }
    }
}
