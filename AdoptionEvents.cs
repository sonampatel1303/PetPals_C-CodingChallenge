using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
   public class AdoptionEvents
    {
        public int EventID {  get; set; }
        public string EventName { get; set; }
        public DateTime EventDate{ get; set; }
        public string Venue {  get; set; }
        public AdoptionEvents() { }

        public AdoptionEvents(int id, string name, DateTime dateTime,string venue)
        {
            EventID = id;
            EventName = name;
            EventDate = dateTime;
            Venue = venue;
        }

        public override string ToString()
        {
            return $"Event name is {EventName} and date is {EventDate} and venue {Venue}";
        }

    }
}
