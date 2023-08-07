using Newtonsoft.Json;
using System;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class TextTypeDTO 
    {

        [JsonProperty("TextID")]
        public string TextID { get; set; }

        [JsonProperty("ScientistID")]
        public string ScientistID { get; set; }

        [JsonProperty("TextLogID")]
        public string TextLogID { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Creation")]
        public DateTimeOffset Created { get; set; }

    }
}