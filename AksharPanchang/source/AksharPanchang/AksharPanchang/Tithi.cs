using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.Constants;
using AksharPanchang.Utils;
using AksharPanchang.ModelObjects;
using System.Runtime.InteropServices;
using AksharPanchang.Config;

namespace AksharPanchang.Panchang
{
    public class Tithi
    {
        private int tithiNumber;
        private Place place;
        private double endTithiJDN;
        private double startTithiJDN;
        private double tithiJDN;
        private double monthBeginJDN;
        private double monthEndJDN;
        private string paksha;

        public int TithiNumber { get => tithiNumber; set => tithiNumber = value; }

        public double EndTithiJDN { get => endTithiJDN; set => endTithiJDN = value; }
        public string Paksha { get => paksha; set => paksha = value; }
        public double MonthBeginJDN { get => monthBeginJDN; set => monthBeginJDN = value; }
        public double MonthEndJDN { get => monthEndJDN; set => monthEndJDN = value; }
        public double TithiJDN { get => tithiJDN; set => tithiJDN = value; }
        public Place Place { get => place; set => place = value; }
        public double StartTithiJDN { get => startTithiJDN; set => startTithiJDN = value; }

        public Tithi(Place place, double tithiJDN)
        {
            this.tithiJDN = tithiJDN;
            this.place = place;
        }

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_ephe_path(string path);

        [DllImport("lib\\swedll64.dll")]
        public static extern long swe_calc_ut(double tjd_ut, int ipl, long iflag, double[] xx, char[] serr);

        public static Tithi getTithi(Place place, double tjd)
        {
            double JDN = tjd;
            swe_set_ephe_path(ApplicationManager.Instance.LIB_DIR);
            double[] sunPos = new double[6];
            double[] moonPos = new double[6];
            double sunLong, moonLong;
            long return_code;
            char[] serr = new char[256];
            char[] serr2 = new char[256];

            return_code = swe_calc_ut(JDN, PlanetConst.SE_SUN, IFlagConst.SEFLG_SWIEPH, sunPos, serr);
            if (return_code == -1)
            {
                Console.WriteLine("Error");
            }
            sunLong = sunPos[0];

            return_code = swe_calc_ut(JDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPos, serr2);
            if (return_code == -1)
            {
                Console.WriteLine("Error");
            }
            moonLong = moonPos[0];

            if (moonLong < sunLong)
                moonLong += 360;
            double tithiFrac = (moonLong - sunLong) % 360;
            /*if (tithiFrac < 0)
            {
                tithiFrac += 360.0;
            }*/
            int tithiNumber = (int)(tithiFrac / 12);
            tithiNumber += 1;
            Tithi tithi = new Tithi(place, JDN);
            tithi.TithiNumber = tithiNumber;
            tithi.Paksha = PakshaConst.getPakshaFromTithiNumber(tithiNumber);
            return tithi;
        }
		//Yadavendra – King Of The Yadav Clan
        public static double getEndTime(Tithi tithi)
        {

            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi nextTithi = currentTithi;
            do
            {
                utcDate = utcDate.AddHours(1);
                nextTithi = Tithi.getTithi(tithi.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber == currentTithi.TithiNumber);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextTithi = Tithi.getTithi(tithi.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber == currentTithi.TithiNumber);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextTithi = Tithi.getTithi(tithi.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber == currentTithi.TithiNumber);
            utcDate = utcDate.AddSeconds(-1);
            return DateTimeUtils.DateTimeToJDN(utcDate);
        }
        public static double getBeginTime(Tithi tithi)
        {
            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi prevTithi = currentTithi;
            do
            {
                utcDate = utcDate.AddHours(-1);
                prevTithi = Tithi.getTithi(tithi.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber == currentTithi.TithiNumber);
            utcDate = utcDate.AddHours(1);
            do
            {
                utcDate = utcDate.AddMinutes(-1);
                prevTithi = Tithi.getTithi(tithi.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber == currentTithi.TithiNumber);
            utcDate = utcDate.AddMinutes(1);
            do
            {
                utcDate = utcDate.AddSeconds(-1);
                prevTithi = Tithi.getTithi(tithi.Place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber == currentTithi.TithiNumber);
            utcDate = utcDate.AddSeconds(1);
            return DateTimeUtils.DateTimeToJDN(utcDate);
        }
    }
}
