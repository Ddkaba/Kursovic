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

        string SQL;
        int Count, IDD, IDC;
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
            radioButton1.Checked = true;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.ForeColor = System.Drawing.Color.Black;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            textBox2.ForeColor = System.Drawing.Color.Black;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Hiden();
            groupBox1.Show();
            groupBox2.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Hiden();
            groupBox3.Show();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                Form form = new Form3();
                form.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            bool check = Check();
            if(check == true)
            {
                SQL = "call gibddmodern.Number_Car('" + textBox1.Text + textBox2.Text + "');";
                MySQL(SQL, 0);
                if (textBox3.Text == "  ") MessageBox.Show("Запись не обнаружена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Поиск завершен", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SQL = "call gibddmodern.Wanted('" + textBox1.Text + textBox2.Text + "')";
                MySQL(SQL, 1);
                if(textBox15.Text == "") MessageBox.Show("Автомобиль не находится в розыске", "Розыск", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Автомобиль находится в розыске", "Розыск", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool check = Check();
            if(check == true)
            {
                if (textBox11.TextLength != 0)
                {
                    if (maskedTextBox1.MaskCompleted)
                    {
                        if (comboBox1.SelectedIndex > -1)
                        {
                            string Stat = comboBox1.SelectedItem.ToString();
                            if (textBox17.TextLength != 0)
                            {
                                if (textBox16.TextLength != 0)
                                {
                                    string Place = textBox17.Text;
                                    string Description = textBox16.Text;
                                    int Number = Convert.ToInt32(textBox11.Text);
                                    int Driver = Convert.ToInt32(maskedTextBox1.Text);
                                    SQL = "SELECT COUNT(*) FROM gibddmodern.protocols;";
                                    MySQL(SQL, 2);
                                    SQL = "SELECT IdDriver FROM gibddmodern.drivers WHERE DriverIsLicenseNumber = " + Driver + ";";
                                    MySQL(SQL, 3);
                                    SQL = "SELECT IdCar FROM gibddmodern.cars WHERE Number = '" + textBox1.Text + textBox2.Text + "';";
                                    MySQL(SQL, 4);
                                    Count = Count + 1;
                                    SQL = "INSERT INTO gibddmodern.protocols VALUES(" + Count + "," + Number + ",'" + Stat + "'," + IDC + "," + IDD + ",'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + Place + "','" + Description + "')";
                                    MySQL(SQL, 5);
                                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else MessageBox.Show("Не заполнено поле описания правонарушения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else MessageBox.Show("Не заполнено поле место правонарушения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else MessageBox.Show("Не выбрана статья правонарушения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Не заполнено поле номер вод.удостоверения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Не заполнено поле номер значка сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Check() //Для проверки заполненности полей
        {
            if (textBox1.Text == "А000АА" && textBox2.Text == "00")
            {
                if (textBox2.Text == "00")
                {
                    if (textBox1.Text == "A000AA") { MessageBox.Show("Номерной знак не введен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                    else {MessageBox.Show("Не заполнена часть с регионом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                }
                else { MessageBox.Show("Не заполнена часть с номером", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            }
            else
            {
                Regex myReg = new Regex(@"^[АВСЕНКМОРТХУ]{1}[0-9]{3}[АВСЕНКМОРТХУ]{2}[0-9]{1}[1-9]{1,2}$");
                if(myReg.IsMatch(textBox1.Text+textBox2.Text) == false) { MessageBox.Show("Номер не соответствует ГОСТу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false;}
            }
            return true;
        }

        public void MySQL(string SQL, int index) //Для выполнения запросов
        {
            Connector NEW = new Connector();
            NEW.SQL(SQL);
            MySqlDataReader reader = NEW.reader1();
            switch (index)
            {
                case 0:
                    string Sur = "", Name = "", MiddleName = "";
                    while(reader.Read())
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
                    break;
                case 1:
                    while(reader.Read())
                    {
                        dateTimePicker2.Value = Convert.ToDateTime(reader["DateWanted"]);
                        textBox13.Text = reader["IdCar"].ToString();
                        textBox14.Text = reader["SpecialSigns"].ToString();
                        textBox15.Text = reader["Causes"].ToString();
                    }
                    break;
                case 2:
                    while(reader.Read()) Count = Convert.ToInt32(reader["COUNT(*)"]);
                    break;
                case 3:
                    while(reader.Read()) IDD = Convert.ToInt32(reader["IdDriver"]);
                    break;
                case 4:
                    while(reader.Read()) IDC = Convert.ToInt32(reader["IdCar"]);
                    break;
            }
        }

        public void Hiden() 
        {
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox3.Hide();
        }
    }
}
