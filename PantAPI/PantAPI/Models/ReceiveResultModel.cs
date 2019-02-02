using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class ReceiveResultModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ReceiveStatus Status { get; set; }
        public string BagId { get; internal set; }
    }

    public enum ReceiveStatus
    {
        OK = 1,
        UNKNOWN = 0
    }
}
