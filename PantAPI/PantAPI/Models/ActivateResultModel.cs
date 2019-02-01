using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class ActivateResultModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivativateStatus Status { get; set; }
        public string UserId { get; internal set; }
        public string BagId { get; internal set; }
    }

    public enum ActivativateStatus
    {
        Unknown =0,
        OK = 1,
        InUse = 2
    }
}
