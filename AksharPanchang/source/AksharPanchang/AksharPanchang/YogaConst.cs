using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class YogaConst
    {

        public const int VISHKUMBHA = 1;
        public const int PREETI = 2;
        public const int AAYUSHMAAN = 3;
        public const int SAUBHAGYA = 4;
        public const int SHOBHANA = 5;
        public const int ATIGANDA = 6;
        public const int SUKARMA = 7;
        public const int DHRITI = 8;
        public const int SHOOLA = 9;
        public const int GANDA = 10;
        public const int VRIDDHI = 11;
        public const int DHRUVA = 12;
        public const int VYAGHATA = 13;
        public const int HARSHANA = 14;
        public const int VAJRA = 15;
        public const int SIDDHI = 16;
        public const int VYATIPATA = 17;
        public const int VARIYAN = 18;
        public const int PARIGHA = 19;
        public const int SHIVA = 20;
        public const int SIDDHA = 21;
        public const int SADHYA = 22;
        public const int SHUBHA = 23;
        public const int SHUKLA = 24;
        public const int BRAHMA = 25;
        public const int INDRA = 26;
        public const int VAIDHRITI = 27;

        public const string VISHKUMBHA_STR = " Vishkumbha ";
        public const string PREETI_STR = " Preeti ";
        public const string AAYUSHMAAN_STR = " Aayushmaan ";
        public const string SAUBHAGYA_STR = " Saubhagya ";
        public const string SHOBHANA_STR = " Shobhana ";
        public const string ATIGANDA_STR = " Atiganda ";
        public const string SUKARMA_STR = " Sukarma ";
        public const string DHRITI_STR = " Dhriti ";
        public const string SHOOLA_STR = " Shoola ";
        public const string GANDA_STR = " Ganda ";
        public const string VRIDDHI_STR = " Vriddhi ";
        public const string DHRUVA_STR = " Dhruva ";
        public const string VYAGHATA_STR = " Vyaghata ";
        public const string HARSHANA_STR = " Harshana ";
        public const string VAJRA_STR = " Vajra ";
        public const string SIDDHI_STR = " Siddhi ";
        public const string VYATIPATA_STR = " Vyatipata ";
        public const string VARIYAN_STR = " Variyan ";
        public const string PARIGHA_STR = " Parigha ";
        public const string SHIVA_STR = " Shiva ";
        public const string SIDDHA_STR = " Siddha ";
        public const string SADHYA_STR = " Sadhya ";
        public const string SHUBHA_STR = " Shubha ";
        public const string SHUKLA_STR = " Shukla ";
        public const string BRAHMA_STR = " Brahma ";
        public const string INDRA_STR = " Indra ";
        public const string VAIDHRITI_STR = " Vaidhriti ";

        public static string getYogaAsString(int yogaNumber)
        {
            switch (yogaNumber)
            {
                case VISHKUMBHA: return VISHKUMBHA_STR;
                case PREETI: return PREETI_STR;
                case AAYUSHMAAN: return AAYUSHMAAN_STR;
                case SAUBHAGYA: return SAUBHAGYA_STR;
                case SHOBHANA: return SHOBHANA_STR;
                case ATIGANDA: return ATIGANDA_STR;
                case SUKARMA: return SUKARMA_STR;
                case DHRITI: return DHRITI_STR;
                case SHOOLA: return SHOOLA_STR;
                case GANDA: return GANDA_STR;
                case VRIDDHI: return VRIDDHI_STR;
                case DHRUVA: return DHRUVA_STR;
                case VYAGHATA: return VYAGHATA_STR;
                case HARSHANA: return HARSHANA_STR;
                case VAJRA: return VAJRA_STR;
                case SIDDHI: return SIDDHI_STR;
                case VYATIPATA: return VYATIPATA_STR;
                case VARIYAN: return VARIYAN_STR;
                case PARIGHA: return PARIGHA_STR;
                case SHIVA: return SHIVA_STR;
                case SIDDHA: return SIDDHA_STR;
                case SADHYA: return SADHYA_STR;
                case SHUBHA: return SHUBHA_STR;
                case SHUKLA: return SHUKLA_STR;
                case BRAHMA: return BRAHMA_STR;
                case INDRA: return INDRA_STR;
                case VAIDHRITI: return VAIDHRITI_STR;

            }
            return "";
        }
        public static int getYogaAsInt(string yogaStr)
        {
            switch (yogaStr)
            {
                case VISHKUMBHA_STR: return VISHKUMBHA;
                case PREETI_STR: return PREETI;
                case AAYUSHMAAN_STR: return AAYUSHMAAN;
                case SAUBHAGYA_STR: return SAUBHAGYA;
                case SHOBHANA_STR: return SHOBHANA;
                case ATIGANDA_STR: return ATIGANDA;
                case SUKARMA_STR: return SUKARMA;
                case DHRITI_STR: return DHRITI;
                case SHOOLA_STR: return SHOOLA;
                case GANDA_STR: return GANDA;
                case VRIDDHI_STR: return VRIDDHI;
                case DHRUVA_STR: return DHRUVA;
                case VYAGHATA_STR: return VYAGHATA;
                case HARSHANA_STR: return HARSHANA;
                case VAJRA_STR: return VAJRA;
                case SIDDHI_STR: return SIDDHI;
                case VYATIPATA_STR: return VYATIPATA;
                case VARIYAN_STR: return VARIYAN;
                case PARIGHA_STR: return PARIGHA;
                case SHIVA_STR: return SHIVA;
                case SIDDHA_STR: return SIDDHA;
                case SADHYA_STR: return SADHYA;
                case SHUBHA_STR: return SHUBHA;
                case SHUKLA_STR: return SHUKLA;
                case BRAHMA_STR: return BRAHMA;
                case INDRA_STR: return INDRA;
                case VAIDHRITI_STR: return VAIDHRITI;
            }
            return 0;
        }
    }
}
