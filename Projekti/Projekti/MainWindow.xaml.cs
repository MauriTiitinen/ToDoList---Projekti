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






namespace Projekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> vlista = new ObservableCollection<string>();
        public ObservableCollection<string> klista = new ObservableCollection<string>();

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
    
        public void lisäätehtävä(string tehtävät)
        {
            klista.Add(tehtävät);
            KTallentaja.Tallenna(klista);
            
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && checkBox.DataContext is string tehtävä)
            {
                vlista.Add(tehtävä);
                klista.Remove(tehtävä);
                KTallentaja.Tallenna(klista);
                VTallentaja.Tallenna(vlista);
            }     
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox && checkBox.DataContext is string tehtävä)
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
                vlista.Remove(valmiit.SelectedItem.ToString());
                VTallentaja.Tallenna(vlista);
            }

            else if (kesken.SelectedItem != null)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void ShowNotification()
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
        

        private void ilmoitus_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification();
        }
    }
}