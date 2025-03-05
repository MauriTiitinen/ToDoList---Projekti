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
        public Window1()
        {
            InitializeComponent();
            Tunnit.SelectedIndex = 0;
            Minuutit.SelectedIndex = 0;
        }
        private void aika_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string tunnit = (Tunnit.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            string minuutit = (Minuutit.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
        }
        private void lisää1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            main.lisäätehtävä(textbox1.Text);
            textbox1.Clear();
        }
    }
}
