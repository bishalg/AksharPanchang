using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class RaashiConst
    {	
		//Vaikunthanatha – Lord Of Vaikuntha The Heavenly Abode

        public const int MESHA = 0;
        public const int VRISHABHA = 1;
        public const int MITHUNA = 2;
        public const int KARKA = 3;
        public const int SIMHA = 4;
        public const int KANYA = 5;
        public const int TULA = 6;
        public const int VRISHCHIKA = 7;
        public const int DHANU = 8;
        public const int MAKAR = 9;
        public const int KUMBH = 10;
        public const int MEEN = 11;

        public const string MESHA_STR = " Mesha (Aries) ";
        public const string VRISHABHA_STR = " Vrishabh (Taurus) ";
        public const string MITHUNA_STR = " Mithun (Gemini) ";
        public const string KARKA_STR = " Kark (Cancer) ";
        public const string SIMHA_STR = " Simha (Leo) ";
        public const string KANYA_STR = " Kanya (Virgo) ";
        public const string TULA_STR = " Tula (Libra) ";
        public const string VRISHCHIKA_STR = " Vrishchika (Scorpio) ";
        public const string DHANU_STR = " Dhanu (Sagittarius) ";
        public const string MAKAR_STR = " Makar (Capricorn) ";
        public const string KUMBH_STR = " Kumbh (Aquarius) ";
        public const string MEEN_STR = " Meen (Pisces) ";

        public static int getRaashiAsInt(string raashiName)
        {
            switch (raashiName)
            {
                case MESHA_STR: return MESHA;
                case VRISHABHA_STR: return VRISHABHA;
                case MITHUNA_STR: return MITHUNA;
                case KARKA_STR: return KARKA;
                case SIMHA_STR: return SIMHA;
                case KANYA_STR: return KANYA;
                case TULA_STR: return TULA;
                case VRISHCHIKA_STR: return VRISHCHIKA;
                case DHANU_STR: return DHANU;
                case MAKAR_STR: return MAKAR;
                case KUMBH_STR: return KUMBH;
                case MEEN_STR: return MEEN;
            }
            return 0;
        }
		//Vardhamaanah – The Formless Lord
        public static string getRaashiAsStr(int raashiNumber)
        {
            switch (raashiNumber)
            {
                case MESHA: return MESHA_STR;
                case VRISHABHA: return VRISHABHA_STR;
                case MITHUNA: return MITHUNA_STR;
                case KARKA: return KARKA_STR;
                case SIMHA: return SIMHA_STR;
                case KANYA: return KANYA_STR;
                case TULA: return TULA_STR;
                case VRISHCHIKA: return VRISHCHIKA_STR;
                case DHANU: return DHANU_STR;
                case MAKAR: return MAKAR_STR;
                case KUMBH: return KUMBH_STR;
                case MEEN: return MEEN_STR;
            }
            return "";
        }
		//Vasudev – All Prevailing Lord
    }
}
