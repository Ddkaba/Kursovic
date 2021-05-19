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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        int Count, CheckCount = 0, IDD, IDC, Counter;
        string SQL, Surname, name, MiddleName, Number, NP;
        bool check = false;
        List<int> Categores = new List<int>();

        private void Form4_Load(object sender, EventArgs e)
        {
            Hiden();
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
            DateTimePicker[] dateTimes = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3, dateTimePicker4, dateTimePicker5, dateTimePicker7, dateTimePicker8 };
            for (int i = 0; i < 7; i++) dateTimes[i].MaxDate = DateTime.Now.Date;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) //Водители
        {
            Metod();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) //Автомобили
        {
            Hiden();
            Cleaner();
            No_Checked();
            panel6.Show();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) //Штрафстоянка
        {
            Metod();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) //Хранение номеров
        {
            Metod();
        }

        private void textBox1_Click_1(object sender, EventArgs e) //Номерной знак
        {
            textBox1.Text = "";
            textBox1.ForeColor = System.Drawing.Color.Black;
        }

        private void textBox2_Click_1(object sender, EventArgs e) //Номерной знак
        {
            textBox2.Text = "";
            textBox2.ForeColor = System.Drawing.Color.Black;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            if (radioButton2.Checked) //Переключатель для добавления водителя в базу данных
            {
                panel7.Show();
                panel8.Show();
                button5.Hide();
                label34.Hide();
                label35.Hide();
                textBox19.Hide();
                dateTimePicker9.Hide();
                button4.Text = "Добавить";
                button4.Enabled = true;
                maskedTextBox6.Enabled = true;
                maskedTextBox7.Enabled = true;
                dateTimePicker7.Enabled = true;
                dateTimePicker8.Enabled = true;
                TextBox[] textBoxes = new TextBox[] { textBox15, textBox16, textBox17, textBox18 };
                for (int i = 0; i < 4; i++) textBoxes[i].Enabled = true;
            }
            if (radioButton3.Checked) //Переключатель для добавления машин на учет
            {
                panel1.Show();
                panel4.Show();
                button3.Hide();
                label22.Hide();
                label24.Show();
                maskedTextBox4.Show();
                dateTimePicker4.Hide();
                comboBox1.Enabled = true;
                button16.Text = "Добавить";
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker3, dateTimePicker4, dateTimePicker5 };
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = true;
                TextBox[] textBoxes = new TextBox[] { textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14 };
                for (int i = 0; i < 7; i++) textBoxes[i].Enabled = true;
                MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4 };
                for (int i = 0; i < 4; i++) maskedTextBoxes[i].Enabled = true;
            }
            if (radioButton4.Checked) //Переключатель для добавления машин на штрафстоянку
            {
                panel1.Show();
                panel3.Show();
                button1.Hide();
                button12.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                dateTimePicker2.Enabled = true;
                button12.Text = "Добавить";
            }
            if (radioButton5.Checked) //Переключатель для добавления новых хранимых номеров
            {
                panel1.Show();
                panel2.Show();
                label23.Hide();
                label25.Show();
                button2.Hide();
                maskedTextBox5.Show();
                dateTimePicker6.Hide();
                button7.Enabled = true;
                button2.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker6.Enabled = true;
                button7.Text = "Добавить";
                TextBox[] textBoxes = new TextBox[] { textBox3, textBox4, textBox5 };
                for (int i = 0; i < 3; i++) textBoxes[i].Enabled = true;
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            textBox1.ForeColor = System.Drawing.Color.Black;
            textBox2.ForeColor = System.Drawing.Color.Black;
            if (radioButton2.Checked)
            {
                check = true;
                panel7.Show();
                panel8.Hide();
                button5.Hide();
                label34.Show();
                label35.Show();
                textBox19.Show();
                dateTimePicker9.Show();
                button4.Enabled = false;
                button4.Text = "Удалить";
                textBox18.Enabled = false;
                textBox19.Enabled = false;
                maskedTextBox7.Enabled = true;
                maskedTextBox6.Enabled = false;
                TextBox[] textBoxes = new TextBox[] { textBox15, textBox16, textBox17 };
                for (int i = 0; i < 3; i++) textBoxes[i].Enabled = true;
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker7, dateTimePicker8, dateTimePicker9};
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
                RadioButton radio = sender as RadioButton;
                if(radio != null) if(radio.Checked) MessageBox.Show("Заполните ФИО и Номер вод. удостоверения для поиск нужной записи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton3.Checked)
            {
                panel1.Show();
                panel4.Show();
                button3.Hide();
                label24.Hide();
                label22.Show();
                maskedTextBox4.Hide();
                dateTimePicker4.Show();
                button16.Text = "Удалить";
                comboBox1.Enabled = false;
                maskedTextBox1.Enabled = true;
                maskedTextBox2.Enabled = false;
                maskedTextBox3.Enabled = false;
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker3, dateTimePicker4, dateTimePicker5 };
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
                TextBox[] textBoxes = new TextBox[] { textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14 };
                for (int i = 0; i < 7; i++) textBoxes[i].Enabled = false;
            }
            if (radioButton4.Checked)
            {
                panel1.Show();
                panel3.Show();
                button1.Hide();
                button12.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = false;
                dateTimePicker2.Enabled = false;
                button12.Text = "Удалить";
            }
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            Counter = 0;
            textBox1.ForeColor = System.Drawing.Color.Black;
            textBox2.ForeColor = System.Drawing.Color.Black;
            if (radioButton5.Checked) //Переключатель для просмотра хранимых номеров
            {
                panel1.Show();
                panel2.Show();
                label23.Show();
                label25.Hide();
                button2.Show();
                maskedTextBox5.Hide();
                dateTimePicker6.Show();
                button2.Enabled = true;
                button7.Enabled = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker6.Enabled = false;
                button7.Text = "Вперед";
                TextBox[] textBoxes = new TextBox[] { textBox3, textBox4, textBox5 };
                for (int i = 0; i < 3; i++) textBoxes[i].Enabled = false;
                SQL = "SELECT COUNT(*) FROM gibddmodern.storingnumbers;";
                MySQL(SQL, 0);
                SQL = "SELECT Number, Date, Endofterm, Surname, Name, MiddleName FROM gibddmodern.storingnumbers INNER JOIN gibddmodern.drivers ON gibddmodern.storingnumbers.IdDriver = gibddmodern.drivers.idDriver LIMIT " + Counter + ",1;";
                button2.Enabled = false;
                if (Count == 1)
                {
                    MySQL(SQL, 3);
                    button7.Enabled = false;

                }
                if (Count > 1)
                {
                    MySQL(SQL, 3);
                    Counter = 0;
                }
            }
            if(radioButton4.Checked) //Переключатель для просмотра авто на штрафстоянке
            {
                panel1.Show();
                panel3.Show();
                button1.Show();
                button12.Enabled = true;
                button12.Text = "Вперед";
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                dateTimePicker2.Enabled = false;
                SQL = "SELECT COUNT(*) FROM gibddmodern.parkingfine;";
                MySQL(SQL, 0);
                SQL = "SELECT Number, IdProtocol, ArrivalDate, Status FROM gibddmodern.parkingfine INNER JOIN gibddmodern.cars ON gibddmodern.parkingfine.IdCar = gibddmodern.cars.idCar LIMIT " + Counter + ",1;";
                button1.Enabled = false;
                if (Count == 1)
                {
                    MySQL(SQL, 5);
                    button12.Enabled = false;

                }
                if (Count > 1)
                {
                    MySQL(SQL, 5);
                    Counter = 0;
                }
            }
            if(radioButton3.Checked) //Переключатель для просмотра авто на учете
            {
                panel1.Show();
                panel4.Show();
                button3.Show();
                label24.Hide();
                label22.Show();
                maskedTextBox4.Hide();
                dateTimePicker4.Show();
                button16.Text = "Вперед";
                comboBox1.Enabled = false;
                MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3 };
                for (int i = 0; i < 3; i++) maskedTextBoxes[i].Enabled = false;
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker3, dateTimePicker4, dateTimePicker5 };
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
                TextBox[] textBoxes = new TextBox[] { textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14 };
                for (int i = 0; i < 7; i++) textBoxes[i].Enabled = false;
                SQL = "SELECT COUNT(*) FROM gibddmodern.cars;";
                MySQL(SQL, 0);
                SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand LIMIT " + Counter + ",1;";
                MySQL(SQL, 4);
                button3.Enabled = false;
                if (Count == 1) button16.Enabled = false;
                if(Count > 1) Counter = 0;
            }
            if(radioButton2.Checked) //Переключатель для просмотра водителей с правами
            {
                panel7.Show();
                panel8.Hide();
                label34.Show();
                label35.Show();
                button5.Show();
                textBox19.Show();
                dateTimePicker9.Show();
                button4.Text = "Вперед";
                button4.Enabled = true;
                maskedTextBox6.Enabled = false;
                maskedTextBox7.Enabled = false;
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker7, dateTimePicker8, dateTimePicker9 };
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
                TextBox[] textBoxes = new TextBox[] { textBox15, textBox16, textBox17, textBox18, textBox19 };
                for (int i = 0; i < 5; i++) textBoxes[i].Enabled = false;
                SQL = "SELECT COUNT(*) FROM gibddmodern.drivers;";
                MySQL(SQL, 0);
                Counter++;
                SQL = "SELECT Surname, gibddmodern.drivers.Name, MiddleName, DateofBirth, PlaceofBirth, PassportData, DriverIsLicenseNumber, DateofIssueLicense, EndDateLicense, gibddmodern.category.Name AS Category FROM(gibddmodern.categorydriver INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver WHERE gibddmodern.drivers.idDriver = " + Counter+"; ";
                MySQL(SQL, 9);
                button5.Enabled = false;
                if (Count == 1) button4.Enabled = false;
                if (Count > 1) Counter = 1;
            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            if (radioButton3.Checked)
            {
                panel1.Show();
                panel4.Show();
                label22.Show();
                label24.Show();
                button3.Hide();
                maskedTextBox4.Show();
                dateTimePicker4.Show();
                comboBox1.Enabled = false;
                maskedTextBox1.Enabled = true;
                button16.Text = "Изменить";
                textBox1.ForeColor = System.Drawing.Color.Black;
                textBox2.ForeColor = System.Drawing.Color.Black;
                TextBox[] textBoxes = new TextBox[] { textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14 };
                for (int i = 0; i < 7; i++) textBoxes[i].Enabled = false;
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker3, dateTimePicker4, dateTimePicker5 };
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
                MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox2, maskedTextBox3, maskedTextBox4 };
                for (int i = 0; i < 3; i++) maskedTextBoxes[i].Enabled = false;
            }
            if (radioButton2.Checked)
            {
                IDC = 0;
                panel7.Show();
                panel8.Hide();
                check = false;
                button5.Hide();
                label34.Hide();
                label35.Hide();
                textBox19.Hide();
                dateTimePicker9.Hide();
                button4.Enabled = false;
                button4.Text = "Изменить";
                textBox18.Enabled = false;
                textBox19.Enabled = false;
                maskedTextBox7.Enabled = true;
                maskedTextBox6.Enabled = false;
                TextBox[] textBoxes = new TextBox[] { textBox15, textBox16, textBox17 };
                for (int i = 0; i < 3; i++) textBoxes[i].Enabled = true;
                DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker7, dateTimePicker8, dateTimePicker9 };
                for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
                RadioButton radio = sender as RadioButton;
                if (radio != null) if (radio.Checked) MessageBox.Show("Заполните ФИО и Номер вод. удостоверения для поиск нужной записи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка НАЗАД для просмотра штрафстоянки
        {
            button12.Enabled = true;
            Counter--;
            if (Counter >= 0)
            {
                SQL = "SELECT Number, IdProtocol, ArrivalDate, Status FROM gibddmodern.parkingfine INNER JOIN gibddmodern.cars ON gibddmodern.parkingfine.IdCar = gibddmodern.cars.idCar LIMIT " + Counter + ",1;";
                MySQL(SQL, 5);
                if (Counter == 0) button1.Enabled = false;
            }
            else button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) //Кнопка НАЗАД для хранения номерных знаков
        {
            button7.Enabled = true;
            Counter--;
            if (Counter >= 0)
            {
                SQL = "SELECT Number, Date, Endofterm, Surname, Name, MiddleName FROM gibddmodern.storingnumbers INNER JOIN gibddmodern.drivers ON gibddmodern.storingnumbers.IdDriver = gibddmodern.drivers.idDriver LIMIT " + Counter + ",1;";
                MySQL(SQL, 3);
                if (Counter == 0) button2.Enabled = false;
            }
            else button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(radioButton14.Checked)
            {
                button16.Enabled = true;
                Counter--;
                if (Counter >= 0)
                {
                    SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand LIMIT " + Counter + ",1;";
                    MySQL(SQL, 4);
                    if (Counter == 0) button3.Enabled = false;
                }
                else button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(radioButton9.Checked)
            {
                bool kek = Checker();
                if (kek == true)
                {
                    if (Categores.Count == 0) MessageBox.Show("Выберите категории водителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else 
                    {
                        SQL = "SELECT idDriver AS IdDriver FROM gibddmodern.drivers WHERE DriverIsLicenseNumber = '" + maskedTextBox7.Text + "';";
                        MySQL(SQL, 1);
                        if (IDD != 0) MessageBox.Show("Водитель с таким номером уже имеется в базе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.drivers";
                            MySQL(SQL, 0);
                            Count++;
                            SQL = "INSERT INTO gibddmodern.drivers VALUES ('" + Count + "','" + textBox15.Text + "', '" + textBox16.Text + "', '" + textBox17.Text + "','" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "', '" + textBox18.Text + "', '" + maskedTextBox6.Text + "', '" + maskedTextBox7.Text + "', '" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "', DATE_ADD('" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR) );";
                            MySQL(SQL, 20);
                            ADD_Categores();
                            foreach (int i in Categores)
                            {
                                SQL = "INSERT INTO gibddmodern.categorydriver VALUES('" + Count + "', '" + i + "')";
                                MySQL(SQL, 20);
                            }
                            Cleaner();
                            MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            if(radioButton12.Checked)
            {
                DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SQL = "SELECT idDriver AS IdDriver FROM gibddmodern.drivers WHERE DriverIsLicenseNumber = '"+maskedTextBox7.Text+"';";
                    MySQL(SQL, 1);
                    SQL = "DELETE FROM gibddmodern.categorydriver WHERE idDriver = '"+IDD+"';";
                    MySQL(SQL, 20);
                    SQL = "DELETE FROM gibddmodern.drivers WHERE idDriver = '" + IDD + "';";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleaner();
                }
            }
            if (radioButton13.Checked)
            {
                bool kek = Checker();
                if (kek == true)
                {
                    if (check == true)
                    {
                        if (maskedTextBox7.MaskCompleted)
                        {
                            ADD_Categores();
                            SQL = "DELETE FROM gibddmodern.categorydriver WHERE idDriver = '" + IDC + "';";
                            MySQL(SQL, 20);
                            SQL = "UPDATE gibddmodern.drivers SET Surname = '" + textBox15.Text + "', Name = '" + textBox16.Text + "' , MiddleName =  '" + textBox17.Text + "', DateofBirth = '" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "', PlaceofBirth = '" + textBox18.Text + "', PassportData = '" + maskedTextBox6.Text + "', DriverIsLicenseNumber = '" + maskedTextBox7.Text + "', DateofIssueLicense = '" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "', EndDateLicense = DATE_ADD('" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR) WHERE idDriver = '" + IDC + "';";
                            MySQL(SQL, 20);
                            foreach (int i in Categores)
                            {
                                SQL = "INSERT INTO gibddmodern.categorydriver VALUES('" + IDC + "', '" + i + "')";
                                MySQL(SQL, 20);
                            }
                            Cleaner();
                            check = false;
                            panel8.Hide();
                            MessageBox.Show("Запись успешно обновлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            if (radioButton14.Checked)
            {
                textBox19.Text = string.Empty; 
                button5.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "SELECT Surname, gibddmodern.drivers.Name, MiddleName, DateofBirth, PlaceofBirth, PassportData, DriverIsLicenseNumber, DateofIssueLicense, EndDateLicense, gibddmodern.category.Name AS Category FROM(gibddmodern.categorydriver INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver WHERE gibddmodern.drivers.idDriver = " + Counter + "; ";
                    MySQL(SQL, 9);
                    if (Counter == Count) button4.Enabled = false;
                }
                else button4.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton14.Checked)
            {
                textBox19.Text = string.Empty;
                button4.Enabled = true;
                Counter--;
                if (Counter >= 1)
                {
                    SQL = "SELECT Surname, gibddmodern.drivers.Name, MiddleName, DateofBirth, PlaceofBirth, PassportData, DriverIsLicenseNumber, DateofIssueLicense, EndDateLicense, gibddmodern.category.Name AS Category FROM(gibddmodern.categorydriver INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver WHERE gibddmodern.drivers.idDriver = " + Counter + "; ";
                    MySQL(SQL, 9);
                    if (Counter == 1) button5.Enabled = false;
                }
                else button5.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e) //Кнопка добавления хранимых номеров, и кнопка ВПЕРЕД для просмотра
        {
            if(radioButton9.Checked)
            {
                bool kek = Checker();
                if (kek == true)
                {
                    kek = Check();
                    if (kek == true)
                    {
                        Surname = textBox3.Text;
                        name = textBox4.Text;
                        MiddleName = textBox5.Text;
                        SQL = "SELECT IdDriver FROM gibddmodern.drivers WHERE Surname = '" + Surname + "' and Name = '" + name + "' and MiddleName = '" + MiddleName + "';";
                        MySQL(SQL, 1);
                        SQL = "SELECT idCar FROM gibddmodern.cars WHERE IdDriver = " + IDD + " and Number = '" + textBox1.Text + textBox2.Text + "'";
                        MySQL(SQL, 2);
                        if (CheckCount != 0)
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.storingnumbers";
                            MySQL(SQL, 0);
                            Count++;
                            SQL = "UPDATE gibddmodern.cars SET Number = '" + maskedTextBox5.Text + "' WHERE idCar = " + CheckCount + ";";
                            MySQL(SQL, 20);
                            SQL = "INSERT INTO gibddmodern.storingnumbers VALUES (" + Count + ",'" + textBox1.Text + textBox2.Text + "', " + IDD + ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', DATE_ADD('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR));";
                            MySQL(SQL, 20);
                            Cleaner();
                            MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Error");
                    }
                }
            }
            if(radioButton14.Checked)
            {
                button2.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "SELECT Number, Date, Endofterm, Surname, Name, MiddleName FROM gibddmodern.storingnumbers INNER JOIN gibddmodern.drivers ON gibddmodern.storingnumbers.IdDriver = gibddmodern.drivers.idDriver LIMIT " + Counter + ",1;";
                    MySQL(SQL, 3);
                    if (Counter == Count - 1) button7.Enabled = false;
                }
                else button7.Enabled = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                bool kek = Checker();
                if (kek == true)
                {
                    kek = Check();
                    if (kek == true)
                    {
                        SQL = "SELECT COUNT(*) FROM gibddmodern.parkingfine INNER JOIN gibddmodern.cars ON gibddmodern.parkingfine.IdCar = gibddmodern.cars.idCar WHERE IdProtocol = " + Convert.ToInt32(textBox6.Text) + " AND Number = '" + textBox1.Text + textBox2.Text + "';";
                        MySQL(SQL, 0);
                        if (Count == 0)
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.parkingfine";
                            MySQL(SQL, 0);
                            Count = Count + 1;
                            SQL = "INSERT INTO gibddmodern.parkingfine VALUES ('" + Count + "', (SELECT idCar FROM gibddmodern.cars WHERE Number = '" + textBox1.Text + textBox2.Text + "'), '" + textBox6.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + textBox7.Text + "');";
                            MySQL(SQL, 20);
                            Cleaner();
                            MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Автомобиль уже находиться на штрафстоянке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if(radioButton14.Checked)
            {
                button1.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "SELECT Number, IdProtocol, ArrivalDate, Status FROM gibddmodern.parkingfine INNER JOIN gibddmodern.cars ON gibddmodern.parkingfine.IdCar = gibddmodern.cars.idCar LIMIT " + Counter + ",1;";
                    MySQL(SQL, 5);
                    if (Counter == Count - 1) button12.Enabled = false;
                }
                else button12.Enabled = false;
            }

            if(radioButton12.Checked)
            {
                bool kek = Check();
                if (kek == true)
                {
                    DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        SQL = "DELETE FROM gibddmodern.parkingfine WHERE IdProtocol = '" + textBox6.Text + "';";
                        MySQL(SQL, 20);
                        MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cleaner();
                    }
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
            {
                bool kek = Checker();
                if (kek == true)
                {
                    kek = Check();
                    if (kek == true)
                    {
                        SQL = "call gibddmodern.UPDATE_CARS('" + CheckCount + "','" + maskedTextBox4.Text + "','" + textBox1.Text + textBox2.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + textBox13.Text + "','" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "', " + textBox14.Text + ");";
                        MySQL(SQL, 20);
                    }
                }
            }
            if (radioButton9.Checked)
            {
                bool kek = Checker();
                if (kek == true)
                {
                    kek = Check();
                    if (kek == true)
                    {
                        panel1.Show();
                        SQL = "SELECT idDriver FROM gibddmodern.drivers WHERE Surname = '" + textBox10.Text + "' AND Name = '" + textBox9.Text + "' AND MiddleName = '" + textBox8.Text + "' AND  DriverIsLicenseNumber = '" + maskedTextBox4.Text + "';";
                        MySQL(SQL, 1);
                        if (IDD == 0) MessageBox.Show("Запись не найдена", "Внимание!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.storingnumbers WHERE IdDriver = " + IDD + ";";
                            MySQL(SQL, 0);
                            if (Count > 0)
                            {
                                DialogResult result = MessageBox.Show("У данного водителя обнаружены хранимые номера.Хотите ими воспользоваться?", "Обнаружены хранимые номера", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    DialogResult DNumber = DialogResult.No;
                                    Counter = -1;
                                    do
                                    {
                                        Counter++;
                                        if (Counter == 2) break;
                                        SQL = "SELECT Number FROM gibddmodern.storingnumbers WHERE IdDriver = " + IDD + " LIMIT " + Counter + ", 1 ;";
                                        MySQL(SQL, 6);
                                        DNumber = MessageBox.Show("Номер: " + Number + ". Хотите им воспользоваться?", "Хранимый номер", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    }
                                    while (DNumber == DialogResult.No);
                                    if (DNumber == DialogResult.Yes)
                                    {
                                        textBox1.ForeColor = System.Drawing.Color.Black;
                                        textBox2.ForeColor = System.Drawing.Color.Black;
                                        SQL = "SELECT Number FROM gibddmodern.storingnumbers WHERE IdDriver = " + IDD + " LIMIT " + Counter + ", 1 ;";
                                        MySQL(SQL, 8);
                                        SQL = "DELETE FROM gibddmodern.storingnumbers WHERE Number = '" + Number + "'";
                                        MySQL(SQL, 20);
                                        NEW();
                                    }
                                    else NEW();
                                }
                                else if (result == DialogResult.No) NEW();
                            }
                            else if (Count == 0) NEW();
                        }
                    }
                }
            }
            if (radioButton14.Checked)
            {
                button3.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand LIMIT " + Counter + ",1;";
                    MySQL(SQL, 4);
                    if (Counter == Count - 1) button16.Enabled = false;
                }
                else button16.Enabled = false;
            }
            if (radioButton12.Checked)
            {
                if(maskedTextBox1.MaskCompleted)
                {
                    DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        SQL = "DELETE FROM gibddmodern.cars WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                        MySQL(SQL, 20);
                        MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cleaner();
                    }
                }
                else MessageBox.Show("VIN - номер не заполнен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                if (maskedTextBox1.MaskCompleted)
                {
                    SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 4);
                }
            }
            if(radioButton13.Checked)
            {
                if(maskedTextBox1.MaskCompleted)
                {
                    SQL = "SELECT idCar FROM gibddmodern.cars WHERE BodyNumber = '"+maskedTextBox1.Text+"';";
                    MySQL(SQL, 2);
                    if (CheckCount != 0)
                    {
                        SQL = "SELECT DriverIsLicenseNumber AS IdDriver FROM gibddmodern.drivers INNER JOIN gibddmodern.cars ON gibddmodern.drivers.idDriver = gibddmodern.cars.IdDriver WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                        MySQL(SQL, 1);
                        maskedTextBox4.Text = IDD.ToString();
                        SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                        MySQL(SQL, 4);
                        TextBox[] textBoxes = new TextBox[] { textBox8, textBox9, textBox10, textBox13, textBox14, };
                        for (int i = 0; i < 5; i++) textBoxes[i].Enabled = true;
                        MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox2, maskedTextBox3, maskedTextBox4 };
                        for (int i = 0; i < 3; i++) maskedTextBoxes[i].Enabled = true;
                        DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker3, dateTimePicker4, dateTimePicker5 };
                        for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = true;
                    }
                    else MessageBox.Show("Автомобиль не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void maskedTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked || radioButton13.Checked)
            {
                if (maskedTextBox7.MaskCompleted)
                {
                    if (textBox15.Text != string.Empty & textBox16.Text != string.Empty & textBox17.Text != string.Empty)
                    {
                        SQL = "SELECT DateofBirth, PlaceofBirth, PassportData, DateofIssueLicense, EndDateLicense, gibddmodern.category.Name AS Category FROM(gibddmodern.categorydriver INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver WHERE gibddmodern.drivers.Surname = '"+textBox15.Text+ "' AND gibddmodern.drivers.Name = '"+textBox16.Text+ "' AND gibddmodern.drivers.MiddleName = '"+textBox17.Text+ "' AND gibddmodern.drivers.DriverIsLicenseNumber = '"+maskedTextBox7.Text+"';";
                        MySQL(SQL, 9);
                        button4.Enabled = true;
                    }
                    else MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (check == false)
            {
                if (radioButton13.Checked)
                {
                    if (maskedTextBox6.MaskCompleted)
                    {
                        DialogResult result = MessageBox.Show("Это тот водитель, которого вы искали?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Categores.Clear();
                            SQL = "SELECT gibddmodern.categorydriver.IdCategory AS Category FROM gibddmodern.categorydriver INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver WHERE PassportData = '" + maskedTextBox6.Text + "';";
                            MySQL(SQL, 10);
                            panel8.Show();
                            CheckBox[] checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16 };
                            for (int j = 0; j < Categores.Count; j++) for (int i = 0; i < 16; i++) if (Categores[j] == (i + 1)) { checkBoxes[i].Checked = true; checkBoxes[i].Enabled = false; break; }
                            TRUE();
                        }
                    }
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if(radioButton9.Checked || radioButton12.Checked)
            {
                if(textBox6.TextLength > 0)
                {
                    if(radioButton9.Checked)
                    {
                        NP = textBox6.Text;
                        textBox1.ForeColor = System.Drawing.Color.Black;
                        textBox2.ForeColor = System.Drawing.Color.Black;
                        SQL = "call gibddmodern.Protocols(" + NP + ")";
                        MySQL(SQL, 7);
                    }
                    else if(radioButton12.Checked)
                    {
                        NP = textBox6.Text;
                        textBox1.ForeColor = System.Drawing.Color.Black;
                        textBox2.ForeColor = System.Drawing.Color.Black;
                        SQL = "SELECT Number, ArrivalDate, Status FROM gibddmodern.parkingfine INNER JOIN gibddmodern.cars ON gibddmodern.parkingfine.IdCar = gibddmodern.cars.idCar WHERE IdProtocol = '"+ NP +"';";
                        MySQL(SQL, 5);
                    }
                }
            }
        }

        private void TRUE()
        {
            check = true;
            DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker7, dateTimePicker8 };
            for (int i = 0; i < 2; i++) dateTimePickers[i].Enabled = true;
            TextBox[] textBoxes = new TextBox[] { textBox15, textBox16, textBox17, textBox18 };
            for (int i = 0; i < 4; i++) textBoxes[i].Enabled = true;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox6, maskedTextBox7 };
            for (int i = 0; i < 2; i++) maskedTextBoxes[i].Enabled = true;
            SQL = "SELECT idDriver AS IdDriver FROM gibddmodern.drivers WHERE PassportData = '" + maskedTextBox6.Text + "';";
            MySQL(SQL, 1);
        }

        public bool Checker()
        {
            if (radioButton2.Checked)
            {
                TextBox[] textBoxes = new TextBox[] { textBox15, textBox16, textBox17, textBox18 };
                for (int i = 0; i < 4; i++) if (textBoxes[i].Text == string.Empty) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox6, maskedTextBox7 };
                for (int i = 0; i < 2; i++) if (maskedTextBoxes[i].MaskCompleted) { } else { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                if (dateTimePicker7.Value == DateTime.Now.Date) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                return true;
            }
            if(radioButton3.Checked)
            {
                TextBox[] textBoxes = new TextBox[] {textBox14, textBox13, textBox12, textBox11, textBox10, textBox9, textBox8};
                for (int i = 0; i < 7; i++) if (textBoxes[i].Text == string.Empty) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4 };
                for (int i = 0; i < 2; i++) if (maskedTextBoxes[i].MaskCompleted) { } else { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                if(comboBox1.Text == string.Empty) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                if(dateTimePicker5.Value == DateTime.Now.Date) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }           
                return true;
            }
            if(radioButton4.Checked)
            {
                TextBox[] textBoxes = new TextBox[] { textBox6, textBox7 };
                for (int i = 0; i < 2; i++) if (textBoxes[i].Text == string.Empty) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            }
            if (radioButton5.Checked)
            {
                TextBox[] textBoxes = new TextBox[] {textBox3, textBox4, textBox5 };
                for(int i=0; i<3; i++) if(textBoxes[i].Text == string.Empty) { MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                if(maskedTextBox5.MaskCompleted) { } else { MessageBox.Show("Поле номерной знак не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            }
            return true;
        }

        public bool Check() //Проверка заполнености номерных знаков
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

        private void ADD_Categores() //Метод для добавления категорий водителя;
        {
            Categores.Clear();
            CheckBox[] checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16 };
            for (int i = 0; i < 16; i++) if (checkBoxes[i].Checked) Categores.Add(i + 1);
        }

        private void NEW() //Метод добавления нового автомобиля на учет
        {
            SQL = "SELECT COUNT(*) FROM gibddmodern.cars WHERE Number ='"+textBox1.Text+textBox2.Text+"';";
            MySQL(SQL, 0);
            if(Count != 0) MessageBox.Show("Авто с таким номером состоит на учете", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Count = 0;
                SQL = "SELECT COUNT(*) FROM (gibddmodern.categorydriver INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory WHERE DriverIsLicenseNumber = "+maskedTextBox4.Text+" AND gibddmodern.category.Name = '"+comboBox1.SelectedItem+"';";
                MySQL(SQL, 0);
                if(Count == 0) MessageBox.Show("У водителя нет нужной категории прав", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    SQL = "SELECT COUNT(*) FROM gibddmodern.cars";
                    MySQL(SQL, 0);
                    Count++;
                    SQL = "call gibddmodern.New_Car("+Count+","+maskedTextBox4.Text+",'"+textBox11.Text+"', '"+textBox12.Text+"','"+comboBox1.SelectedItem+"','"+textBox1.Text+textBox2.Text+"','"+maskedTextBox1.Text+"','"+maskedTextBox2.Text+"','"+maskedTextBox3.Text+"','"+textBox13.Text+"', '"+ dateTimePicker3.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker5.Value.ToString("yyyy-MM-dd")+"', '"+textBox14.Text+"');";
                    MySQL(SQL, 20);
                    Cleaner();
                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Cleaner() //Метод для очистки всех полей
        {
            textBox1.Text = "А000АА";
            textBox2.Text = "00";
            comboBox1.Text = string.Empty;
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5, maskedTextBox6, maskedTextBox7};
            for (int i = 0; i < 7; i++) maskedTextBoxes[i].Text = string.Empty;
            TextBox[] textBoxes = new TextBox[] { textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19 };
            for (int i = 0; i < 17; i++) textBoxes[i].Text = string.Empty;
            CheckBox[] checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16 };
            for (int i = 0; i < 16; i++) checkBoxes[i].Checked = false;
            DateTimePicker[] dateTimes = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3, dateTimePicker4, dateTimePicker5, dateTimePicker6, dateTimePicker7, dateTimePicker8, dateTimePicker9 };
            for (int i = 0; i < 9; i++) dateTimes[i].Value = DateTime.Now.Date;
        }

        private void Metod()
        {
            Hiden();
            Shower();
            Cleaner();
            No_Checked();
        }

        private void Hiden() //Убирает все нанели
        {
            Panel[] panels = new Panel[] {panel1, panel2, panel3, panel4, panel6, panel7, panel8};
            for(int i = 0; i< 7; i++) panels[i].Hide();
        }

        private void Shower() //Показывает определенные radiobuttons 
        {
            panel6.Show();
            RadioButton[] radioButtons = new RadioButton[] { radioButton9, radioButton12, radioButton13, radioButton14 };
            for (int i = 0; i < 4; i++) radioButtons[i].Show();
            if (radioButton5.Checked) { radioButton12.Hide(); radioButton13.Hide(); }
            if(radioButton4.Checked) radioButton13.Hide();
        }

        private void No_Checked() //Убирает checked с radiobuttons на панеле №6
        {
            RadioButton[] radioButtons = new RadioButton[] {radioButton9,radioButton12,radioButton13,radioButton14};
            for (int i = 0; i < 4; i++) radioButtons[i].Checked = false;
        }

        private void Num(string Number) //Разбивает результат запроса на две подстроки
        {
            textBox1.Text = Number.Substring(0, 6);
            textBox2.Text = Number.Substring(6);
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
                    while(reader.Read()) IDD = IDC = Convert.ToInt32(reader["IdDriver"]);
                    break;
                case 2:
                    while(reader.Read()) CheckCount = Convert.ToInt32(reader["idCar"]);
                    break;
                case 3:
                    while(reader.Read())
                    {
                        Number = reader["Number"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["Date"]);
                        dateTimePicker6.Value = Convert.ToDateTime(reader["Endofterm"]);
                        Surname = reader["Surname"].ToString();
                        name = reader["Name"].ToString();
                        MiddleName = reader["MiddleName"].ToString();
                    }
                    Num(Number);
                    textBox3.Text = Surname;
                    textBox4.Text = name;
                    textBox5.Text = MiddleName;
                    break;
                case 4:
                    while(reader.Read())
                    {
                        if(radioButton14.Checked) maskedTextBox1.Text = reader["BodyNumber"].ToString();
                        Number = reader["Number"].ToString();
                        textBox10.Text = reader["Surname"].ToString();
                        textBox9.Text = reader["Name"].ToString();
                        textBox8.Text = reader["MiddleName"].ToString();
                        textBox11.Text = reader["NameBrand"].ToString();
                        textBox12.Text = reader["NameModel"].ToString();
                        comboBox1.Text = reader["Category"].ToString();
                        textBox13.Text = reader["Color"].ToString();
                        textBox14.Text = reader["IdEmployee"].ToString();
                        maskedTextBox2.Text = reader["EngineNumber"].ToString();
                        maskedTextBox3.Text = reader["PassportNumber"].ToString();
                        dateTimePicker3.Value = Convert.ToDateTime(reader["TechnicalInspectionDate"]);
                        dateTimePicker4.Value = Convert.ToDateTime(reader["RegistrationDate"]);
                        dateTimePicker5.Value = Convert.ToDateTime(reader["ReleaseDate"]);
                    }
                    Num(Number);
                    break;
                case 5:
                    while(reader.Read())
                    {
                        if(radioButton12.Checked)
                        {
                            textBox7.Text = reader["Status"].ToString();
                            dateTimePicker2.Value = Convert.ToDateTime(reader["ArrivalDate"]);
                            Number = reader["Number"].ToString();
                        }
                        else
                        {
                            textBox6.Text = reader["IdProtocol"].ToString();
                            textBox7.Text = reader["Status"].ToString();
                            dateTimePicker2.Value = Convert.ToDateTime(reader["ArrivalDate"]);
                            Number = reader["Number"].ToString();
                        }
                    }
                    Num(Number);
                    break;
                case 6:
                    while(reader.Read()) Number = reader["Number"].ToString();
                    break;
                case 7:
                    while(reader.Read())
                    {
                        Number = reader["Number"].ToString();
                        dateTimePicker2.Value = Convert.ToDateTime(reader["Date"]);
                    }
                    Num(Number);
                    break;
                case 8:
                    while (reader.Read()) Number = reader["Number"].ToString();
                    Num(Number);
                    break;
                case 9:
                    while (reader.Read())
                    {
                        if(radioButton14.Checked)
                        {
                            textBox15.Text = reader["Surname"].ToString();
                            textBox16.Text = reader["Name"].ToString();
                            textBox17.Text = reader["MiddleName"].ToString();
                            maskedTextBox7.Text = reader["DriverIsLicenseNumber"].ToString();
                        }
                        textBox18.Text = reader["PlaceofBirth"].ToString();
                        textBox19.Text += reader["Category"].ToString() + ". ";
                        maskedTextBox6.Text = reader["PassportData"].ToString();
                        dateTimePicker7.Value = Convert.ToDateTime(reader["DateofBirth"]);
                        dateTimePicker8.Value = Convert.ToDateTime(reader["DateofIssueLicense"]);
                        dateTimePicker9.Value = Convert.ToDateTime(reader["EndDateLicense"]);
                    }
                    break;
                case 10:
                    while (reader.Read()) Categores.Add(Convert.ToInt32(reader["Category"]));
                    break;
            }
        }
    }
}
