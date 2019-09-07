using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;
using AksharPanchang.Panchang;

namespace AksharPanchang.Viewmodel
{
    public sealed class MainWindowViewObj : INotifyPropertyChanged
    {
        Place place;
        TimeZoneInfo timeZone;
        private Masa masa;
        private Masa masaAtSunrise;
        private Nakshatra nakshatra;
        private Yoga yoga;
        private Karana karana;
        private Samvat samvat;
        private string monthEndOption; // 'P' for full moon and 'A' for no-moon
        private List<EventNote> eventNoteList;
        private string selectedAyanamsa;

        string hourTxtStr;
        string minuteTxtStr;
        string secondsTxtStr;
        string milliSecondsTxtStr;
        private List<String> am_pm_List;
        private List<String> month_List;
        private string dateTxtStr;
        private string yearTxtStr;
        private string month_Str;
        private string sunriseLabelStr;
        private string sunsetLabelStr;
        private string moonriseLabelStr;
        private string moonsetLabelStr;
        private string tithiLabelStr;
        private Tithi panchangTithi;
        private Tithi tithiAtSunrise;
        private string sunSignLabelStr;
        private string moonSignLabelStr;
        private string pakshaLabelStr;
        private string masaLabelStr;
        private string nakshatraLabelBtnStr;
        private string yogaLabelBtnStr;
        private string karanaLabelBtnStr;
        private string rituLabelStr;
        private string samvatLabelStr;
        private string solarEclipseLabelStr;
        private string varaLabelStr;
        private string tithiAtSunriseStr;
        private List<Ayanamsa> ayanamsaList;
        private string importanceLabelStr;
        private string praharLabelStr;
        private string muhurtaLabelStr;

        private DateTime dateTime;
		// Prajapati – Lord Of All Creatures,, 
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly MainWindowViewObj instance = new MainWindowViewObj();

        public Tithi PanchangTithi { get => panchangTithi; set => panchangTithi = value; }

        private ReadOnlyCollection<TimeZoneInfo> allTimeZones;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        static MainWindowViewObj()
        {
        }

        private MainWindowViewObj()
        {
        }

        public static MainWindowViewObj Instance
        {
            get
            {
                return instance;
            }
        }
		// Punyah – Supremely Pure,, Purshottam – The Supreme Soul,, 
        public ReadOnlyCollection<TimeZoneInfo> AllTimeZones
        {
            get
            {
                return this.allTimeZones;
            }

            set
            {
                if (value != this.allTimeZones)
                {
                    this.allTimeZones = value;
                }
            }
        }

        public Place Place { get => place; set => place = value; }
        public TimeZoneInfo TimeZone { get => timeZone; set => timeZone = value; }


        public List<string> AM_PM_List
        {
            get
            {
                return new List<string>() { "AM", "PM" };
            }
            set
            {
                am_pm_List = value;
            }
        }

        public List<string> Month_List
        {
            get
            {
                return new DateTimeFormatInfo().MonthNames.Take(12).ToList();
            }
            set
            {
                month_List = value;
            }
        }

        public string HourTxtStr
        {
            get
            {
                return this.hourTxtStr;
            }

            set
            {
                if (value != this.hourTxtStr)
                {
                    this.hourTxtStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MinuteTxtStr
        {
            get
            {
                return this.minuteTxtStr;
            }

            set
            {
                if (value != this.minuteTxtStr)
                {
                    this.minuteTxtStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string SecondsTxtStr
        {
            get
            {
                return this.secondsTxtStr;
            }

            set
            {
                if (value != this.secondsTxtStr)
                {
                    this.secondsTxtStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DateTxtStr
        {
            get
            {
                return this.dateTxtStr;
            }

            set
            {
                if (value != this.dateTxtStr)
                {
                    this.dateTxtStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string YearTxtStr
        {
            get
            {
                return this.yearTxtStr;
            }

            set
            {
                if (value != this.yearTxtStr)
                {
                    this.yearTxtStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
		// Ravilochana – One Who Eye Is The Sun
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Month_Str
        {
            get
            {
                return this.month_Str;
            }

            set
            {
                if (value != this.month_Str)
                {
                    this.month_Str = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string SunriseLabelStr
        {
            get
            {
                return this.sunriseLabelStr;
            }

            set
            {
                if (value != this.sunriseLabelStr)
                {
                    this.sunriseLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string SunsetLabelStr
        {
            get
            {
                return this.sunsetLabelStr;
            }

            set
            {
                if (value != this.sunsetLabelStr)
                {
                    this.sunsetLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MoonriseLabelStr
        {
            get
            {
                return this.moonriseLabelStr;
            }

            set
            {
                if (value != this.moonriseLabelStr)
                {
                    this.moonriseLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MoonsetLabelStr
        {
            get
            {
                return this.moonsetLabelStr;
            }

            set
            {
                if (value != this.moonsetLabelStr)
                {
                    this.moonsetLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
		//Sahasraakash – Thousand-Eyed Lord,, 
        public string TithiLabelStr
        {
            get
            {
                return this.tithiLabelStr;
            }

            set
            {
                if (value != this.tithiLabelStr)
                {
                    this.tithiLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PakshaLabelStr
        {
            get
            {
                return this.pakshaLabelStr;
            }

            set
            {
                if (value != this.pakshaLabelStr)
                {
                    this.pakshaLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MasaLabelStr
        {
            get
            {
                return this.masaLabelStr;
            }

            set
            {
                if (value != this.masaLabelStr)
                {
                    this.masaLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string SunSignLabelStr
        {
            get
            {
                return this.sunSignLabelStr;
            }

            set
            {
                if (value != this.sunSignLabelStr)
                {
                    this.sunSignLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MoonSignLabelStr
        {
            get
            {
                return this.moonSignLabelStr;
            }

            set
            {
                if (value != this.moonSignLabelStr)
                {
                    this.moonSignLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
		//Sahasrajit – One Who Vanquishes Thousands
        public Masa Masa
        {
            get
            {
                return this.masa;
            }

            set
            {
                if (value != this.masa)
                {
                    this.masa = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string NakshatraLabelBtnStr
        {
            get
            {
                return this.nakshatraLabelBtnStr;
            }

            set
            {
                if (value != this.nakshatraLabelBtnStr)
                {
                    this.nakshatraLabelBtnStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string YogaLabelBtnStr
        {
            get
            {
                return this.yogaLabelBtnStr;
            }

            set
            {
                if (value != this.yogaLabelBtnStr)
                {
                    this.yogaLabelBtnStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string KaranaLabelBtnStr
        {
            get
            {
                return this.karanaLabelBtnStr;
            }

            set
            {
                if (value != this.karanaLabelBtnStr)
                {
                    this.karanaLabelBtnStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Nakshatra Nakshatra
        {
            get
            {
                return this.nakshatra;
            }

            set
            {
                if (value != this.nakshatra)
                {
                    this.nakshatra = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Yoga Yoga
        {
            get
            {
                return this.yoga;
            }

            set
            {
                if (value != this.yoga)
                {
                    this.yoga = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public Karana Karana { get => karana; set => karana = value; }
        public string RituLabelStr
        {
            get
            {
                return this.rituLabelStr;
            }

            set
            {
                if (value != this.rituLabelStr)
                {
                    this.rituLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string SamvatLabelStr
        {
            get
            {
                return this.samvatLabelStr;
            }

            set
            {
                if (value != this.samvatLabelStr)
                {
                    this.samvatLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
		// Sahasrapaat – Thousand-Footed Lord
		 //Sakshi – All Witnessing Lord,, 
        public Samvat Samvat { get => samvat; set => samvat = value; }
        public string MonthEndOption { get => monthEndOption; set => monthEndOption = value; }
        public string SolarEclipseLabelStr
        {

            get
            {
                return this.solarEclipseLabelStr;
            }

            set
            {
                if (value != this.solarEclipseLabelStr)
                {
                    this.solarEclipseLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string VaraLabelStr
        {
            get
            {
                return this.varaLabelStr;
            }
            set
            {
                if (value != this.varaLabelStr)
                {
                    this.varaLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<EventNote> EventNoteList { get => eventNoteList; set => eventNoteList = value; }
        public string TithiAtSunriseStr
        {
            get
            {
                return this.tithiAtSunriseStr;
            }
            set
            {
                if (value != this.tithiAtSunriseStr)
                {
                    this.tithiAtSunriseStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<Ayanamsa> AyanamsaList { get => ayanamsaList; set => ayanamsaList = value; }
        public string SelectedAyanamsa { get => selectedAyanamsa; set => selectedAyanamsa = value; }
        public string ImportanceLabelStr
        {

            get
            {
                return this.importanceLabelStr;
            }
            set
            {
                if (value != this.importanceLabelStr)
                {
                    this.importanceLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }
		// Sanatana – The Eternal Lord,, Sarvajana – Omniscient Lord
        public string PraharLabelStr
        {
            get
            {
                return this.praharLabelStr;
            }
            set
            {
                if (value != this.praharLabelStr)
                {
                    this.praharLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string MuhurtaLabelStr
        {
            get
            {
                return this.muhurtaLabelStr;
            }
            set
            {
                if (value != this.muhurtaLabelStr)
                {
                    this.muhurtaLabelStr = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Tithi TithiAtSunrise
        {
            get
            {
                return this.tithiAtSunrise;
            }
            set
            {
                if (value != this.tithiAtSunrise)
                {
                    this.tithiAtSunrise = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Masa MasaAtSunrise
        {
            get
            {
                return this.masaAtSunrise;
            }
            set
            {
                if (value != this.masaAtSunrise)
                {
                    this.masaAtSunrise = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
