using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.Constants;
using AksharPanchang.ModelObjects;
using AksharPanchang.Utils;

namespace AksharPanchang.Panchang
{
    public class Masa
    {

        private string name;
        private double JDN;
        private bool isAdhika;

        public Masa(double jd)
        {
            this.JDN = jd;
        }

        public override string ToString()
        {
            string str = name;
            if (isAdhika)
                str += " (Adhika) ";
            return str;
        }

        public string Name { get => name; set => name = value; }

        [DllImport("lib\\swedll64.dll")]
        public static extern long swe_calc_ut(double tjd_ut, int ipl, long iflag, double[] xx, char[] serr);


        public static Masa getMasa(Place place, double JDN, string monthEndOption)
        {
            if(monthEndOption.Equals("P"))
                return getMasa(place, JDN);
            else
                return getMasaAmanta(place, JDN);
        }
		//Sarvapalaka – Protector Of All
        private static Masa getMasaAmanta(Place place, double JDN)
        {
            Tithi tithi = Tithi.getTithi(place, JDN);

            // last new moon
            double lastNewMoonJDN = getAmavasyaMonthBegin(place, tithi);

            // next new moon
            double nextNewMoonJDN = getAmavasyaMonthEnd(place, tithi);

            Raashi lastRaashi = Raashi.getRaashi(lastNewMoonJDN, PlanetConst.SE_SUN);
            Raashi transitRaashi = Raashi.getRaashi(nextNewMoonJDN, PlanetConst.SE_SUN);

            Masa masa = new Masa(JDN);
            bool isAdhika = false;
            if (lastRaashi.Number == transitRaashi.Number)
            {
                isAdhika = true;
            }
            int month = transitRaashi.Number + 1;
            /*if (tithi.Paksha == PakshaConst.KRISHNA_PAKSHA && !isAdhika)
                month++;*/
            if (isAdhika)
                month++;
            if (month > 12)
                month = month % 12;
            masa.name = MasaConst.getMasaAsString(month);
            masa.isAdhika = isAdhika;
            return masa;
        }
        private static Masa getMasa(Place place, double JDN)
        {
            Tithi tithi = Tithi.getTithi(place, JDN);

            // last new moon
            double lastNewMoonJDN = getAmavasyaMonthBegin(place, tithi);

            // next new moon
            double nextNewMoonJDN = getAmavasyaMonthEnd(place, tithi);
            Raashi lastRaashi = Raashi.getRaashi(lastNewMoonJDN, PlanetConst.SE_SUN);
            Raashi transitRaashi = Raashi.getRaashi(nextNewMoonJDN, PlanetConst.SE_SUN);

            Masa masa = new Masa(JDN);
            bool isAdhika = false;
            if (lastRaashi.Number == transitRaashi.Number)
            {
                isAdhika = true;
            }
            int month = transitRaashi.Number + 1;
            if (tithi.Paksha == PakshaConst.KRISHNA_PAKSHA && !isAdhika)
                month++;
            if (isAdhika)
                month++;
            if (month > 12)
                month = month % 12;
            masa.name = MasaConst.getMasaAsString(month);
            masa.isAdhika = isAdhika;
            return masa;
        }
        public static double getAmavasyaMonthBegin(Place place, Tithi tithi)
        {
            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi prevTithi = currentTithi;
            if (currentTithi.TithiNumber == 1)
                return Tithi.getBeginTime(currentTithi);
            do
            {
                utcDate = utcDate.AddDays(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 3 && prevTithi.TithiNumber != 2 && prevTithi.TithiNumber != 1);
            utcDate = utcDate.AddDays(1);
            do
            {
                utcDate = utcDate.AddHours(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 1);
            utcDate = utcDate.AddHours(1);
            do
            {
                utcDate = utcDate.AddMinutes(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 1);
            utcDate = utcDate.AddMinutes(1);
            do
            {
                utcDate = utcDate.AddSeconds(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 1);
            return Tithi.getBeginTime(prevTithi);
        }
        public static double getPoornimaMonthBegin(Place place, Tithi tithi)
        {
            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi prevTithi = currentTithi;
            if (currentTithi.TithiNumber == 1)
                return Tithi.getBeginTime(currentTithi);
            do
            {
                utcDate = utcDate.AddDays(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 18 && prevTithi.TithiNumber != 17 && prevTithi.TithiNumber != 16);
            utcDate = utcDate.AddDays(1);
            do
            {
                utcDate = utcDate.AddHours(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 16);
            utcDate = utcDate.AddHours(1);
            do
            {
                utcDate = utcDate.AddMinutes(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 16);
            utcDate = utcDate.AddMinutes(1);
            do
            {
                utcDate = utcDate.AddSeconds(-1);
                prevTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (prevTithi.TithiNumber != 16);
            return Tithi.getBeginTime(prevTithi);
        }

        public static double getAmavasyaMonthEnd(Place place, Tithi tithi)
        {
            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);

            Tithi currentTithi = tithi;
            Tithi nextTithi = currentTithi;
            if (currentTithi.TithiNumber == 30)
                return Tithi.getEndTime(currentTithi);
            do
            {
                utcDate = utcDate.AddDays(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 28 && nextTithi.TithiNumber != 29 && nextTithi.TithiNumber != 30);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 30);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 30);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 30);
            return Tithi.getEndTime(nextTithi);
        }
        public static double getPoornimaMonthEnd(Place place, Tithi tithi)
        {
            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi nextTithi = currentTithi;
            if (currentTithi.TithiNumber == 15)
                return Tithi.getEndTime(currentTithi);
            do
            {
                utcDate = utcDate.AddDays(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 13 && nextTithi.TithiNumber != 14 && nextTithi.TithiNumber != 15);
            utcDate = utcDate.AddDays(-1);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber !=15);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 15);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 15);
            return Tithi.getEndTime(nextTithi);
        }
        public static double getNextPoornima(Place place, Tithi tithi)
        {

            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi nextTithi = currentTithi;
            if (currentTithi.TithiNumber == 14)
                return Tithi.getEndTime(currentTithi);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 14);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 14);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 14);
            return Tithi.getEndTime(nextTithi);
        }
        public static double getNextAmavasya(Place place, Tithi tithi)
        {
            DateTime utcDate = DateTimeUtils.JDNToDateTime(tithi.TithiJDN);
            Tithi currentTithi = tithi;
            Tithi nextTithi = currentTithi;
            if (currentTithi.TithiNumber == 29)
                return Tithi.getEndTime(currentTithi);
            do
            {
                utcDate = utcDate.AddHours(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 29);
            utcDate = utcDate.AddHours(-1);
            do
            {
                utcDate = utcDate.AddMinutes(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 29);
            utcDate = utcDate.AddMinutes(-1);
            do
            {
                utcDate = utcDate.AddSeconds(1);
                nextTithi = Tithi.getTithi(place, DateTimeUtils.DateTimeToJDN(utcDate));
            } while (nextTithi.TithiNumber != 29);
            return Tithi.getEndTime(nextTithi);
        }
    }
}
