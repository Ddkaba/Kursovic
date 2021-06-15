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

        int index, Human, ID = 0;
        string Role = "", SQL;
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            bool check =  Check(textBox1.Text,textBox2.Text);
            if(check == true)
            {
                SQL = "SELECT idHuman, Role FROM gibddmodern.password WHERE Login = '" + textBox1.Text + "' and Password = SHA1('" + textBox2.Text + "');";
                MySQL(SQL, 0);
                Check(Human, Role);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Show();
            panel2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted)
            {
                SQL = "SELECT idDriver FROM gibddmodern.drivers WHERE PassportData = " + maskedTextBox1.Text + ";";
                MySQL(SQL, 1);
                index = 1;
                CheckMask(ID);
            }
            else MessageBox.Show("Не заполнено поле паспортных данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.MaskCompleted)
            {
                SQL = "SELECT idEmployee AS idDriver FROM gibddmodern.employees WHERE PassportData = " + maskedTextBox1.Text + ";";
                MySQL(SQL, 1);
                index = 2;
                CheckMask(ID);
            }
            else MessageBox.Show("Не заполнено поле паспортных данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool check = Check(textBox3.Text, textBox4.Text);
            if (check == true)
            {
                if (index == 1)
                {
                    SQL = "INSERT INTO gibddmodern.password (idHuman, Role, Login, Password) VALUES ('" + ID + "','Водитель','" + textBox3.Text + "',SHA1('" + textBox4.Text + "'));";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBox2.Hide();
                }
                if (index == 2)
                {
                    SQL = "INSERT INTO gibddmodern.password (idHuman, Role, Login, Password) VALUES ('" + ID + "','Сотрудник','" + textBox3.Text + "',SHA1('" + textBox4.Text + "'));";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBox2.Hide();
                }
            }
        }

        public bool Check(string Text1, string Text2) //Метод для проверки заполненности двух полей
        {
           if(Text1 == "" || Text2 == "")
           {
                if(Text1 == "")
                {
                    if (Text2 == "")
                    {
                        MessageBox.Show("Не заполнены оба поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Не заполнено первое поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Не заполнено второе поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
           }
           return true;
        }

        public void Check(int Human, string Role) //Метод для проверки у входа в систему
        {
            if(Human == 0) MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(Human != 0 & Role != "")
            {
                if(Role == "Сотрудник")
                {
                    SQL = "SELECT idPosition AS idDriver FROM gibddmodern.employees WHERE idEmployee = " + Human+";";
                    MySQL(SQL, 1);
                    if(ID == 1 || ID == 2 || ID == 3 || ID == 4 || ID == 7 || ID == 8) 
                    {
                        Form form2 = new CheckForm();
                        form2.Show();
                        this.Hide();
                    }
                    if(ID == 5)
                    {
                        Form form4 = new DocumentForm();
                        form4.Show();
                        this.Hide();
                    }
                    if(ID == 6 || ID == 9 || ID == 10 || ID == 11 || ID == 12)
                    {
                        Form form5 = new MenuForm();
                        form5.Show();
                        this.Hide();
                    }
                }    
                if(Role == "Водитель")
                {
                    Form form3 = new FineForm();
                    form3.Show();
                    this.Hide();
                }    
            }
        }

        public void CheckMask(int ID) //Метод для проверки паспортных данных в системе
        {
            if (ID == 0) MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else { panel2.Show(); maskedTextBox1.Text = string.Empty; }
        }

        public void MySQL(string SQL, int index) //Метод для выполнения запросов
        {
            Connector NEW = new Connector();
            NEW.SQL(SQL);
            MySqlDataReader reader = NEW.reader1();
            switch (index)
            {
                case 0:
                    while (reader.Read())
                    {
                        Human = Convert.ToInt32(reader["idHuman"]);
                        Role = reader["Role"].ToString();
                    }
                    break;
                case 1:
                    while (reader.Read()) ID = Convert.ToInt32(reader["idDriver"]);
                    break;
            }
        }

    }
}