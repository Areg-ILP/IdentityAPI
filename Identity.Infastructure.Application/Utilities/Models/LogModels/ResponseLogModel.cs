using Newtonsoft.Json;
using System.Collections.Generic;

namespace Identity.Infastructure.Application.Utilities.Models.LogModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ResponseLogModel
    {
        [JsonProperty]
        public int StatusCode { get; set; }
        [JsonProperty]
        public string Body { get; set; }
        [JsonProperty]
        public List<string> Headers { get; set; }
    }
}
