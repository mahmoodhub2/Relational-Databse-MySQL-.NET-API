/*
  FILE : MainWindow.xaml.cs
* PROJECT :  Assignment 4#
* PROGRAMMER : Mahmood Al-Zubaidi and Nawriz Ibrahim
* FIRST VERSION : 09/Dec/2021
* DESCRIPTION : The purpose of this function is to demonstrate the use of linking c# with mysql by writing queriies to it and retrieve/edit data in the database.
*/

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace R_D_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string timeItTook = "";

        DispatcherTimer timer;
        TimeSpan time;

       

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }



        // ------------------------------------------------------------
        // Function: Initialize
        // Description: it initlizes the connection attributes with the database's credentials.
        // Parameters: none
        // Returns: none
        // ------------------------------------------------------------

        private void Initialize()
        {
            server = "localhost";
            database = "guessing_game";
            uid = "root";
            password = "@#$%^^mmmIOO/";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        Stopwatch stopWatch = new Stopwatch();

        int c = 1;
        private void Name_Click(object sender, RoutedEventArgs e)
        {
            if(nameInput.Text == "")
            {
                errName.Visibility = Visibility.Visible;
                errName.Text = "Input filed is required";
            }
            else
            {
                errName.Visibility = Visibility.Hidden;
                dbRetriever(c);
            }
            
        }



        int totalScores = 0; // the overall score of the user
        // ------------------------------------------------------------
        // Function: correct_answer_Click
        // Description: it gets executed once the correct-answer's button gets clicked so that it inserts the inpuuted answer and the time it took to answer it to the database.
        // Parameters:  object sender, RoutedEventArgs e
        // Returns: none
        // ------------------------------------------------------------

        private void correct_answer_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            int ansewrScores = 20000;

            stopWatch.Stop();
            timeItTook = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();
            ansewrScores -= Convert.ToInt32(timeItTook);
            ansewrScores = (ansewrScores / 1000) / 2;
            if(ansewrScores < 0)
            {
                ansewrScores = 0;
            }
            totalScores += ansewrScores;
            string query = "insert into selected_answers(Selected_Answer_Question,Selected_Answer,CorrectAnswer,TimeItTook) values('" + Question.Text + "','" + correct_answer.Content + "','" + correct_answer.Content + "','" + timeItTook + "');";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            connection.Open();
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Close();

            c++;
            connection.Close();

            dbRetriever(c);
        }


        // ------------------------------------------------------------
        // Function: wrong_answer_Click
        // Description: it gets executed once the wrong-answer's button gets clicked so that it inserts the inpuuted answer and the time it took to answer it to the database.
        // Parameters: object sender, RoutedEventArgs e
        // Returns: none
        // ------------------------------------------------------------

        private void wrong_answer_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            int ansewrScores = 20000;

            stopWatch.Stop();
            timeItTook = stopWatch.ElapsedMilliseconds.ToString();
            stopWatch.Reset();
            ansewrScores = (ansewrScores / 1000) / 2;
            if (ansewrScores < 0)
            {
                ansewrScores = 0;
            }

            string answer = "";
            if (sender.Equals(potential_answer1)) {
                answer = potential_answer1.Content.ToString();
            }
            else if (sender.Equals(potential_answer2)) {
                answer = potential_answer2.Content.ToString();
            }
            else if (sender.Equals(potential_answer3)) {
                answer = potential_answer3.Content.ToString();
            }

            string query = "insert into selected_answers(Selected_Answer_Question,Selected_Answer,CorrectAnswer,TimeItTook) values('" + Question.Text + "','" + answer + "','" + correct_answer.Content + "','" + timeItTook + "');";
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            connection.Open();
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Close();

            c++;
            connection.Close();

            dbRetriever(c);
        }




        // ------------------------------------------------------------
        // Function: dbRetriever
        // Description: retrives the questions from the database and inserts then into the button's content.
        // Parameters: int i, witch increments each time a question gets clicked so that it goes to the next question in the database.
        // Returns: none.
        // ------------------------------------------------------------
        int stopIndicater = 0;

        private void dbRetriever(int i)
        {
            MySqlCommand cmd = new MySqlCommand("select * from questions_answers where QuestionID ='" + i.ToString() + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            connection.Open();

            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Question.Visibility = Visibility.Visible;
                Question.Text = dr.GetString("Question");

                correct_answer.Visibility = Visibility.Visible;
                potential_answer1.Visibility = Visibility.Visible;
                potential_answer2.Visibility = Visibility.Visible;
                potential_answer3.Visibility = Visibility.Visible;


                correct_answer.Content = dr.GetString("correct_answer");
                potential_answer1.Content = dr.GetString("potential_answer1");
                potential_answer2.Content = dr.GetString("potential_answer2");
                potential_answer3.Content = dr.GetString("potential_answer3");

                time = TimeSpan.FromMilliseconds(20000);

                timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    Timer.Text = time.ToString("c");
                    if (time == TimeSpan.Zero) timer.Stop();
                    time = time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                timer.Start();


                break;
            }
            dr.Close();
            connection.Close();
            if (stopIndicater == 10)
            {
                finalMsg();
            }
            stopIndicater++;
        }




        // ------------------------------------------------------------
        // Function: finalMsg
        // Description: it inserts the total score of the user into the database. And showes the wright answers and the inputted answers and the time it took for the user 
        // to answer these questions in the milleseconds.
        // Parameters: none.
        // Returns: none.
        // ------------------------------------------------------------

        public void finalMsg()
        {
            string query = "insert into users(score, userName) values('" + totalScores + "', '" + nameInput.Text + "'); ";

            MySqlCommand cmdd = new MySqlCommand(query);
            cmdd.CommandType = CommandType.Text;
            cmdd.Connection = connection;
            connection.Open();
            MySqlDataReader dr2;
            dr2 = cmdd.ExecuteReader();
            dr2.Close();
            connection.Close();

            reviewWinners.Visibility = Visibility.Visible;

            Score.Visibility = Visibility.Visible;

            Score.Text = "  your score is " + totalScores.ToString() + "\n\n";

            MySqlCommand cmd = new MySqlCommand("select * from selected_answers order by TimeItTook");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            connection.Open();

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
                foreach(char c in temp)
                {
                    if(c == '\n')
                    {
                        counter++;
                    }
                    if(counter == 10)
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
                    
                    if(counter2 == 1)
                    {
                        winOrLose.Text += "Question\t\t\tInputted Answer\tCorrect Answer\ttime it took\n";
                        winOrLose.Text += "\n" +  holder;
                    }
                    else
                    {
                        winOrLose.Text += holder;
                    }
                    i2++;
                    if(i2 == 10)
                    {
                        break;
                    }
                }
                data_reader.Close();
            }


            connection.Close();
        }

        // ------------------------------------------------------------
        // Function: reviewWinners_Click
        // Description: it hides the current window then reveales the leaderboard's window.
        // Parameters:  object sender, RoutedEventArgs e
        // Returns: none.
        // ------------------------------------------------------------

        private void reviewWinners_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            Window n = new LeaderBoard();
            n.Visibility = Visibility.Visible;
           
        }
    }
}
