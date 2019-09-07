using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.Utils;
using AksharPanchang.ModelObjects;
using AksharPanchang.Panchang;
using AksharPanchang.Constants;
namespace AksharPanchang.Panchang
{
    public class Samvat
    {
        private int samvatNum;
        private string name;
        public const double sidereal_year = 365.25636;

        public Samvat(int samvatNum, string name)
        {
            this.samvatNum = samvatNum;
            this.name = name;
        }

        public int SamvatNum { get => samvatNum; set => samvatNum = value; }
        public string Name { get => name; set => name = value; }
		//Vishwatma – Soul Of The Universe
        
        private static double daysSinceKaliYuga(double jd)
        {
            double days = jd - 588465.5;
            return days;
        }
        public static Samvat getSamvatSar(Place place, double jd, string monthEndOption)
        {
            double daysSinceKali = daysSinceKaliYuga(jd);
            Masa masa = Masa.getMasa(place, jd, monthEndOption);

            int kali = (int)((daysSinceKali + (4 - MasaConst.getMasaAsNumber(masa.Name)) * 30) / sidereal_year);

            int shaka = kali - 3179;

            int vikram = shaka + 135;

            int number = (vikram + 10);

            //Console.WriteLine(vikram);

            return new Samvat(vikram, SamvatConst.getSavatAsStr(number));
        }
    }
}
