using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
namespace AksharPanchang.Panchang
{
	//Aniruddha – One Who Cannot Be Obstructed
    [DelimitedRecord("|")]
    public class EventNote
    {
        private string masa;
        private string paksha;
        private string tithi;
        private string vara;
        private string notes;

        public string Masa { get => masa; set => masa = value; }
        public string Paksha { get => paksha; set => paksha = value; }
        public string Tithi { get => tithi; set => tithi = value; }
        public string Vara { get => vara; set => vara = value; }
        public string Notes { get => notes; set => notes = value; }
    }
}
