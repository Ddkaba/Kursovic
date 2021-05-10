using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovic
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        string SQL;
        private void button1_Click(object sender, EventArgs e)
        {
            Form form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            SQL = "SELECT NameBrand, NameModel, Number, BodyNumber, EngineNumber, PassportNumber, ReleaseDate, RegistrationDate, TechnicalInspectionDate, gibddmodern.category.Name AS Category FROM(gibddmodern.garagecc INNER JOIN gibddmodern.carbrands ON gibddmodern.garagecc.IdBrand = gibddmodern.carbrands.idBrand) INNER JOIN gibddmodern.category ON gibddmodern.garagecc.Category = gibddmodern.category.idCategory WHERE idCar = 1; ";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
