using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class RituConst
    {
        public const string VASANT_STR = " Vasant (Spring) ";
        public const string GRISHM_STR = " Grishm (Summer) ";
        public const string VARSHA_STR = " Varsha (Monsoon) ";
        public const string SHARAD_STR = " Sharad (Early Autumn) ";
        public const string HEMANT_STR = " Hemant (Late Autumn) ";
        public const string SHISHIR_STR = " Shishir (Winter/Snowy) ";
		//Vishwamurti – Of The Form Of The Entire Universe
        public static string getRituFromMasa(string masa)
        {
            switch (masa)
            {
                case MasaConst.CHAITRA_STR:
                case MasaConst.VAISHAKH_STR: return VASANT_STR;
                case MasaConst.JYESHTHA_STR:
                case MasaConst.ASHAADH_STR: return GRISHM_STR;
                case MasaConst.SHRAVAN_STR:
                case MasaConst.BADHRPAD_STR: return VARSHA_STR;
                case MasaConst.KUNWAAR_STR:
                case MasaConst.KARTIK_STR: return SHARAD_STR;
                case MasaConst.MARGHASHIRSHA_STR:
                case MasaConst.PUSHYA_STR: return HEMANT_STR;
                case MasaConst.MAGHA_STR:
                case MasaConst.PHALGUN_STR: return SHISHIR_STR;
            }
            return "";
        }
    }
}
