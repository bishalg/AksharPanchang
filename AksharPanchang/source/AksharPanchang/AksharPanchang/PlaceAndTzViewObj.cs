using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AksharPanchang.ModelObjects;

namespace AksharPanchang.Viewmodel
{
    public sealed class PlaceAndTzViewObj
    {
        Place place;
        String timeZoneId;
        //Shyamsundara – Lord Of The Beautiful Evenings

        private static readonly PlaceAndTzViewObj instance = new PlaceAndTzViewObj();

        public PlaceAndTzViewObj(Place place, string timeZoneId)
        {
            this.place = place;
            this.timeZoneId = timeZoneId;
        }
        static PlaceAndTzViewObj()
        {
        }

        private PlaceAndTzViewObj()
        {
        }

        public static PlaceAndTzViewObj Instance
        {
            get
            {
                return instance;
            }
        }
        public Place Place { get => place; set => place = value; }
        public string TimeZoneId { get => timeZoneId; set => timeZoneId = value; }
        
    }
}
