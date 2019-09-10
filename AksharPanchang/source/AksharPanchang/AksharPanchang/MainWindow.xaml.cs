using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AksharPanchang.Config;
using AksharPanchang.ModelObjects;
using AksharPanchang.Properties;
using AksharPanchang.Viewmodel;
using AksharPanchang.Panchang;
using AksharPanchang.Utils;
using AksharPanchang.Constants;
using System.Runtime.InteropServices;

namespace AksharPanchang
{
    /*
	 Hiranyagarbha – The All Powerful Creator
	*/
    public partial class MainWindow : Window
    {
        private MainWindowViewObj model = MainWindowViewObj.Instance;
        private PropertiesManipulator propertiesManipulator;

        [DllImport("lib\\swedll64.dll")]
        public static extern int swe_houses(double tjd_ut, double geolat, double geolon, int hsys, double[] cusps, double[] ascmc);

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_ephe_path(string path);
        [DllImport("lib\\swedll64.dll")]
        public static extern double swe_get_ayanamsa_ut(double tjd_ut);

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_sid_mode(int sid_mode, double t0, double ayan_t0);
		//,, Hrishikesh – The Lord Of All Senses
        public MainWindow()
        {
            InitializeComponent();
            ApplicationManager.Instance.SETTINGS_FILE = AppDomain.CurrentDomain.BaseDirectory + "\\conf\\settings.properties";
            ApplicationManager.Instance.EVENTNOTES_FILE = AppDomain.CurrentDomain.BaseDirectory + "\\conf\\eventnotes.csv";
            ApplicationManager.Instance.LIB_DIR = AppDomain.CurrentDomain.BaseDirectory + "\\lib";
            model.AllTimeZones = TimeZoneInfo.GetSystemTimeZones();
            if (!File.Exists(ApplicationManager.Instance.SETTINGS_FILE))
            {
                File.Create(ApplicationManager.Instance.SETTINGS_FILE);

            }
            if (!File.Exists(ApplicationManager.Instance.EVENTNOTES_FILE))
            {
                File.Create(ApplicationManager.Instance.EVENTNOTES_FILE);
            }
            propertiesManipulator = new PropertiesManipulator(ApplicationManager.Instance.SETTINGS_FILE);
            double longitude = Convert.ToDouble(propertiesManipulator.get(ApplicationManager.Instance.PLACE_LONG));
            double latitude = Convert.ToDouble(propertiesManipulator.get(ApplicationManager.Instance.PLACE_LAT));
            string timeZoneId = propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID);
            string monthEndOption = propertiesManipulator.get(ApplicationManager.Instance.MONTH_END_OPTION);
            string ayanamsaId = propertiesManipulator.get(ApplicationManager.Instance.AYANAMSA_ID);

            model.Place = new Place(longitude, latitude);
            model.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            model.MonthEndOption = monthEndOption;
            DateTime now = DateTime.Now;

            model.DateTime = now;
            model.DateTime = DateTime.SpecifyKind(now, DateTimeKind.Unspecified);

            model.HourTxtStr = now.Hour.ToString();
            model.MinuteTxtStr = now.Minute.ToString();
            model.SecondsTxtStr = now.Second.ToString();
            model.DateTxtStr = now.Day.ToString();
            model.YearTxtStr = now.Year.ToString();

            this.MonthCombo.ItemsSource = model.Month_List;
            model.Month_Str = now.ToString("MMMM", CultureInfo.InvariantCulture);

            model.AyanamsaList = AyanamsaConst.getAllAyanamsa();
            if (ayanamsaId != null && !ayanamsaId.Equals(""))
            {
                int ayanamsaIdInt;
                bool isValid = int.TryParse(ayanamsaId, out ayanamsaIdInt);
                if(isValid)
                    model.SelectedAyanamsa = AyanamsaConst.getAyanamsaName(ayanamsaIdInt);
                else
                    model.SelectedAyanamsa = AyanamsaConst.SE_SIDM_LAHIRI_STR;
            }
                
            else
                model.SelectedAyanamsa = AyanamsaConst.SE_SIDM_LAHIRI_STR;

            this.MonthCombo.DataContext = model;
            this.hourTxt.DataContext = model;
            this.minuteTxt.DataContext = model;
            this.secondsTxt.DataContext = model;
            this.dateText.DataContext = model;
            this.yearText.DataContext = model;
            this.TithiLabel.DataContext = model;
            this.TithiAtSunriseLabel.DataContext = model;
            this.ImportanceLabel.DataContext = model;
            this.VaraLabel.DataContext = model;
            this.PakshaLabel.DataContext = model;
            this.MasaLabel.DataContext = model;
            this.SunriseLabel.DataContext = model;
            this.SunsetLabel.DataContext = model;
            this.PraharLabel.DataContext = model;
            this.MoonriseLabel.DataContext = model;
            this.MoonsetLabel.DataContext = model;
            this.SunSignLabel.DataContext = model;
            this.MoonSignLabel.DataContext = model;
            this.NakshatraLabelBtn.DataContext = model;
            this.YogaLabelBtn.DataContext = model;
            this.KaranaLabelBtn.DataContext = model;
            this.RituLabel.DataContext = model;
            this.SamvatLabel.DataContext = model;
            this.MuhurtaLabel.DataContext = model;
            updateView();


        }
        private void ChangeSettings_Click(object sender, RoutedEventArgs e)
        {
            PlaceAndTzWindow placeAndTzWindow = PlaceAndTzWindow.Instance;
            placeAndTzWindow.ShowInTaskbar = false;
            placeAndTzWindow.Owner = Application.Current.MainWindow;
            placeAndTzWindow.ShowDialog();
            updateView();
        }
		///,, Jagadguru – Preceptor Of The Universe
        private void UpdatePanchang_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() && ValidTime())
            {
                int year = Convert.ToInt32(model.YearTxtStr);
                int month = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(model.Month_Str) + 1;
                int date = Convert.ToInt32(model.DateTxtStr);
                int hour = Convert.ToInt32(model.HourTxtStr);
                int minute = Convert.ToInt32(model.MinuteTxtStr);
                int seconds = Convert.ToInt32(model.SecondsTxtStr);
                model.DateTime = new DateTime(year, month, date, hour, minute, seconds);
                updateView();
            }
        }
        private bool ValidateDate()
        {
            int year;
            int date;
            bool isValidYear = int.TryParse(model.YearTxtStr, out year);
            int month = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(model.Month_Str) + 1;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (!isValidYear)
            {
                MessageBox.Show("Invalid Year!");
                return false;
            }
            else
            {
                bool isValidDate = int.TryParse(model.DateTxtStr, out date);
                if (!isValidDate)
                {
                    MessageBox.Show("Invalid Date!");
                    return false;
                }
                if (!(date >= 1 && date <= daysInMonth))
                {
                    MessageBox.Show("Invalid Date!");
                    return false;
                }
            }
            return true;
        }
		///,, Jagadisha – Protector Of All
        private bool ValidTime()
        {
            int hour;
            bool isValidHour = int.TryParse(model.HourTxtStr, out hour);
            if (!(isValidHour && hour >= 0 && hour <= 23))
            {
                MessageBox.Show("Invalid hour!");
                return false;
            }
            int minute;
            bool isValidMinute = int.TryParse(model.MinuteTxtStr, out minute);
            if (!(isValidMinute && minute >= 0 && minute <= 59))
            {
                MessageBox.Show("Invalid Minutes!");
                return false;
            }
            int seconds;
            bool isValidSecond = int.TryParse(model.SecondsTxtStr, out seconds);
            if (!(isValidSecond && seconds >= 0 && seconds <= 59))
            {
                MessageBox.Show("Invalid seconds!");
                return false;
            }
            return true;
        }

        private void PlusOneDay_Click(object sender, RoutedEventArgs e)
        {

            model.DateTime = model.DateTime.AddDays(1);
            updateView();
        }

        private void MinusOneDay_Click(object sender, RoutedEventArgs e)
        {
            model.DateTime = model.DateTime.AddDays(-1);
            updateView();
        }

        private void PlusOneHour_Click(object sender, RoutedEventArgs e)
        {
            model.DateTime = model.DateTime.AddHours(1);
            updateView();

        }
		////Jagannath – Lord Of The Universe,, 
        private void MinusOneHour_Click(object sender, RoutedEventArgs e)
        {
            model.DateTime = model.DateTime.AddHours(-1);
            updateView();

        }

        public void updateView()
        {
            if (ValidateDate() && ValidTime())
            {

                model.HourTxtStr = model.DateTime.Hour.ToString();
                model.MinuteTxtStr = model.DateTime.Minute.ToString();
                model.SecondsTxtStr = model.DateTime.Second.ToString();

                model.DateTxtStr = model.DateTime.Day.ToString();
                model.YearTxtStr = model.DateTime.Year.ToString();
                model.Month_Str = model.DateTime.ToString("MMMM", CultureInfo.InvariantCulture);

                DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, model.TimeZone.Id, "UTC");
                DateTime sunRiseDt = getSunrise();
                model.SunriseLabelStr = sunRiseDt.ToString("HH:mm:ss  dddd, dd MMMM");

                DateTime sunsetDt = getSunSet();
                model.SunsetLabelStr = sunsetDt.ToString("HH:mm:ss  dddd, dd MMMM");

                model.PraharLabelStr = getPrahar();

                DateTime moonRiseDt = getMoonRise();
                model.MoonriseLabelStr = moonRiseDt.ToString("HH:mm:ss  dddd, dd MMMM");

                DateTime moonSetDt = getMoonSet();
                model.MoonsetLabelStr = moonSetDt.ToString("HH:mm:ss  dddd, dd MMMM");

                model.PanchangTithi = Tithi.getTithi(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
           
                model.TithiAtSunriseStr = getTithiAtSunrise();
                model.VaraLabelStr = getVara();
                
                Raashi sunSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_SUN);
                model.SunSignLabelStr = RaashiConst.getRaashiAsStr(sunSign.Number);

                Raashi moonSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_MOON);
                model.MoonSignLabelStr = RaashiConst.getRaashiAsStr(moonSign.Number);

                model.TithiLabelStr = TithiConst.getTithiAsString(model.PanchangTithi.TithiNumber);
                model.PakshaLabelStr = model.PanchangTithi.Paksha;

                model.Masa = Masa.getMasa(model.Place, DateTimeUtils.DateTimeToJDN(utcDate), model.MonthEndOption);
                model.MasaLabelStr = model.Masa.ToString();

                model.MasaAtSunrise = getMasaAtSunrise();

                updateImportance();

                model.RituLabelStr = Ritu.getRitu(model.Masa);

                Nakshatra nakshatra = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                model.Nakshatra = nakshatra;
                model.NakshatraLabelBtnStr = nakshatra.ToString();

                Yoga yoga = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                model.Yoga = yoga;
                model.YogaLabelBtnStr = yoga.ToString();

                Karana karana = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                model.Karana = karana;
                model.KaranaLabelBtnStr = karana.ToString();

                Samvat samvat = Samvat.getSamvatSar(model.Place, DateTimeUtils.DateTimeToJDN(utcDate), model.MonthEndOption);
                model.SamvatLabelStr = samvat.SamvatNum + ", " + samvat.Name;

                model.MuhurtaLabelStr = getMuhurta();

            }
        }
		///Janardhana – One Who Bestows Boons On One And All,, 
        private void MoreSunrise_Click(object sender, RoutedEventArgs e)
        {
            DateTime temp = model.DateTime.AddDays(-2);
            DateTime prevSunrise2 = getSunrise(temp);
            string sunRise1Str1 = prevSunrise2.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(-1);
            DateTime prevSunrise1 = getSunrise(temp);
            string sunRise1Str2 = prevSunrise1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime;
            DateTime sunRise = getSunrise(temp);
            string sunRise1Str3 = sunRise.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(+1);
            DateTime nextSunRise1 = getSunrise(temp);
            string sunRise1Str4 = nextSunRise1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(+2);
            DateTime nextSunRise2 = getSunrise(temp);
            string sunRise1Str5 = nextSunRise2.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            MessageBox.Show("Sunrises \n" + sunRise1Str1 + "\n" + sunRise1Str2 + "\n" + sunRise1Str3 + "\n" + sunRise1Str4 + "\n" + sunRise1Str5);
        }

        private void MoreSunset_Click(object sender, RoutedEventArgs e)
        {
            DateTime temp = model.DateTime.AddDays(-2);
            DateTime prevSunSet2 = getSunSet(temp);
            string sunset1 = prevSunSet2.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(-1);
            DateTime prevSunSet1 = getSunSet(temp);
            string sunset2 = prevSunSet1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime;
            DateTime sunSet = getSunSet(temp);
            string sunset3 = sunSet.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(1);
            DateTime nextSunSet1 = getSunSet(temp);
            string sunset4 = nextSunSet1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(2);
            DateTime nextSunSet2 = getSunSet(temp);
            string sunset5 = nextSunSet2.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            MessageBox.Show("Sunsets \n" + sunset1 + "\n" + sunset2 + "\n" + sunset3 + "\n" + sunset4 + "\n" + sunset5);
        }
		//Jayantah – Conqueror Of All Enemies
        private void MoreMoonRise_Click(object sender, RoutedEventArgs e)
        {
            DateTime temp = model.DateTime.AddDays(-2);
            DateTime prevMoonRise2 = getMoonRise(temp);
            string moonRise1 = prevMoonRise2.ToString("HH:mm:ss  dddd, dd MMMM yyy");


            temp = model.DateTime.AddDays(-1);
            DateTime prevMoonRise1 = getMoonRise(temp);
            string moonRise2 = prevMoonRise1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime;
            DateTime moonRise = getMoonRise(temp);
            string moonRise3 = moonRise.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(1);
            DateTime nextMoonRise1 = getMoonRise(temp);
            string moonRise4 = nextMoonRise1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(2);
            DateTime nextMoonRise2 = getMoonRise(temp);
            string moonRise5 = nextMoonRise2.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            MessageBox.Show("Moonrises \n" + moonRise1 + "\n" + moonRise2 + "\n" + moonRise3 + "\n" + moonRise4 + "\n" + moonRise5);

        }
		/* Jyotiraaditya – The Resplendence Of The Sun*/
        private void MoreMoonSet_Click(object sender, RoutedEventArgs e)
        {
            DateTime temp = model.DateTime.AddDays(-2);
            DateTime prevMoonSet2 = getMoonSet(temp);
            string moonSet1 = prevMoonSet2.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(-1);
            DateTime prevMoonSet1 = getMoonSet(temp);
            string moonSet2 = prevMoonSet1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime;
            DateTime moonSet = getMoonSet(temp);
            string moonSet3 = moonSet.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(1);
            DateTime nextMoonSet1 = getMoonSet(temp);
            string moonSet4 = nextMoonSet1.ToString("HH:mm:ss  dddd, dd MMMM yyy");

            temp = model.DateTime.AddDays(2);
            DateTime nextMoonSet2 = getMoonSet(temp);
            string moonSet5 = nextMoonSet2.ToString("HH:mm:ss  dddd, dd MMMM yyy");
           
            MessageBox.Show("Moonsets \n" + moonSet1 + "\n" + moonSet2 + "\n" + moonSet3 + "\n" + moonSet4 + "\n" + moonSet5);
        }

        private void MoreNakshatra_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() && ValidTime())
            {
                DateTime dt = model.DateTime;
                DateTime utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                Nakshatra currentNakshatra = model.Nakshatra;
                Nakshatra nextNakshatra1, nextNakshatra2, nextNakshatra3;
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    nextNakshatra1 = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentNakshatra.Pada == nextNakshatra1.Pada);
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    nextNakshatra1 = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentNakshatra.Pada == nextNakshatra1.Pada);
                dt = dt.AddSeconds(-1);
                String currentNakshatraEndTime = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                String nextNakshatraTime1 = dt.ToString("HH:mm:ss  dddd, dd MMMM yyy");

                nextNakshatra2 = nextNakshatra1;
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    nextNakshatra2 = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (nextNakshatra1.Pada == nextNakshatra2.Pada);
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    nextNakshatra2 = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (nextNakshatra1.Pada == nextNakshatra2.Pada);
                dt = dt.AddSeconds(-1);
                String nextNakshatraEndTime1 = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                String nextNakshatraTime2 = dt.ToString("HH:mm:ss  dddd, dd MMMM yyy");

                nextNakshatra3 = nextNakshatra2;
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    nextNakshatra3 = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (nextNakshatra2.Pada == nextNakshatra3.Pada);
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    nextNakshatra3 = Nakshatra.getNakshatra(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (nextNakshatra2.Pada == nextNakshatra3.Pada);
                dt = dt.AddSeconds(-1);
                String nextNakshatraEndTime2 = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                

                MessageBox.Show(currentNakshatra + " upto " + currentNakshatraEndTime
                    + "\n\n" + nextNakshatra1 + " from " + nextNakshatraTime1 + " upto " + nextNakshatraEndTime1
                    + "\n\n" + nextNakshatra2 + " from " + nextNakshatraTime2 + " upto " + nextNakshatraEndTime2);
            }
        }
		// Kamalnath – The Lord Of Goddess Lakshmi 
        private void MoreYoga_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() && ValidTime())
            {
                
                DateTime dt = model.DateTime;
                DateTime utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                Yoga currentYoga = model.Yoga;
                Yoga yoga1, yoga2, yoga3;
                do
                {
                    dt = dt.AddHours(2);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga1 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentYoga.Name.Equals(yoga1.Name));
                dt = dt.AddHours(-2);
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga1 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentYoga.Name.Equals(yoga1.Name));
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga1 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentYoga.Name.Equals(yoga1.Name));
                dt = dt.AddSeconds(-1);
                String currnetYogaEndTime = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                String nextYogaBeginTime1 = dt.ToString("HH:mm:ss  dddd, dd MMMM yyy");

                yoga2 = yoga1;
                do
                {
                    dt = dt.AddHours(2);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga2 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (yoga1.Name.Equals(yoga2.Name));
                dt = dt.AddHours(-2);
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga2 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (yoga1.Name.Equals(yoga2.Name));
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga2 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (yoga1.Name.Equals(yoga2.Name));
                dt = dt.AddSeconds(-1);
                String nextYogaEndTime1 = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                String nextYogaBeginTime2 = dt.ToString("HH:mm:ss  dddd, dd MMMM yyy");

                yoga3 = yoga2;
                do
                {
                    dt = dt.AddHours(2);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga3 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (yoga2.Name.Equals(yoga3.Name));
                dt = dt.AddHours(-2);
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga3 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (yoga2.Name.Equals(yoga3.Name));
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    yoga3 = Yoga.getYoga(DateTimeUtils.DateTimeToJDN(utcDate));
                } while (yoga2.Name.Equals(yoga3.Name));
                dt = dt.AddSeconds(-1);               
                String nextYogaEndTime2 = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");

                MessageBox.Show(currentYoga + " upto " + currnetYogaEndTime
                    + "\n\n" + yoga1 + "from " + nextYogaBeginTime1 + " upto " + nextYogaEndTime1
                    + "\n\n" + yoga2 + "from " + nextYogaBeginTime2 + " upto " + nextYogaEndTime2);
            }
        }
		// ,, Kamalnayan – The Lord With Lotus Shaped Eyes
        private void MoreKarana_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() && ValidTime())
            {
                Karana karana = model.Karana;
                DateTime dt = model.DateTime;
                DateTime utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                Karana currentKarana = model.Karana;
                Karana karana1, karana2, karana3;

                do
                {
                    dt = dt.AddHours(2);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana1 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentKarana.KaranaNum == karana1.KaranaNum);
                dt = dt.AddHours(-2);
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana1 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentKarana.KaranaNum == karana1.KaranaNum);
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana1 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (currentKarana.KaranaNum == karana1.KaranaNum);
                dt = dt.AddSeconds(-1);
                String currentKaranaEndTime = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                String nextKaranaBeginTime1 = dt.ToString("HH:mm:ss  dddd, dd MMMM yyy");

                karana2 = karana1;
                do
                {
                    dt = dt.AddHours(2);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana2 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (karana1.KaranaNum == karana2.KaranaNum);
                dt = dt.AddHours(-2);
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana2 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (karana1.KaranaNum == karana2.KaranaNum);
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana2 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (karana1.KaranaNum == karana2.KaranaNum);
                dt = dt.AddSeconds(-1);
                String nextKaranaEndTime1 = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                String nextKaranaBeginTime2 = dt.ToString("HH:mm:ss  dddd, dd MMMM yyy");

                karana3 = karana2;
                do
                {
                    dt = dt.AddHours(2);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana3 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (karana2.KaranaNum == karana3.KaranaNum);
                dt = dt.AddHours(-2);
                do
                {
                    dt = dt.AddMinutes(20);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana3 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (karana2.KaranaNum == karana3.KaranaNum);
                dt = dt.AddMinutes(-20);
                do
                {
                    dt = dt.AddSeconds(1);
                    utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
                    karana3 = Karana.getKarana(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
                } while (karana2.KaranaNum == karana3.KaranaNum);
                dt = dt.AddSeconds(-1);
                String nextKaranaEndTime2 = dt.AddSeconds(-1).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                

                MessageBox.Show(currentKarana + " upto " + currentKaranaEndTime
                    + "\n\n" + karana1 + " from " + nextKaranaBeginTime1 + " upto " + nextKaranaEndTime1
                    + "\n\n" + karana2 + " from " + nextKaranaBeginTime2 + " upto " + nextKaranaEndTime2);
            }
        }

        private void SuryaGrahan_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() && ValidTime())
            {
                DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
                Grahan grahan = Grahan.getGrahan(model.Place, DateTimeUtils.DateTimeToJDN(utcDate), GrahanConst.SURYA_GRAHAN);
                MessageBox.Show(grahan.Description
                    +"\n\n Begin Time "+ DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(grahan.BeginJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy")
                +"\n End Time " + DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(grahan.EndJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy"));
                
            }
        }
		//	Kamsantak – Slayer Of Kamsa
        private void ChandraGrahan_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() && ValidTime())
            {
                DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
                Grahan grahan = Grahan.getGrahan(model.Place, DateTimeUtils.DateTimeToJDN(utcDate), GrahanConst.CHANDRA_GRAHAN);
                MessageBox.Show(grahan.Description
                    + "\n\n Begin Time " + DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(grahan.BeginJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy")
                + "\n End Time " + DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(grahan.EndJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy"));
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            PlaceAndTzWindow.Instance.Close();
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowInTaskbar = false;
            aboutWindow.Owner = Application.Current.MainWindow;
            aboutWindow.ShowDialog();

        }

        private void Poornima_Click(object sender, RoutedEventArgs e)
        {
            double nextJDN;
            if (model.PanchangTithi.TithiNumber != 15)
            {
                nextJDN = Masa.getNextPoornima(model.Place, model.PanchangTithi);
                Tithi poornimaTithi = Tithi.getTithi(model.Place, nextJDN + DateTimeUtils.JD_INTERVAL_LONG2);
                double nextJDNEnd = Tithi.getEndTime(poornimaTithi);
                string poornimaDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN).AddSeconds(1), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                string poornimaEndStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDNEnd), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                MessageBox.Show("Next Poornima(Full Moon) \n\n starts at \n\n" + poornimaDateTimeStr + "\n\n ends at \n"+ poornimaEndStr);
            }
            else { 
                Tithi newTithi = Tithi.getTithi(model.Place, DateTimeUtils.DateTimeToJDN(model.DateTime.AddDays(10)));
                nextJDN = Masa.getNextPoornima(model.Place, newTithi);
                Tithi poornimaTithi = Tithi.getTithi(model.Place, nextJDN + DateTimeUtils.JD_INTERVAL_LONG2);
                double nextJDNEnd = Tithi.getEndTime(poornimaTithi);
                string poornimaDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN).AddSeconds(1), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                string poornimaEndStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDNEnd), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                MessageBox.Show("Next Poornima(Full Moon) \n\n starts at \n\n" + poornimaDateTimeStr + "\n\n ends at \n" + poornimaEndStr);
            }
        }
	//	Kanjalochana – The Lotus-Eyed God
        private void Amavasya_Click(object sender, RoutedEventArgs e)
        {
            double nextJDN;
            if (model.PanchangTithi.TithiNumber != 30)
            {
                nextJDN = Masa.getNextAmavasya(model.Place, model.PanchangTithi);
                Tithi amavasyaTithi = Tithi.getTithi(model.Place, nextJDN+DateTimeUtils.JD_INTERVAL_LONG2);
                double nextJDNEnd = Tithi.getEndTime(amavasyaTithi);
                string amavasyaDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN).AddSeconds(1), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                string amavasyaEndStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDNEnd), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                MessageBox.Show("Next Amavasya (No Moon) \n\n starts at \n" + amavasyaDateTimeStr + " \n\n ends at \n " + amavasyaEndStr);
            }
            else
            {
                Tithi newTithi = Tithi.getTithi(model.Place, DateTimeUtils.DateTimeToJDN(model.DateTime.AddDays(10)));
                nextJDN = Masa.getNextAmavasya(model.Place, newTithi);
                Tithi amavasyaTithi = Tithi.getTithi(model.Place, nextJDN+DateTimeUtils.JD_INTERVAL_LONG2);
                double nextJDNEnd = Tithi.getEndTime(amavasyaTithi);
                string amavasyaDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN).AddSeconds(1), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                string amavasyaEndStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDNEnd).AddSeconds(1), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
                MessageBox.Show("Next Amavasya (No Moon) \n\n starts at \n" + amavasyaDateTimeStr + " \n\n ends at \n " + amavasyaEndStr);
            }
        }
		//Keshava – 
        private void Sankranti_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi sunSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_SUN);
            Raashi nextSunSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSunSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SUN);
            }while (sunSign.Number == nextSunSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSunSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SUN);
            } while (sunSign.Number == nextSunSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSunSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SUN);
            } while (sunSign.Number == nextSunSign.Number);
            string sankrantiStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next Sankranti (Sun moving to "+RaashiConst.getRaashiAsStr(nextSunSign.Number)+ ") is at \n\n" + sankrantiStr);
        }
		/// Krishna – Dark-Complexioned Lord
        private void ResetToNow_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            model.DateTime = now;
            model.DateTime = DateTime.SpecifyKind(now, DateTimeKind.Unspecified);
            updateView();
        }

        private void GetPlanetPos_Click(object sender, RoutedEventArgs e)
        {
            Raashi planetSign;
            long return_code;
            

            int[] planets = { PlanetConst.SE_SUN,
                                PlanetConst.SE_MOON,
                                PlanetConst.SE_MARS,
                                PlanetConst.SE_MERCURY,
                                PlanetConst.SE_JUPITER,
                                PlanetConst.SE_VENUS,
                                PlanetConst.SE_SATURN,
                                PlanetConst.RAHU
            };
            if (ValidateDate() && ValidTime())
            {
                DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, model.TimeZone.Id, "UTC");

                swe_set_ephe_path("lib");
                swe_set_sid_mode(AyanamsaConst.getAyanamsaId(model.SelectedAyanamsa), 0, 0);

                double[] cusps = new double[13];
                double[] ascmc = new double[10];
                double[] pos = new double[6];
                double ayanamsa;
                ayanamsa = swe_get_ayanamsa_ut(DateTimeUtils.DateTimeToJDN(utcDate));
                return_code = swe_houses(DateTimeUtils.DateTimeToJDN(utcDate), model.Place.Latitude, model.Place.Longitude, 'E', cusps, ascmc);

                ascmc[0] = ascmc[0] - ayanamsa;

                DMS totDMS = PanchangUtil.deciToDeg(ascmc[0]);
                Raashi ascSign = PanchangUtil.getRaashi(totDMS);

                string message = "";

                message+="Ascendant "+ RaashiConst.getRaashiAsStr(ascSign.Number) + " " + ascSign.Degree + "* " + ascSign.Minute + " \" " + ascSign.Sec + "`";
                message += "\n\n";
                Raashi rahuSign =null;
                foreach (int i in planets)
                {
                    planetSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), i);
                    if (i == PlanetConst.RAHU)
                        rahuSign = planetSign;
                    message += PlanetConst.getPlanetName(i) + " " + RaashiConst.getRaashiAsStr(planetSign.Number) + " " + planetSign.Degree + "* " + planetSign.Minute + " \" " + planetSign.Sec +"`";
                    message += "\n\n";
                }
                if(rahuSign!=null)
                    message +=PlanetConst.getPlanetName(-99) + " " + RaashiConst.getRaashiAsStr((rahuSign.Number+6)%12) + " " + rahuSign.Degree + "* " + rahuSign.Minute + " \" " + rahuSign.Sec + "`";
                MessageBox.Show(message);
            }
        }
		//// Lakshmikantam – The Lord Of Goddess Lakshmi
        private DateTime getSunrise()
        {
            DateTime dt = model.DateTime;
            DateTime dt1 = dt;
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            double sunRiseJDN1 = RiseAndSet.getSunrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempSunriseDate1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunRiseJDN1), "UTC", model.TimeZone.Id);

			//Lokadhyaksha – Lord Of All The Three Lokas (Worlds)
            DateTime dt2 = dt1;
            DateTime utcDate2 = utcDate1;
            double sunRiseJDN2 = sunRiseJDN1;
            DateTime tempSunriseDate2 = tempSunriseDate1;
            while (tempSunriseDate1.Day !=dt.Day
                && tempSunriseDate2.Day != dt.Day)
            {
                dt1 = dt1.AddDays(-1);
                utcDate1 = DateTimeUtils.ConvertToTz(dt1, model.TimeZone.Id, "UTC");
                sunRiseJDN1 = RiseAndSet.getSunrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
                tempSunriseDate1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunRiseJDN1), "UTC", model.TimeZone.Id);

                dt2 = dt2.AddDays(1);
                utcDate2 = DateTimeUtils.ConvertToTz(dt2, model.TimeZone.Id, "UTC");
                sunRiseJDN2 = RiseAndSet.getSunrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate2));
                tempSunriseDate2 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunRiseJDN2), "UTC", model.TimeZone.Id);
            }
            if(tempSunriseDate1.Day == dt.Day)
            {
                return tempSunriseDate1;
            }
            else
            {
                return tempSunriseDate2;
            }
        }
        private DateTime getSunrise(DateTime dt)
        {
            DateTime dt1 = dt;
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            double sunRiseJDN1 = RiseAndSet.getSunrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempSunriseDate1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunRiseJDN1), "UTC", model.TimeZone.Id);
			//Madan – The Lord Of Love
            DateTime dt2 = dt1;
            DateTime utcDate2 = utcDate1;
            double sunRiseJDN2 = sunRiseJDN1;
            DateTime tempSunriseDate2 = tempSunriseDate1;
            while (tempSunriseDate1.Day != dt.Day
                && tempSunriseDate2.Day != dt.Day)
            {
                dt1 = dt1.AddDays(-1);
                utcDate1 = DateTimeUtils.ConvertToTz(dt1, model.TimeZone.Id, "UTC");
                sunRiseJDN1 = RiseAndSet.getSunrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
                tempSunriseDate1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunRiseJDN1), "UTC", model.TimeZone.Id);

                dt2 = dt2.AddDays(1);
                utcDate2 = DateTimeUtils.ConvertToTz(dt2, model.TimeZone.Id, "UTC");
                sunRiseJDN2 = RiseAndSet.getSunrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate2));
                tempSunriseDate2 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunRiseJDN2), "UTC", model.TimeZone.Id);
            }
            if (tempSunriseDate1.Day == dt.Day)
            {
                return tempSunriseDate1;
            }
            else
            {
                return tempSunriseDate2;
            }
        }
        private DateTime getPreviousSunrise()
        {

            DateTime dt = model.DateTime.AddDays(-1);
            return getSunrise(dt);
        }
        private DateTime getNextSunrise(DateTime dateTime)
        {
            DateTime dt = dateTime.AddDays(1);
            return getSunrise(dt);
        }
        private DateTime getSunSet()
        {
			//Madhava – Knowledge Filled God
            DateTime dt = model.DateTime;
            DateTime dt1 = dt;
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            double sunSetJDN1 = RiseAndSet.getSunset(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempSunSet1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunSetJDN1), "UTC", model.TimeZone.Id);

            DateTime dt2 = dt1;
            DateTime utcDate2 = utcDate1;
            double sunSetJDN2 = sunSetJDN1;
            DateTime tempSunSet2 = tempSunSet1;
            while (tempSunSet1.Day != dt.Day
               && tempSunSet2.Day != dt.Day)
            {
                dt1 = dt1.AddDays(-1);
                utcDate1 = DateTimeUtils.ConvertToTz(dt1, model.TimeZone.Id, "UTC");
                sunSetJDN1 = RiseAndSet.getSunset(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
                tempSunSet1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunSetJDN1), "UTC", model.TimeZone.Id);

                dt2 = dt2.AddDays(1);
                utcDate2 = DateTimeUtils.ConvertToTz(dt2, model.TimeZone.Id, "UTC");
                sunSetJDN2= RiseAndSet.getSunset(model.Place, DateTimeUtils.DateTimeToJDN(utcDate2));
                tempSunSet2= DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunSetJDN2), "UTC", model.TimeZone.Id);
            }
			/// Madhusudan – Slayer Of Demon Madhu
            if (tempSunSet1.Day == dt.Day)
            {
                return tempSunSet1;
            }
            else
            {
                return tempSunSet2;
            }
        }
        private DateTime getSunSet(DateTime dt)
        {
            DateTime dt1 = dt;
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            double sunSetJDN1 = RiseAndSet.getSunset(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempSunSet1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunSetJDN1), "UTC", model.TimeZone.Id);

            DateTime dt2 = dt1;
            DateTime utcDate2 = utcDate1;
            double sunSetJDN2 = sunSetJDN1;
            DateTime tempSunSet2 = tempSunSet1;
            while (tempSunSet1.Day != dt.Day
               && tempSunSet2.Day != dt.Day)
            {
                dt1 = dt1.AddDays(-1);
                utcDate1 = DateTimeUtils.ConvertToTz(dt1, model.TimeZone.Id, "UTC");
                sunSetJDN1 = RiseAndSet.getSunset(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
                tempSunSet1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunSetJDN1), "UTC", model.TimeZone.Id);

                dt2 = dt2.AddDays(1);
                utcDate2 = DateTimeUtils.ConvertToTz(dt2, model.TimeZone.Id, "UTC");
                sunSetJDN2 = RiseAndSet.getSunset(model.Place, DateTimeUtils.DateTimeToJDN(utcDate2));
                tempSunSet2 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(sunSetJDN2), "UTC", model.TimeZone.Id);
            } // ,, Mahendra – Lord Of Indra
            if (tempSunSet1.Day == dt.Day)
            {
                return tempSunSet1;
            }
            else
            {
                return tempSunSet2;
            }
        }
        private DateTime getPreviousSunSet( DateTime dateTime)
        {
            DateTime dt = dateTime.AddDays(-1);
            return getSunSet(dt);
        }
        
        private DateTime getMoonRise()
        {
            // Manmohan – All Pleasing Lord
            DateTime dt = model.DateTime;
            DateTime dt1 = dt;
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            double moonRiseJDN1 = RiseAndSet.getMoonrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempMoonRiseDate1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(moonRiseJDN1), "UTC", model.TimeZone.Id);
            return tempMoonRiseDate1;
        }
        private DateTime getMoonRise(DateTime dt)
        {
            // Manohar – Beautiful Lord
            DateTime dt1 = dt;
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            double moonRiseJDN1 = RiseAndSet.getMoonrise(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempMoonRiseDate1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(moonRiseJDN1), "UTC", model.TimeZone.Id);
            return tempMoonRiseDate1;
        }
        private DateTime getMoonSet()
        {
            DateTime dt = model.DateTime;
            DateTime dt1 = getMoonRise(dt);
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt1, model.TimeZone.Id, "UTC");
            double moonSetJDN1 = RiseAndSet.getMoonSet(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempMoonSet1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(moonSetJDN1), "UTC", model.TimeZone.Id);
            return tempMoonSet1;
        }
        private DateTime getMoonSet(DateTime dt)
        {
            DateTime dt1 = getMoonRise(dt); 
            DateTime utcDate1 = DateTimeUtils.ConvertToTz(dt1, model.TimeZone.Id, "UTC");
            double moonSetJDN1 = RiseAndSet.getMoonSet(model.Place, DateTimeUtils.DateTimeToJDN(utcDate1));
            DateTime tempMoonSet1 = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(moonSetJDN1), "UTC", model.TimeZone.Id);
            return tempMoonSet1;
        }
        private String getVara()
        {
            DateTime dt = model.DateTime;
            DateTime sunRiseDateTime = getSunrise();
            if(sunRiseDateTime <= dt)
            {
                return VaraConst.getVaraFromDayOfWeek(dt.DayOfWeek);
            }
			//Murlimanohar – The Flute Playing God
            else
            {
                return VaraConst.getVaraFromDayOfWeek(dt.AddDays(-1).DayOfWeek);
            }
        }
		//Nandgopala – The Son Of Nand
        private void PreviousTithi_Click(object sender, RoutedEventArgs e)
        {
            Tithi tithi = model.PanchangTithi;
            Tithi prevTithi = Tithi.getTithi(model.Place, Tithi.getBeginTime(tithi) - DateTimeUtils.JD_INTERVAL_LONG2);
            DateTime currentTithiEndTime = DateTimeUtils.JDNToDateTime(Tithi.getBeginTime(prevTithi));

            model.DateTime = DateTimeUtils.ConvertToTz(currentTithiEndTime, "UTC", model.TimeZone.Id);
            updateView();
        }


        private void NextTithi_Click(object sender, RoutedEventArgs e)
        {
            Tithi tithi = model.PanchangTithi;
            Tithi nextTithi = Tithi.getTithi(model.Place, Tithi.getEndTime(tithi) + DateTimeUtils.JD_INTERVAL_LONG2);
            DateTime currentTithiEndTime = DateTimeUtils.JDNToDateTime(Tithi.getBeginTime(nextTithi));
            
            model.DateTime = DateTimeUtils.ConvertToTz(currentTithiEndTime, "UTC", model.TimeZone.Id);
            updateView();

        }
		//, Narayana – The Refuge Of Everyone
        private void EditEvents_Click(object sender, RoutedEventArgs e)
        {
            EventNotesWindow eventNotesWindow = new EventNotesWindow();
            eventNotesWindow.Show();
        }

        private string getTithiAtSunrise()
        {
            DateTime dt = getSunrise();
            DateTime utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            Tithi tithi = Tithi.getTithi(model.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            model.TithiAtSunrise = tithi;
            return TithiConst.getTithiAsString(tithi.TithiNumber);
        }
        private Masa getMasaAtSunrise()
        {
            DateTime dt = getSunrise();
            DateTime utcDate = DateTimeUtils.ConvertToTz(dt, model.TimeZone.Id, "UTC");
            Masa masa = Masa.getMasa(model.Place, DateTimeUtils.DateTimeToJDN(utcDate), model.MonthEndOption);
            return masa;
        }
		///,, Niranjana – The Unblemished Lord
        private string getPrahar()
        {
            DateTime dt = model.DateTime;
            DateTime sunRiseDateTime = getSunrise();
            DateTime sunSetDateTime;
            if (sunRiseDateTime <= dt) // day prahar
            {
                sunSetDateTime = getSunSet();
                long sunRiseLong = sunRiseDateTime.Ticks;
                long sunSetLomg = sunSetDateTime.Ticks;
                long praharRange = (sunSetLomg - sunRiseLong) / 4;
                DateTime firstPraharEnd = new DateTime(sunRiseLong + praharRange);
                DateTime secondPraharEnd = new DateTime(sunRiseLong + praharRange * 2);
                DateTime thirdPraharEnd = new DateTime(sunRiseLong + praharRange * 3);
                DateTime fourthPraharEnd = sunSetDateTime;
                if(dt >= sunRiseDateTime && dt < firstPraharEnd)
                {
                    return "Day Prahar 1 - Purvaanh ";
                }
                else if (dt >= firstPraharEnd && dt < secondPraharEnd)
                {
                    return "Day Prahar 2 - Madhyaanh ";
                }
                else if (dt >= secondPraharEnd && dt < thirdPraharEnd)
                {
                    return "Day Prahar 3 - Aparaanh ";
                }
                else if (dt >= thirdPraharEnd && dt < fourthPraharEnd)
                {
                    return "Day Prahar 4 - Sandhyakaal ";
                }
                DateTime nextSunrise = getNextSunrise(dt); // from sunset to midnight
                long thisSunsetLong = sunSetDateTime.Ticks;
                long nextSunriseLong = nextSunrise.Ticks;
                long nightPraharRange = (nextSunriseLong - thisSunsetLong) / 4;
                DateTime nightFirstPraharEnd = new DateTime(thisSunsetLong + nightPraharRange);
                DateTime nightSecondPraharEnd = new DateTime(thisSunsetLong + nightPraharRange*2);
                DateTime nightThirdPraharEnd = new DateTime(thisSunsetLong + nightPraharRange*3) ;
                DateTime nightFourthPraharEnd = nextSunrise;                
                if (dt >= sunSetDateTime && dt < nightFirstPraharEnd)
                {
                    return "Night Prahar 1 - Pradosh ";
                }
                else if (dt >= nightFirstPraharEnd && dt < nightSecondPraharEnd )
                {
                    return "Night Prahar 2 - Nishith ";
                }
                else if (dt >= nightSecondPraharEnd && dt < nightThirdPraharEnd)
                {
                    return "Night Prahar 3 - Triyama ";
                }
                else if (dt >= nightThirdPraharEnd && dt < nightFourthPraharEnd)
                {
                    return "Night Prahar 4 - Ushakaal ";
                }
                else
                {
                    return "";
                }
            }
			//Nirguna – Without Any Properties
			//Padmahasta – One Who Has Hands Like Lotus
			//Padmanabha – The Lord Who Has A Lotus Shaped Navel
            else
            {
                DateTime previousSunset = getPreviousSunSet(dt);
                long previousSunsetLong = previousSunset.Ticks;
                long sunRiseLong = sunRiseDateTime.Ticks;
                long nightPraharRange = (sunRiseLong - previousSunsetLong) / 4;
                DateTime nightFirstPraharEnd = new DateTime(previousSunsetLong + nightPraharRange);
                DateTime nightSecondPraharEnd = new DateTime(previousSunsetLong + nightPraharRange * 2);
                DateTime nightThirdPraharEnd = new DateTime(previousSunsetLong + nightPraharRange * 3);
                DateTime nightFourthPraharEnd = sunRiseDateTime;

                if (dt >= previousSunset && dt < nightFirstPraharEnd)
                {
                    return "Night Prahar 1 - Pradosh ";
                }
                else if (dt >= nightFirstPraharEnd && dt < nightSecondPraharEnd)
                {
                    return "Night Prahar 2 - Nishith ";
                }
                else if (dt >= nightSecondPraharEnd && dt < nightThirdPraharEnd)
                {
                    return "Night Prahar 3 - Triyama ";
                }
                else if (dt >= nightThirdPraharEnd && dt < nightFourthPraharEnd)
                {
                    return "Night Prahar 4 - Ushakaal ";
                }
                else
                {
                    return "";
                }
				//Parabrahmana – The Supreme Absolute Truth
            }
        }

        private void updateImportance()
        {
            string message = "";
            if (model.MasaLabelStr.Contains("(Adhika)"))
            {
                message = "Adhik Masa";
            }
            else
            {

                if (model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                    && (model.PanchangTithi.TithiNumber % 15 == TithiConst.SAPTAMI
                    && model.VaraLabelStr.Equals(VaraConst.RAVIWAAR_STR))
                    ||
                  (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                    && (model.TithiAtSunrise.TithiNumber % 15 == TithiConst.SAPTAMI
                    && model.VaraLabelStr.Equals(VaraConst.RAVIWAAR_STR))
                    ))
                {
                    message += " Vijaya Saptami ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                   && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                   ||
                   (
                   model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                   && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI
                   ))
                {
                    message += " Bhalachandra Sankashti Chaturthi ";
                }
                //Paramatma – Lord Of All Beings
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                    && model.PanchangTithi.TithiNumber % 15 == TithiConst.PANCHAMI)
                    ||
                    (
                    model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                    && model.MonthEndOption.Equals("P")
                    && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                    && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PANCHAMI
                    ))
                {
                    message += " Rang Panchami ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                    && model.PanchangTithi.TithiNumber % 15 == TithiConst.SAPTAMI)
                    ||
                        (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                    && model.MonthEndOption.Equals("P")
                    && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                    && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.SAPTAMI))
                {
                    message += " Sheetala Saptami ";
                }
                // Parampurush – Supreme Personality,, Parthasarthi – Charioteer Of Partha – Arjuna
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                    && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                    ||
                        (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                    && model.MonthEndOption.Equals("P")
                    && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                    && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Paapmochani Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.PRATHAMA)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PRATHAMA))
                {
                    message += " Gudi Padwa ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                   && model.PanchangTithi.TithiNumber % 15 >= TithiConst.PRATHAMA
                   && model.PanchangTithi.TithiNumber % 15 <= TithiConst.NAVMI)
                   ||
                        (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                   && model.TithiAtSunrise.TithiNumber % 15 >= TithiConst.PRATHAMA
                   && model.TithiAtSunrise.TithiNumber % 15 <= TithiConst.NAVMI))
                {
                    message += " Chaitra Navratri ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.NAVMI)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.NAVMI))
                {
                    message += " Ram Navmi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                   && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                   ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                   && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Kamada Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.CHAITRA_STR)
                  && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.CHAITRA_STR)
                  && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Hanuman Jayanti ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                  ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Vikata Sankashti Chaturthi ";
                }


                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                   && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                   ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                   && model.MonthEndOption.Equals("P")
                   && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                   && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Varuthini Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.TRITIYA)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.TRITIYA))
                {
                    message += " Parashuram Jayanti - Akshaya Tritiya ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.SAPTAMI)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.SAPTAMI))
                {
                    message += " Ganga Saptami ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.NAVMI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.NAVMI))
                {
                    message += " Seeta Navmi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Mohini Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURDASHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURDASHI))
                {
                    message += " Narasimha Jayanti ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.VAISHAKH_STR)
                  && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Buddha Poornima ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.PRATHAMA)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PRATHAMA))
                {
                    message += " Naarad Jayanti ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Ekdanta Sankashti Chaturhi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Apara Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.PanchangTithi.TithiNumber == TithiConst.AMAVASYA)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.TithiAtSunrise.TithiNumber == TithiConst.AMAVASYA))
                {
                    message += " Shani Jayanti ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.DASHMI)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.DASHMI))
                {
                    message += " Ganga Dashahera ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Nirjala Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.JYESHTHA_STR)
                 && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.JYESHTHA_STR)
                 && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Vat Savitri Poornima ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " KrishnaPingala Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.ASHAADH_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                  ||
                       (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.ASHAADH_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Yogini Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.DWITIYA)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.DWITIYA))
                {
                    message += " Jagannath Rath Yatra ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Devshayani Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.ASHAADH_STR)
                 && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Guru Poornima ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Gajaanan Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Kamika Ekadashi ";
                }
                if (model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                && model.VaraLabelStr.Equals(VaraConst.SOMWAAR_STR)
                ||
                     (model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.SHRAVAN_STR)
                && model.VaraLabelStr.Equals(VaraConst.SOMWAAR_STR)))
                {
                    message += " Shraavan Somwar ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Putrada Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Putrada Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                 ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.SHRAVAN_STR)
                 && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Nariyali Poornima ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Heramba Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Ajaa Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.TRITIYA)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.TRITIYA))
                {
                    message += " Hartaalika Teej ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Ganesh Chaturthi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.PANCHAMI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PANCHAMI))
                {
                    message += " Rishi Panchami ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.ASHTAMI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.ASHTAMI))
                {
                    message += " Radha Ashtami ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Parivartini Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURDASHI)
               ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.BADHRPAD_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURDASHI))
                {
                    message += " Anant Chaturdashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR))
              ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)))
                {
                    message += " Pitru Paksha ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
               ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Vighnaharta Sankashti Chaturthi ";
                }


                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Indira Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
               && model.PanchangTithi.TithiNumber == TithiConst.AMAVASYA)
               ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
               && model.TithiAtSunrise.TithiNumber == TithiConst.AMAVASYA))
                {
                    message += " Sarva Pitru Amavasya ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
             && model.MonthEndOption.Equals("P")
             && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
             && model.PanchangTithi.TithiNumber % 15 >= TithiConst.PRATHAMA
             && model.PanchangTithi.TithiNumber % 15 <= TithiConst.NAVMI)
             ||
                  (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
             && model.MonthEndOption.Equals("P")
             && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
             && model.TithiAtSunrise.TithiNumber % 15 >= TithiConst.PRATHAMA
             && model.TithiAtSunrise.TithiNumber % 15 <= TithiConst.NAVMI))
                {
                    message += " Navratri ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.DASHMI)
                ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.DASHMI))
                {
                    message += " Dashahera ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
               ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Papankusha Ekadashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.KUNWAAR_STR)
                  && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                  ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.KUNWAAR_STR)
                  && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Sharad Poornima ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Karwa Chauth / Vakratunda Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Ramaa Ekadashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.TRAYODASI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.TRAYODASI))
                {
                    message += " Dhan Teras ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
                && model.PanchangTithi.TithiNumber == TithiConst.AMAVASYA)
                ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
                && model.TithiAtSunrise.TithiNumber == TithiConst.AMAVASYA))
                {
                    message += " Deepavali ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.PRATHAMA)
                ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PRATHAMA))
                {
                    message += " Govardhan Pooja ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.DWITIYA)
               ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.DWITIYA))
                {
                    message += " Bhai Dooj ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.KARTIK_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                  ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.KARTIK_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Praodhini Ekadashi - Tulsi Vivah ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.MARGHASHIRSHA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.MARGHASHIRSHA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Ganaadhipati Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.MARGHASHIRSHA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                  ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.MARGHASHIRSHA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Utpanna Ekadashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.MARGHASHIRSHA_STR)
                  && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                  ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.MARGHASHIRSHA_STR)
                  && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Mokshada Ekadashi - Gita Jayanti ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.PUSHYA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.PUSHYA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Akuratha Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.PUSHYA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.PUSHYA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Safalaa Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.PUSHYA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.PUSHYA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Putrada Ekadashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.Masa.Name.Equals(MasaConst.MAGHA_STR)
                && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
                ||
                    (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                && model.MonthEndOption.Equals("P")
                && model.MasaAtSunrise.Name.Equals(MasaConst.MAGHA_STR)
                && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Shatt Shila Ekadashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.Masa.Name.Equals(MasaConst.MAGHA_STR)
                 && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
                 ||
                     (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
                 && model.MonthEndOption.Equals("P")
                 && model.MasaAtSunrise.Name.Equals(MasaConst.MAGHA_STR)
                 && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Lambodara Sankashti Chaturthi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.MAGHA_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.PANCHAMI)
               ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.MAGHA_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PANCHAMI))
                {
                    message += " Vasant Panchami ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.MAGHA_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
               ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.MAGHA_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Jayaa Ekadashi";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
             && model.MonthEndOption.Equals("P")
             && model.Masa.Name.Equals(MasaConst.MAGHA_STR)
             && model.PanchangTithi.TithiNumber % 15 == TithiConst.TRAYODASI)
             ||
                 (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
             && model.MonthEndOption.Equals("P")
             && model.MasaAtSunrise.Name.Equals(MasaConst.MAGHA_STR)
             && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.TRAYODASI))
                {
                    message += " Vishwakarma Jayanti ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.Masa.Name.Equals(MasaConst.PHALGUN_STR)
              && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURTHI)
              ||
                  (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.MasaAtSunrise.Name.Equals(MasaConst.PHALGUN_STR)
              && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURTHI))
                {
                    message += " Dwijapriya Sankashti Chaturthi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.PHALGUN_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
               ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.PHALGUN_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Vijayaa Ekadashi ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.Masa.Name.Equals(MasaConst.PHALGUN_STR)
               && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURDASHI)
               ||
                   (model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)
               && model.MonthEndOption.Equals("P")
               && model.MasaAtSunrise.Name.Equals(MasaConst.PHALGUN_STR)
               && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURDASHI))
                {
                    message += " Maha Shivratri ";
                }
                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.Masa.Name.Equals(MasaConst.PHALGUN_STR)
              && model.PanchangTithi.TithiNumber % 15 == TithiConst.EKADASHI)
              ||
                  (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.MasaAtSunrise.Name.Equals(MasaConst.PHALGUN_STR)
              && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.EKADASHI))
                {
                    message += " Aamalki Ekadashi ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.Masa.Name.Equals(MasaConst.PHALGUN_STR)
              && model.PanchangTithi.TithiNumber % 15 == TithiConst.CHATURDASHI)
              ||
                  (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
              && model.MonthEndOption.Equals("P")
              && model.MasaAtSunrise.Name.Equals(MasaConst.PHALGUN_STR)
              && model.TithiAtSunrise.TithiNumber % 15 == TithiConst.CHATURDASHI))
                {
                    message += " Holika Dahan ";
                }

                if ((model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.Masa.Name.Equals(MasaConst.PHALGUN_STR)
                  && model.PanchangTithi.TithiNumber == TithiConst.POORNIMA)
                  ||
                      (model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)
                  && model.MonthEndOption.Equals("P")
                  && model.MasaAtSunrise.Name.Equals(MasaConst.PHALGUN_STR)
                  && model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA))
                {
                    message += " Holi ";
                }

                if ((model.PanchangTithi.TithiNumber == TithiConst.AMAVASYA
                   && model.VaraLabelStr.Equals(VaraConst.SOMWAAR_STR))
                   ||
                   ((model.TithiAtSunrise.TithiNumber == TithiConst.AMAVASYA
                   && model.VaraLabelStr.Equals(VaraConst.SOMWAAR_STR))))
                {
                    message += " Somwati Amavasya  ";
                }
                if ((model.PanchangTithi.TithiNumber == TithiConst.AMAVASYA
                   && model.MonthEndOption.Equals("P")
                   && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                   && model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA))
                   ||
                       (model.TithiAtSunrise.TithiNumber == TithiConst.AMAVASYA
                   && model.MonthEndOption.Equals("P")
                   && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                   && model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)))
                {
                    message += " Hariyali Amavasya ";
                }
                if ((model.PanchangTithi.TithiNumber % 15 == TithiConst.TRITIYA
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                    && model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA))
                    ||
                        (model.TithiAtSunrise.TithiNumber % 15 == TithiConst.TRITIYA
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                    && model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)))
                {
                    message += " Hariyali Teej ";
                }
                if ((model.PanchangTithi.TithiNumber % 15 == TithiConst.PANCHAMI
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                    && model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA))
                    ||
                        (model.TithiAtSunrise.TithiNumber % 15 == TithiConst.PANCHAMI
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                    && model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)))
                {
                    message += " Naag Panchami  ";
                }
                if ((model.PanchangTithi.TithiNumber == TithiConst.POORNIMA
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                    && model.PanchangTithi.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA))
                    ||
                        (model.TithiAtSunrise.TithiNumber == TithiConst.POORNIMA
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.SHRAVAN_STR)
                    && model.TithiAtSunrise.Paksha.Equals(PakshaConst.SHUKLA_PAKSHA)))
                {
                    message += " Raksha Bandhan  ";
                }
                if ((model.PanchangTithi.TithiNumber % 15 == TithiConst.ASHTAMI
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                    && model.PanchangTithi.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA))
                    ||
                         (model.TithiAtSunrise.TithiNumber % 15 == TithiConst.ASHTAMI
                    && model.MonthEndOption.Equals("P")
                    && model.Masa.Name.Equals(MasaConst.BADHRPAD_STR)
                    && model.TithiAtSunrise.Paksha.Equals(PakshaConst.KRISHNA_PAKSHA)))

                {
                    message += " Krishna Janmaashtami ";
                }
            }
            
            model.ImportanceLabelStr = message;
        }

        private void ChandraTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi moonSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_MOON);
            Raashi nextMoonSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMoonSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MOON);
            } while (moonSign.Number == nextMoonSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMoonSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MOON);
            } while (moonSign.Number == nextMoonSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMoonSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MOON);
            } while (moonSign.Number == nextMoonSign.Number);

            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next Chandra (Moon) Transit into " + RaashiConst.getRaashiAsStr(nextMoonSign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void MarsTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi marsSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_MARS);
            Raashi nextMarsSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMarsSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MARS);
            } while (marsSign.Number == nextMarsSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMarsSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MARS);
            } while (marsSign.Number == nextMarsSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMarsSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MARS);
            } while (marsSign.Number == nextMarsSign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next  Mangal (Mars)  Transit into " + RaashiConst.getRaashiAsStr(nextMarsSign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void MercuryTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi mercurySign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_MERCURY);
            Raashi nextMercurySign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMercurySign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MERCURY);
            } while (mercurySign.Number == nextMercurySign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMercurySign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MERCURY);
            } while (mercurySign.Number == nextMercurySign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextMercurySign = Raashi.getRaashi(nextJDN, PlanetConst.SE_MERCURY);
            } while (mercurySign.Number == nextMercurySign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next  Budh (Mercury) Transit into " + RaashiConst.getRaashiAsStr(nextMercurySign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void JupiterTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi jupiterSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_JUPITER);
            Raashi nextJupiterSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddDays(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextJupiterSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_JUPITER);
            } while (jupiterSign.Number == nextJupiterSign.Number);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextJupiterSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_JUPITER);
            } while (jupiterSign.Number == nextJupiterSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextJupiterSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_JUPITER);
            } while (jupiterSign.Number == nextJupiterSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextJupiterSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_JUPITER);
            } while (jupiterSign.Number == nextJupiterSign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next  Brihaspati/Guru (Jupiter) Transit into " + RaashiConst.getRaashiAsStr(nextJupiterSign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void VenusTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi venusSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_VENUS);
            Raashi nextVenusSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddDays(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextVenusSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_VENUS);
            } while (venusSign.Number == nextVenusSign.Number);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextVenusSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_VENUS);
            } while (venusSign.Number == nextVenusSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextVenusSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_VENUS);
            } while (venusSign.Number == nextVenusSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextVenusSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_VENUS);
            } while (venusSign.Number == nextVenusSign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next  Shukr (Venus) Transit into " + RaashiConst.getRaashiAsStr(nextVenusSign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void SaturnTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi saturnSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.SE_SATURN);
            Raashi nextSaturnSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddYears(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSaturnSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SATURN);
            } while (saturnSign.Number == nextSaturnSign.Number);
            utcDate = utcDate.AddYears(-1);
            do
            {
                utcDate = utcDate.AddMonths(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSaturnSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SATURN);
            } while (saturnSign.Number == nextSaturnSign.Number);
            utcDate = utcDate.AddMonths(-1);
            do
            {
                utcDate = utcDate.AddDays(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSaturnSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SATURN);
            } while (saturnSign.Number == nextSaturnSign.Number);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSaturnSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SATURN);
            } while (saturnSign.Number == nextSaturnSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSaturnSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SATURN);
            } while (saturnSign.Number == nextSaturnSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextSaturnSign = Raashi.getRaashi(nextJDN, PlanetConst.SE_SATURN);
            } while (saturnSign.Number == nextSaturnSign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next  Shani (Saturn) Transit into " + RaashiConst.getRaashiAsStr(nextSaturnSign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void RahuTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi rahuSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.RAHU);
            Raashi nextRahuSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddDays(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next Rahu Transit into " + RaashiConst.getRaashiAsStr(nextRahuSign.Number) + ") is at \n\n" + transitDateTimeStr);
        }

        private void KetuTransit_Click(object sender, RoutedEventArgs e)
        {
            DateTime utcDate = DateTimeUtils.ConvertToTz(model.DateTime, propertiesManipulator.get(ApplicationManager.Instance.TIMEZONE_ID), "UTC");
            Raashi rahuSign = Raashi.getRaashi(DateTimeUtils.DateTimeToJDN(utcDate), PlanetConst.RAHU);
            Raashi nextRahuSign;
            double nextJDN;
            do
            {
                utcDate = utcDate.AddDays(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextJDN = DateTimeUtils.DateTimeToJDN(utcDate);
                nextRahuSign = Raashi.getRaashi(nextJDN, PlanetConst.RAHU);
            } while (rahuSign.Number == nextRahuSign.Number);
            string transitDateTimeStr = DateTimeUtils.ConvertToTz(DateTimeUtils.JDNToDateTime(nextJDN), "UTC", model.TimeZone.Id).ToString("HH:mm:ss  dddd, dd MMMM yyy");
            MessageBox.Show("Next Ketu Transit into " + RaashiConst.getRaashiAsStr((nextRahuSign.Number+6)%12) + ") is at \n\n" + transitDateTimeStr);
        }
        private string getMuhurta()
        {
            string muhurtaStr = "";
            DateTime dt = model.DateTime;
            DateTime sunRiseDateTime = getSunrise();
            DateTime nextSunRiseDateTime;
            int muhurtaNum;
            if (sunRiseDateTime <= dt)
            {
                nextSunRiseDateTime = getNextSunrise(dt);
                long sunRiseLong = sunRiseDateTime.Ticks;
                long nextSunRiseLong = nextSunRiseDateTime.Ticks;
                long muhurtaRange = (nextSunRiseLong - sunRiseLong) / 30;
                muhurtaNum = (int)((dt.Ticks - sunRiseLong) / muhurtaRange) + 1;
            }
            else
            {
                nextSunRiseDateTime = sunRiseDateTime;
                sunRiseDateTime = getPreviousSunrise();
                long nextSunRiseLong = nextSunRiseDateTime.Ticks;
                long sunRiseLong = sunRiseDateTime.Ticks;
                long muhurtaRange = (nextSunRiseLong - sunRiseLong) / 30;
                muhurtaNum = (int)((dt.Ticks - sunRiseLong) / muhurtaRange) + 1 ;
            }
            muhurtaStr = MuhurtaConst.getMuhurtaAsString(muhurtaNum);
            if (MuhurtaConst.ASHUBH.Contains(muhurtaNum))
                muhurtaStr += " - Ashubh (Inauspicious) ";
            if (MuhurtaConst.ATI_SHUBH.Contains(muhurtaNum))
                muhurtaStr += " - Ati Shubh (Very Auspicious) ";
            if (MuhurtaConst.SHUBH.Contains(muhurtaNum))
                muhurtaStr += " - Shubh (Auspicious) ";
            if(muhurtaNum == 8)
            {
                if (model.VaraLabelStr.Equals(VaraConst.SOMWAAR_STR)
                     || model.VaraLabelStr.Equals(VaraConst.SHUKRWAAR_STR))
                    muhurtaStr += " - Ashubh (Inauspicious) ";
                else
                    muhurtaStr += " - Shubh (Auspicious) ";
            }
            if (muhurtaNum == 14)
            {
                if (model.VaraLabelStr.Equals(VaraConst.RAVIWAAR_STR))
                    muhurtaStr += " - Ashubh (Inauspicious) ";
                else
                    muhurtaStr += " - Shubh (Auspicious) ";
            }
            return muhurtaStr;
        }

        private void Muhurta_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = model.DateTime;
            DateTime sunRiseDateTime = getSunrise();
            DateTime nextSunriseDateTime = getNextSunrise(dt);
            long sunriseLong = sunRiseDateTime.Ticks;
            long nextSunriseLong = nextSunriseDateTime.Ticks;

            long muhurtaRange = (nextSunriseLong - sunriseLong) /30;

            MuhurtaWindow muhurtaWindow = new MuhurtaWindow(sunRiseDateTime, muhurtaRange, model.VaraLabelStr);
            muhurtaWindow.ShowInTaskbar = false;
            muhurtaWindow.Owner = Application.Current.MainWindow;
            muhurtaWindow.ShowDialog();

        }
    }  
}
