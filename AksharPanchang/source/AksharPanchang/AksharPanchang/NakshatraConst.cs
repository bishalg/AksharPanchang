using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class NakshatraConst
    {


        public const int ASHWINI = 1;
        public const int BHARANI = 2;
        public const int KRITIKA = 3;
        public const int ROHINI = 4;
        public const int MRIGASHIRSHA = 5;
        public const int AARDRA = 6;
        public const int PUNARVASU = 7;
        public const int PUSHYA = 8;
        public const int ASHLESHA = 9;
        public const int MAGHA = 10;
        public const int PURVA_PHALGUNI = 11;
        public const int UTTARA_PHALGUNI = 12;
        public const int HASTA = 13;
        public const int CHITRA = 14;
        public const int SWATI = 15;
        public const int VISHAKHA = 16;
        public const int ANURADHA = 17;
        public const int JYESHTHA = 18;
        public const int MULA = 19;
        public const int PURVA_ASHADHA = 20;
        public const int UTTARA_ASHADHA = 21;
        public const int SHARAVAN = 22;
        public const int DHANISHTA = 23;
        public const int SHATABHISHA = 24;
        public const int PURVA_BHADRAPADA = 25;
        public const int UTTARA_BHADRAPADA = 26;
        public const int REVATI = 27;
        public const int ABHIJIT = 28;

        public const string ASHWINI_STR = " Ashwini ";
        public const string BHARANI_STR = " Bharani ";
        public const string KRITIKA_STR = " Kritika ";
        public const string ROHINI_STR = " Rohini ";
        public const string MRIGASHIRSHA_STR = " Margashirsha ";
        public const string AARDRA_STR = " Aardra ";
        public const string PUNARVASU_STR = " Punarvashu ";
        public const string PUSHYA_STR = " Pushya ";
        public const string ASHLESHA_STR = " Ashlesha ";
        public const string MAGHA_STR = " Magha ";
        public const string PURVA_PHALGUNI_STR = " Purva Phalguni ";
        public const string UTTARA_PHALGUNI_STR = " Uttara Phalguni ";
        public const string HASTA_STR = " Hasta ";
        public const string CHITRA_STR = " Chitra ";
        public const string SWATI_STR = " Swati ";
        public const string VISHAKHA_STR = " Vishakha ";
        public const string ANURADHA_STR = " Anuradha ";
        public const string JYESHTHA_STR = " Jyeshtha ";
        public const string MULA_STR = " Mula ";
        public const string PURVA_ASHADHA_STR = " Purva Ashadha ";
        public const string UTTARA_ASHADHA_STR = " Uttara Ashadha ";
        public const string SHARAVAN_STR = " Shravan ";
        public const string DHANISHTA_STR = " Dhanishta ";
        public const string SHATABHISHA_STR = " Shatabhisha ";
        public const string PURVA_BHADRAPADA_STR = " Purva Bhadrapada ";
        public const string UTTARA_BHADRAPADA_STR = " Uttara Bhadrapada ";
        public const string REVATI_STR = " Revati ";
        public const string ABHIJIT_STR = " Abhijit ";

        public static string getNakshatraAsString(int nakshatraNumber)
        {
            switch (nakshatraNumber)
            {
                case ASHWINI: return ASHWINI_STR;
                case BHARANI: return BHARANI_STR;
                case KRITIKA: return KRITIKA_STR;
                case ROHINI: return ROHINI_STR;
                case MRIGASHIRSHA: return MRIGASHIRSHA_STR;
                case AARDRA: return AARDRA_STR;
                case PUNARVASU: return PUNARVASU_STR;
                case PUSHYA: return PUSHYA_STR;
                case ASHLESHA: return ASHLESHA_STR;
                case MAGHA: return MAGHA_STR;
                case PURVA_PHALGUNI: return PURVA_PHALGUNI_STR;
                case UTTARA_PHALGUNI: return UTTARA_PHALGUNI_STR;
                case HASTA: return HASTA_STR;
                case CHITRA: return CHITRA_STR;
                case SWATI: return SWATI_STR;
                case VISHAKHA: return VISHAKHA_STR;
                case ANURADHA: return ANURADHA_STR;
                case JYESHTHA: return JYESHTHA_STR;
                case MULA: return MULA_STR;
                case PURVA_ASHADHA: return PURVA_ASHADHA_STR;
                case UTTARA_ASHADHA: return UTTARA_ASHADHA_STR;
                case SHARAVAN: return SHARAVAN_STR;
                case DHANISHTA: return DHANISHTA_STR;
                case SHATABHISHA: return SHATABHISHA_STR;
                case PURVA_BHADRAPADA: return PURVA_BHADRAPADA_STR;
                case UTTARA_BHADRAPADA: return UTTARA_BHADRAPADA_STR;
                case REVATI: return REVATI_STR;
                case ABHIJIT: return ABHIJIT_STR;
            }
            return "";
        }
		//Shreshta – The Most Glorious Lord
        public static int getNakshatraAsInt(string nakshatraStr)
        {
            switch (nakshatraStr)
            {
                case ASHWINI_STR: return ASHWINI;
                case BHARANI_STR: return BHARANI;
                case KRITIKA_STR: return KRITIKA;
                case ROHINI_STR: return ROHINI;
                case MRIGASHIRSHA_STR: return MRIGASHIRSHA;
                case AARDRA_STR: return AARDRA;
                case PUNARVASU_STR: return PUNARVASU;
                case PUSHYA_STR: return PUSHYA;
                case ASHLESHA_STR: return ASHLESHA;
                case MAGHA_STR: return MAGHA;
                case PURVA_PHALGUNI_STR: return PURVA_PHALGUNI;
                case UTTARA_PHALGUNI_STR: return UTTARA_PHALGUNI;
                case HASTA_STR: return HASTA;
                case CHITRA_STR: return CHITRA;
                case SWATI_STR: return SWATI;
                case VISHAKHA_STR: return VISHAKHA;
                case ANURADHA_STR: return ANURADHA;
                case JYESHTHA_STR: return JYESHTHA;
                case MULA_STR: return MULA;
                case PURVA_ASHADHA_STR: return PURVA_ASHADHA;
                case UTTARA_ASHADHA_STR: return UTTARA_ASHADHA;
                case SHARAVAN_STR: return SHARAVAN;
                case DHANISHTA_STR: return DHANISHTA;
                case SHATABHISHA_STR: return SHATABHISHA;
                case PURVA_BHADRAPADA_STR: return PURVA_BHADRAPADA;
                case UTTARA_BHADRAPADA_STR: return UTTARA_BHADRAPADA;
                case REVATI_STR: return REVATI;
                case ABHIJIT_STR: return ABHIJIT;
            }
            return 0;
        }
    }
}
