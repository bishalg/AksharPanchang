using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Constants
{
    public class SamvatConst
    {

        private static Dictionary<int, string> SamvatMap;
        static SamvatConst()
        {
            SamvatMap = new Dictionary<int, string>();
            SamvatMap.Add(1, " Prabhava ");
            SamvatMap.Add(2, " Vibhava ");
            SamvatMap.Add(3, " Shukla ");
            SamvatMap.Add(4, " Pramod ");
            SamvatMap.Add(5, " Prajapati ");
            SamvatMap.Add(6, " Angira ");
            SamvatMap.Add(7, " Shrimukh ");
            SamvatMap.Add(8, " Bhav ");
            SamvatMap.Add(9, " Yuvaa ");
            SamvatMap.Add(10, " Dhata ");
            SamvatMap.Add(11, " Ishwar ");
            SamvatMap.Add(12, " Bahudhaanya ");
            SamvatMap.Add(13, " Pramathi ");
            SamvatMap.Add(14, " Vikram ");
            SamvatMap.Add(15, " Vishu ");
            SamvatMap.Add(16, " Chitrabhanu ");
            SamvatMap.Add(17, " Swabhanu ");
            SamvatMap.Add(18, " Taaran ");
            SamvatMap.Add(19, " Paarthiv ");
            SamvatMap.Add(20, " Vyay ");
            SamvatMap.Add(21, " Sarvajit ");
            SamvatMap.Add(22, " Sarvadhaari ");
            SamvatMap.Add(23, " Virodhi ");
            SamvatMap.Add(24, " Vikrati ");
            SamvatMap.Add(25, " Khar ");
            SamvatMap.Add(26, " Nandan ");
            SamvatMap.Add(27, " Vijay ");
            SamvatMap.Add(28, " Jay ");
            SamvatMap.Add(29, " Manmat ");
            SamvatMap.Add(30, " Durmukh ");
            SamvatMap.Add(31, " Hemlamb ");
            SamvatMap.Add(32, " Vilamb ");
            SamvatMap.Add(33, " Vikaari ");
            SamvatMap.Add(34, " Sharvari ");
            SamvatMap.Add(35, " Plav ");
            SamvatMap.Add(36, " Shubhkrit ");
            SamvatMap.Add(37, " Shobhan ");
            SamvatMap.Add(38, " Krodhi ");
            SamvatMap.Add(39, " Vishwavasu ");
            SamvatMap.Add(40, " Paraabhav ");
            SamvatMap.Add(41, " Plavang ");
            SamvatMap.Add(42, " Keelak ");
            SamvatMap.Add(43, " Saumya ");
            SamvatMap.Add(44, " Saadharan ");
            SamvatMap.Add(45, " Virodhkrit ");
            SamvatMap.Add(46, " Paridhaavi ");
            SamvatMap.Add(47, " Pramadi ");
            SamvatMap.Add(48, " Aanand ");
            SamvatMap.Add(49, " Raakshas ");
            SamvatMap.Add(50, " Nal ");
            SamvatMap.Add(51, " Pingal ");
            SamvatMap.Add(52, " Kaal ");
            SamvatMap.Add(53, " Siddharth ");
            SamvatMap.Add(54, " Raudri ");
            SamvatMap.Add(55, " Durmati ");
            SamvatMap.Add(56, " Dundubhi ");
            SamvatMap.Add(57, " Rudhirodgari ");
            SamvatMap.Add(58, " Raktaaksh ");
            SamvatMap.Add(59, " Krodhan ");
            SamvatMap.Add(60, " Akshay ");
        }
		//Vrishaparvaa – Lord Of Dharma
        public static int getSavatAsInt(string samvatStr)
        {
            var myKey = SamvatMap.FirstOrDefault(x => x.Value == samvatStr).Key;
            return (int)myKey;
        }
        public static string getSavatAsStr(int samvatNumber)
        {
            return SamvatMap[samvatNumber%60];
        }
    }
}
