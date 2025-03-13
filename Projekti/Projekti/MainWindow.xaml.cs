using System;
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
using System.Windows.Forms;
using System.Text.Json;
using System.IO;






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

            // Create menu items
            MenuItem generalItem = new MenuItem { Header = "General Settings" };
            generalItem.Click += GeneralSettings_Click;

            MenuItem appearanceItem = new MenuItem { Header = "Appearance Settings" };
            appearanceItem.Click += AppearanceSettings_Click;

            MenuItem advancedItem = new MenuItem { Header = "Advanced Settings" };
            advancedItem.Click += AdvancedSettings_Click;

            // Add items to the menu
            settingsMenu.Items.Add(generalItem);
            settingsMenu.Items.Add(appearanceItem);
            settingsMenu.Items.Add(advancedItem);

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
                BalloonTipText = "Nonnii teeppä niitä tehtäviä",
                BalloonTipIcon = ToolTipIcon.Info
            };

            notifyIcon.ShowBalloonTip(5000);
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the menu
            if (settingsMenu.IsOpen)
            {
                settingsMenu.IsOpen = false;
            }
            else
            {
                settingsMenu.PlacementTarget = sender as System.Windows.Controls.Button;
                settingsMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom; // Menu opens downward
                settingsMenu.IsOpen = true;
            }
        }

        private void GeneralSettings_Click(object sender, RoutedEventArgs e)
        {
            
            GeneralSettings generalSetting = new GeneralSettings();
            generalSetting.Show();
        }

        private void AppearanceSettings_Click(object sender, RoutedEventArgs e)
        {
            AppearanceSettings appearancesetting = new AppearanceSettings();
            appearancesetting.Show();
        }

        private void AdvancedSettings_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSettings advancedSetting = new AdvancedSettings();
            advancedSetting.Show();
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
            if (valmiit.SelectedItem != null)
            {
                vlista.Remove((Tehtävä)valmiit.SelectedItem);
                VTallentaja.Tallenna(vlista);
            }

            else if (kesken.SelectedItem != null)
            {
                klista.Remove((Tehtävä)kesken.SelectedItem);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        internal static class VTallentaja
        {
            private static string tiedostonimi = "valmiit.json";
            public static bool Tallenna(ObservableCollection<Tehtävä> vlista)
            {
                try
                {
                    var json_muoto = JsonSerializer.Serialize(vlista);
                    File.WriteAllText(tiedostonimi, json_muoto);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public static ObservableCollection<Tehtävä> LueValmiit()
            {
                try
                {
                    var json_muoto = File.ReadAllText(tiedostonimi);
                    var vlista = JsonSerializer.Deserialize<ObservableCollection<Tehtävä>>(json_muoto);
                    if (vlista == null)
                        return new ObservableCollection<Tehtävä>();
                    return vlista;
                }
                catch
                {
                    return new ObservableCollection<Tehtävä>();
                }

            }
        }
        internal static class KTallentaja
        {
            private static string tiedostonimi = "kesken.json";
            public static bool Tallenna(ObservableCollection<Tehtävä> klista)
            {
                try
                {
                    var json_muoto1 = JsonSerializer.Serialize(klista);
                    File.WriteAllText(tiedostonimi, json_muoto1);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public static ObservableCollection<Tehtävä> LueKesken()
            {
                try
                {
                    var json_muoto1 = File.ReadAllText(tiedostonimi);
                    var klista = JsonSerializer.Deserialize<ObservableCollection<Tehtävä>>(json_muoto1);
                    if (klista == null)
                        return new ObservableCollection<Tehtävä>();
                    return klista;
                }
                catch
                {
                    return new ObservableCollection<Tehtävä>();
                }

            }
        }
    }
}