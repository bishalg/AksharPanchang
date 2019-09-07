using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.Panchang;
namespace AksharPanchang.Constants
{	
    public class Ritu
    {
		//Vishwakarma – Creator Of The Universe
        public static string getRitu(Masa masa)
        {
            return RituConst.getRituFromMasa(masa.Name);

        }
    }
}
