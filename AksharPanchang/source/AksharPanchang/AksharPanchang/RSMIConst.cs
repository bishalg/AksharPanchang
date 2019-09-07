using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
	//Vishwarupa – One Who Displays The Universal Form
    public static class RSMIConst
    {
        public const int SE_CALC_RISE = 1;

        public const int SE_CALC_SET = 2;

        public const int SE_CALC_MTRANSIT = 4;      /* upper meridian transit (southern for northern geo. latitudes) */

        public const int SE_CALC_ITRANSIT = 8;      /* lower meridian transit (northern, below the horizon) */

        /* the following bits can be added (or’ed) to SE_CALC_RISE or SE_CALC_SET */

        public const int SE_BIT_DISC_CENTER = 256;     /* for rising or setting of disc center */

        public const int SE_BIT_DISC_BOTTOM = 8192;    /* for rising or setting of lower limb of disc */

        public const int SE_BIT_GEOCTR_NO_ECL_LAT = 128; /* use topocentric position of object and ignore its ecliptic latitude */

        public const int SE_BIT_NO_REFRACTION = 512;     /* if refraction is not to be considered */

        public const int SE_BIT_CIVIL_TWILIGHT = 1024;    /* in order to calculate civil twilight */

        public const int SE_BIT_NAUTIC_TWILIGHT = 2048;    /* in order to calculate nautical twilight */

        public const int SE_BIT_ASTRO_TWILIGHT = 4096;   /* in order to calculate astronomical twilight */

        public const int SE_BIT_FIXED_DISC_SIZE = (16 * 1024); /* neglect the effect of distance on disc size */

        public const int SE_BIT_HINDU_RISING = (SE_BIT_DISC_CENTER | SE_BIT_NO_REFRACTION | SE_BIT_GEOCTR_NO_ECL_LAT);
    }
}
