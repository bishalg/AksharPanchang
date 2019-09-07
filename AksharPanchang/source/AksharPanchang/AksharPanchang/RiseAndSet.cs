using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;
using AksharPanchang.Constants;
namespace AksharPanchang.Panchang
{
    public class RiseAndSet
    {
        [DllImport("lib\\swedll64.dll")]
        public static extern int swe_rise_trans(double tjd_ut, int ipl, char[] starname, int epheflag,
            int rsmi, double[] geopos, double atpress, double attemp, double[] tret, char[] serr);
        [DllImport("lib\\swedll64.dll")]
        public static extern void swe_set_topo(double geolon, double geolat, double altitude);
        [DllImport("lib\\swedll64.dll")]
        public static extern double swe_julday(int year, int month, int day, double hour, int gregflag); /* Gregorian calendar: 1, Julian calendar: 0 */
       
        public static double getSunrise(Place place, double tjd)
        {
            swe_set_topo(place.Longitude, place.Latitude, 0);
            double[] geopos = new double[3];
            geopos[0] = place.Longitude;
            geopos[1] = place.Latitude;
            geopos[2] = 0;
            double dt = place.Longitude / 360.0;
            tjd = tjd - dt;
            int rsmi = RSMIConst.SE_CALC_RISE | RSMIConst.SE_BIT_HINDU_RISING;
            double[] datm = new double[2];
            double[] triseRef = new double[1];

            //double *ptrGeoPos = geopos; 
            char[] serr = new char[256];
            char[] starname = new char[256];
            int return_code;
            return_code = swe_rise_trans(tjd, PlanetConst.SE_SUN, starname, IFlagConst.SEFLG_SWIEPH, rsmi, geopos, datm[0], datm[1], triseRef, serr);

            if (return_code == -1)
                Console.WriteLine("Error");
            return triseRef[0];
        }
		//Vishnu – All Prevailing Lord
        public static double getSunset(Place place, double tjd)
        {
            swe_set_topo(place.Longitude, place.Latitude, 0);

            double[] geopos = new double[3];
            geopos[0] = place.Longitude;
            geopos[1] = place.Latitude;
            geopos[2] = 0;
            double dt = place.Longitude / 360.0;
            tjd = tjd - dt;

            int rsmi = RSMIConst.SE_CALC_SET | RSMIConst.SE_BIT_DISC_CENTER | RSMIConst.SE_BIT_NO_REFRACTION;
            double[] datm = new double[2];
            double[] tsetRef = new double[1];
            char[] serr = new char[256];
            char[] starname = new char[256];

            int return_code;
            return_code = swe_rise_trans(tjd, PlanetConst.SE_SUN, starname, IFlagConst.SEFLG_SWIEPH, rsmi, geopos, datm[0], datm[1], tsetRef, serr);

            if (return_code == -1)
                Console.WriteLine("Error");
            return tsetRef[0];
        }
        public static double getMoonrise(Place place, double tjd)
        {
            swe_set_topo(place.Longitude, place.Latitude, 0);

            double[] geopos = new double[3];
            geopos[0] = place.Longitude;
            geopos[1] = place.Latitude;
            geopos[2] = 0;
            double dt = place.Longitude / 360.0;
            tjd = tjd - dt;
            int rsmi = RSMIConst.SE_CALC_RISE | RSMIConst.SE_BIT_HINDU_RISING;
            double[] datm = new double[2];
            double[] triseRef = new double[1];

            //double *ptrGeoPos = geopos; 
            char[] serr = new char[256];
            char[] starname = new char[256];
            int return_code;
            return_code = swe_rise_trans(tjd, PlanetConst.SE_MOON, starname, IFlagConst.SEFLG_SWIEPH, rsmi, geopos, datm[0], datm[1], triseRef, serr);

            if (return_code == -1)
                Console.WriteLine("Error");

            return triseRef[0];
        }
		//Vishwadakshinah – Skilfull And Efficient Lord	
        public static double getMoonSet(Place place, double tjd)
        {
            swe_set_topo(place.Longitude, place.Latitude, 0);

            double[] geopos = new double[3];
            geopos[0] = place.Longitude;
            geopos[1] = place.Latitude;
            geopos[2] = 0;
            double dt = place.Longitude / 360.0;
            tjd = tjd - dt;

            int rsmi = RSMIConst.SE_CALC_SET | RSMIConst.SE_BIT_DISC_CENTER | RSMIConst.SE_BIT_NO_REFRACTION;
            double[] datm = new double[2];
            double[] tsetRef = new double[1];
            char[] serr = new char[256];
            char[] starname = new char[256];

            int return_code;
            return_code = swe_rise_trans(tjd, PlanetConst.SE_MOON, starname, IFlagConst.SEFLG_SWIEPH, rsmi, geopos, datm[0], datm[1], tsetRef, serr);

            if (return_code == -1)
                Console.WriteLine("Error");
            return tsetRef[0];
        }
    }
}
