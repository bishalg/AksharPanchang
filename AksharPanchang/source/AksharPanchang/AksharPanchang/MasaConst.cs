using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class MasaConst
    {
        public const int CHAITRA = 1;
        public const int VAISHAKH = 2;
        public const int JYESHTHA = 3;
        public const int ASHAADH = 4;
        public const int SHRAVAN = 5;
        public const int BADHRPAD = 6;
        public const int KUNWAAR = 7;
        public const int KARTIK = 8;
        public const int MARGHASHIRSHA = 9;
        public const int PUSHYA = 10;
        public const int MAGHA = 11;
        public const int PHALGUN = 12;
        public const int ADHIKAMASA = 13;

        public const string CHAITRA_STR = " Chaitra ";
        public const string VAISHAKH_STR = " Vaishakh ";
        public const string JYESHTHA_STR = " Jyeshtha ";
        public const string ASHAADH_STR = " Ashadh ";
        public const string SHRAVAN_STR = " Shravan / Saavan ";
        public const string BADHRPAD_STR = " Bhadrapad / Bhaadon ";
        public const string KUNWAAR_STR = " Aashwin/ Kunwar / Aashwayuja ";
        public const string KARTIK_STR = " Kartik ";
        public const string MARGHASHIRSHA_STR = " Marghshirsha / Agahan ";
        public const string PUSHYA_STR = " Pushya / Pos ";
        public const string MAGHA_STR = " Maagh ";
        public const string PHALGUN_STR = " Phalgun ";
        public const string ADHIKAMASA_STR = " Adhika ";
		//,, Sarveshwar – Lord Of All Gods
        public static string getMasaAsString(int masaNumber)
        {
            switch (masaNumber)
            {
                case MasaConst.CHAITRA: return MasaConst.CHAITRA_STR;
                case MasaConst.VAISHAKH: return MasaConst.VAISHAKH_STR;
                case MasaConst.JYESHTHA: return MasaConst.JYESHTHA_STR;
                case MasaConst.ASHAADH: return MasaConst.ASHAADH_STR;
                case MasaConst.SHRAVAN: return MasaConst.SHRAVAN_STR;
                case MasaConst.BADHRPAD: return MasaConst.BADHRPAD_STR;
                case MasaConst.KUNWAAR: return MasaConst.KUNWAAR_STR;
                case MasaConst.KARTIK: return MasaConst.KARTIK_STR;
                case MasaConst.MARGHASHIRSHA: return MasaConst.MARGHASHIRSHA_STR;
                case MasaConst.PUSHYA: return MasaConst.PUSHYA_STR;
                case MasaConst.MAGHA: return MasaConst.MAGHA_STR;
                case MasaConst.PHALGUN: return MasaConst.PHALGUN_STR;
                case MasaConst.ADHIKAMASA: return MasaConst.ADHIKAMASA_STR;
            }
            return "";
        }
        public static int getMasaAsNumber(string masaStr)
        {
            switch (masaStr)
            {
                case MasaConst.CHAITRA_STR: return MasaConst.CHAITRA;
                case MasaConst.VAISHAKH_STR: return MasaConst.VAISHAKH;
                case MasaConst.JYESHTHA_STR: return MasaConst.JYESHTHA;
                case MasaConst.ASHAADH_STR: return MasaConst.ASHAADH;
                case MasaConst.SHRAVAN_STR: return MasaConst.SHRAVAN;
                case MasaConst.BADHRPAD_STR: return MasaConst.BADHRPAD;
                case MasaConst.KUNWAAR_STR: return MasaConst.KUNWAAR;
                case MasaConst.KARTIK_STR: return MasaConst.KARTIK;
                case MasaConst.MARGHASHIRSHA_STR: return MasaConst.MARGHASHIRSHA;
                case MasaConst.PUSHYA_STR: return MasaConst.PUSHYA;
                case MasaConst.MAGHA_STR: return MasaConst.MAGHA;
                case MasaConst.PHALGUN_STR: return MasaConst.PHALGUN;
                case MasaConst.ADHIKAMASA_STR: return MasaConst.ADHIKAMASA;
            }
            return 0;
        }
    }
}
