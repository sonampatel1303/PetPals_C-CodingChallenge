using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    public abstract class Donation
    {
        // Attributes
        public string DonorName { get; set; }
        public decimal Amount { get; set; }

        // Constructor
        protected Donation(string donorName, decimal amount)
        {
            DonorName = donorName;
            Amount = amount;
        }

        // Abstract method to be implemented in derived classes
        public abstract void RecordDonation();
    }

}
