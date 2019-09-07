using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class MuhurtaConst
    {
        public static int [] SHUBH = new int []{ 3, 5, 6, 7, 9, 13, 18, 19, 20, 22, 23, 24, 25, 27, 28, 30};
        public static int[] ATI_SHUBH = new int[] {26, 29 };
        public static int[] ASHUBH = new int[] { 1, 2, 4,10, 11, 12, 15, 16, 17, 21 };
        public const int RUDRA = 1;
        public const int AAHI = 2;
        public const int MITRA = 3;
        public const int PITRU = 4;
        public const int VASU = 5;
        public const int VARAH = 6;
        public const int VISHWEDEVA = 7;
        public const int VIDHI = 8;
        public const int SATAMUKHI = 9;
        public const int PURUHUT = 10;
        public const int VAHINI = 11;
        public const int NAKTANKARA = 12;
        public const int VARUN = 13;
        public const int ARYAMA = 14;
        public const int BHAG = 15;
        public const int GIRISH = 16;
        public const int AJAPAAD = 17;
        public const int AHIR_BUDHNYA = 18;
        public const int PUSHYA = 19;
        public const int ASHWINI = 20;
        public const int YAM = 21;
        public const int AGNI = 22;
        public const int VIDHATRU = 23;
        public const int KNAD = 24;
        public const int ADITI = 25;
        public const int JEEV_AMRIT = 26;
        public const int VISHNU = 27;
        public const int DYUMADGADYUTI = 28;
        public const int BRAHMA = 29;
        public const int SAMUDRAM = 30;

        public const string RUDRA_STR = " Rudra ";
        public const string AAHI_STR = " Aahi ";
        public const string MITRA_STR = " Mitra ";
        public const string PITRU_STR = " Pitru ";
        public const string VASU_STR = " Vasu ";
        public const string VARAH_STR = " Varah ";
        public const string VISHWEDEVA_STR = " Vishwedeva ";
        public const string VIDHI_STR = " Vidhi ";
        public const string SATAMUKHI_STR = " Satamukhi ";
        public const string PURUHUT_STR = " Puruhut " ;
        public const string VAHINI_STR = " Vahini ";
        public const string NAKTANKARA_STR = " Naktankara ";
        public const string VARUN_STR = " Varun ";
        public const string ARYAMA_STR = " Aryama ";
        public const string BHAG_STR = " Bhag ";
        public const string GIRISH_STR = " Girish ";
        public const string AJAPAAD_STR = " Ajapaad ";
        public const string AHIR_BUDHNYA_STR = "Ahir-Budhnya ";
        public const string PUSHYA_STR = " Pushya ";
        public const string ASHWINI_STR = " Ashwini " ;
        public const string YAM_STR = " Yama ";
        public const string AGNI_STR = " Agni ";
        public const string VIDHATRU_STR = " Vidhatru ";
        public const string KNAD_STR = " Knad ";
        public const string ADITI_STR = " Aditi ";
        public const string JEEV_AMRIT_STR = "Jeev / Amrit ";
        public const string VISHNU_STR = " Vishnu ";
        public const string DYUMADGADYUTI_STR = " Dyumadgadyuti ";
        public const string BRAHMA_STR = " Brahma ";
        public const string SAMUDRAM_STR = " Samudram ";

        public static string getMuhurtaAsString(int muhurtaNum)
        {
            switch (muhurtaNum)
            {
                case RUDRA: return RUDRA_STR;
                case AAHI: return AAHI_STR;
                case MITRA: return MITRA_STR;
                case PITRU: return PITRU_STR;
                case VASU: return VASU_STR;
                case VARAH: return VARAH_STR;
                case VISHWEDEVA: return VISHWEDEVA_STR;
                case VIDHI: return VIDHI_STR;
                case SATAMUKHI: return SATAMUKHI_STR;
                case PURUHUT: return PURUHUT_STR;
                case VAHINI: return VAHINI_STR;
                case NAKTANKARA: return NAKTANKARA_STR;
                case VARUN: return VARUN_STR;
                case ARYAMA: return ARYAMA_STR;
                case BHAG: return BHAG_STR;
                case GIRISH: return GIRISH_STR;
                case AJAPAAD: return AJAPAAD_STR;
                case AHIR_BUDHNYA: return AHIR_BUDHNYA_STR;
                case PUSHYA: return PUSHYA_STR;
                case ASHWINI: return ASHWINI_STR;
                case YAM: return YAM_STR;
                case AGNI: return AGNI_STR;
                case VIDHATRU: return VIDHATRU_STR;
                case KNAD: return KNAD_STR;
                case ADITI: return ADITI_STR;
                case JEEV_AMRIT: return JEEV_AMRIT_STR;
                case VISHNU: return VISHNU_STR;
                case DYUMADGADYUTI: return DYUMADGADYUTI_STR;
                case BRAHMA: return BRAHMA_STR;
                case SAMUDRAM: return SAMUDRAM_STR;
            }
            return "";
        }
		//Satyavachana – One Who Speaks Only The Truth
        public static int getMuhurtaAsInt(string muhurtaStr)
        {
            switch (muhurtaStr)
            {
                case RUDRA_STR: return RUDRA;
                case AAHI_STR: return AAHI;
                case MITRA_STR: return MITRA;
                case PITRU_STR: return PITRU;
                case VASU_STR: return VASU;
                case VARAH_STR: return VARAH;
                case VISHWEDEVA_STR: return VISHWEDEVA;
                case VIDHI_STR: return VIDHI;
                case SATAMUKHI_STR: return SATAMUKHI;
                case PURUHUT_STR: return PURUHUT;
                case VAHINI_STR: return VAHINI;
                case NAKTANKARA_STR: return NAKTANKARA;
                case VARUN_STR: return VARUN;
                case ARYAMA_STR: return ARYAMA;
                case BHAG_STR: return BHAG;
                case GIRISH_STR: return GIRISH;
                case AJAPAAD_STR: return AJAPAAD;
                case AHIR_BUDHNYA_STR: return AHIR_BUDHNYA;
                case PUSHYA_STR: return PUSHYA;
                case ASHWINI_STR: return ASHWINI;
                case YAM_STR: return YAM;
                case AGNI_STR: return AGNI;
                case VIDHATRU_STR: return VIDHATRU;
                case KNAD_STR: return KNAD;
                case ADITI_STR: return ADITI;
                case JEEV_AMRIT_STR: return JEEV_AMRIT;
                case VISHNU_STR: return VISHNU;
                case DYUMADGADYUTI_STR: return DYUMADGADYUTI;
                case BRAHMA_STR: return BRAHMA;
                case SAMUDRAM_STR: return SAMUDRAM;
            }
            return -1;
        }
    }
}
