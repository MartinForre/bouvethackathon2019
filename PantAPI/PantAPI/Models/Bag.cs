using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class Bag
    {
        public string BagId { get; set; }
        public string UserId { get; set; }
        public DateTime BagCreatedDate { get; set; }
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
