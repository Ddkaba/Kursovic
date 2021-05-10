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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        int Count,Counter;
        string SQL;
        List<int> Categores = new List<int>();

        private void Form6_Load(object sender, EventArgs e)
        {
            radioButton9.Checked = false;
            radioButton12.Checked = false;
            radioButton13.Checked = false;
            radioButton14.Checked = false;
            button2.Hide();
            button3.Hide();
            panel8.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                Cleaner();
            }
            if (radioButton14.Checked)
            {
                Cleaner();
                button3.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "SELECT Surname, gibddmodern.employees.Name, MiddleName, DateofBirth, Sex, NamePosition, NameRank, PassportData, DriversLicenseNumber, DateofIssueLicense, EndDateLicense, Address, gibddmodern.employees.Number   FROM(gibddmodern.employees INNER JOIN gibddmodern.rank ON gibddmodern.employees.idRank = gibddmodern.rank.idRank) INNER JOIN gibddmodern.positions ON gibddmodern.employees.idPosition = gibddmodern.positions.idPosition WHERE gibddmodern.employees.idEmployee = " + Counter + "; ";
                    MySQL(SQL, 1);
                    SQL = "SELECT gibddmodern.category.Name AS Category FROM(gibddmodern.categotyemployee INNER JOIN gibddmodern.category ON gibddmodern.categotyemployee.idCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.employees ON gibddmodern.categotyemployee.IdEmployee = gibddmodern.employees.idEmployee WHERE gibddmodern.employees.idEmployee = " + Counter + ";";
                    MySQL(SQL, 2);
                    if (Counter == Count) button2.Enabled = false;
                }
                else button2.Enabled = false;
            }
            if (radioButton12.Checked)
            {

            }
            if (radioButton13.Checked)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cleaner();
            button2.Enabled = true;
            Counter--;
            if (Counter >= 1)
            {
                SQL = "SELECT Surname, gibddmodern.employees.Name, MiddleName, DateofBirth, Sex, NamePosition, NameRank, PassportData, DriversLicenseNumber, DateofIssueLicense, EndDateLicense, Address, gibddmodern.employees.Number   FROM(gibddmodern.employees INNER JOIN gibddmodern.rank ON gibddmodern.employees.idRank = gibddmodern.rank.idRank) INNER JOIN gibddmodern.positions ON gibddmodern.employees.idPosition = gibddmodern.positions.idPosition WHERE gibddmodern.employees.idEmployee = " + Counter + "; ";
                MySQL(SQL, 1);
                SQL = "SELECT gibddmodern.category.Name AS Category FROM(gibddmodern.categotyemployee INNER JOIN gibddmodern.category ON gibddmodern.categotyemployee.idCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.employees ON gibddmodern.categotyemployee.IdEmployee = gibddmodern.employees.idEmployee WHERE gibddmodern.employees.idEmployee = " + Counter + ";";
                MySQL(SQL, 2);
                if (Counter == 1) button3.Enabled = false;
            }
            else button3.Enabled = false;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            panel8.Show();
            button2.Show();
            button3.Hide();
            label14.Hide();
            textBox7.Hide();
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            maskedTextBox1.Enabled = true;
            maskedTextBox2.Enabled = true;
            maskedTextBox3.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            dateTimePicker3.Enabled = true;
            button2.Text = "Добавить";
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            Counter = 0;
            panel8.Hide();
            button2.Show();
            button3.Show();
            label14.Show();
            textBox7.Show();
            button2.Text = "Вперед";
            button3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = false;
            maskedTextBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            SQL = "SELECT COUNT(*) FROM gibddmodern.drivers;";
            MySQL(SQL, 0);
            Counter++;
            SQL = "SELECT Surname, gibddmodern.employees.Name, MiddleName, DateofBirth, Sex, NamePosition, NameRank, PassportData, DriversLicenseNumber, DateofIssueLicense, EndDateLicense, Address, gibddmodern.employees.Number   FROM(gibddmodern.employees INNER JOIN gibddmodern.rank ON gibddmodern.employees.idRank = gibddmodern.rank.idRank) INNER JOIN gibddmodern.positions ON gibddmodern.employees.idPosition = gibddmodern.positions.idPosition WHERE gibddmodern.employees.idEmployee = " + Counter + "; ";
            MySQL(SQL, 1);
            SQL = "SELECT gibddmodern.category.Name AS Category FROM(gibddmodern.categotyemployee INNER JOIN gibddmodern.category ON gibddmodern.categotyemployee.idCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.employees ON gibddmodern.categotyemployee.IdEmployee = gibddmodern.employees.idEmployee WHERE gibddmodern.employees.idEmployee = " + Counter + ";";
            MySQL(SQL, 2);
            if (Count == 1)
            {
                button2.Enabled = false;
            }
            if (Count > 1)
            {
                Counter = 1;
                button2.Enabled = true;
            }

        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            panel8.Hide();
            button2.Show();
            button3.Hide();
            label14.Hide();
            textBox7.Hide();
            button2.Text = "Удалить";
            button2.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox8.Enabled = false; 
            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = true;
            maskedTextBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            RadioButton radio = sender as RadioButton;
            if (radio != null)
            {
                if (radio.Checked) MessageBox.Show("Заполните ФИО и Номер паспорта для поиск нужной записи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            panel8.Hide();
            button2.Show();
            button3.Hide();
            label14.Show();
            textBox7.Show();
            button2.Text = "Изменить";
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                if (maskedTextBox2.MaskCompleted)
                {
                    if (textBox1.Text != string.Empty & textBox2.Text != string.Empty & textBox3.Text != string.Empty)
                    {
                        //SQL = "";
                        //MySQL(SQL, 3);
                        button2.Enabled = true;
                    }
                    else MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Cleaner()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            maskedTextBox1.Text = string.Empty;
            maskedTextBox2.Text = string.Empty;
            maskedTextBox3.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            dateTimePicker3.Text = string.Empty;
        }

        private void ADD_Categores() //Метод для добавления категорий водителя;
        {
            if (checkBox1.Checked) Categores.Add(1);
            if (checkBox2.Checked) Categores.Add(2);
            if (checkBox3.Checked) Categores.Add(3);
            if (checkBox4.Checked) Categores.Add(4);
            if (checkBox5.Checked) Categores.Add(5);
            if (checkBox6.Checked) Categores.Add(6);
            if (checkBox7.Checked) Categores.Add(7);
            if (checkBox8.Checked) Categores.Add(8);
            if (checkBox9.Checked) Categores.Add(9);
            if (checkBox10.Checked) Categores.Add(10);
            if (checkBox11.Checked) Categores.Add(11);
            if (checkBox12.Checked) Categores.Add(12);
            if (checkBox13.Checked) Categores.Add(13);
            if (checkBox14.Checked) Categores.Add(14);
            if (checkBox15.Checked) Categores.Add(15);
            if (checkBox16.Checked) Categores.Add(16);
        }

        public void MySQL(string SQL, int index) //Метод для выполнения запросов
        {
            Connector NEW = new Connector();
            NEW.SQL(SQL);
            MySqlDataReader reader = NEW.reader1();
            switch (index)
            {
                case 0:
                    while (reader.Read()) Count = Convert.ToInt32(reader["COUNT(*)"]);
                    break;
                case 1:
                    while(reader.Read())
                    {
                        textBox1.Text = reader["Surname"].ToString();
                        textBox2.Text = reader["Name"].ToString();
                        textBox3.Text = reader["MiddleName"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["DateofBirth"]);
                        textBox4.Text = reader["Sex"].ToString();
                        textBox5.Text = reader["NamePosition"].ToString();
                        textBox6.Text = reader["NameRank"].ToString();
                        maskedTextBox1.Text = reader["PassportData"].ToString();
                        maskedTextBox2.Text = reader["DriversLicenseNumber"].ToString();
                        if(reader["DateofIssueLicense"].ToString() == "") dateTimePicker2.Text = string.Empty;
                        else dateTimePicker2.Value = Convert.ToDateTime(reader["DateofIssueLicense"]);
                        if(reader["EndDateLicense"].ToString() == "") dateTimePicker3.Text = string.Empty;
                        else dateTimePicker3.Value = Convert.ToDateTime(reader["EndDateLicense"]);
                        textBox8.Text = reader["Address"].ToString();
                        maskedTextBox3.Text = reader["Number"].ToString();
                    }
                    break;
                case 2:
                    while(reader.Read()) textBox7.Text += reader["Category"].ToString() + ". ";
                    break;
            }
        }
    }
}
