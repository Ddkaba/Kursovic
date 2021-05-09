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

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }

        int index;
        int ID = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Check(textBox1.Text,textBox2.Text);
            string SQL = "SELECT idHuman, Role FROM gibddmodern.password WHERE Login = '"+ textBox1.Text + "' and Password = SHA1('"+ textBox2.Text + "');";
            MySQL(SQL, index=0);
        }

        public void MySQL(string SQL, int index)
        {
            Connector NEW = new Connector();
            NEW.SQL(SQL);
            MySqlDataReader reader = NEW.reader1();
            switch(index)
            {
                case 0:
                    int Human = 0;
                    string Role = "";
                    while (reader.Read())
                    {
                        Human = Convert.ToInt32(reader["idHuman"]);
                        Role = reader["Role"].ToString();
                    }
                    Check(Human, Role);
                    break;
                case 1:
                    while (reader.Read())
                    {
                        ID = Convert.ToInt32(reader["idDriver"]);
                    }
                    CheckMask(ID);
                    break;
                case 2:
                    while (reader.Read())
                    {
                        ID = Convert.ToInt32(reader["idEmployee"]);
                    }
                    CheckMask(ID);
                    break;
            }
        }

        public void Check(string Text1, string Text2)
        {
           if(Text1 == "" || Text2 == "")
           {
                if(Text1 == "")
                {
                    if(Text2 == "") MessageBox.Show("Не заполнены оба поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    else MessageBox.Show("Не заполнено первое поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Не заполнено второе поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }

        public void Check(int Human, string Role)
        {
            if(Human == 0) MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(Human != 0 & Role != "")
            {
                if(Role == "Сотрудник")
                {
                    Form form2 = new Form2();
                    form2.Show();
                    this.Hide();
                }    
                if(Role == "Водитель")
                {
                    Form form3 = new Form3();
                    form3.Show();
                    this.Hide();
                }    
            }
        }

        public void CheckMask(int ID)
        {
            if(ID == 0) MessageBox.Show("Запись не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else panel2.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            index = 1;
            string SQL = "SELECT idDriver FROM gibddmodern.drivers WHERE PassportData = "+maskedTextBox1.Text+";";
            MySQL(SQL, index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            index = 2;
            string SQL = "SELECT idEmployee FROM gibddmodern.employees WHERE PassportData = " + maskedTextBox1.Text + ";";
            MySQL(SQL, index);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(index == 1)
            {
                Check(textBox3.Text, textBox4.Text);
                string SQL = "INSERT INTO gibddmodern.password (idHuman, Role, Login, Password) VALUES ('" + ID + "','Водитель','" + textBox3.Text + "',SHA1('" + textBox4.Text + "'));";
                MySQL(SQL, index = 4);
            }
            if (index == 2)
            {
                Check(textBox3.Text, textBox4.Text);
                string SQL = "INSERT INTO gibddmodern.password (idHuman, Role, Login, Password) VALUES ('" + ID + "','Сотрудник','" + textBox3.Text + "',SHA1('" + textBox4.Text + "'));";
                MySQL(SQL, index = 4);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Show();
            panel2.Hide();
        }
    }
}