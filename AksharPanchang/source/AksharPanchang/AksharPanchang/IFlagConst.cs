///Dayalu – Repositiory Of Compassion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public static class IFlagConst
    {
		/////,, Dayanidhi – The Compassionate Lord
        public const int SEFLG_JPLEPH = 1;                   // use JPL ephemeris;
        public const int SEFLG_SWIEPH = 2;                   // use SWISSEPH ephemeris, default;
        public const int SEFLG_MOSEPH = 4;                 // use Moshier ephemeris;
        public const int SEFLG_HELCTR = 8;                // return heliocentric position
        public const int SEFLG_TRUEPOS = 16;               // return true positions, not apparent
        public const int SEFLG_J2000 = 32;                // no precession, i.e. give J2000 equinox
        public const int SEFLG_NONUT = 64;                // no nutation, i.e. mean equinox of date
        public const int SEFLG_SPEED3 = 128;               // speed from 3 positions (do not use it, SEFLG_SPEED is
                                                           // faster and preciser.)
        public const int SEFLG_SPEED = 256;               // high precision speed (analyt. comp.)
        public const int SEFLG_NOGDEFL = 512;              // turn off gravitational deflection
        public const int SEFLG_NOABERR = 1024;             // turn off 'annual' aberration of light
        public const int SEFLG_ASTROMETRIC = (SEFLG_NOABERR | SEFLG_NOGDEFL); // astrometric positions
        public const int SEFLG_EQUATORIAL = 2048;            // equatorial positions are wanted
        public const int SEFLG_XYZ = 4096;            // cartesian, not polar, coordinates
        public const int SEFLG_RADIANS = 8192;             // coordinates in radians, not degrees
        public const int SEFLG_BARYCTR = 16384;          // barycentric positions	
        public const int SEFLG_TOPOCTR = (32 * 1024);    // topocentric positions	
        public const int SEFLG_SIDEREAL = (64 * 1024);    // sidereal positions
        public const int SEFLG_ICRS = (128 * 1024);  // ICRS (DE406 reference frame)
        public const int SEFLG_DPSIDEPS_1980 = (256 * 1024); /* reproduce JPL Horizons
                                      * 1962 - today to 0.002 arcsec. */
        public const int SEFLG_JPLHOR = SEFLG_DPSIDEPS_1980;
        public const int SEFLG_JPLHOR_APPROX = (512 * 1024);   /* approximate JPL Horizons 1962 - today */
    }
	
	///Devadidev – The God Of The Gods
	///Devakinandan – Son Of Mother Devaki
}
