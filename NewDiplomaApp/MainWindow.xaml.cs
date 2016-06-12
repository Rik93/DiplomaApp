using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using MySql.Data.MySqlClient;


using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

namespace DiplomaApp
{



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private System.Windows.Point previousMouseLocation;
        private System.Windows.Point previousLabelLocation;
        private bool isMouseDown;
        private bool isPathChosen = false;
        private bool isPDFloaded = false;
        public string pathToBackground;
        public string pathToGeneratedPDF;
        public DatabaseClass database;
        private System.Windows.Point newLabelPosition;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Files (*.pdf)|*.pdf";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                pathToBackground = dlg.FileName;
                pdf.LoadFromFile(pathToBackground);

                try
                {
                    //zapisanie strony pdf jako image
                    Image bmp = pdf.SaveAsImage(0);

                    //konwersja z Image na BitmapImage
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, ImageFormat.Bmp);

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.EndInit();

                    //ustawienie tła
                    DiplomaBackground.Source = image;

                    //w przypadku poprawnego wczytania pliku i poprawnego wykonania działań na nim
                    Description.Visibility = Visibility.Hidden;
                    ParticipantNameLabel.Visibility = Visibility.Visible;
                    TextPathToPDF.Text = pathToBackground;
                    LabelPathToPDF.Visibility = Visibility.Visible;
                    TextPathToPDF.Visibility = Visibility.Visible;
                    checkBoxBackground.IsChecked = true;
                    isPDFloaded = true;
                    if (isPathChosen && Globals.database != null && Globals.database.isConnected()) GeneratePDFs.IsEnabled = true;
                    MessageBox.Show("Poprawnie wczytano obraz.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (OutOfMemoryException ex)
                {
                    MessageBox.Show("Za mało pamięci, aby kontynuować. \nKomunikat błędu: " + ex, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("Nie znaleziono pliku. \nKomunikat błędu: " + ex, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }




        }

        void MouseUpHandler(Object sender, RoutedEventArgs args)
        {

            Console.WriteLine("cos");
            // This method is called whenever the PreviewMouseUp event fires.
        }

        void MouseDownHandler(Object sender, RoutedEventArgs args)
        {
            Console.WriteLine("csf");
            // This method is called whenever the PreviewMouseDown event fires.
        }

        private void Label_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Label_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Label_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point p = Mouse.GetPosition(this);
            if (isMouseDown)
            {
                this.Cursor = Cursors.Hand;
                newLabelPosition.X = previousLabelLocation.X + (p.X - previousMouseLocation.X);
                newLabelPosition.Y = previousLabelLocation.Y + (p.Y - previousMouseLocation.Y);

                //ograniczenia z lewej i prawej strony              
                if (newLabelPosition.X < DiplomaBackground.Margin.Left)
                {
                    newLabelPosition.X = DiplomaBackground.Margin.Left;
                    Console.WriteLine("lewa");
                }
                else if (newLabelPosition.X + ParticipantNameLabel.ActualWidth >= DiplomaBackground.Margin.Left + DiplomaBackground.Width)
                {
                    newLabelPosition.X = DiplomaBackground.Margin.Left + DiplomaBackground.Width - ParticipantNameLabel.ActualWidth;
                    Console.WriteLine("prawa");
                }


                //ograniczenia z góry i dołu
                if (newLabelPosition.Y < DiplomaBackground.Margin.Top)
                {
                    newLabelPosition.Y = DiplomaBackground.Margin.Top;
                    Console.WriteLine("góra");
                }
                else if (newLabelPosition.Y + ParticipantNameLabel.ActualHeight >= DiplomaBackground.Margin.Top + DiplomaBackground.Height)
                {
                    newLabelPosition.Y = DiplomaBackground.Margin.Top + DiplomaBackground.Height - ParticipantNameLabel.ActualHeight;
                    Console.WriteLine("dół!");
                }

                ParticipantNameLabel.Margin = new Thickness(newLabelPosition.X, newLabelPosition.Y, 0, 0);

                previousMouseLocation = p;
                previousLabelLocation = new System.Windows.Point(ParticipantNameLabel.Margin.Left, ParticipantNameLabel.Margin.Top);

                double posY = (newLabelPosition.Y - DiplomaBackground.Margin.Top) / DiplomaBackground.Height;
                double posX = (newLabelPosition.X - DiplomaBackground.Margin.Left) / DiplomaBackground.Width;

                Console.WriteLine("X: {0}, Y: {1}", posX, posY);

            }

        }

        private void ParticipantNameLabel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            Console.WriteLine("Mouse DOWN!");

            previousLabelLocation = new System.Windows.Point(ParticipantNameLabel.Margin.Left, ParticipantNameLabel.Margin.Top);
            previousMouseLocation = Mouse.GetPosition(this);
        }

        private void ParticipantNameLabel_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            this.Cursor = Cursors.Arrow;
            Console.WriteLine("Mouse UP!");
        }



        private void ParticipantNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Cursor = Cursors.Arrow;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            double posY = (newLabelPosition.Y + (ParticipantNameLabel.ActualHeight / 2) - DiplomaBackground.Margin.Top) / DiplomaBackground.Height;
            double posX = (newLabelPosition.X - DiplomaBackground.Margin.Left) / DiplomaBackground.Width;

            Globals.database.selectFromDatabase();

            while (Globals.database.rdr.Read())
            {

                string firstName = Globals.database.rdr.GetString(0);
                string lastName = Globals.database.rdr.GetString(1);
                string email = Globals.database.rdr.GetString(2);

                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(page);
                document.Options.NoCompression = true;
                document.Options.ColorMode = PdfColorMode.Cmyk;
                document.Version = 14;
                page.Size = PdfSharp.PageSize.A4;

                XPdfForm pageFromExternalFile = XPdfForm.FromFile(pathToBackground);
                graph.DrawImage(pageFromExternalFile, new XRect(0, 0, pageFromExternalFile.PointWidth, pageFromExternalFile.PointHeight));

                // dodanie tekstu w PDFie
                XFont font = new XFont("Roboto Condensed Light", 40, XFontStyle.Bold);
                graph.DrawString(firstName + " " + lastName, font, XBrushes.Black, page.Width.Point / 2, page.Height.Point * posY, XStringFormats.Center);


                string fileName = pathToGeneratedPDF + "\\" + firstName + lastName + ".pdf";
                document.Save(fileName);

            }
            Globals.database.rdr.Close();



        }

        private void ConnectWithDB_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // łączenie z bazą
                string connectionString = "Server=ttr.skalppg.pl;Database=skalp_ttr;Uid=skalp_dyplomy;Pwd=pK2FOJsE;";
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

        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {

            Window1 secondWindow = new Window1();
            secondWindow.ShowDialog();


            if (Globals.database != null && Globals.database.isConnected()) checkBoxDb.IsChecked = true;
            if (Globals.database != null && Globals.database.isConnected() && isPathChosen && isPDFloaded) GeneratePDFs.IsEnabled = true;
        }

        private void buttonPath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            pathToGeneratedPDF = dialog.SelectedPath;
            checkBoxPath.IsChecked = true;
            isPathChosen = true;
            Console.WriteLine(Globals.database.isConnected());
            if (isPathChosen && Globals.database != null  && Globals.database.isConnected()) GeneratePDFs.IsEnabled = true;
        }

       
    }
}
