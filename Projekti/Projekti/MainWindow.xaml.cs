using System.Collections.ObjectModel;
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
        private ObservableCollection<string> vlista = new ObservableCollection<string>();
        private ObservableCollection<string> klista = new ObservableCollection<string>();
        public void kupdate()
        {
            kesken.ItemsSource = klista;
        }
        public void vupdate()
        {
            valmiit.ItemsSource = vlista;
        }
        public MainWindow()
        {
            InitializeComponent();
            StartClock();
            vlista = VTallentaja.LueValmiit();
            valmiit.ItemsSource = vlista;
            klista = KTallentaja.LueKesken();
            kesken.ItemsSource = klista;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lisää_Click(object sender, RoutedEventArgs e)
        {
            var teksti = textbox.Text;
            klista.Add(teksti);
            kupdate();
            KTallentaja.Tallenna(klista);
        }
        private void siirrä_Click(object sender, RoutedEventArgs e)
        {
            if(kesken.SelectedItem != null)
            {
                vlista.Add(kesken.SelectedItem.ToString());
                klista.Remove(kesken.SelectedItem.ToString());
                vupdate();
                kupdate();
                VTallentaja.Tallenna(vlista);
                KTallentaja.Tallenna(klista);
            }
            else if (valmiit.SelectedItem != null)
            {
                klista.Add(valmiit.SelectedItem.ToString());
                vlista.Remove(valmiit.SelectedItem.ToString());
                vupdate();
                kupdate();
                VTallentaja.Tallenna(vlista);
                KTallentaja.Tallenna(klista);
            }
        }
        private void poista_Click(object sender, RoutedEventArgs e)
        {
            if(valmiit.SelectedItem != null)
            {
                vlista.Remove(valmiit.SelectedItem.ToString());
                vupdate();
                VTallentaja.Tallenna(vlista);
            }
                
            else if(kesken.SelectedItem != null)
            {
                klista.Remove(kesken.SelectedItem.ToString());
                kupdate();
                KTallentaja.Tallenna(klista);
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