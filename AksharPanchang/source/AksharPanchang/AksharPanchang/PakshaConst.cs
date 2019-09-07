using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
		
    public class PakshaConst
    {
        public const string SHUKLA_PAKSHA = " Shukla Paksha ";
        public const string KRISHNA_PAKSHA = " Krishna Paksha ";
        public static string getPakshaFromTithiNumber(int tithiNumber)
        {
            if (tithiNumber <= 15)
            {
                return SHUKLA_PAKSHA;
            }
            else
                return KRISHNA_PAKSHA;
        }
    }
}
