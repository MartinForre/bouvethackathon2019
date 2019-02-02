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
        public double Value { get; set; }
        public double Weight { get; set; }
    }
}
