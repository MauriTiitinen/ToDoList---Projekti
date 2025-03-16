using System;
using System.Collections;
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
using static Projekti.MainWindow;

namespace Projekti
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private Tehtävä _task;
        public Window2(Tehtävä tehtävä)
        {
            InitializeComponent();
            _task = tehtävä;
            textbox2.Text = tehtävä.TaskName;
            Päivämäärä1.SelectedDate = tehtävä.ReminderTime?.Date;
            if (tehtävä.ReminderTime.HasValue)
            {
                Tunnit.Text = tehtävä.ReminderTime.Value.Hour.ToString("D2");
                Minuutit.Text = tehtävä.ReminderTime.Value.Minute.ToString("D2");
            }
        }
        private void tallenna_Click(object sender, RoutedEventArgs e)
        {
          
            if (Päivämäärä1.SelectedDate.HasValue && !string.IsNullOrEmpty(Tunnit.Text) && !string.IsNullOrEmpty(Minuutit.Text))
            {
                int hour = int.Parse(Tunnit.Text);
                int minute = int.Parse(Minuutit.Text);
                _task.ReminderTime = Päivämäärä1.SelectedDate.Value.AddHours(hour).AddMinutes(minute);
                if (_task.ReminderTime <= DateTime.Now)
                {
                    System.Windows.MessageBox.Show("Valittu aika on jo mennyt!");
                    return;
                }
            }
            else
            {
                _task.ReminderTime = null;
            }

            _task.TaskName = textbox2.Text;
            Close();
        }

        private void Peruuta_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
