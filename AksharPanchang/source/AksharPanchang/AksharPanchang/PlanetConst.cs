using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public static class PlanetConst
    {
        public const int SE_ECL_NUT = -1;
        public const int SE_SUN = 0;
        public const int SE_MOON = 1;
        public const int SE_MERCURY = 2;
        public const int SE_VENUS = 3;
        public const int SE_MARS = 4;
        public const int SE_JUPITER = 5;
        public const int SE_SATURN = 6;
        public const int SE_URANUS = 7;
        public const int SE_NEPTUNE = 8;
        public const int SE_PLUTO = 9;
        public const int SE_MEAN_NODE = 10;
        public const int SE_TRUE_NODE = 11;
        public const int SE_MEAN_APOG = 12;
        public const int SE_OSCU_APOG = 13;
        public const int SE_EARTH = 14;
        public const int SE_CHIRON = 15;
        public const int SE_PHOLUS = 16;
        public const int SE_CERES = 17;
        public const int SE_PALLAS = 18;
        public const int SE_JUNO = 19;
        public const int SE_VESTA = 20;
        public const int SE_INTP_APOG = 21;
        public const int SE_INTP_PERG = 22;
        public const int SE_NPLANETS = 23;
        public const int SE_FICT_OFFSET = 40;
        public const int SE_NFICT_ELEM = 15;
        public const int SE_AST_OFFSET = 10000;

        /* Hamburger or Uranian "planets" */
        public const int SE_CUPIDO = 40;
        public const int SE_HADES = 41;
        public const int SE_ZEUS = 42;
        public const int SE_KRONOS = 43;
        public const int SE_APOLLON = 44;
        public const int SE_ADMETOS = 45;
        public const int SE_VULKANUS = 46;
        public const int SE_POSEIDON = 47;

        /* other fictitious bodies */
        public const int SE_ISIS = 48;
        public const int SE_NIBIRU = 49;
        public const int SE_HARRINGTON = 50;
        public const int SE_NEPTUNE_LEVERRIER = 51;
        public const int SE_NEPTUNE_ADAMS = 52;
        public const int SE_PLUTO_LOWELL = 53;
        public const int SE_PLUTO_PICKERING = 54;
		/* 
		Sumedha – Intelligent Lord,, Suresham – Lord Of All Demi-Gods	*/
        public const string SUN_STR = " Surya (Sun) ";
        public const string MOON_STR = " Chandra (Moon) ";
        public const string MARS_STR = " Mangal (Mars) ";
        public const string MERCURY_STR = " Budh (Mercury) ";
        public const string JUPITER_STR = " Brihaspati/Guru (Jupiter) ";
        public const string VENUS_STR = " Shukr (Venus) ";
        public const string SATURN_STR = " Shani (Saturn) ";

        public const int RAHU = 11;
        public const string RAHU_STR = " Rahu ";
        public const int KETU = -99;
        public const string KETU_STR = " Ketu ";

        public const int DHOOM = 81;
        public const string DHOOM_STR = " Dhoom ";
        public const int VYATIPAT = 82;
        public const string VYATIPAT_STR = " Vyatipat ";
        public const int PARIVESH = 83;
        public const string PARIVESH_STR = " PARIVESH ";
        public const int CHAP = 84;
        public const string CHAP_STR = " CHAP ";
        public const int UPAKETU = 85;
        public const string UPAKETU_STR = " Upaketu ";

        public static string getPlanetName(int num)
        {
            switch (num)
            {
                case SE_SUN: return SUN_STR;
                case SE_MOON: return MOON_STR;
                case SE_MERCURY: return MERCURY_STR;
                case SE_VENUS: return VENUS_STR;
                case SE_MARS: return MARS_STR;
                case SE_JUPITER: return JUPITER_STR;
                case SE_SATURN: return SATURN_STR;
                case RAHU: return RAHU_STR;
                case KETU: return KETU_STR;

                case DHOOM: return DHOOM_STR;
                case VYATIPAT: return VYATIPAT_STR;
                case PARIVESH: return PARIVESH_STR;
                case CHAP: return CHAP_STR;
                case UPAKETU: return UPAKETU_STR;
            }
            return "";
        }
        public static int getPlanetNumber(string name)
        {
            switch (name)
            {
                case SUN_STR: return SE_SUN;
                case MOON_STR: return SE_MOON;
                case MERCURY_STR: return SE_MERCURY;
                case VENUS_STR: return SE_VENUS;
                case MARS_STR: return SE_MARS;
                case JUPITER_STR: return SE_JUPITER;
                case SATURN_STR: return SE_SATURN;
                case RAHU_STR: return RAHU;
                case KETU_STR: return KETU;

                case DHOOM_STR: return DHOOM;
                case VYATIPAT_STR: return VYATIPAT;
                case PARIVESH_STR: return PARIVESH;
                case CHAP_STR: return CHAP;
                case UPAKETU_STR: return UPAKETU;
            }
            return -1;
        }
    }
}
