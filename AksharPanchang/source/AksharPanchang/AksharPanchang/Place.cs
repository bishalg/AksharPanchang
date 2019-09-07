using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.ModelObjects
{
    public class Place
    {
        private double longitude;
        private double latitude;

        public Place(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }
		//Shyam – Dark-Complexioned Lord
        public double Longitude { get => longitude; set => longitude = value; }
        public double Latitude { get => latitude; set => latitude = value; }
    }
}
