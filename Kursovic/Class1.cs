using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Kursovic
{
    class Connector
    {

        MySqlConnection connection = null;
        MySqlDataReader reader = null;
        public void SQL(string sql)
        {
            string str = "server=localhost;user=root;password=2506Russia5002;database=gibddmodern;port=3306";
            connection = new MySqlConnection(str);
            connection.Open();
            try
            {
                MySqlCommand mySql = new MySqlCommand(sql, connection);
                reader = mySql.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public MySqlDataReader reader1()
        {
            return reader;
        }
    }
}

