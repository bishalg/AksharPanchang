//Bali – The Lord Of Strength
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang
{
    public class GrahanConst
    {
		///Chaturbhuj – Four-Armed Lord
        public const string SURYA_GRAHAN = " Suyra Grahan (Solar Eclipse) ";
        public const string CHANDRA_GRAHAN = " Chandra Grahan (Lunar Eclipse) ";
        public const string AANSHIK_GRAHAN = " - Aanshik (Partial) ";
        public const string POORNA_GRAHAN = " - Poorna (Full) ";

        public const int SE_ECL_CENTRAL = 1;
        public const int SE_ECL_NONCENTRAL = 2;
        public const int SE_ECL_TOTAL = 4;
        public const int SE_ECL_ANNULAR = 8;
        public const int SE_ECL_PARTIAL = 16;
        public const int SE_ECL_ANNULAR_TOTAL = 32;
    }
}
