using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace R_D_Game
{
    /// <summary>
    /// Interaction logic for LeaderBoard.xaml
    /// </summary>
    public partial class LeaderBoard : Window
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        // ------------------------------------------------------------
        // Type: Instructor
        // Description: initlizes the connection's attributes with the database's credentials, and retrieves the player's in a decending order.
        // Parameters: none.
        // Returns: none.
        // ------------------------------------------------------------

        public LeaderBoard()
        {

            InitializeComponent();



            server = "localhost";
            database = "guessing_game";
            uid = "root";
            password = "@#$%^^mmmIOO/";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);

            string query = "select * from users order by score desc";


            showWinners.Text += "User's ID\t\tUser's Score\tUserName\n";


            MySqlCommand cmd = new MySqlCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            connection.Open();
            MySqlDataReader dr2;
            dr2 = cmd.ExecuteReader();
            dr2.Close();


            string temp = "";
            string temp2 = "";
            var data_reader = cmd.ExecuteReader();
            if (data_reader.HasRows)
            {
                int count = data_reader.FieldCount;
                while (data_reader.Read())
                {
                    for (var i = 0; i < count; i++)
                    {
                        temp2 += data_reader.GetValue(i) + "\t" + "\t";
                    }

                    temp2 += "\n";
                }
                temp = temp2;

                int counter = 0;
                string[] strArr = new string[10];
                foreach (char c in temp)
                {
                    if (c == '\n')
                    {
                        counter++;
                    }
                    if (counter == 10)
                    {
                        break;
                    }
                    else
                    {
                        strArr[counter] += c;
                    }

                }


                string holder = "";
                int i2 = 0;
                int counter2 = 0;
                while (i2 < strArr.Length)
                {
                    counter2++;
                    holder = strArr[i2];

                    if (counter2 == 1)
                    {
                        showWinners.Text += "\n" + /*counter2.ToString() + "\t" +*/ holder;
                    }
                    else
                    {
                        showWinners.Text += "\n" + /*counter2.ToString() +*/ holder;
                    }
                    i2++;
                    if (i2 == 10)
                    {
                        break;
                    }
                }
                data_reader.Close();
            }


            connection.Close();


        }
    }
}
