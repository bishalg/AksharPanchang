/// Anantajit – Ever Victorious Lord
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Utils
{
	///Anaya – One Who Has No Leader
    public class DMS
    {
        private int degree;
        private int minute;
        private int sec;

        public DMS(int degree, int minute, int sec)
        {
            this.degree = degree;
            this.minute = minute;
            this.sec = sec;
        }

        public int Degree { get => degree; set => degree = value; }
        public int Minute { get => minute; set => minute = value; }
        public int Sec { get => sec; set => sec = value; }
    }
}
