using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace Projekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Tehtävä> vlista = new ObservableCollection<Tehtävä>();
        public ObservableCollection<Tehtävä> klista = new ObservableCollection<Tehtävä>();
        private DispatcherTimer countdownTimer;
        private NotifyIcon notifyIcon;

        private ContextMenu settingsMenu;
        
        public MainWindow()
        {
            InitializeComponent();
            StartClock();
            vlista = VTallentaja.LueValmiit();
            valmiit.ItemsSource = vlista;
            klista = KTallentaja.LueKesken();
            kesken.ItemsSource = klista;

            settingsMenu = new ContextMenu();

            System.Windows.Controls.CheckBox Theme = new System.Windows.Controls.CheckBox { Content = "Theme 1" };
            Theme.Checked += Theme_Checked;
            Theme.Unchecked += Theme_UnChecked;

            settingsMenu.Items.Add(Theme);   

            countdownTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            countdownTimer.Tick += CountdownCheck;
            countdownTimer.Start();
        }
        private void notifikaatio()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true,
                BalloonTipTitle = "To-Do list",
                BalloonTipText = "Tehtävän aikaraja on tullut vastaan",
                BalloonTipIcon = ToolTipIcon.Info
            };

            notifyIcon.ShowBalloonTip(5000);
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (settingsMenu.IsOpen)
            {
                settingsMenu.IsOpen = false;
            }
            else
            {
                settingsMenu.PlacementTarget = sender as System.Windows.Controls.Button;
                settingsMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom; 
                settingsMenu.IsOpen = true;
            }
        }
        private void Theme_Checked(object sender, RoutedEventArgs e)
        {
            this.Background = System.Windows.Media.Brushes.DarkGray;
            grid.Background = System.Windows.Media.Brushes.DarkSlateGray;
            kesken.Background = System.Windows.Media.Brushes.DimGray;
            valmiit.Background = System.Windows.Media.Brushes.DimGray;
            kesken.Foreground = System.Windows.Media.Brushes.White;
            valmiit.Foreground = System.Windows.Media.Brushes.White;
            LisääT.Background = System.Windows.Media.Brushes.DimGray;
            LisääT.Foreground = System.Windows.Media.Brushes.White;
            val.Foreground = System.Windows.Media.Brushes.White;
            kes.Foreground = System.Windows.Media.Brushes.White;
            ots.Foreground = System.Windows.Media.Brushes.White;
            Kello.Foreground = System.Windows.Media.Brushes.White;
            Paiva.Foreground = System.Windows.Media.Brushes.White;
            SettingsButton.Background = System.Windows.Media.Brushes.DimGray;
            SettingsButton.Foreground = System.Windows.Media.Brushes.White;
            
        }
        private void Theme_UnChecked(object sender, RoutedEventArgs e)
        {
            this.Background = System.Windows.Media.Brushes.White;
            grid.Background = System.Windows.Media.Brushes.Bisque;
            kesken.Background = System.Windows.Media.Brushes.White;
            valmiit.Background = System.Windows.Media.Brushes.White;
            kesken.Foreground = System.Windows.Media.Brushes.Black;
            valmiit.Foreground = System.Windows.Media.Brushes.Black;
            LisääT.Background = System.Windows.Media.Brushes.White;
            LisääT.Foreground = System.Windows.Media.Brushes.Black;
            val.Foreground = System.Windows.Media.Brushes.Black;
            kes.Foreground = System.Windows.Media.Brushes.Black;
            ots.Foreground = System.Windows.Media.Brushes.Black;
            Kello.Foreground = System.Windows.Media.Brushes.Black;
            Paiva.Foreground = System.Windows.Media.Brushes.Black;
            SettingsButton.Background = System.Windows.Media.Brushes.White;
            SettingsButton.Foreground = System.Windows.Media.Brushes.Black;
        }
        public void lisäätehtävä(string nimi, DateTime? remindertime)
        {
            Tehtävä tehtävä = new Tehtävä(nimi, remindertime);
            klista.Add(tehtävä);
            KTallentaja.Tallenna(klista);
        }
        public class Tehtävä
        {
            public string TaskName { get; set; }
            public DateTime? ReminderTime { get; set; }
            public bool Done { get; set; }
            public Tehtävä(string taskName, DateTime? reminderTime)
            {
                TaskName = taskName;
                ReminderTime = reminderTime;
                Done = false;
            }
            public override string ToString()
            {
                return TaskName;
            }
        }
        private void CountdownCheck(object sender, EventArgs e)
        {
            for (int i = klista.Count - 1; i >= 0; i--)
            {
                if (klista[i].ReminderTime.HasValue)
                {
                    TimeSpan remaining = klista[i].ReminderTime.Value - DateTime.Now;
                    if (remaining.TotalSeconds <= 0 && !klista[i].Done)
                    {
                        notifikaatio();
                        klista[i].Done = true;
                    }
                }
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && checkBox.DataContext is Tehtävä tehtävä)
            {
                vlista.Add(tehtävä);
                klista.Remove(tehtävä);
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
            }     
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && checkBox.DataContext is Tehtävä tehtävä)
            {
                klista.Add(tehtävä);
                vlista.Remove(tehtävä);
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
            }
        }
        private void poista_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button && button.Tag is Tehtävä tehtävä)
            {
                klista.Remove(tehtävä);
                vlista.Remove(tehtävä);
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }
        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button && button.Tag is Tehtävä tehtävä)
            {
                Window2 win2 = new Window2(tehtävä);
                win2.ShowDialog();
                kesken.Items.Refresh();
                valmiit.Items.Refresh();
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
            }
        }
    }
}