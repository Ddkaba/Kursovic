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

        int Count, CheckCount = 0, IDD, Counter;
        string SQL, Surname, name, MiddleName, Number, NP;
        List<int> Categores = new List<int>();

        private void Form4_Load(object sender, EventArgs e)
        {
            Hiden();

            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) //Сотрудники
        {
            Shower();
            Hiden();
            Cleaner();
            panel6.Show();
            No_Checked();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) //Водители
        {
            Hiden();
            Shower();
            Cleaner();
            panel6.Show();
            No_Checked();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) //Автомобили
        {
            Cleaner();
            Hiden();
            panel6.Show();
            No_Checked();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) //Штрафстоянка
        {
            Hiden();
            Shower();
            Cleaner();
            panel6.Show();
            No_Checked();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) //Хранение номеров
        {
            Hiden();
            Shower();
            Cleaner();
            panel6.Show();
            No_Checked();
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
                label35.Hide();
                textBox19.Hide();
                label34.Hide();
                dateTimePicker9.Hide();
                button4.Text = "Добавить";
                textBox15.Enabled = true;
                textBox16.Enabled = true;
                textBox17.Enabled = true;
                textBox18.Enabled = true;
                maskedTextBox6.Enabled = true;
                maskedTextBox7.Enabled = true;
                dateTimePicker7.Enabled = true;
                dateTimePicker8.Enabled = true;

            }
            if(radioButton3.Checked) //Переключатель для добавления машин на учет
            {
                panel1.Show();
                panel4.Show();
                button3.Hide();
                label24.Show();
                label22.Hide();
                dateTimePicker4.Hide();
                maskedTextBox4.Show();
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
                textBox12.Enabled = true;
                textBox13.Enabled = true;
                textBox14.Enabled = true;
                comboBox1.Enabled = true;
                maskedTextBox1.Enabled = true;
                maskedTextBox2.Enabled = true;
                maskedTextBox3.Enabled = true;
                maskedTextBox4.Enabled = true;
                dateTimePicker3.Enabled = true;
                dateTimePicker4.Enabled = true;
                dateTimePicker5.Enabled = true;
                button16.Text = "Добавить";
            }
            if(radioButton4.Checked) //Переключатель для добавления машин на штрафстоянку
            {
                panel1.Show();
                panel3.Show();
                button12.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                dateTimePicker2.Enabled = true;
                button1.Hide();
                button12.Text = "Добавить";
            }
            if(radioButton5.Checked) //Переключатель для добавления новых хранимых номеров
            {
                panel1.Show();
                panel2.Show();
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker6.Enabled = true;
                label23.Hide();
                label25.Show();
                maskedTextBox5.Show();
                dateTimePicker6.Hide();
                button2.Hide();
                button7.Text = "Добавить";
                button7.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            textBox1.ForeColor = System.Drawing.Color.Black;
            textBox2.ForeColor = System.Drawing.Color.Black;
            if (radioButton3.Checked)
            {
                panel1.Show();
                panel4.Show();
                button3.Hide();
                label24.Hide();
                label22.Show();
                dateTimePicker4.Show();
                maskedTextBox4.Hide();
                button16.Text = "Удалить";
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;
                comboBox1.Enabled = false;
                maskedTextBox1.Enabled = true;
                maskedTextBox2.Enabled = false;
                maskedTextBox3.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                dateTimePicker5.Enabled = false;
            }
            if(radioButton4.Checked)
            {
                panel1.Show();
                panel3.Show();
                textBox6.Enabled = true;
                button12.Enabled = true;
                textBox7.Enabled = false;
                dateTimePicker2.Enabled = false;
                button1.Hide();
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
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker6.Enabled = false;
                button7.Enabled = true;
                button2.Enabled = true;
                label23.Show();
                label25.Hide();
                maskedTextBox5.Hide();
                dateTimePicker6.Show();
                button2.Show();
                button7.Text = "Вперед";
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
                dateTimePicker4.Show();
                maskedTextBox4.Hide();
                button16.Text = "Вперед";
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;
                comboBox1.Enabled = false;
                maskedTextBox1.Enabled = false;
                maskedTextBox2.Enabled = false;
                maskedTextBox3.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                dateTimePicker5.Enabled = false;
                SQL = "SELECT COUNT(*) FROM gibddmodern.cars;";
                MySQL(SQL, 0);
                SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand LIMIT " + Counter + ",1;";
                button3.Enabled = false;
                if (Count == 1)
                {
                    MySQL(SQL, 4);
                    button16.Enabled = false;
                }
                if(Count > 1)
                {
                    MySQL(SQL, 4);
                    Counter = 0;
                }
            }
            if(radioButton2.Checked) //Переключатель для просмотра водителей с правами
            {
                button5.Show();
                panel7.Show();
                panel8.Hide();
                label35.Show();
                textBox19.Show();
                label34.Show();
                dateTimePicker9.Show();
                textBox15.Enabled = false;
                textBox16.Enabled = false;
                textBox17.Enabled = false;
                textBox18.Enabled = false;
                textBox19.Enabled = false;
                maskedTextBox6.Enabled = false;
                maskedTextBox7.Enabled = false;
                dateTimePicker7.Enabled = false;
                dateTimePicker8.Enabled = false;
                dateTimePicker9.Enabled = false;
                SQL = "SELECT COUNT(*) FROM gibddmodern.drivers;";
                MySQL(SQL, 0);
                Counter++;
                SQL = "SELECT Surname, gibddmodern.drivers.Name, MiddleName, DateofBirth, PlaceofBirth, PassportData, DriverIsLicenseNumber, DateofIssueLicense, EndDateLicense, gibddmodern.category.Name AS Category FROM(gibddmodern.categorydriver INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver WHERE gibddmodern.drivers.idDriver = "+Counter+"; ";
                button5.Enabled = false;
                if (Count == 1)
                {
                    MySQL(SQL, 9);
                    button4.Enabled = false;
                }
                if (Count > 1)
                {
                    MySQL(SQL, 9);
                    Counter = 1;
                }
            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                Cleaner();
                panel1.Show();
                panel4.Show();
                panel1.Show();
                panel4.Show();
                button3.Hide();
                label24.Show();
                label22.Show();
                dateTimePicker4.Show();
                maskedTextBox4.Show();
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;
                comboBox1.Enabled = false;
                maskedTextBox1.Enabled = true;
                maskedTextBox2.Enabled = false;
                maskedTextBox3.Enabled = false;
                maskedTextBox4.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                dateTimePicker5.Enabled = false;
                button16.Text = "Изменить";
                textBox1.ForeColor = System.Drawing.Color.Black;
                textBox2.ForeColor = System.Drawing.Color.Black;
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
                SQL = "SELECT idDriver AS IdDriver FROM gibddmodern.drivers WHERE DriverIsLicenseNumber = '" + maskedTextBox7.Text + "';";
                MySQL(SQL,1);
                if(IDD != 0) MessageBox.Show("Водитель с таким номером уже имеется в базе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    SQL = "SELECT COUNT(*) FROM gibddmodern.drivers";
                    MySQL(SQL, 0);
                    Count++;
                    SQL = "INSERT INTO gibddmodern.drivers VALUES ('"+Count+"','"+textBox15.Text+"', '"+textBox16.Text+"', '"+textBox17.Text+ "','" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "', '"+textBox18.Text+"', '"+maskedTextBox6.Text+"', '"+maskedTextBox7.Text+ "', '" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "', DATE_ADD('" + dateTimePicker8.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR) );";
                    MySQL(SQL, 20);
                    ADD_Categores();
                    foreach(int i in Categores)
                    {
                        SQL = "INSERT INTO gibddmodern.categorydriver VALUES('" + Count + "', '"+Categores[i]+"')";
                        MySQL(SQL, 20);
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

        private void button7_Click(object sender, EventArgs e) //Кнопка добавления хранимых номеров, и кнопка ВПЕРЕД для просмотра
        {
            if(radioButton9.Checked)
            {
                Surname = textBox3.Text;
                name = textBox4.Text;
                MiddleName = textBox5.Text;
                SQL = "SELECT IdDriver FROM gibddmodern.drivers WHERE Surname = '"+ Surname +"' and Name = '"+ name +"' and MiddleName = '"+ MiddleName +"';";
                MySQL(SQL, 1);
                SQL = "SELECT idCar FROM gibddmodern.cars WHERE IdDriver = " +IDD+ " and Number = '" + textBox1.Text + textBox2.Text + "'";
                MySQL(SQL, 2);
                if (CheckCount != 0)
                {
                    SQL = "SELECT COUNT(*) FROM gibddmodern.storingnumbers";
                    MySQL(SQL, 0);
                    Count++;
                    SQL = "UPDATE gibddmodern.cars SET Number = '"+maskedTextBox5.Text+"' WHERE idCar = "+CheckCount+";";
                    MySQL(SQL, 20);
                    SQL = "INSERT INTO gibddmodern.storingnumbers VALUES (" + Count + ",'" + textBox1.Text + textBox2.Text + "', "+IDD+ ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', DATE_ADD('"+ dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR));";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Error");
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
            if(radioButton9.Checked)
            {
                SQL = "SELECT COUNT(*) FROM gibddmodern.parkingfine INNER JOIN gibddmodern.cars ON gibddmodern.parkingfine.IdCar = gibddmodern.cars.idCar WHERE IdProtocol = " + Convert.ToInt32(textBox6.Text) + " AND Number = '"+textBox1.Text +textBox2.Text+"';";
                MySQL(SQL, 0);
                if(Count == 0)
                {
                    SQL = "SELECT COUNT(*) FROM gibddmodern.parkingfine";
                    MySQL(SQL, 0);
                    Count = Count + 1;
                    SQL = "INSERT INTO gibddmodern.parkingfine VALUES ('"+Count+ "', (SELECT idCar FROM gibddmodern.cars WHERE Number = '"+textBox1.Text+textBox2.Text+"'), '"+textBox6.Text+ "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '"+textBox7.Text+"');";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Автомобиль уже находиться на штрафстоянке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    SQL = "DELETE FROM gibddmodern.parkingfine WHERE IdProtocol = '" + textBox6.Text + "';";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleaner();
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
            {
                SQL = "call gibddmodern.UPDATE_CARS('" + CheckCount + "','" + maskedTextBox4.Text + "','" + textBox1.Text + textBox2.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + textBox13.Text + "','" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "', " + textBox14.Text + ");";
                MySQL(SQL, 20);
            }
            if (radioButton9.Checked)
            {
                panel1.Show();
                SQL = "SELECT idDriver FROM gibddmodern.drivers WHERE Surname = '" + textBox10.Text + "' AND Name = '" + textBox9.Text + "' AND MiddleName = '" + textBox8.Text + "' AND  DriverIsLicenseNumber = '" + maskedTextBox4.Text + "';";
                MySQL(SQL, 1);
                if (IDD == 0)
                {
                    MessageBox.Show("Запись не найдена", "Внимание!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                                if (Counter == 2)
                                {
                                    break;
                                }
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
                            else
                            {
                                NEW();
                            }
                        }
                        else if (result == DialogResult.No)
                        {
                            NEW();
                        }
                    }
                    else if (Count == 0)
                    {
                        NEW();
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
                DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SQL = "DELETE FROM gibddmodern.cars WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleaner();
                }
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
                    SQL = "SELECT DriverIsLicenseNumber AS IdDriver FROM gibddmodern.drivers INNER JOIN gibddmodern.cars ON gibddmodern.drivers.idDriver = gibddmodern.cars.IdDriver WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 1);
                    maskedTextBox4.Text = IDD.ToString();
                    SQL = "SELECT Surname, Name, MiddleName, NameBrand, NameModel, Number, Category, Color, BodyNumber, EngineNumber, PassportNumber, TechnicalInspectionDate, ReleaseDate, RegistrationDate, IdEmployee FROM(gibddmodern.cars INNER JOIN gibddmodern.drivers ON gibddmodern.cars.IdDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.carbrands ON gibddmodern.cars.IdBrand = gibddmodern.carbrands.idBrand WHERE BodyNumber = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 4);
                    textBox8.Enabled = true;
                    textBox9.Enabled = true;
                    textBox10.Enabled = true;
                    textBox13.Enabled = true;
                    textBox14.Enabled = true;
                    maskedTextBox2.Enabled = true;
                    maskedTextBox3.Enabled = true;
                    maskedTextBox4.Enabled = true;
                    dateTimePicker3.Enabled = true;
                    dateTimePicker4.Enabled = true;
                    dateTimePicker5.Enabled = true;
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

        public bool Check() //Проверка заполнености номерных знаков
        {
            if (textBox1.Text == "A000AA" & textBox2.Text == "47")
            {
                if (textBox1.Text == "A000AA")
                {
                    if (textBox2.Text == "47") { MessageBox.Show("Номерной знак не введен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                    else { MessageBox.Show("Не заполнена часть с номером", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                }
                else { MessageBox.Show("Не заполнена часть с регионом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
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

        private void ADD_Categores()
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

        private void NEW() //Метод добавления нового автомобиля на учет
        {
            SQL = "SELECT COUNT(*) FROM gibddmodern.cars WHERE Number ='"+textBox1.Text+textBox2.Text+"';";
            MySQL(SQL, 0);
            if(Count != 0)
            {
                MessageBox.Show("Авто с таким номером состоит на учете", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Count = 0;
                SQL = "SELECT COUNT(*) FROM (gibddmodern.categorydriver INNER JOIN gibddmodern.drivers ON gibddmodern.categorydriver.idDriver = gibddmodern.drivers.idDriver) INNER JOIN gibddmodern.category ON gibddmodern.categorydriver.IdCategory = gibddmodern.category.idCategory WHERE DriverIsLicenseNumber = "+maskedTextBox4.Text+" AND gibddmodern.category.Name = '"+comboBox1.SelectedItem+"';";
                MySQL(SQL, 0);
                if(Count == 0)
                {
                    MessageBox.Show("У водителя нет нужной категории прав", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SQL = "SELECT COUNT(*) FROM gibddmodern.cars";
                    MySQL(SQL, 0);
                    Count++;
                    SQL = "call gibddmodern.New_Car("+Count+","+maskedTextBox4.Text+",'"+textBox11.Text+"', '"+textBox12.Text+"','"+comboBox1.SelectedItem+"','"+textBox1.Text+textBox2.Text+"','"+maskedTextBox1.Text+"','"+maskedTextBox2.Text+"','"+maskedTextBox3.Text+"','"+textBox13.Text+"', '"+ dateTimePicker3.Value.ToString("yyyy-MM-dd") + "', '" + dateTimePicker5.Value.ToString("yyyy-MM-dd")+"', '"+textBox14.Text+"');";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Cleaner() //Метод для очистки всех полей
        {
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.ForeColor = System.Drawing.Color.Gray;
            textBox1.Text = "А000АА";
            textBox2.Text = "47";
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;
            textBox16.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox18.Text = string.Empty;
            textBox19.Text = string.Empty;
            comboBox1.Text = string.Empty;
            maskedTextBox1.Text = string.Empty;
            maskedTextBox2.Text = string.Empty;
            maskedTextBox3.Text = string.Empty;
            maskedTextBox4.Text = string.Empty;
            maskedTextBox5.Text = string.Empty;
            maskedTextBox6.Text = string.Empty;
            maskedTextBox7.Text = string.Empty;
            dateTimePicker1.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            dateTimePicker3.Text = string.Empty;
            dateTimePicker4.Text = string.Empty;
            dateTimePicker5.Text = string.Empty;
            dateTimePicker6.Text = string.Empty;
            dateTimePicker7.Text = string.Empty;
            dateTimePicker8.Text = string.Empty;
            dateTimePicker9.Text = string.Empty;
        }

        private void Hiden() //Убирает все нанели
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
        }

        private void Shower() //Показывает определенные radiobuttons 
        {
            radioButton9.Show();
            radioButton12.Show();
            radioButton14.Show();
            radioButton13.Show();
            if(radioButton5.Checked)
            {
                radioButton12.Hide();
                radioButton13.Hide();
            }
            if(radioButton4.Checked)
            {
                radioButton13.Hide();
            }
        }

        private void No_Checked() //Убирает checked с radiobuttons на панеле №6
        {
            radioButton9.Checked = false;
            radioButton12.Checked = false;
            radioButton13.Checked = false;
            radioButton14.Checked = false;
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
                    while(reader.Read()) IDD = Convert.ToInt32(reader["IdDriver"]);
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
                        if(radioButton14.Checked)
                        {
                            maskedTextBox1.Text = reader["BodyNumber"].ToString();
                        }
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
                        textBox15.Text = reader["Surname"].ToString();
                        textBox16.Text = reader["Name"].ToString();
                        textBox17.Text = reader["MiddleName"].ToString();
                        textBox18.Text = reader["PlaceofBirth"].ToString();
                        textBox19.Text += reader["Category"].ToString() + ". ";
                        maskedTextBox6.Text = reader["PassportData"].ToString();
                        maskedTextBox7.Text = reader["DriverIsLicenseNumber"].ToString();
                        dateTimePicker7.Value = Convert.ToDateTime(reader["DateofBirth"]);
                        dateTimePicker8.Value = Convert.ToDateTime(reader["DateofIssueLicense"]);
                        dateTimePicker9.Value = Convert.ToDateTime(reader["EndDateLicense"]);
                    }
                    break;
            }
        }
    }
}
