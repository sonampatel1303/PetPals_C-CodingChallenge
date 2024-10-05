using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    using System;

    public class CashDonation : Donation
    {
        // Additional Attributes
        public DateTime DonationDate { get; set; }

        // Constructor
        public CashDonation(string donorName, decimal amount, DateTime donationDate)
            : base(donorName, amount)
        {
            DonationDate = donationDate;
        }

        // Implementation of RecordDonation()
        public override void RecordDonation()
        {
            // Logic to record a cash donation
            System.Console.WriteLine($"Cash Donation: {Amount} from {DonorName} on {DonationDate}");
        }
    }

}
