using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;
using AksharPanchang.Constants;
using AksharPanchang.Utils;
using AksharPanchang.Viewmodel;

namespace AksharPanchang.Panchang
{
    public class Yoga
    {
        private string name;
        private double JDN;
        private double nextJDN;
        private double prevJDN;

        public override string ToString()
        {
            string str =  " "+name + " ";
            return str;
        }


        public string Name { get => name; set => name = value; }
        public double NextJDN { get => nextJDN; set => nextJDN = value; }
        public double PrevJDN { get => prevJDN; set => prevJDN = value; }
        public Yoga(double jDN)
        {
            JDN = jDN;
        }

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_ephe_path(string path);

        [DllImport("lib\\swedll64.dll")]
        public static extern long swe_calc_ut(double tjd_ut, int ipl, long iflag, double[] xx, char[] serr);

        [DllImport("lib\\swedll64.dll")]
        public static extern double swe_get_ayanamsa_ut(double tjd_ut);

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_sid_mode(int sid_mode, double t0, double ayan_t0);

        /*public static void Main()
        {
            DateTime dateTime = new DateTime(2019, 05, 20, 6, 0, 0);
            dateTime = DateTimeUtils.ConvertToTz(dateTime, "India Standard Time", "UTC");
            Place place = new Place(75.7764300, 23.1823900, 494);
            Yoga yoga = getYoga(place, DateTimeUtils.DateTimeToJDN(dateTime));
            Console.WriteLine(yoga);
            DateTime startTime = DateTimeUtils.JDNToDateTime(yoga.prevJDN);
            DateTime istStartTime = DateTimeUtils.ConvertToTz(startTime, "UTC", "India Standard Time");
            Console.WriteLine(istStartTime);
            DateTime nextTime = DateTimeUtils.JDNToDateTime(yoga.nextJDN);
            DateTime istNextTime = DateTimeUtils.ConvertToTz(nextTime, "UTC", "India Standard Time");
            Console.WriteLine(istNextTime);

            Console.Read();
        }*/

        public static Yoga getYoga(double tjd)
        {
            double JDN = tjd;
            swe_set_ephe_path("lib");
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            double[] sunPos = new double[6];
            double[] moonPos = new double[6];
            double sunLong, moonLong;
            long return_code;
            char[] serr = new char[256];
            double ayanamsa;
            return_code = swe_calc_ut(JDN, PlanetConst.SE_SUN, IFlagConst.SEFLG_SWIEPH, sunPos, serr);
            if (return_code == -1)
            {
                Console.WriteLine("Error");
            }
            ayanamsa = swe_get_ayanamsa_ut(JDN);
            sunLong = sunPos[0] - ayanamsa;

            char[] serr2 = new char[256];
            return_code = swe_calc_ut(JDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos, serr2);
            if (return_code == -1)
            {
                Console.WriteLine("Error");
            }
            ayanamsa = swe_get_ayanamsa_ut(JDN);
            moonLong = moonPos[0] - ayanamsa;
            double total = sunLong + moonLong;

            int yogaNumber = (int)Math.Ceiling(total * 27 / 360);
            if (yogaNumber < 0)
                yogaNumber = (yogaNumber + 27) % 27;
            if (yogaNumber == 0)
                yogaNumber = 27;
            if (yogaNumber > 27)
                yogaNumber = yogaNumber % 27;

            Yoga yoga = new Yoga(JDN);
            yoga.Name = YogaConst.getYogaAsString(yogaNumber);
            return yoga;
        }
    }
}
