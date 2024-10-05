using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    public class Dog : Pet
    {
        // Additional Attributes
        public string DogBreed { get; set; }

        // Constructor
        public Dog()
        {

        }
        public Dog(string name, int age, string breed, string dogBreed,bool adopted)
            : base(name, age, breed,adopted)
        {
            DogBreed = dogBreed;
        }

        // ToString() method to include DogBreed
        public override string ToString()
        {
            return base.ToString() + $", Dog Breed: {DogBreed}";
        }
    }

}
