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
    public partial class Form8 : Form
    {
        int Count, Counter;
        string SQL;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e) 
        {
            panel1.Hide();
            panel2.Hide();
            button2.Hide();
            button3.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка для возвращения в меню
        {
            Form form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if(radioButton9.Checked) //Кнопка для добавления экипажей
            {
                bool check = Check();
                if (check == true)
                {
                    if (maskedTextBox3.Text == maskedTextBox4.Text) MessageBox.Show("Выбран один и тот же сотрудник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        SQL = "SELECT COUNT(*) FROM gibddmodern.crews INNER JOIN gibddmodern.employees ON gibddmodern.crews.idEmployee = gibddmodern.employees.idEmployee WHERE PassportData = '" + maskedTextBox3.Text + "';";
                        MySQL(SQL, 0);
                        if (Count > 0) MessageBox.Show("Сотрудник уже состоит в экипаже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.crews INNER JOIN gibddmodern.employees ON gibddmodern.crews.idEmployee = gibddmodern.employees.idEmployee WHERE PassportData = '" + maskedTextBox4.Text + "';";
                            MySQL(SQL, 0);
                            if (Count > 0) MessageBox.Show("Сотрудник уже состоит в экипаже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                SQL = "SELECT COUNT(*) FROM gibddmodern.crews INNER JOIN gibddmodern.garagecc ON gibddmodern.crews.idCar = gibddmodern.garagecc.idCar WHERE Number = '" + maskedTextBox1.Text + "';";
                                MySQL(SQL, 0);
                                if (Count >= 1) MessageBox.Show("Автомобиль уже имеет экипаж", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                {
                                    MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] {maskedTextBox3, maskedTextBox4 };
                                    for(int i = 0; i<2 ;i++)
                                    {
                                        SQL = "INSERT INTO gibddmodern.crews VALUES ((SELECT idCar FROM gibddmodern.garagecc WHERE Number = '" + maskedTextBox1.Text + "'), (SELECT idEmployee FROM gibddmodern.employees WHERE PassportData = '" + maskedTextBoxes[i].Text + "'));";
                                        MySQL(SQL, 20);
                                    }
                                    Cleaner();
                                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            if (radioButton14.Checked) //Кнопка ВПЕРЕД для просмотра экипажей
            {
                Cleaner();
                button3.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "call gibddmodern.CREWS(" + Counter + ");";
                    MySQL(SQL, 1);
                    if (Counter == Count) button2.Enabled = false;
                }
                else button2.Enabled = false;
            }
            if(radioButton12.Checked)  //Кнопка для удаления экипажей
            {
                DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SQL = "SELECT idCar AS 'COUNT(*)' FROM gibddmodern.garagecc WHERE Number = '" + maskedTextBox2.Text + "';";
                    MySQL(SQL, 0);
                    SQL = "DELETE FROM gibddmodern.crews WHERE idCar = '" + Count + "';";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleaner();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //Кнопка НАЗАД для просмотра экипажей
        {
            Cleaner();
            button2.Enabled = true;
            Counter--;
            if (Counter >= 1)
            {
                SQL = "call gibddmodern.CREWS(" + Counter + ");";
                MySQL(SQL, 1);
                if (Counter == 1) button3.Enabled = false;
            }
            else button3.Enabled = false;
        }



        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                if(maskedTextBox1.MaskCompleted)
                {
                    SQL = "SELECT idCar AS 'COUNT(*)' FROM gibddmodern.garagecc WHERE Number = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 0);
                    SQL = "SELECT NameBrand, NameModel, gibddmodern.category.Name FROM (gibddmodern.garagecc INNER JOIN gibddmodern.carbrands ON gibddmodern.garagecc.IdBrand = gibddmodern.carbrands.idBrand) INNER JOIN gibddmodern.category ON gibddmodern.garagecc.Category = gibddmodern.category.idCategory WHERE IdCar = '"+Count+"';";
                    MySQL(SQL, 2);
                    button2.Enabled = true;
                }
                else button2.Enabled = false;
            }
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked )
            {
                if (maskedTextBox2.MaskCompleted)
                {
                    SQL = "SELECT idCar AS 'COUNT(*)' FROM gibddmodern.garagecc WHERE Number = '" + maskedTextBox2.Text + "';";
                    MySQL(SQL, 0);
                    SQL = "call gibddmodern.CREWS(" + Count + ");";
                    MySQL(SQL, 1);
                    button2.Enabled = true;
                }
                else button2.Enabled = false; 
            }
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                if (maskedTextBox3.MaskCompleted)
                {
                    SQL = "SELECT idEmployee AS 'COUNT(*)' FROM gibddmodern.employees WHERE PassportData = '" + maskedTextBox3.Text + "';";
                    MySQL(SQL, 0);
                    SQL = "SELECT Surname, Name, MiddleName, (SELECT NamePosition FROM gibddmodern.positions Where gibddmodern.positions.idPosition = gibddmodern.employees.idPosition) AS Position,  (SELECT NameRank FROM gibddmodern.rank WHERE gibddmodern.rank.idRank = gibddmodern.employees.idRank) AS NameRank FROM gibddmodern.employees WHERE idEmployee = '" + Count + "';";
                    MySQL(SQL, 3);
                }
                else
                {
                    TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
                    for (int i = 0; i < 5; i++) textBoxes[i].Text = string.Empty;
                }
            }
        }

        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                if (maskedTextBox4.MaskCompleted)
                {
                    SQL = "SELECT idEmployee AS 'COUNT(*)' FROM gibddmodern.employees WHERE PassportData = '" + maskedTextBox4.Text + "';";
                    MySQL(SQL, 0);
                    SQL = "SELECT Surname, Name, MiddleName, (SELECT NamePosition FROM gibddmodern.positions Where gibddmodern.positions.idPosition = gibddmodern.employees.idPosition) AS Position,  (SELECT NameRank FROM gibddmodern.rank WHERE gibddmodern.rank.idRank = gibddmodern.employees.idRank) AS NameRank FROM gibddmodern.employees WHERE idEmployee = '" + Count + "';";
                    MySQL(SQL, 4);
                }
                else
                {
                    TextBox[] textBoxes = new TextBox[] { textBox6, textBox7, textBox8, textBox9, textBox10 };
                    for (int i = 0; i < 5; i++) textBoxes[i].Text = string.Empty;
                }    
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e) //Переключатель для добавления экипажей
        {
            Cleaner();
            panel1.Show();
            panel2.Hide();
            button2.Show();
            button3.Hide();
            label16.Show();
            label17.Show();
            maskedTextBox3.Show();
            maskedTextBox4.Show();
            panel1.Enabled = true;
            button2.Enabled = false;
            button2.Text = "Добавить";
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13 };
            for (int i = 0; i < 13; i++) textBoxes[i].Enabled = false;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e) //Переключатель для удаления экипажей
        {
            Cleaner();
            panel1.Show();
            panel2.Show();
            button2.Show();
            button3.Hide();
            label16.Hide();
            label17.Hide();
            maskedTextBox3.Hide();
            maskedTextBox4.Hide();
            panel1.Enabled = false;
            button2.Enabled = false;
            button2.Text = "Удалить";
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e) //Переключатель для просмотра экипажей
        {
            Counter = 0;
            Cleaner();
            panel1.Show();
            panel2.Hide();
            button3.Show();
            button2.Show();
            label16.Hide();
            label17.Hide();
            maskedTextBox3.Hide();
            maskedTextBox4.Hide();
            panel1.Enabled = false;
            button2.Enabled = true;
            button2.Text = "Вперед";
            SQL = "SELECT COUNT(*) FROM (SELECT Count(idCar) FROM gibddmodern.crews GROUP BY idCar) AS COUNT;";
            MySQL(SQL, 0);
            Counter++;
            SQL = "call gibddmodern.CREWS(" + Counter + ");";
            MySQL(SQL, 1);
            button3.Enabled = false;
            if (Count == 1) button2.Enabled = false;
            if (Count > 1) Counter = 1;
        }

        public void Cleaner() //Метод для очистки всех полей
        {
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13};
            for (int i = 0; i < 13; i++) textBoxes[i].Text = string.Empty;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4 };
            for (int i = 0; i < 4; i++) maskedTextBoxes[i].Text = string.Empty;
        }

        public bool Check() //Метод для проверки всех полей
        {
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };
            for (int i = 0; i < 10; i++)
            {
                if (textBoxes[i].Text == string.Empty)
                {
                    MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public void MySQL(string SQL, int index) //Метод для выполнения запросов
        {
            int Checker = 0;
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
                        if(Checker == 0)
                        {
                            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
                            for (int i = 0; i < 5; i++) textBoxes[i].Text = reader[i].ToString();
                        }
                        else
                        {
                            TextBox[] textBoxes1 = new TextBox[] { textBox6, textBox7, textBox8, textBox9, textBox10 };
                            for (int i = 0; i < 5; i++) textBoxes1[i].Text = reader[i].ToString();
                        }
                        textBox11.Text = reader["Brand"].ToString();
                        textBox12.Text = reader["Model"].ToString();
                        textBox13.Text = reader["Category"].ToString();
                        maskedTextBox1.Text = reader["Number"].ToString();
                        Checker++;
                    }
                    break;
                case 2:
                    while(reader.Read())
                    {
                        TextBox[] textBoxes1 = new TextBox[] { textBox11, textBox12, textBox13 };
                        for (int i = 0; i < 3; i++) textBoxes1[i].Text = reader[i].ToString();
                    }
                    break;
                case 3:
                    while(reader.Read())
                    {
                        TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
                        for (int i = 0; i < 5; i++) textBoxes[i].Text = reader[i].ToString();
                    }
                    break;
                case 4:
                    while(reader.Read())
                    {
                        TextBox[] textBoxes1 = new TextBox[] { textBox6, textBox7, textBox8, textBox9, textBox10 };
                        for (int i = 0; i < 5; i++) textBoxes1[i].Text = reader[i].ToString();
                    }
                    break;
            }
        }
    }
}
