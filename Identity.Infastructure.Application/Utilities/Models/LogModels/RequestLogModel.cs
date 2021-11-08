using Newtonsoft.Json;
using System.Collections.Generic;

namespace Identity.Infastructure.Application.Utilities.Models.LogModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RequestLogModel
    {
        [JsonProperty]
        public string Method { get; set; }
        [JsonProperty]
        public string Path { get; set; }
        [JsonProperty]
        public string Query { get; set; }
        [JsonProperty]
        public string RequestTime { get; set; }
        [JsonProperty]
        public string Body { get; set; }
        [JsonProperty]
        public List<string> Headers { get; set; }
    }
}
