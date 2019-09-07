//Devesh – Lord Of The Lords
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;
using AksharPanchang.Utils;
using AksharPanchang.Constants;

namespace AksharPanchang.Panchang
{
    //Dharmadhyaksha – The Lord OF Dharma
	///Dwarkapati – Lord Of Dwarka
	public class Karana
    {

        private int karanaNum;
        private double JDN;
        private double nextJDN;
        private double prevJDN;

        public override string ToString()
        {
            string str = " "+ KaranaConst.getKaranaAsStr(karanaNum) + " ";
            return str;
        }

        
        public double NextJDN { get => nextJDN; set => nextJDN = value; }
        public double PrevJDN { get => prevJDN; set => prevJDN = value; }
        public int KaranaNum { get => karanaNum; set => karanaNum = value; }

        public Karana(double jDN)
        {
            JDN = jDN;
        }

        public Karana(int karanaNum)
        {
            this.karanaNum = karanaNum;
        }

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_ephe_path(string path);

        [DllImport("lib\\swedll64.dll")]
        public static extern long swe_calc_ut(double tjd_ut, int ipl, long iflag, double[] xx, char[] serr);

		///Gopal – One Who Plays With The Cowherds, The Gopas
        public static Karana getKarana(Place place, double tjd)
        {
            double JDN = tjd;
            swe_set_ephe_path("lib");
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
            moonLong = moonPos[0];
            if (moonLong < sunLong)
                moonLong = moonLong + 360;

            int karanaNumber = (int)Math.Ceiling((moonLong - sunLong) / 6);
            Karana karana = new Karana(JDN);
            if (karanaNumber < 0)
                karanaNumber = (karanaNumber + 60) % 60;
            if (karanaNumber == 0)
                karanaNumber = 60;
            if (karanaNumber > 60)
                karanaNumber = karanaNumber % 60;
            karana.KaranaNum = karanaNumber;
            return karana;
        }
        public static double getEndTime(Karana karana)
        {
            int karanaNumber = karana.KaranaNum;
            int newKarana = karanaNumber;
            int uptoKarana = karanaNumber + 1;
            if (uptoKarana > 60)
                uptoKarana = uptoKarana % 60;
            double nextJDN = karana.JDN;
            while (newKarana != uptoKarana)
            {
                nextJDN += DateTimeUtils.JD_INTERVAL;
                double[] sunPosUpto = new double[6];
                double[] moonPosUpto = new double[6];
                double sunLongUpto, moonLongUpto;
                long return_code3;
                char[] serr3 = new char[256];
                char[] serr4 = new char[256];

                return_code3 = swe_calc_ut(nextJDN, PlanetConst.SE_SUN, IFlagConst.SEFLG_SWIEPH, sunPosUpto, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                sunLongUpto = sunPosUpto[0];

                return_code3 = swe_calc_ut(nextJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPosUpto, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                moonLongUpto = moonPosUpto[0];
                if (moonLongUpto < sunLongUpto)
                    moonLongUpto = moonLongUpto + 360;
                newKarana = (int)Math.Ceiling((moonLongUpto - sunLongUpto) / 6);
                if (newKarana < 0)
                    newKarana = (newKarana + 60) % 60;
                if (newKarana == 0)
                    newKarana = 60;
                if (newKarana > 60)
                    newKarana = newKarana % 60;
            }
            return nextJDN;
        }
        public static double getEndTime1(Karana karana)
        {
            int karanaNumber = karana.KaranaNum;
            int nextKaranaNumber = karanaNumber +1;
            if (nextKaranaNumber > 60)
                nextKaranaNumber = nextKaranaNumber % 60;
            int uptoKarana = karanaNumber + 2;
            if (uptoKarana > 60)
                uptoKarana = uptoKarana % 60;
            double nextJDN = karana.JDN;
            while (nextKaranaNumber != uptoKarana)
            {
                nextJDN += DateTimeUtils.JD_INTERVAL;
                double[] sunPosUpto = new double[6];
                double[] moonPosUpto = new double[6];
                double sunLongUpto, moonLongUpto;
                long return_code3;
                char[] serr3 = new char[256];
                char[] serr4 = new char[256];

                return_code3 = swe_calc_ut(nextJDN, PlanetConst.SE_SUN, IFlagConst.SEFLG_SWIEPH, sunPosUpto, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                sunLongUpto = sunPosUpto[0];

                return_code3 = swe_calc_ut(nextJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPosUpto, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                moonLongUpto = moonPosUpto[0];
                if (moonLongUpto < sunLongUpto)
                    moonLongUpto = moonLongUpto + 360;
                nextKaranaNumber = (int)Math.Ceiling((moonLongUpto - sunLongUpto) / 6);
                if (nextKaranaNumber < 0)
                    nextKaranaNumber = (nextKaranaNumber + 60) % 60;
                if (nextKaranaNumber == 0)
                    nextKaranaNumber = 60;
                if (nextKaranaNumber > 60)
                    nextKaranaNumber = nextKaranaNumber % 60;
            }
            return nextJDN;
        }
        public static double getBeginTime(Karana karana)
        {
            int karanaNumber = karana.KaranaNum;
            int prevKaranaUpto = karanaNumber - 1;
            if (prevKaranaUpto == 0)
                prevKaranaUpto = 60;
            if(prevKaranaUpto < 0)
                prevKaranaUpto = (prevKaranaUpto + 60) % 60;
            int prevKarana = karanaNumber;
            double prevJDN = karana.JDN;
            while (prevKarana != prevKaranaUpto)
            {
                prevJDN -= DateTimeUtils.JD_INTERVAL;
                double[] sunPosUpto2 = new double[6];
                double[] moonPosUpto2 = new double[6];
                double sunLongUpto2, moonLongUpto2;
                long return_code3;
                char[] serr3 = new char[256];
                char[] serr4 = new char[256];

                return_code3 = swe_calc_ut(prevJDN, PlanetConst.SE_SUN, IFlagConst.SEFLG_SWIEPH, sunPosUpto2, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                sunLongUpto2 = sunPosUpto2[0];

                return_code3 = swe_calc_ut(prevJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPosUpto2, serr4);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                moonLongUpto2 = moonPosUpto2[0];
                if (moonLongUpto2 < sunLongUpto2)
                    moonLongUpto2 = moonLongUpto2 + 360;
                prevKarana = (int)Math.Ceiling((moonLongUpto2 - sunLongUpto2) / 6);
                if (prevKarana < 0)
                    prevKarana = (prevKarana + 60) % 60;
                if (prevKarana == 0)
                    prevKarana = 60;
                if (prevKarana > 60)
                    prevKarana = prevKarana % 60;
            }
            return prevJDN;
        }
        public static double getBeginTime1(Karana karana)
        {
            int karanaNumber = karana.KaranaNum;
            int prevKaranaUpto = karanaNumber - 2;
            if (prevKaranaUpto == 0)
                prevKaranaUpto = 60;
            if (prevKaranaUpto < 0)
                prevKaranaUpto = (prevKaranaUpto + 60) % 60;
            int prevKarana = karanaNumber-1;
            if (prevKarana == 0)
                prevKarana = 60;
            if (prevKarana < 0)
                prevKarana = (prevKarana + 60) % 60;
            double prevJDN = karana.JDN;
            while (prevKarana != prevKaranaUpto)
            {
                prevJDN -= DateTimeUtils.JD_INTERVAL;
                double[] sunPosUpto2 = new double[6];
                double[] moonPosUpto2 = new double[6];
                double sunLongUpto2, moonLongUpto2;
                long return_code3;
                char[] serr3 = new char[256];
                char[] serr4 = new char[256];

                return_code3 = swe_calc_ut(prevJDN, PlanetConst.SE_SUN, IFlagConst.SEFLG_SWIEPH, sunPosUpto2, serr3);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                sunLongUpto2 = sunPosUpto2[0];

                return_code3 = swe_calc_ut(prevJDN, PlanetConst.SE_MOON, IFlagConst.SEFLG_SWIEPH, moonPosUpto2, serr4);
                if (return_code3 == -1)
                {
                    Console.WriteLine("Error");
                }
                moonLongUpto2 = moonPosUpto2[0];
                if (moonLongUpto2 < sunLongUpto2)
                    moonLongUpto2 = moonLongUpto2 + 360;
                prevKarana = (int)Math.Ceiling((moonLongUpto2 - sunLongUpto2) / 6);
                if (prevKarana < 0)
                    prevKarana = (prevKarana + 60) % 60;
                if (prevKarana == 0)
                    prevKarana = 60;
                if (prevKarana > 60)
                    prevKarana = prevKarana % 60;
            }
            return prevJDN;
        }
		/* ,, Gopalpriya -  Lover Of Cowherd */
    }
}
