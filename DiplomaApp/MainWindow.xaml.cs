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

            var result = dlg.ShowDialog();

            if (result == true)
            {
                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                pdf.LoadFromFile(dlg.FileName);
                Console.WriteLine(dlg.FileName);

                Image bmp = pdf.SaveAsImage(0);
                bmp.Save("image.bmp", ImageFormat.Bmp);
                DiplomaBackground.Source = new BitmapImage(new System.Uri(@"C:\Users\Robert\Documents\Visual Studio 2015\Projects\DiplomaApp\DiplomaApp\bin\Debug\image.bmp"));

            }





            //DiplomaBackground.Source = new BitmapImage("convertToBmp.bmp");







            //PdfDocument pdf = new PdfDocument();
            //pdf.Info.Title = "My First PDF";
            //PdfPage pdfPage = pdf.AddPage();
            //XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            //// opening external pdf
            //XPdfForm pageFromExternalFile = XPdfForm.FromFile("1_1_2f.pdf");
            //graph.DrawImage(pageFromExternalFile, new XRect(0, 0, pageFromExternalFile.PointWidth, pageFromExternalFile.PointHeight));

            //// adding text on this page
            //XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            //graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
            //string pdfFilename = "firstpage.pdf";
            //pdf.Save(pdfFilename);
            //Process.Start(pdfFilename);

            // WebBrowser1.Navigate(new System.Uri(@"c:\firstpage.pdf"));

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

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(page);
            document.Options.NoCompression = true;
            document.Options.ColorMode = PdfColorMode.Cmyk;
            document.Version = 14;
            page.Size = PdfSharp.PageSize.A4;



            XPdfForm pageFromExternalFile = XPdfForm.FromFile("uczestnictwo.pdf");
            graph.DrawImage(pageFromExternalFile, new XRect(0, 0, pageFromExternalFile.PointWidth, pageFromExternalFile.PointHeight));

            //// adding text on this page
            XFont font = new XFont("Roboto Condensed Light", 20);
            graph.DrawString("This is my first PDF document  blalalalalala alalala alalalala alalq", font, XBrushes.Black, page.Width.Point / 2, page.Height.Point * posY, XStringFormats.Center);




            //string pdfFilename = "firstpage.pdf";
            //document.Save(pdfFilename);
            //Process.Start(pdfFilename);

            string fileName = DateTime.Now.ToString("yyMMddhhmmss") + ".pdf";
            document.Save(fileName);





        }

        private void ConnectWithDB_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                // łączenie z bazą
                // bom
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
            finally
            {

            }

        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            Window1 secondWindow = new Window1();
            secondWindow.ShowDialog();
        }
    }
}
