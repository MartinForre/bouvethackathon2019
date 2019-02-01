using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace PantAPI.Models
{
    public class Bag : TableEntity
    {
        public Bag(string userId, string bagId) : base(userId, bagId)
        {
        }

        public Bag()
        {
        }

        public string BagId => RowKey;
        public string UserId => PartitionKey;
        public DateTime CreatedDate { get; set; }
        public DateTime? ActivatedDate { get; set; }

        public BagStatus Status { get; set; }
    }

    public enum BagStatus
    {
        Created = 0,
        Active = 1,
        Recieved = 2,
    }
}
