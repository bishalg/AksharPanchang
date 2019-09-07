///Ajay – The Conqueror Of Life And Death
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.ModelObjects
{
	//Akshara – Indestructible
    public class Ayanamsa
    {
        private int id;
        private string name;

        public Ayanamsa(int id)
        {
            this.id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
