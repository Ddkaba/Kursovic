using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Kursovic
{
    public partial class GaraceCCForm : Form
    {
        public GaraceCCForm()
        {
            InitializeComponent();
        }

        int Count , Counter;
        string SQL;

        private void Form9_Load(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            button2.Hide();
            button3.Hide();
            DateTimePicker[] dateTimes = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3 };
            for (int i = 0; i < 3; i++) dateTimes[i].MaxDate = DateTime.Now.Date;
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка для возвращения в меню
        {
            Form form5 = new MenuForm();
            form5.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if (radioButton9.Checked) //Кнопка ДОБАВИТЬ для добавления автомобилей в гараж
            {
                bool check = true;
                if (comboBox1.Text == string.Empty) { MessageBox.Show("Не была выбрана категория", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); check = false; }
                TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3 };
                for (int i = 0; i < 3; i++) if (textBoxes[i].Text == string.Empty) { MessageBox.Show("Проверьте поля Марка и Модель авто и Код сотрудника, возможно они не заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); check = false; }
                MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4 };
                for (int i = 0; i < 4; i++) if (maskedTextBoxes[i].Text == string.Empty) { MessageBox.Show("Проверьте поля Номерной знак, VIN, СТС, Номер двигателя, возможно они не заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); check = false; }
                if (dateTimePicker1.Value.Date == DateTime.Now.Date) { MessageBox.Show("Дата не заполнена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); check = false; }
                Regex myReg = new Regex(@"^[АВСЕНКМОРТХУ]{1}[0-9]{4}[0-9]{1}[1-9]{1,2}$");
                if (myReg.IsMatch(maskedTextBox1.Text) == false) { MessageBox.Show("Номер не соответствует ГОСТу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); check = false; }
                if (check == true)
                {
                    SQL = "SELECT COUNT(*) FROM gibddmodern.garagecc WHERE Number = '" + maskedTextBox1.Text+"';";
                    MySQL(SQL, 0);
                    if (Count == 0)
                    {
                        SQL = "SELECT COUNT(*) FROM gibddmodern.garagecc WHERE BodyNumber = '" + maskedTextBox2.Text + "';";
                        MySQL(SQL, 0);
                        if(Count == 0)
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.garagecc;";
                            MySQL(SQL, 0);
                            Count++;
                            SQL = "call gibddmodern.UPDATE_GARAGECC('"+Count+"','"+textBox1.Text+"','"+textBox2.Text+"','"+maskedTextBox1.Text+"','"+maskedTextBox2.Text+"','"+maskedTextBox3.Text+"','"+maskedTextBox4.Text+ "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','"+comboBox1.SelectedItem+"', '"+textBox3.Text+"');";
                            MySQL(SQL, 20);
                            MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cleaner();
                        }
                        else MessageBox.Show("Автомобиль уже стоит в гараже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Автомобиль с таким номером уже стоит в гараже", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if(radioButton12.Checked) //Кнопка Удалить для удаления автомобилей из гаража
            {
                DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SQL = "DELETE FROM gibddmodern.crews WHERE idCar = (SELECT idCar FROM gibddmodern.garagecc WHERE BodyNumber = '" + maskedTextBox5.Text + "'); DELETE FROM gibddmodern.garagecc WHERE BodyNumber = '" + maskedTextBox5.Text + "';";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleaner();
                }
            }
            if (radioButton14.Checked) //Кнопка ВПЕРЕД для просмотра автомобилей в гараже
            {
                button3.Enabled = true;
                Counter++;
                if (Counter <= Count)
                {
                    SQL = "SELECT NameBrand, NameModel, Number, BodyNumber, EngineNumber, PassportNumber, ReleaseDate, RegistrationDate, TechnicalInspectionDate, gibddmodern.category.Name AS Category FROM(gibddmodern.garagecc INNER JOIN gibddmodern.carbrands ON gibddmodern.garagecc.IdBrand = gibddmodern.carbrands.idBrand) INNER JOIN gibddmodern.category ON gibddmodern.garagecc.Category = gibddmodern.category.idCategory WHERE idCar = '" + Counter + "'; ";
                    MySQL(SQL, 1);
                    if (Counter == Count) button2.Enabled = false;
                }
                else { button2.Enabled = false; Cleaner(); }
            }
        }

        private void button3_Click(object sender, EventArgs e) //Кнопка НАЗАД для просмотра автомобилей в гараже
        {
            button2.Enabled = true;
            Counter--;
            if (Counter >= 1)
            {
                SQL = "SELECT NameBrand, NameModel, Number, BodyNumber, EngineNumber, PassportNumber, ReleaseDate, RegistrationDate, TechnicalInspectionDate, gibddmodern.category.Name AS Category FROM(gibddmodern.garagecc INNER JOIN gibddmodern.carbrands ON gibddmodern.garagecc.IdBrand = gibddmodern.carbrands.idBrand) INNER JOIN gibddmodern.category ON gibddmodern.garagecc.Category = gibddmodern.category.idCategory WHERE idCar = '" + Counter + "'; ";
                MySQL(SQL, 1);
                if (Counter == 1) button3.Enabled = false;
            }
            else button3.Enabled = false;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            panel1.Show();
            panel2.Hide();
            label9.Hide();
            button2.Show();
            button3.Hide();
            label12.Show();
            textBox3.Show();
            panel1.Enabled = true;
            button2.Enabled = true;
            dateTimePicker3.Hide();
            button2.Text = "Добавить";
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        { //Переключатель для просмотра автомобилей в гараже
            Counter = 0;
            if (radioButton14.Checked)
            {
                Cleaner();
                label9.Show();
                panel1.Show();
                panel2.Hide();
                button3.Show();
                button2.Show();
                label12.Hide();
                textBox3.Hide();
                dateTimePicker3.Show();
                button2.Enabled = true;
                panel1.Enabled = false;
                button2.Text = "Вперед";
                SQL = "SELECT COUNT(*) FROM gibddmodern.garagecc;";
                MySQL(SQL, 0);
                Counter++;
                SQL = "SELECT NameBrand, NameModel, Number, BodyNumber, EngineNumber, PassportNumber, ReleaseDate, RegistrationDate, TechnicalInspectionDate, gibddmodern.category.Name AS Category FROM(gibddmodern.garagecc INNER JOIN gibddmodern.carbrands ON gibddmodern.garagecc.IdBrand = gibddmodern.carbrands.idBrand) INNER JOIN gibddmodern.category ON gibddmodern.garagecc.Category = gibddmodern.category.idCategory WHERE idCar = '" + Counter + "'; ";
                MySQL(SQL, 1);
                button3.Enabled = false;
                if (Count == 1) button2.Enabled = false;
                if (Count > 1) Counter = 1;
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Cleaner();
            panel1.Show();
            panel2.Show();
            label9.Show();
            button2.Show();
            button3.Hide();
            label12.Hide();
            textBox3.Hide();
            dateTimePicker3.Show();
            button2.Enabled = false;
            panel1.Enabled = false;
            button2.Text = "Удалить";
        }

        private void maskedTextBox5_TextChanged(object sender, EventArgs e) 
        {
            if (radioButton12.Checked) //Переключать для поиска автомобилей с помощью VIN при удалении
            {
                if (maskedTextBox5.MaskCompleted)
                {
                    SQL = "SELECT NameBrand, NameModel, Number, EngineNumber, PassportNumber, ReleaseDate, RegistrationDate, TechnicalInspectionDate, gibddmodern.category.Name AS Category FROM(gibddmodern.garagecc INNER JOIN gibddmodern.carbrands ON gibddmodern.garagecc.IdBrand = gibddmodern.carbrands.idBrand) INNER JOIN gibddmodern.category ON gibddmodern.garagecc.Category = gibddmodern.category.idCategory WHERE  BodyNumber = '" + maskedTextBox5.Text + "'; ";
                    MySQL(SQL, 1);
                    button2.Enabled = true;
                }
                else button2.Enabled = false;
            }
        }

        private void Cleaner() //Метод для очистки всех полей
        {
            comboBox1.Text = string.Empty;
            TextBox[] textBoxes = new TextBox[] {textBox1, textBox2, textBox3};
            for (int i = 0; i < 3; i++) textBoxes[i].Text = string.Empty;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5};
            for (int i = 0; i < 5; i++) maskedTextBoxes[i].Text = string.Empty;
            DateTimePicker[] dateTimes = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3};
            for (int i = 0; i < 3; i++) dateTimes[i].Value = DateTime.Now.Date;
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
                        textBox1.Text = reader["NameBrand"].ToString();
                        textBox2.Text = reader["NameModel"].ToString();
                        comboBox1.Text = reader["Category"].ToString();
                        maskedTextBox1.Text = reader["Number"].ToString();
                        maskedTextBox3.Text = reader["EngineNumber"].ToString();
                        maskedTextBox4.Text = reader["PassportNumber"].ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(reader["ReleaseDate"]);
                        dateTimePicker2.Value = Convert.ToDateTime(reader["TechnicalInspectionDate"]);
                        dateTimePicker3.Value = Convert.ToDateTime(reader["RegistrationDate"]);
                        if (radioButton14.Checked) maskedTextBox2.Text = reader["BodyNumber"].ToString();
                    }
                    break;
            }
        }
    }
}
