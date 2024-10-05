using Petpals.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Repository
{
   public interface IAdoptable
    {
       bool Adopt(int petid);
        void RegisterParticipant(string name, int age, string breed);
        public List<AdoptionEvents>GetUpcomingEvents();

    }
}
