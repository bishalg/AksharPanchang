///Aparajeet – Who Cannot Be Defeated
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;
using AksharPanchang.Constants;
using System.Runtime.InteropServices;

namespace AksharPanchang.Panchang
{
	///Avyukta – One Who Is As Clear As Crystal
    public class Grahan
    {
        private string description;
        private double beginJDN;
        private double endJDN;
        private double maxJDN;

        public double BeginJDN { get => beginJDN; set => beginJDN = value; }
        public double EndJDN { get => endJDN; set => endJDN = value; }
        public double MaxJDN { get => maxJDN; set => maxJDN = value; }
        public string Description { get => description; set => description = value; }

        public override string ToString()
        {
            return description;
        }

        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_topo(double geolon, double geolat, double altitude);

        [DllImport("lib\\swedll64.dll")]
        public static extern int swe_sol_eclipse_when_loc(double tjd_start, int ifalg, double[] geopos,
            double[] tret, double[] attr, bool backwardSearch, char[] serr);

        [DllImport("lib\\swedll64.dll")]
        public static extern int swe_lun_eclipse_when_loc(double tjd_start, int ifalg, double[] geopos,
            double[] tret, double[] attr, bool backwardSearch, char[] serr);
		
		
		///Balgopal – The Child Krishna, The All Attractive
        public static Grahan getGrahan(Place place, double jdn, string s_or_c)
        {
            swe_set_topo(place.Longitude, place.Latitude,0);
            double[] geopos = new double[3];
            geopos[0] = place.Longitude;
            geopos[1] = place.Latitude;
            geopos[2] = 0;

            double[] trise = new double[10];
            double[] attr = new double[10];
            double dt = place.Longitude / 360.0;
            bool backwardSearch = false;
            char[] serr = new char[256];

            int result = 0;

            if (s_or_c.Equals(GrahanConst.SURYA_GRAHAN))
                result = swe_sol_eclipse_when_loc(jdn, IFlagConst.SEFLG_SWIEPH, geopos, trise, attr, backwardSearch,
                    serr);
            else if (s_or_c.Equals(GrahanConst.CHANDRA_GRAHAN))
                result = swe_lun_eclipse_when_loc(jdn, IFlagConst.SEFLG_SWIEPH, geopos, trise, attr, backwardSearch,
                    serr);

            if (result == -1)
            {
                Console.WriteLine("Error");
            }

            Grahan grahan = new Grahan();
            if (s_or_c.Equals(GrahanConst.SURYA_GRAHAN))
            {
                grahan.beginJDN = trise[1];
                grahan.endJDN = trise[4];
                grahan.maxJDN = trise[0];
            }

            if (s_or_c.Equals(GrahanConst.CHANDRA_GRAHAN))
            {
                grahan.beginJDN = trise[6];
                grahan.endJDN = trise[7];
                grahan.maxJDN = trise[0];
            }

            if ((result & GrahanConst.SE_ECL_TOTAL) == GrahanConst.SE_ECL_TOTAL)
                grahan.description = s_or_c + GrahanConst.POORNA_GRAHAN;
            if ((result & GrahanConst.SE_ECL_PARTIAL) == GrahanConst.SE_ECL_PARTIAL)
                grahan.description = s_or_c + GrahanConst.AANSHIK_GRAHAN;

            return grahan;
        }
    }
}
