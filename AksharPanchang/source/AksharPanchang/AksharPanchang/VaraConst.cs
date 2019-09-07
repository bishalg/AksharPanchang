using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class VaraConst
    {
        public const int SOMWAAR = 1;
        public const int MANGALWAAR = 2;
        public const int BUDHWAAR = 3;
        public const int GURUWAAR = 4;
        public const int SHUKRWAAR = 5;
        public const int SHANIWAAR = 6;
        public const int RAVIWAAR = 7;

        public const string SOMWAAR_STR = " Somwaar ";
        public const string MANGALWAAR_STR = " Mangalwaar ";
        public const string BUDHWAAR_STR = " Budhwaar ";
        public const string GURUWAAR_STR = " Guruwaar ";
        public const string SHUKRWAAR_STR = " Shukrwaar ";
        public const string SHANIWAAR_STR = " Shaniwaar ";
        public const string RAVIWAAR_STR = " Raviwaar ";

        public static string getVaraAsString(int varaNumber)
        {
            switch (varaNumber)
            {
                case VaraConst.SOMWAAR: return VaraConst.SOMWAAR_STR;
                case VaraConst.MANGALWAAR: return VaraConst.MANGALWAAR_STR;
                case VaraConst.BUDHWAAR: return VaraConst.BUDHWAAR_STR;
                case VaraConst.GURUWAAR: return VaraConst.GURUWAAR_STR;
                case VaraConst.SHUKRWAAR: return VaraConst.SHUKRWAAR_STR;
                case VaraConst.SHANIWAAR: return VaraConst.SHANIWAAR_STR;
                case VaraConst.RAVIWAAR: return VaraConst.RAVIWAAR_STR;
            }
            return "";
        }
		//Yoginampati – Lord Of The Yogis
        public static int getVaraAsNumber(string varaStr)
        {
            switch (varaStr)
            {
                case VaraConst.SOMWAAR_STR: return VaraConst.SOMWAAR;
                case VaraConst.MANGALWAAR_STR: return VaraConst.MANGALWAAR;
                case VaraConst.BUDHWAAR_STR: return VaraConst.BUDHWAAR;
                case VaraConst.GURUWAAR_STR: return VaraConst.GURUWAAR;
                case VaraConst.SHUKRWAAR_STR: return VaraConst.SHUKRWAAR;
                case VaraConst.SHANIWAAR_STR: return VaraConst.SHANIWAAR;
                case VaraConst.RAVIWAAR_STR: return VaraConst.RAVIWAAR;
            }
            return 0;
        }
        public static string getVaraFromDayOfWeek(DayOfWeek weekday)
        {
            switch (weekday)
            {
                case DayOfWeek.Monday : return VaraConst.SOMWAAR_STR;
                case DayOfWeek.Tuesday: return VaraConst.MANGALWAAR_STR;
                case DayOfWeek.Wednesday: return VaraConst.BUDHWAAR_STR;
                case DayOfWeek.Thursday: return VaraConst.GURUWAAR_STR;
                case DayOfWeek.Friday: return VaraConst.SHUKRWAAR_STR;
                case DayOfWeek.Saturday: return VaraConst.SHANIWAAR_STR;
                case DayOfWeek.Sunday : return VaraConst.RAVIWAAR_STR;
            }
            return "";
        }
    }
}
