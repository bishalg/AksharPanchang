///Aditya - The Son Of Aditi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Config
{
	
	//Ajanma - One who is unborn
    public sealed class ApplicationManager
    {
        public string SETTINGS_FILE;
        public string EVENTNOTES_FILE;        
        public string LIB_DIR;
        public string PLACE_LONG = "place.long";
        public string PLACE_LAT = "place.lat";
        public string TIMEZONE_ID = "timeZoneId";
        public string MONTH_END_OPTION = "MonthEndOption";
        public string AYANAMSA_ID = "ayanamsa_id";

        private static readonly ApplicationManager instance = new ApplicationManager();
        static ApplicationManager()
        {
        }
        private ApplicationManager()
        {

        }
        public static ApplicationManager Instance
        {
            get
            {
                return instance;
            }
        }

        public string LIB_DIR1 { get => LIB_DIR; set => LIB_DIR = value; }

        public static implicit operator ApplicationManager(string v)
        {
            throw new NotImplementedException();
        }
    }
}
