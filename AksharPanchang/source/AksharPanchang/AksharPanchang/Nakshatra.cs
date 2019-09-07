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
    public class Nakshatra
    {


        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_ephe_path(string path);

        [DllImport("lib\\swedll64.dll")]
        public static extern long swe_calc_ut(double tjd_ut, int ipl, long iflag, double[] xx, char[] serr);

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_sid_mode(int sid_mode, double t0, double ayan_t0);

        [DllImport("lib\\swedll64.dll")]
        public static extern double swe_get_ayanamsa_ut(double tjd_ut);

        private string name;
        private double nakshatraJDN;
        private double startJDN;
        private double endJDN;
        private int pada;

        public override string ToString()
        {
            string str = " " + name;
            str = str + " Pada " + pada + " ";
            return str;
        }

        public string Name { get => name; set => name = value; }
        public double StartJDN { get => startJDN; set => startJDN = value; }
        public double EndJDN { get => endJDN; set => endJDN = value; }
        public int Pada { get => pada; set => pada = value; }

        public Nakshatra(double nakshatraJDN)
        {
            this.nakshatraJDN = nakshatraJDN;
        }

        public static void Main()
        {
            DateTime dateTime = new DateTime(2019, 08, 3, 22, 03, 0);
            dateTime = DateTimeUtils.ConvertToTz(dateTime, "India Standard Time", "UTC");
            Place place = new Place(75.7764300, 23.1823900);
            Nakshatra nak = getNakshatra(DateTimeUtils.DateTimeToJDN(dateTime));
            DateTime startTime = DateTimeUtils.JDNToDateTime(nak.startJDN);
            DateTime endTime = DateTimeUtils.JDNToDateTime(nak.EndJDN);
            DateTime istStartTime = DateTimeUtils.ConvertToTz(startTime, "UTC", "India Standard Time");
            DateTime istEndTime = DateTimeUtils.ConvertToTz(endTime, "UTC", "India Standard Time");

            Console.Read();
        }
        public static Nakshatra getNakshatra(double tjd)
        {
            double JDN = tjd;
            swe_set_ephe_path("lib");
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            double[] moonPos = new double[6];
            double moonLong;
            double ayanamsa;
            long return_code;
            char[] serr = new char[256];
            return_code = swe_calc_ut(JDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos, serr);
            if (return_code == -1)
            {
                Console.WriteLine("Error");
            }
            ayanamsa = swe_get_ayanamsa_ut(JDN);
            moonLong = moonPos[0] - ayanamsa;

            Nakshatra nakshatra = new Nakshatra(JDN);
            var padaNumber = moonLong * 27 / 360;
            if (padaNumber < 0)
                padaNumber = padaNumber + 4;
            var x = padaNumber - Math.Truncate(padaNumber);
            if (x < 0)
                x = x + 4;
            nakshatra.Pada = ((int)(x * 800) / 200) + 1;
            int nakshatraNumber = (int)Math.Ceiling(moonLong * 27 / 360);

            if (nakshatraNumber < 0)
                nakshatraNumber = (nakshatraNumber + 27) % 27;
            if (nakshatraNumber == 0)
                nakshatraNumber = 27;
            if (nakshatraNumber > 27)
                nakshatraNumber = nakshatraNumber%27;
            nakshatra.Name = NakshatraConst.getNakshatraAsString(nakshatraNumber);

            return nakshatra;
        }
		//,, Shantah – Peaceful Lord
        public static double getEndTime(Nakshatra nakshatra)
        {
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            int nakshatraNumber = NakshatraConst.getNakshatraAsInt(nakshatra.Name);
            int nextNakshatraNum = nakshatraNumber;
            int endNakshatra = nakshatraNumber + 1;
            if (endNakshatra > 27)
                endNakshatra = endNakshatra % 27;
            double nextJDN = nakshatra.nakshatraJDN;
            while (nextNakshatraNum != endNakshatra)
            {
                nextJDN += DateTimeUtils.JD_INTERVAL; ;
                double[] moonPos2 = new double[6];
                double moonLongNext;
                long return_code2;
                char[] serr2 = new char[256];
                double ayanamsa;
                return_code2 = swe_calc_ut(nextJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos2, serr2);
                if (return_code2 == -1)
                {
                    Console.WriteLine("Error");
                }
                ayanamsa = swe_get_ayanamsa_ut(nextJDN);
                moonLongNext = moonPos2[0] - ayanamsa;
                nextNakshatraNum = (int)Math.Ceiling((moonLongNext * 27 / 360));
                if (nextNakshatraNum < 0)
                    nextNakshatraNum = (nextNakshatraNum + 27) % 27;
                if (nextNakshatraNum == 0)
                    nextNakshatraNum = 27;
                if (nextNakshatraNum > 27)
                    nextNakshatraNum = nextNakshatraNum % 27;
            }
            return nextJDN;
        }
        public static double getEndTime1(Nakshatra nakshatra)
        {
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            int nakshatraNumber = NakshatraConst.getNakshatraAsInt(nakshatra.Name);
            int nextNakshatraNum = nakshatraNumber+1;
            if (nextNakshatraNum > 27)
                nextNakshatraNum = nextNakshatraNum % 27;
            int endNakshatra = nakshatraNumber + 2;
            if (endNakshatra > 27)
                endNakshatra = endNakshatra % 27;

            double nextJDN = nakshatra.nakshatraJDN;
            while (nextNakshatraNum != endNakshatra)
            {
                nextJDN += DateTimeUtils.JD_INTERVAL; ;
                double[] moonPos2 = new double[6];
                double moonLongNext;
                long return_code2;
                char[] serr2 = new char[256];
                double ayanamsa;
                return_code2 = swe_calc_ut(nextJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos2, serr2);
                if (return_code2 == -1)
                {
                    Console.WriteLine("Error");
                }
                ayanamsa = swe_get_ayanamsa_ut(nextJDN);
                moonLongNext = moonPos2[0] - ayanamsa;
                nextNakshatraNum = (int)Math.Ceiling((moonLongNext * 27 / 360));
                if (nextNakshatraNum < 0)
                    nextNakshatraNum = (nextNakshatraNum + 27) % 27;
                if (nextNakshatraNum == 0)
                    nextNakshatraNum = 27;
                if (nextNakshatraNum > 27)
                    nextNakshatraNum = nextNakshatraNum % 27;
            }
            return nextJDN;
        }
        
        public static double getBeginTime(Nakshatra nakshatra)
        {
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            int nakshatraNumber = NakshatraConst.getNakshatraAsInt(nakshatra.Name);
            int prevNakshatraUpto = nakshatraNumber - 1;
            if (prevNakshatraUpto == 0)
                prevNakshatraUpto = 27;
            if (prevNakshatraUpto < 0)
                prevNakshatraUpto = (prevNakshatraUpto + 27) % 27;
            int prevNakshatra = nakshatraNumber;
            double prevJDN = nakshatra.nakshatraJDN;
            while (prevNakshatra != prevNakshatraUpto)
            {
                prevJDN -= DateTimeUtils.JD_INTERVAL;
                double[] moonPos3 = new double[6];
                double moonLongPrev;
                long return_code3;
                char[] serr3 = new char[256];
                double ayanamsa;
                return_code3 = swe_calc_ut(prevJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos3, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                ayanamsa = swe_get_ayanamsa_ut(prevJDN);
                moonLongPrev = moonPos3[0] - ayanamsa;
                prevNakshatra = (int)Math.Ceiling((moonLongPrev * 27 / 360));
                if (prevNakshatra < 0)
                    prevNakshatra = (prevNakshatra + 27) % 27;
                if (prevNakshatra == 0)
                    prevNakshatra = 27;
                if (prevNakshatra > 27)
                    prevNakshatra = prevNakshatra % 27;
            }
            return prevJDN;
        }
        public static double getBeginTime1(Nakshatra nakshatra)
        {
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            int nakshatraNumber = NakshatraConst.getNakshatraAsInt(nakshatra.Name);
            int prevNakshatraUpto = nakshatraNumber - 2;
            if (prevNakshatraUpto == 0)
                prevNakshatraUpto = 27;
            if (prevNakshatraUpto < 0)
                prevNakshatraUpto = (prevNakshatraUpto + 27) % 27;
            int prevNakshatra = nakshatraNumber-1;
            if (prevNakshatra == 0)
                prevNakshatra = 27;
            if (prevNakshatra < 0)
                prevNakshatra = (prevNakshatra + 27) % 27;
            double prevJDN = nakshatra.nakshatraJDN;
            while (prevNakshatra != prevNakshatraUpto)
            {
                prevJDN -= DateTimeUtils.JD_INTERVAL;
                double[] moonPos3 = new double[6];
                double moonLongPrev;
                long return_code3;
                char[] serr3 = new char[256];
                double ayanamsa;
                return_code3 = swe_calc_ut(prevJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos3, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                ayanamsa = swe_get_ayanamsa_ut(prevJDN);
                moonLongPrev = moonPos3[0] - ayanamsa;
                prevNakshatra = (int)Math.Ceiling((moonLongPrev * 27 / 360));
                if (prevNakshatra < 0)
                    prevNakshatra = (prevNakshatra+27)%27;
                if (prevNakshatra == 0)
                    prevNakshatra = 27;
                if (prevNakshatra > 27)
                    prevNakshatra = prevNakshatra % 27;
            }
            return prevJDN;
        }
    }
}
