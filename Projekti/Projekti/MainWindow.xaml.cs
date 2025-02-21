using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Projekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void kupdate()
        {
            kesken.ItemsSource = null;
            kesken.ItemsSource = klista;
        }
        public void vupdate()
        {
            valmiit.ItemsSource = null;
            valmiit.ItemsSource = vlista;
        }
        List<string> klista = [];
        List<string> vlista = [];
        public MainWindow()
        {
            InitializeComponent();
            StartClock();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lisää_Click(object sender, RoutedEventArgs e)
        {
            var teksti = textbox.Text;
            klista.Add(teksti);
            kupdate();
        }
        private void siirrä_Click(object sender, RoutedEventArgs e)
        {
            if(kesken.SelectedItem != null)
            {
                vlista.Add(kesken.SelectedItem.ToString());
                klista.Remove(kesken.SelectedItem.ToString());
                vupdate();
                kupdate();
            }
            else if (valmiit.SelectedItem != null)
            {
                klista.Add(valmiit.SelectedItem.ToString());
                vlista.Remove(valmiit.SelectedItem.ToString());
                vupdate();
                kupdate();
            }
        }
        private void poista_Click(object sender, RoutedEventArgs e)
        {
            if(valmiit.SelectedItem != null)
            {
                vlista.Remove(valmiit.SelectedItem.ToString());
                vupdate();
            }
                
            else if(kesken.SelectedItem != null)
            {
                klista.Remove(kesken.SelectedItem.ToString());
                kupdate();
            }
        }
        
        

        private DispatcherTimer timer = new DispatcherTimer();

        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                Kello.Content = DateTime.Now.ToString("HH:mm");
                Paiva.Content = DateTime.Now.ToString("dd-MM-yyyy");
            };
            timer.Start();
        }

        private void textbox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
    }
}