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

        int Count,Counter,IDD, IDC;
        string SQL;
        bool check = false;
        List<int> Categores = new List<int>();

        private void Form6_Load(object sender, EventArgs e)
        {
            button2.Hide();
            button3.Hide();
            panel8.Hide();
            dateTimePicker2.MaxDate = DateTime.Now.Date;
            dateTimePicker1.MaxDate = DateTime.Now.Date;
            RadioButton[] radioButtons = new RadioButton[] { radioButton9, radioButton12, radioButton13, radioButton14 };
            for (int i = 0; i < 4; i++) radioButtons[i].Checked = false;
        }

        private void button1_Click(object sender, EventArgs e) //Кнопка для возвращения в меню
        {
            Form form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IDD = 0; 
            if (radioButton9.Checked) //Кнопка ДОБАВИТЬ для добавления новых сотрудников
            {
                bool Check = Checker();
                if(Check == true)
                {
                    SQL = "SELECT idEmployee AS IdDriver FROM gibddmodern.employees WHERE PassportData = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 3);
                    if (IDD != 0) MessageBox.Show("Сотрудник уже имеется в базе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        SQL = "SELECT COUNT(*) FROM gibddmodern.employees";
                        MySQL(SQL, 0);
                        Count++;
                        if(maskedTextBox2.MaskCompleted)
                        {
                            ADD_Categores();
                            if(Categores.Count == 0) MessageBox.Show("Выберете категории сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                SQL = "INSERT INTO gibddmodern.employees(`idEmployee`, `Surname`, `Name`, `MiddleName`, `DateOfBirth`, `Sex`, `idPosition`, `idRank`, `PassportData`, `DriversLicenseNumber`, `DateofIssueLicense`, `EndDateLicense`, `Address`, `Number`) VALUES ('" + Count+"', '"+textBox1.Text+"', '"+textBox2.Text+"', '"+textBox3.Text+ "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '"+textBox4.Text+ "', (SELECT idPosition FROM gibddmodern.positions WHERE NamePosition = '"+comboBox1.SelectedItem+ "'), (SELECT idRank FROM gibddmodern.rank WHERE NameRank = '"+comboBox2.SelectedItem+"'), '"+maskedTextBox1.Text+"', '"+maskedTextBox2.Text+ "', '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', DATE_ADD('" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR), '"+textBox8.Text+"', '"+maskedTextBox3.Text+"' );";
                                MySQL(SQL, 20);
                                foreach (int i in Categores)
                                {
                                    SQL = "INSERT INTO gibddmodern.categotyemployee VALUES('" + Count + "', '" + i + "')";
                                    MySQL(SQL, 20);
                                }
                                Cleaner();
                                MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            SQL = "INSERT INTO gibddmodern.employees(`idEmployee`, `Surname`, `Name`, `MiddleName`, `DateOfBirth`, `Sex`, `idPosition`, `idRank`, `PassportData`, `Address`, `Number`) VALUES ('" + Count + "', '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + textBox4.Text + "', (SELECT idPosition FROM gibddmodern.positions WHERE NamePosition = '" + comboBox1.SelectedItem + "'), (SELECT idRank FROM gibddmodern.rank WHERE NameRank = '" + comboBox2.SelectedItem + "'), '" + maskedTextBox1.Text + "', '" + textBox8.Text + "', '" + maskedTextBox3.Text + "' );";
                            MySQL(SQL, 20);
                            Cleaner();
                            MessageBox.Show("Запись успешно добавлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            if (radioButton14.Checked) //Кнопка ВПЕРЕД для просмотра сотрудников
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
            if (radioButton12.Checked) //Кнопка УДАЛИТЬ для удаления сотрудников
            {
                DialogResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SQL = "SELECT idEmployee AS IdDriver FROM gibddmodern.employees WHERE PassportData = '" + maskedTextBox1.Text + "';";
                    MySQL(SQL, 3);
                    SQL = "DELETE FROM gibddmodern.categotyemployee WHERE IdEmployee = '" + IDD + "';";
                    MySQL(SQL, 20);
                    SQL = "DELETE FROM gibddmodern.employees WHERE idEmployee = '" + IDD + "';";
                    MySQL(SQL, 20);
                    MessageBox.Show("Запись успешно удалена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cleaner();
                }
            }
            if (radioButton13.Checked) //Кнопка Изменить для изменения сотрудников
            {
                if (check == true)
                {
                    bool Check = Checker();
                    if (Check == true)
                    {
                        if (maskedTextBox2.MaskCompleted)
                        {
                            ADD_Categores();
                            SQL = "DELETE FROM gibddmodern.categotyemployee WHERE IdEmployee = '" + IDC + "';";
                            MySQL(SQL, 20);
                            SQL = "UPDATE gibddmodern.employees SET Surname = '" + textBox1.Text + "', Name = '" + textBox2.Text + "' , MiddleName =  '" + textBox3.Text + "', DateOfBirth = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', Sex = '" + textBox4.Text + "', idPosition = (SELECT idPosition FROM gibddmodern.positions WHERE NamePosition = '" + comboBox1.SelectedItem + "'), idRank = (SELECT idRank FROM gibddmodern.rank WHERE NameRank = '" + comboBox2.SelectedItem + "'), PassportData = '" + maskedTextBox1.Text + "', DriversLicenseNumber = '" + maskedTextBox2.Text + "', DateofIssueLicense = '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', EndDateLicense = DATE_ADD('" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "',INTERVAL 1 YEAR), Address = '" + textBox8.Text + "', Number ='" + maskedTextBox3.Text + "' WHERE idEmployee = '" + IDC + "';";
                            MySQL(SQL, 20);
                            foreach (int i in Categores)
                            {
                                SQL = "INSERT INTO gibddmodern.categotyemployee VALUES('" + IDC + "', '" + i + "')";
                                MySQL(SQL, 20);
                            }
                            Cleaner();
                            check = false;
                            MessageBox.Show("Запись успешно обновлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            SQL = "UPDATE gibddmodern.employees SET Surname = '" + textBox1.Text + "', Name = '" + textBox2.Text + "' , MiddleName =  '" + textBox3.Text + "', DateOfBirth = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', Sex = '" + textBox4.Text + "', idPosition = (SELECT idPosition FROM gibddmodern.positions WHERE NamePosition = '" + comboBox1.SelectedItem + "'), idRank = (SELECT idRank FROM gibddmodern.rank WHERE NameRank = '" + comboBox2.SelectedItem + "'), PassportData = '" + maskedTextBox1.Text + "', Address = '" + textBox8.Text + "', Number ='" + maskedTextBox3.Text + "' WHERE idEmployee = '" + IDC + "';";
                            MySQL(SQL, 20);
                            Cleaner();
                            check = false;
                            MessageBox.Show("Запись успешно обновлена", "Удачно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //Кнопка НАЗАД для просмотра автомобилей в гараже
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
            button2.Show();
            button3.Hide();
            label11.Hide();
            label14.Hide();
            textBox7.Hide();
            dateTimePicker3.Hide();
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            button2.Text = "Добавить";
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = false;
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox7, textBox8 };
            for (int i = 0; i < 6; i++) textBoxes[i].Enabled = true;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3 };
            for (int i = 0; i < 3; i++) maskedTextBoxes[i].Enabled = true;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        { //Переключатель для просмотра автомобилей в гараже
            Cleaner();
            Counter = 0;
            check = true;
            panel8.Hide();
            button2.Show();
            button3.Show();
            label11.Show();
            label14.Show();
            textBox7.Show();
            dateTimePicker3.Show();
            button2.Text = "Вперед";
            button3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox7, textBox8 };
            for (int i = 0; i < 6; i++) textBoxes[i].Enabled = false;
            DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3 };
            for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3 };
            for (int i = 0; i < 3; i++) maskedTextBoxes[i].Enabled = false;
            SQL = "SELECT COUNT(*) FROM gibddmodern.drivers;";
            MySQL(SQL, 0);
            Counter++;
            SQL = "SELECT Surname, gibddmodern.employees.Name, MiddleName, DateofBirth, Sex, NamePosition, NameRank, PassportData, DriversLicenseNumber, DateofIssueLicense, EndDateLicense, Address, gibddmodern.employees.Number   FROM(gibddmodern.employees INNER JOIN gibddmodern.rank ON gibddmodern.employees.idRank = gibddmodern.rank.idRank) INNER JOIN gibddmodern.positions ON gibddmodern.employees.idPosition = gibddmodern.positions.idPosition WHERE gibddmodern.employees.idEmployee = " + Counter + "; ";
            MySQL(SQL, 1);
            SQL = "SELECT gibddmodern.category.Name AS Category FROM(gibddmodern.categotyemployee INNER JOIN gibddmodern.category ON gibddmodern.categotyemployee.idCategory = gibddmodern.category.idCategory) INNER JOIN gibddmodern.employees ON gibddmodern.categotyemployee.IdEmployee = gibddmodern.employees.idEmployee WHERE gibddmodern.employees.idEmployee = " + Counter + ";";
            MySQL(SQL, 2);
            if (Count == 1) button2.Enabled = false;
            if (Count > 1) button2.Enabled = true;

        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            IZDEL();
            Cleaner();
            panel8.Hide();
            button2.Show();
            button3.Hide();
            label11.Show();
            label14.Hide();
            textBox7.Hide();
            dateTimePicker3.Show();
            button2.Text = "Удалить";
            RadioButton radio = sender as RadioButton;
            if (radio != null) if (radio.Checked) MessageBox.Show("Заполните ФИО и Номер паспорта для поиск нужной записи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e) 
        {
            IZDEL();
            Cleaner();
            panel8.Hide();
            check = false;
            button2.Show();
            button3.Hide();
            label11.Hide();
            label14.Hide();
            textBox7.Hide();
            dateTimePicker3.Hide();
            button2.Text = "Изменить";
            RadioButton radio = sender as RadioButton;
            if (radio != null) if (radio.Checked) MessageBox.Show("Заполните ФИО и Номер паспорта для поиск нужной записи", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked || radioButton13.Checked) //Переключать для поиска сотрудников с помощью фамилии, имени, отчеству и паспортным данным при удалении
            {
                if (maskedTextBox1.MaskCompleted)
                {
                    if (textBox1.Text != string.Empty & textBox2.Text != string.Empty & textBox3.Text != string.Empty)
                    {
                        SQL = "SELECT DateOfBirth, Sex, NameRank, NamePosition, DriversLicenseNumber, DateofIssueLicense, EndDateLicense, Address, gibddmodern.employees.Number FROM(gibddmodern.employees INNER JOIN gibddmodern.positions ON gibddmodern.employees.idPosition = gibddmodern.positions.idPosition) INNER JOIN gibddmodern.rank ON gibddmodern.employees.idRank = gibddmodern.rank.idRank WHERE Surname = '" + textBox1.Text + "' AND Name = '" + textBox2.Text + "' AND MiddleName = '" + textBox3.Text + "' AND PassportData = '" + maskedTextBox1.Text + "';";
                        MySQL(SQL, 1);
                        button2.Enabled = true;
                    }
                    else MessageBox.Show("Некоторое поле не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked || radioButton13.Checked)
            {
                if (maskedTextBox2.MaskCompleted)
                {
                    dateTimePicker2.Enabled = true;
                    panel8.Show();
                }
                else
                {
                    dateTimePicker2.Enabled = false;
                    panel8.Hide();
                }
            }
        }



        private void comboBox2_TextChanged(object sender, EventArgs e)
        { //Поиск категорий сотрудника при изменении его записи и запись их в checkbox
            if (check == false) 
            {
                if (radioButton13.Checked)
                {
                    if (comboBox2.Text.Length > 3)
                    {
                        DialogResult result = MessageBox.Show("Это тот сотрудник, которого вы искали?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            SQL = "SELECT COUNT(*) FROM gibddmodern.categotyemployee INNER JOIN gibddmodern.employees ON gibddmodern.categotyemployee.IdEmployee = gibddmodern.employees.idEmployee WHERE PassportData = '" + maskedTextBox1.Text + "';";
                            MySQL(SQL, 0);
                            if (Count != 0)
                            {
                                Categores.Clear();
                                SQL = "SELECT gibddmodern.categotyemployee.idCategory AS Category FROM gibddmodern.categotyemployee INNER JOIN gibddmodern.employees ON gibddmodern.categotyemployee.IdEmployee = gibddmodern.employees.idEmployee WHERE gibddmodern.employees.PassportData = '" + maskedTextBox1.Text + "';";
                                MySQL(SQL, 4);
                                panel8.Show();
                                CheckBox[] checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16 };
                                for (int j = 0; j < Categores.Count; j++) for (int i = 0; i < 16; i++) if (Categores[j] == (i + 1)) { checkBoxes[i].Checked = true; checkBoxes[i].Enabled = false; break; }
                                TRUE();
                            }
                            else TRUE();
                        }
                    }
                }
            }
        }

        private void TRUE() 
        {
            check = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox7, textBox8 };
            for (int i = 0; i < 6; i++) textBoxes[i].Enabled = true;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3 };
            for (int i = 0; i < 3; i++) maskedTextBoxes[i].Enabled = true;
            SQL = "SELECT idEmployee AS IdDriver FROM gibddmodern.employees WHERE PassportData = '" + maskedTextBox1.Text + "';";
            MySQL(SQL, 3);
        }

        public void IZDEL()
        {
            button2.Enabled = true;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            maskedTextBox1.Enabled = true;
            maskedTextBox2.Enabled = false;
            maskedTextBox3.Enabled = false;
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3 };
            for (int i = 0; i < 3; i++) textBoxes[i].Enabled = true;
            TextBox[] textBoxes1 = new TextBox[] { textBox4, textBox8 };
            for (int i = 0; i < 2; i++) textBoxes1[i].Enabled = false;
            DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3 };
            for (int i = 0; i < 3; i++) dateTimePickers[i].Enabled = false;
        }

        private bool Checker() //Метод для проверки заполненности полей
        {
            if (textBox1.Text != string.Empty)
            {
                if (textBox2.Text != string.Empty)
                {
                    if (textBox3.Text != string.Empty)
                    {
                        if (textBox4.Text != string.Empty)
                        {
                            if (comboBox1.Text != string.Empty)
                            {
                                if (comboBox2.Text != string.Empty)
                                {
                                    if (maskedTextBox1.MaskCompleted)
                                    {
                                        if (maskedTextBox3.MaskCompleted)
                                        {
                                            if (textBox8.Text != string.Empty)
                                            {
                                                return true;
                                            }
                                            else { MessageBox.Show("Не заполнено поле Адрес", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                                        }
                                        else { MessageBox.Show("Не заполнено поле Телефон", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                                    }
                                    else { MessageBox.Show("Не заполнено поле Паспорт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                                }
                                else { MessageBox.Show("Не выбрано Звание", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                            }
                            else { MessageBox.Show("Не выбрана Позиция", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                        }
                        else { MessageBox.Show("Не заполнено поле Пол", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                    }
                    else { MessageBox.Show("Не заполнено поле Отчество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                }
                else { MessageBox.Show("Не заполнено поле Имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            }
            else { MessageBox.Show("Не заполнено поле Фамилия", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
        }

        private void Cleaner() //Метод для очистки всех полей
        {
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            CheckBox[] checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16 };
            for (int i = 0; i < 16; i++) { checkBoxes[i].Checked = false; checkBoxes[i].Enabled = true; }
            TextBox[] textBoxes = new TextBox[] {textBox1, textBox2, textBox3, textBox4, textBox7, textBox8 };
            for (int i = 0; i < 6; i++) textBoxes[i].Text = string.Empty;
            MaskedTextBox[] maskedTextBoxes = new MaskedTextBox[] { maskedTextBox1, maskedTextBox2, maskedTextBox3};
            for (int i = 0; i < 3; i++) maskedTextBoxes[i].Text = string.Empty;
            DateTimePicker[] dateTimePickers = new DateTimePicker[] { dateTimePicker1, dateTimePicker2, dateTimePicker3};
            for (int i = 0; i < 3; i++) dateTimePickers[i].Text = string.Empty;
        }

        private void ADD_Categores() //Метод для добавления категорий сотрудника
        {
            Categores.Clear();
            CheckBox[] checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16 };
            for (int i = 0; i < 16; i++) if (checkBoxes[i].Checked) Categores.Add(i + 1);
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
                    while (reader.Read())
                    {
                        if(radioButton14.Checked)
                        {
                            textBox1.Text = reader["Surname"].ToString();
                            textBox2.Text = reader["Name"].ToString();
                            textBox3.Text = reader["MiddleName"].ToString();
                            maskedTextBox1.Text = reader["PassportData"].ToString();
                        }
                        dateTimePicker1.Value = Convert.ToDateTime(reader["DateofBirth"]);
                        textBox4.Text = reader["Sex"].ToString();
                        comboBox1.Text = reader["NamePosition"].ToString();
                        comboBox2.Text = reader["NameRank"].ToString();
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
                    while (reader.Read()) textBox7.Text += reader["Category"].ToString() + ". ";
                    break;
                case 3:
                    while (reader.Read()) IDD = IDC = Convert.ToInt32(reader["IdDriver"]);
                    break;
                case 4:
                    while (reader.Read()) Categores.Add(Convert.ToInt32(reader["Category"]));
                        break;
            }
        }
    }
}
