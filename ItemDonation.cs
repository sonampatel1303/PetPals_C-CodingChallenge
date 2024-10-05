using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    public class ItemDonation : Donation
    {
        // Additional Attributes
        public string ItemType { get; set; }

        // Constructor
        public ItemDonation(string donorName, decimal amount, string itemType)
            : base(donorName, amount)
        {
            ItemType = itemType;
        }

        // Implementation of RecordDonation()
        public override void RecordDonation()
        {
            // Logic to record an item donation
            System.Console.WriteLine($"Item Donation: {ItemType} valued at {Amount} from {DonorName}");
        }
    }

}
