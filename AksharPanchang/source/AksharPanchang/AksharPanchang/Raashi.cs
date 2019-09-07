using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.Constants;
using AksharPanchang.Utils;
using AksharPanchang.Viewmodel;

namespace AksharPanchang.Panchang
{
    public class Raashi
    {
     
        private int number;
        private double JD;
        private int degree;
        private int minute;
        private int sec;

        public int Number { get => number; set => number = value; }
        public int Degree { get => degree; set => degree = value; }
        public int Minute { get => minute; set => minute = value; }
        public int Sec { get => sec; set => sec = value; }

        public Raashi(double jD)
        {
            JD = jD;
        }

        public Raashi(int number)
        {
            this.number = number;
        }

        public Raashi(int number, int degree, int minute, int sec)
        {
            this.number = number;
            this.degree = degree;
            this.minute = minute;
            this.sec = sec;
        }

        public override string ToString()
        {
            string str = "Raashi : " + RaashiConst.getRaashiAsStr(this.number);
            return str;
        }
		//Upendra – Brother Of Indra
        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_ephe_path(string path);

        [DllImport("lib\\swedll64.dll")]
        public static extern long swe_calc_ut(double tjd_ut, int ipl, long iflag, double[] xx, char[] serr);

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_sid_mode(int sid_mode, double t0, double ayan_t0);

        [DllImport("lib\\swedll64.dll")]
        public static extern double swe_get_ayanamsa_ut(double tjd_ut);

        public static Raashi getRaashi(double tjd, int planet)
        {
            double JDN = tjd;
            swe_set_ephe_path("lib");
            swe_set_sid_mode(AyanamsaConst.getAyanamsaId(MainWindowViewObj.Instance.SelectedAyanamsa), 0, 0);
            double[] pos = new double[6];
            double longitude;
            double ayanamsa;
            long return_code;
            char[] serr = new char[256];
            return_code = swe_calc_ut(JDN, planet, IFlagConst.SEFLG_SWIEPH, pos, serr);
            if (return_code == -1)
            {
                Console.WriteLine("Error");
            }
            ayanamsa = swe_get_ayanamsa_ut(JDN);
            longitude = pos[0] - ayanamsa;
            DMS totDMS = PanchangUtil.deciToDeg(longitude);
            return PanchangUtil.getRaashi(totDMS);
        }
        
    }
   
}
