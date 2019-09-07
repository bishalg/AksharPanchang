using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class TithiConst
    {
        public const int PRATHAMA = 1;
        public const int DWITIYA = 2;
        public const int TRITIYA = 3;
        public const int CHATURTHI = 4;
        public const int PANCHAMI = 5;
        public const int SHASHTHI = 6;
        public const int SAPTAMI = 7;
        public const int ASHTAMI = 8;
        public const int NAVMI = 9;
        public const int DASHMI = 10;
        public const int EKADASHI = 11;
        public const int DWADASHI = 12;
        public const int TRAYODASI = 13;
        public const int CHATURDASHI = 14;
        public const int POORNIMA = 15;
        public const int AMAVASYA = 30;

        public const string PRATHAMA_STR = " Prathama / Padyami /Padwa ";
        public const string DWITIYA_STR = " Dwitiya / Vidiya / Dooj ";
        public const string TRITIYA_STR = " Tritiya / Thadiya / Teej ";
        public const string CHATURTHI_STR = " Chaturthi / Chauth ";
        public const string PANCHAMI_STR = " Panchami ";
        public const string SHASHTHI_STR = " Shashthi / Chhath ";
        public const string SAPTAMI_STR = " Saptami / Saatam ";
        public const string ASHTAMI_STR = " Ashtami / Aatham ";
        public const string NAVMI_STR = " Navmi ";
        public const string DASHMI_STR = " Dashmi ";
        public const string EKADASHI_STR = " Ekadashi/Gyaaras ";
        public const string DWADASHI_STR = " Dwadashi/ Baaras ";
        public const string TRAYODASI_STR = " Trayodashi/ Teras ";
        public const string CHATURDASHI_STR = " Chaturdashi/Chaudas ";
        public const string POORNIMA_STR = " Poornima / Poonam ";
        public const string AMAVASYA_STR = " Amavasya ";

        public static string getTithiAsString(int tithiNumber)
        {
            if (tithiNumber == 15)
            {
                return TithiConst.POORNIMA_STR;
            }
            if (tithiNumber == 30)
            {
                return TithiConst.AMAVASYA_STR;
            }
            switch (tithiNumber % 15)
            {
                case TithiConst.PRATHAMA: return TithiConst.PRATHAMA_STR;
                case TithiConst.DWITIYA: return TithiConst.DWITIYA_STR;
                case TithiConst.TRITIYA: return TithiConst.TRITIYA_STR;
                case TithiConst.CHATURTHI: return TithiConst.CHATURTHI_STR;
                case TithiConst.PANCHAMI: return TithiConst.PANCHAMI_STR;
                case TithiConst.SHASHTHI: return TithiConst.SHASHTHI_STR;
                case TithiConst.SAPTAMI: return TithiConst.SAPTAMI_STR;
                case TithiConst.ASHTAMI: return TithiConst.ASHTAMI_STR;
                case TithiConst.NAVMI: return TithiConst.NAVMI_STR;
                case TithiConst.DASHMI: return TithiConst.DASHMI_STR;
                case TithiConst.EKADASHI: return TithiConst.EKADASHI_STR;
                case TithiConst.DWADASHI: return TithiConst.DWADASHI_STR;
                case TithiConst.TRAYODASI: return TithiConst.TRAYODASI_STR;
                case TithiConst.CHATURDASHI: return TithiConst.CHATURDASHI_STR;
                case TithiConst.POORNIMA: return TithiConst.POORNIMA_STR;
                case TithiConst.AMAVASYA: return TithiConst.AMAVASYA_STR;
            }
            return "";
        }
		//Yogi – The Supreme Master,, 
        public static int getTithiAsNumber(string tithiStr)
        {
            switch (tithiStr)
            {
                case TithiConst.PRATHAMA_STR: return TithiConst.PRATHAMA;
                case TithiConst.DWITIYA_STR: return TithiConst.DWITIYA;
                case TithiConst.TRITIYA_STR: return TithiConst.TRITIYA;
                case TithiConst.CHATURTHI_STR: return TithiConst.CHATURTHI;
                case TithiConst.PANCHAMI_STR: return TithiConst.PANCHAMI;
                case TithiConst.SHASHTHI_STR: return TithiConst.SHASHTHI;
                case TithiConst.SAPTAMI_STR: return TithiConst.SAPTAMI;
                case TithiConst.ASHTAMI_STR: return TithiConst.ASHTAMI;
                case TithiConst.NAVMI_STR: return TithiConst.NAVMI;
                case TithiConst.DASHMI_STR: return TithiConst.DASHMI;
                case TithiConst.EKADASHI_STR: return TithiConst.EKADASHI;
                case TithiConst.DWADASHI_STR: return TithiConst.DWADASHI;
                case TithiConst.TRAYODASI_STR: return TithiConst.TRAYODASI;
                case TithiConst.CHATURDASHI_STR: return TithiConst.CHATURDASHI;
                case TithiConst.POORNIMA_STR: return TithiConst.POORNIMA;
                case TithiConst.AMAVASYA_STR: return TithiConst.AMAVASYA;
            }
            return 0;
        }
    }
}
