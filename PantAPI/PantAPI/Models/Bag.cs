using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace PantAPI.Models
{
    public class Bag : TableEntity
    {
        public Bag(string userId, string bagId) : base(userId, bagId)
        {
            CreatedDate = DateTime.UtcNow;
        }

        public Bag()
        {
        }

        public string BagId => RowKey;
        public string UserId => PartitionKey;
        public DateTime CreatedDate { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public DateTime? ReceiveDate { get; set; }

        public Decimal Value { get; set; } = 0;

        public Decimal? Weight { get; set; }

        public BagStatus Status { get; set; }
        public string ReceiveLocation { get; internal set; }
    }

    public enum BagStatus
    {
        Created = 0,
        Active = 1,
        Recieved = 2,
    }
}
