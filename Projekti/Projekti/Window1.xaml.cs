using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Threading;


namespace Projekti
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private NotifyIcon notifyIcon;
        
        public string tunnit { get; set; }
        public string minuutit { get; set; }
        public Window1()
        {
            InitializeComponent();
        }

 
        private void aika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            tunnit = (Tunnit.SelectedItem as ComboBoxItem)?.Content.ToString();
            minuutit = (Minuutit.SelectedItem as ComboBoxItem)?.Content.ToString();
        }
        private void lisää1_Click(object sender, RoutedEventArgs e)
        {
            string päivä = Päivämäärä.SelectedDate?.ToString("dd-MM-yyyy");
            string aika = $"{tunnit}:{minuutit}";
            MainWindow main = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (string.IsNullOrWhiteSpace(textbox1.Text))
            {
                System.Windows.MessageBox.Show("Laita jotain kenttii");
                return;
            }
            else if (!string.IsNullOrWhiteSpace(tunnit) && !string.IsNullOrWhiteSpace(minuutit) && päivä != null)
            {
                DateTime selectedDateTime = Päivämäärä.SelectedDate.Value.Date +
                                            TimeSpan.FromHours(int.Parse(tunnit)) +
                                            TimeSpan.FromMinutes(int.Parse(minuutit));

                if (selectedDateTime <= DateTime.Now)
                {
                    System.Windows.MessageBox.Show("Valittu aika on jo mennyt!");
                    return;
                }
                main.lisäätehtävä(textbox1.Text, selectedDateTime);
            }
            else
            {
                main.lisäätehtävä(textbox1.Text, null);
            }
            textbox1.Clear();
        }
    }
}