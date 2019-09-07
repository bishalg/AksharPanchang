using System;
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
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using AksharPanchang.Viewmodel;
using AksharPanchang.Config;
using AksharPanchang.Properties;
using AksharPanchang.ModelObjects;
using AksharPanchang.Constants;

namespace AksharPanchang
{
    /// <summary>
    /// Interaction logic for PlaceAndTzWindow.xaml
    /// </summary>
    public sealed partial class PlaceAndTzWindow : Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;

        const uint SC_CLOSE = 0xF060;

        const int WM_SHOWWINDOW = 0x00000018;
        const int WM_CLOSE = 0x10;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.hwndSourceHook));
            }
        }


        IntPtr hwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SHOWWINDOW)
            {
                IntPtr hMenu = GetSystemMenu(hwnd, false);
                if (hMenu != IntPtr.Zero)
                {
                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
            else if (msg == WM_CLOSE)
            {
                handled = true;
            }
            return IntPtr.Zero;
        }

        
        public PlaceAndTzWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewObj.Instance.AllTimeZones;
            this.TimeZoneCombo.SelectedItem = MainWindowViewObj.Instance.TimeZone;
            PropertiesManipulator propertiesManipulator = new PropertiesManipulator(ApplicationManager.Instance.SETTINGS_FILE);
            string longitude  = propertiesManipulator.get(ApplicationManager.Instance.PLACE_LONG);
            string latitude = propertiesManipulator.get(ApplicationManager.Instance.PLACE_LAT);
            string monthEndOption = propertiesManipulator.get(ApplicationManager.Instance.MONTH_END_OPTION);
            if (monthEndOption.Equals("P"))
                this.PurnimantRadioBtn.IsChecked = true;
            if (monthEndOption.Equals("A"))
                this.AmantRadioBtn.IsChecked = true;
            PlaceLongTxt.Text = longitude;
            PlaceLatTxt.Text = latitude;
            this.AyanamsaCombo.DataContext = MainWindowViewObj.Instance;
            //this.AyanamsaCombo.ItemsSource = MainWindowViewObj.Instance.AyanamsaList;
            this.AyanamsaCombo.SelectedValue = AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa);
        }
        private static readonly PlaceAndTzWindow instance = new PlaceAndTzWindow();
        public static PlaceAndTzWindow Instance
        {
            get
            {
                return instance;
            }
        }

        private void savePlaceAndTz(object sender, RoutedEventArgs e)
        {
            if (ValidatePlace())
            {

                if (this.PurnimantRadioBtn.IsChecked == true)
                    MainWindowViewObj.Instance.MonthEndOption = "P";
                if (this.AmantRadioBtn.IsChecked == true)
                    MainWindowViewObj.Instance.MonthEndOption = "A";

                PropertiesManipulator propertiesManipulator = new PropertiesManipulator(ApplicationManager.Instance.SETTINGS_FILE);
                propertiesManipulator.set(ApplicationManager.Instance.PLACE_LONG, PlaceLongTxt.Text);
                propertiesManipulator.set(ApplicationManager.Instance.PLACE_LAT, PlaceLatTxt.Text);
                propertiesManipulator.set(ApplicationManager.Instance.MONTH_END_OPTION, MainWindowViewObj.Instance.MonthEndOption.ToString());
               

                propertiesManipulator.set(ApplicationManager.Instance.TIMEZONE_ID, ((TimeZoneInfo)TimeZoneCombo.SelectedItem).Id);
                double longitude = Convert.ToDouble(propertiesManipulator.get(ApplicationManager.Instance.PLACE_LONG));
                double latitude = Convert.ToDouble(propertiesManipulator.get(ApplicationManager.Instance.PLACE_LAT));
                MainWindowViewObj.Instance.Place = new Place(longitude, latitude);
                MainWindowViewObj.Instance.TimeZone = (TimeZoneInfo)TimeZoneCombo.SelectedItem;

                propertiesManipulator.set(ApplicationManager.Instance.AYANAMSA_ID, ((Ayanamsa)(AyanamsaCombo.SelectedItem)).Id);
                MainWindowViewObj.Instance.SelectedAyanamsa = ((Ayanamsa)(AyanamsaCombo.SelectedItem)).Name;
                propertiesManipulator.Save();
                this.Hide();                
            }
        }
		
		//Sudarshana – Handsome Lord
		
        public bool ValidatePlace()
        {
            string longitude = this.PlaceLongTxt.Text;
            string latitude = this.PlaceLatTxt.Text;
            double longitudeDbl;
            double latitudeDbl;
            bool isDoubleLongitude = double.TryParse(longitude, out longitudeDbl);
            bool isDoubleLatitude = double.TryParse(latitude, out latitudeDbl);
            bool validLogitude = false;
            if (isDoubleLongitude)
            {
                if (longitudeDbl > -90.0 && longitudeDbl < 90.0)
                    validLogitude = true;
                else
                {
                    MessageBox.Show("Invalid longitude!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid longitude!");
                return false;
            }
            if (validLogitude)
            {
                if (isDoubleLatitude)
                {
                    if (latitudeDbl > -180.0 && latitudeDbl < 180)
                        return true;
                    else
                    {
                        MessageBox.Show("Invalid latitude!");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid latitude!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid longitude!");
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void SelectPurnimant(object sender, RoutedEventArgs e)
        {
            MainWindowViewObj.Instance.MonthEndOption = "P";
        }

        private void SelectAmant(object sender, RoutedEventArgs e)
        {
            MainWindowViewObj.Instance.MonthEndOption = "A";
        }
    }
}
