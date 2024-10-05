using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    public class Pet
    {
        // Attributes
       // public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public bool IsAdopted {  get; set; }

        // Constructor
        public Pet()
        {

        }
        public Pet(string name, int age, string breed,bool adopted)
        {
            
            Name = name;
            Age = age;
            Breed = breed;
            IsAdopted = adopted;
        }

        // ToString() method to provide a string representation of the pet
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Breed: {Breed},Available for Adoption{IsAdopted}";
        }
    }

}

