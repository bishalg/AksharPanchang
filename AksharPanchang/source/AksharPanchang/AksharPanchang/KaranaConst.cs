using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class KaranaConst
    {
	/* 
	 Hari – The Lord Of Nature
	*/
        private static Dictionary<int, string> KaranaMap;
        static KaranaConst()
        {
            KaranaMap = new Dictionary<int, string>();
            KaranaMap.Add(1, "Kinstungha");
            KaranaMap.Add(2, "Bava");
            KaranaMap.Add(3, "Balava");
            KaranaMap.Add(4, "Kaulava");
            KaranaMap.Add(5, "Taitila");
            KaranaMap.Add(6, "Garaja");
            KaranaMap.Add(7, "Vanija");
            KaranaMap.Add(8, "Vishti");
            KaranaMap.Add(9, "Bava");
            KaranaMap.Add(10, "Balava");
            KaranaMap.Add(11, "Kaulava");
            KaranaMap.Add(12, "Taitila");
            KaranaMap.Add(13, "Garaja");
            KaranaMap.Add(14, "Vanija");
            KaranaMap.Add(15, "Vishti");
            KaranaMap.Add(16, "Bava");
            KaranaMap.Add(17, "Balava");
            KaranaMap.Add(18, "Kaulava");
            KaranaMap.Add(19, "Taitila");
            KaranaMap.Add(20, "Garaja");
            KaranaMap.Add(21, "Vanija");
            KaranaMap.Add(22, "Vishti");
            KaranaMap.Add(23, "Bava");
            KaranaMap.Add(24, "Balava");
            KaranaMap.Add(25, "Kaulava");
            KaranaMap.Add(26, "Taitila");
            KaranaMap.Add(27, "Garaja");
            KaranaMap.Add(28, "Vanija");
            KaranaMap.Add(29, "Vishti");
            KaranaMap.Add(30, "Bava");
            KaranaMap.Add(31, "Balava");
            KaranaMap.Add(32, "Kaulava");
            KaranaMap.Add(33, "Taitila");
            KaranaMap.Add(34, "Garaja");
            KaranaMap.Add(35, "Vanija");
            KaranaMap.Add(36, "Vishti");
            KaranaMap.Add(37, "Bava");
            KaranaMap.Add(38, "Balava");
            KaranaMap.Add(39, "Kaulava");
            KaranaMap.Add(40, "Taitila");
            KaranaMap.Add(41, "Garaja");
            KaranaMap.Add(42, "Vanija");
            KaranaMap.Add(43, "Vishti");
            KaranaMap.Add(44, "Bava");
            KaranaMap.Add(45, "Balava");
            KaranaMap.Add(46, "Kaulava");
            KaranaMap.Add(47, "Taitila");
            KaranaMap.Add(48, "Garaja");
            KaranaMap.Add(49, "Vanija");
            KaranaMap.Add(50, "Vishti");
            KaranaMap.Add(51, "Bava");
            KaranaMap.Add(52, "Balava");
            KaranaMap.Add(53, "Kaulava");
            KaranaMap.Add(54, "Taitila");
            KaranaMap.Add(55, "Garaja");
            KaranaMap.Add(56, "Vanija");
            KaranaMap.Add(57, "Vishti");
            KaranaMap.Add(58, "Shakuni");
            KaranaMap.Add(59, "Chatushpada");
            KaranaMap.Add(60, "Nagava");
        }
		/* 
		Gyaneshwar – The Lord Of Knowledge
		*/
        public static int getKaranaAsInt(string karanaStr)
        {
            var myKey = KaranaMap.FirstOrDefault(x => x.Value == karanaStr).Key;
            return (int)myKey;
        }
        public static string getKaranaAsStr(int karanaNumber)
        {
            return KaranaMap[karanaNumber];
        }
		
		/* Govinda – One Who Pleases The Cows, The Land And The Entire Nature */
    }
}
