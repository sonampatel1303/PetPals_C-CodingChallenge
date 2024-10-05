using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    public class Cat : Pet
    {
        // Additional Attributes
        public string CatColor { get; set; }

        // Constructor
        public Cat(string name, int age, string breed, string catColor,bool adopted)
            : base(name, age, breed,adopted)
        {
            CatColor = catColor;
        }

        // ToString() method to include CatColor
        public override string ToString()
        {
            return base.ToString() + $", Cat Color: {CatColor}";
        }
    }

}
