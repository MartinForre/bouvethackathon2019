using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class BalanceResultModel
    {
        public decimal Balance { get; set; }
        public List<BagInfo> Details {get; set;}
    }

    public class BagInfo
    {
        public DateTime Registred { get; set; }
        public DateTime? Received { get; set; }
        public decimal Value { get; set; }
        public decimal? Weight { get; set; }
    }

}
