using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class ReceiveModel
    {
        public string BagId { get; set; }
        public string Location { get; set; }
        public decimal Value { get; set; }
        public decimal Weight { get; set; }
    }
}
