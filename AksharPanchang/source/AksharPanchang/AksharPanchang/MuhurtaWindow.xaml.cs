using AksharPanchang.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AksharPanchang
{
    /// <summary>
    /// Interaction logic for Muhurta.xaml
    /// </summary>
    public partial class MuhurtaWindow : Window
    {
        string muhurtaLabelStr;

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

		//Satyavrata – The Truth Dedicated Lord
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

        public MuhurtaWindow(DateTime sunRiseTime, long muhurtaRange, string vara)
        {
            InitializeComponent();
            DateTime muhurta = sunRiseTime;
            string message = "";
            for(int i = 1; i<=30; i++)
            {
                message += String.Format("{0,-15}", MuhurtaConst.getMuhurtaAsString(i));
                if (MuhurtaConst.ASHUBH.Contains(i))
                    message += String.Format("{0,-35}"," Ashubh (Inauspicious) ");
                if (MuhurtaConst.ATI_SHUBH.Contains(i))
                    message += String.Format("{0,-35}"," Ati Shubh (Very Auspicious) ");
                if (MuhurtaConst.SHUBH.Contains(i))
                    message += String.Format("{0,-35}"," Shubh (Auspicious) ");
                if (i == 8)
                {
                    if (vara.Equals(VaraConst.SOMWAAR_STR)
                         || vara.Equals(VaraConst.SHUKRWAAR_STR))
                        message += String.Format("{0,-35}"," Ashubh (Inauspicious) ");
                    else
                        message += String.Format("{0,-35}"," Shubh (Auspicious) ");
                }
                if (i == 14)
                {
                    if (vara.Equals(VaraConst.RAVIWAAR_STR))
                        message += String.Format("{0,-35}"," Ashubh (Inauspicious) ");
                    else
                        message += String.Format("{0,-35}"," Shubh (Auspicious) ");
                }
                message += String.Format("{0,-15}","From");
                message += String.Format("{0,-50}",muhurta.ToString("HH: mm: ss dddd, dd MMMM yyy"));
                message += String.Format("{0,-10}", "upto");
                message += String.Format("{0,-50}", new DateTime(muhurta.Ticks + muhurtaRange).AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy"));
                message += "\n";
                muhurta = new DateTime(muhurta.Ticks + muhurtaRange);
            }
            MuhurtaLabelStr = message;
            this.MuhurtaLabel.DataContext = this;

        }

        public string MuhurtaLabelStr { get => muhurtaLabelStr; set => muhurtaLabelStr = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
