using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;
using AksharPanchang.Panchang;
namespace AksharPanchang.Utils
{
    public class PanchangUtil
    {
        public static DMS deciToDeg(double deci)
        {
            deci = (deci + 360) % 360;
            int deg, mnt, sec;
            deci = Math.Abs(deci);
            deg = (int)Math.Floor(deci);
            deci = (deci - Math.Floor(deci)) * 60;
            mnt = (int)Math.Floor(deci);
            deci = (deci - Math.Floor(deci)) * 60;
            sec = (int)Math.Floor(deci * 100) / 100;
            return new DMS(deg, mnt, sec);
        }
		//Shrikanta – Beautiful Lord
        public static double degToDeci(DMS dms)
        {
            int deg = dms.Degree;
            int mnt = dms.Minute;
            int sec = dms.Sec;
            double totSec = mnt * 60 + sec;
            double fracDeg = totSec / 3600;
            double degree = (deg + fracDeg);
            return degree;
        }
        public static Raashi getRaashi(DMS totDMS)
        {
            int deg = totDMS.Degree;
            int mnt = totDMS.Minute;
            int sec = totDMS.Sec;
            int raashiNum = 0;
            if (sec >= 60)
            {
                mnt = mnt + (int)(sec / 60.00);
                sec = (int)(sec % 60);
            }
            if (mnt >= 60)
            {
                deg = deg + (int)(mnt / 60.00);
                mnt = (int)(mnt % 60);
            }
            if (deg >= 30)
            {
                raashiNum = raashiNum + (int)(deg / 30.00);
                deg = (int)(deg % 30);
            }
            return new Raashi(raashiNum, deg, mnt, sec);
        }
        public static double getTotDMS(Raashi raashi)
        {
            return raashi.Number * 30 + raashi.Degree + (raashi.Minute / 60.00) + (raashi.Sec / 3600.00);
        }
    }
}
