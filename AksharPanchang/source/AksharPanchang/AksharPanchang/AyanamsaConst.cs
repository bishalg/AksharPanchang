/// Amrut – One Who Is Sweet As Nectar
using AksharPanchang.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
	
    public class AyanamsaConst
    {

        public const int SE_SIDM_FAGAN_BRADLEY = 0;

        public const int SE_SIDM_LAHIRI = 1;

        public const int SE_SIDM_DELUCE = 2;

        public const int SE_SIDM_RAMAN = 3;

        public const int SE_SIDM_USHASHASHI = 4;

        public const int SE_SIDM_KRISHNAMURTI = 5;

        public const int SE_SIDM_DJWHAL_KHUL = 6;

        public const int SE_SIDM_YUKTESHWAR = 7;

        public const int SE_SIDM_JN_BHASIN = 8;

        public const int SE_SIDM_SURYASIDDHANTA = 21;

        public const int SE_SIDM_SURYASIDDHANTA_MSUN = 22;

        public const int SE_SIDM_ARYABHATA = 23;

        public const int SE_SIDM_ARYABHATA_MSUN = 24;

        public const int SE_SIDM_SS_REVATI = 25;

        public const int SE_SIDM_SS_CITRA = 26;

        public const int SE_SIDM_TRUE_CITRA = 27;

        public const int SE_SIDM_TRUE_REVATI = 28;

        public const int SE_SIDM_TRUE_PUSHYA = 29;
              
        public const int SE_SIDM_TRUE_MULA = 35;

        public const int SE_SIDM_ARYABHATA_522 = 37;

       
        public const string SE_SIDM_FAGAN_BRADLEY_STR = " Fagan Bradley ";

        public const string SE_SIDM_LAHIRI_STR = " Lahiri ";

        public const string SE_SIDM_DELUCE_STR = " Deluce ";

        public const string SE_SIDM_RAMAN_STR = " Raman ";

        public const string SE_SIDM_USHASHASHI_STR = " Usha Shashi ";

        public const string SE_SIDM_KRISHNAMURTI_STR = " Krishnamurti ";

        public const string SE_SIDM_DJWHAL_KHUL_STR = " DJWhal Khul ";

        public const string SE_SIDM_YUKTESHWAR_STR = " Yukteshwar ";

        public const string SE_SIDM_JN_BHASIN_STR = " JN Bhasin ";

        public const string SE_SIDM_SURYASIDDHANTA_STR = " Surya Siddhanta " ;

        public const string SE_SIDM_SURYASIDDHANTA_MSUN_STR = "Surya Siddhanta MSUN ";

        public const string SE_SIDM_ARYABHATA_STR = " Aryabhata ";

        public const string SE_SIDM_ARYABHATA_MSUN_STR = "Aryabhatta MSUN ";

        public const string SE_SIDM_SS_REVATI_STR = " Revati ";

        public const string SE_SIDM_SS_CITRA_STR = " Chitra ";

        public const string SE_SIDM_TRUE_CITRA_STR = "True Chitra ";

        public const string SE_SIDM_TRUE_REVATI_STR = " True Revati ";

        public const string SE_SIDM_TRUE_PUSHYA_STR = " True Pushya ";
        
        public const string SE_SIDM_TRUE_MULA_STR = " True Mula ";

        internal static int getAyanamsaId(object selectedAyanamsa)
        {
            throw new NotImplementedException();
        }

        public const string SE_SIDM_ARYABHATA_522_STR = " Aryabhata 522 ";
		
		/// Anaadih – One Who has no beginning
        public static int getAyanamsaId(string name)
        {
            switch (name)
            {
                case SE_SIDM_FAGAN_BRADLEY_STR : return SE_SIDM_FAGAN_BRADLEY;

                case SE_SIDM_LAHIRI_STR : return SE_SIDM_LAHIRI;

                case SE_SIDM_DELUCE_STR : return SE_SIDM_DELUCE;

                case SE_SIDM_RAMAN_STR : return SE_SIDM_RAMAN;

                case SE_SIDM_USHASHASHI_STR : return SE_SIDM_USHASHASHI;

                case SE_SIDM_KRISHNAMURTI_STR : return SE_SIDM_KRISHNAMURTI;

                case SE_SIDM_DJWHAL_KHUL_STR : return SE_SIDM_DJWHAL_KHUL;

                case SE_SIDM_YUKTESHWAR_STR : return SE_SIDM_YUKTESHWAR;

                case SE_SIDM_JN_BHASIN_STR : return SE_SIDM_JN_BHASIN;

                case SE_SIDM_SURYASIDDHANTA_STR : return SE_SIDM_SURYASIDDHANTA;

                case SE_SIDM_SURYASIDDHANTA_MSUN_STR : return SE_SIDM_SURYASIDDHANTA_MSUN;

                case SE_SIDM_ARYABHATA_STR : return SE_SIDM_ARYABHATA;

                case SE_SIDM_ARYABHATA_MSUN_STR : return SE_SIDM_ARYABHATA_MSUN;

                case SE_SIDM_SS_REVATI_STR : return SE_SIDM_SS_REVATI;

                case SE_SIDM_SS_CITRA_STR : return SE_SIDM_SS_CITRA;

                case SE_SIDM_TRUE_CITRA_STR : return SE_SIDM_TRUE_CITRA;

                case SE_SIDM_TRUE_REVATI_STR : return SE_SIDM_TRUE_REVATI;

                case SE_SIDM_TRUE_PUSHYA_STR : return SE_SIDM_TRUE_PUSHYA;

                case SE_SIDM_TRUE_MULA_STR : return SE_SIDM_TRUE_MULA;

                case SE_SIDM_ARYABHATA_522_STR : return SE_SIDM_ARYABHATA_522;
            }
            return SE_SIDM_LAHIRI;
        }
        public static string getAyanamsaName(int id)
        {
            switch(id)
            {
                case SE_SIDM_FAGAN_BRADLEY: return SE_SIDM_FAGAN_BRADLEY_STR;

                case SE_SIDM_LAHIRI: return SE_SIDM_LAHIRI_STR;

                case SE_SIDM_DELUCE: return SE_SIDM_DELUCE_STR;

                case SE_SIDM_RAMAN: return SE_SIDM_RAMAN_STR;

                case SE_SIDM_USHASHASHI: return SE_SIDM_USHASHASHI_STR;

                case SE_SIDM_KRISHNAMURTI: return SE_SIDM_KRISHNAMURTI_STR;

                case SE_SIDM_DJWHAL_KHUL: return SE_SIDM_DJWHAL_KHUL_STR;

                case SE_SIDM_YUKTESHWAR: return SE_SIDM_YUKTESHWAR_STR;

                case SE_SIDM_JN_BHASIN: return SE_SIDM_JN_BHASIN_STR;

                case SE_SIDM_SURYASIDDHANTA: return SE_SIDM_SURYASIDDHANTA_STR;

                case SE_SIDM_SURYASIDDHANTA_MSUN: return SE_SIDM_SURYASIDDHANTA_MSUN_STR;

                case SE_SIDM_ARYABHATA: return SE_SIDM_ARYABHATA_STR;

                case SE_SIDM_ARYABHATA_MSUN: return SE_SIDM_ARYABHATA_MSUN_STR;

                case SE_SIDM_SS_REVATI: return SE_SIDM_SS_REVATI_STR;

                case SE_SIDM_SS_CITRA: return SE_SIDM_SS_CITRA_STR;

                case SE_SIDM_TRUE_CITRA: return SE_SIDM_TRUE_CITRA_STR;

                case SE_SIDM_TRUE_REVATI: return SE_SIDM_TRUE_REVATI_STR;

                case SE_SIDM_TRUE_PUSHYA: return SE_SIDM_TRUE_PUSHYA_STR;

                case SE_SIDM_TRUE_MULA: return SE_SIDM_TRUE_MULA_STR;

                case SE_SIDM_ARYABHATA_522: return SE_SIDM_ARYABHATA_522_STR;
            }
            return SE_SIDM_LAHIRI_STR;
        }
        public static List<Ayanamsa> getAllAyanamsa()
        {
            List<Ayanamsa> ayanamsaList = new List<Ayanamsa>();
            Ayanamsa ayanamsa;
            ayanamsa = new Ayanamsa(SE_SIDM_FAGAN_BRADLEY);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_FAGAN_BRADLEY);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_LAHIRI);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_LAHIRI);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_DELUCE);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_DELUCE);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_RAMAN);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_RAMAN);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_USHASHASHI);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_USHASHASHI);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_KRISHNAMURTI);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_KRISHNAMURTI);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_DJWHAL_KHUL);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_DJWHAL_KHUL);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_YUKTESHWAR);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_YUKTESHWAR);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_JN_BHASIN);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_JN_BHASIN);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_SURYASIDDHANTA);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_SURYASIDDHANTA);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_SURYASIDDHANTA_MSUN);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_SURYASIDDHANTA_MSUN);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_ARYABHATA);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_ARYABHATA);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_ARYABHATA_MSUN);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_ARYABHATA_MSUN);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_SS_REVATI);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_SS_REVATI);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_SS_CITRA);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_SS_CITRA);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_TRUE_CITRA);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_TRUE_CITRA);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_TRUE_REVATI);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_TRUE_REVATI);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_TRUE_PUSHYA);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_TRUE_PUSHYA);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_TRUE_MULA);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_TRUE_MULA);
            ayanamsaList.Add(ayanamsa);

            ayanamsa = new Ayanamsa(SE_SIDM_ARYABHATA_522);
            ayanamsa.Name = getAyanamsaName(SE_SIDM_ARYABHATA_522);
            ayanamsaList.Add(ayanamsa);

            return ayanamsaList;
        }
    }
}
