using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace DiplomaApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            hiddenPassword.Visibility = Visibility.Visible;
            visiblePassword.Visibility = Visibility.Hidden;
            hiddenPassword.Password = visiblePassword.Text;
        }
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            hiddenPassword.Visibility = Visibility.Hidden;
            visiblePassword.Visibility = Visibility.Visible;
            visiblePassword.Text = hiddenPassword.Password;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = "Server=" + serverName.Text +
                    ";Database=" + dbName.Text +
                    ";Uid=" + userName.Text +
                    ";Pwd=" + hiddenPassword.Password + ";";

                Console.WriteLine(connectionString);

                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM ttr_dyplomy WHERE lastname = \"Troka\"";

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr.GetString(0) + ": "
                        + rdr.GetString(1) + " " + rdr.GetString(2));
                }



                conn.Close();
            }
            catch
            {
                MessageBox.Show("Błąd połączenia", "Błąd");
            }
            finally
            {

            }
        }
    }
}
