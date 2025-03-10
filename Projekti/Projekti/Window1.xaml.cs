using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace Projekti
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
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
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (string.IsNullOrWhiteSpace(tunnit) && string.IsNullOrWhiteSpace(minuutit) && päivä == null)
                main.lisäätehtävä(textbox1.Text);
            else if (string.IsNullOrWhiteSpace(textbox1.Text) || string.IsNullOrWhiteSpace(tunnit) || string.IsNullOrWhiteSpace(minuutit) || päivä == null)
            {
                MessageBox.Show("Kirjoita tehtävä ja valitse päivämäärä ja aika tai jätä päivämäärä ja aika tyhjäksi");
                return;
            }
            else
                main.lisäätehtävä(textbox1.Text + "   " + "Muistutus:  " + päivä + " " + aika);
            textbox1.Clear();
        }
    }
}