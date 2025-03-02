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
        public MainWindow()
        {
            InitializeComponent();
            StartClock();
            vlista = VTallentaja.LueValmiit();
            valmiit.ItemsSource = vlista;
            klista = KTallentaja.LueKesken();
            kesken.ItemsSource = klista;
        }
        private void lisää_Click(object sender, RoutedEventArgs e)
        {
            if(textbox.Text != null && !string.IsNullOrWhiteSpace(textbox.Text))
            {
                klista.Add(textbox.Text);
                textbox.Clear();
                KTallentaja.Tallenna(klista);
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is string tehtävä)
            {
                vlista.Add(tehtävä);
                klista.Remove(tehtävä);
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
            }     
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is string tehtävä)
            {
                klista.Add(tehtävä);
                vlista.Remove(tehtävä);
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
            }
        }
        private void poista_Click(object sender, RoutedEventArgs e)
        {
            if(valmiit.SelectedItem != null)
            {
                vlista.Remove(valmiit.SelectedItem.ToString());
                VTallentaja.Tallenna(vlista);
            }
                
            else if(kesken.SelectedItem != null)
            {
                klista.Remove(kesken.SelectedItem.ToString());
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

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}