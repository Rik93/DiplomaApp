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


                Globals.database = new DatabaseClass(dbName.Text, serverName.Text, userName.Text, hiddenPassword.Password);
                 
                Console.WriteLine(DatabaseClass.counter); //ile obiektów klasy DatabaseClass


                MessageBox.Show("Poprawnie połączono z bazą.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

            }
            catch(MySqlException Ex)
            {
                MessageBox.Show("Błąd połączenia. \n Komunikat błędu: " + Ex, "Błąd");
            }
            finally
            {

            }
        }
    }
}
