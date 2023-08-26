using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class LogDTO : TableEntity
    {

        // RowKey is the Log ID
        public DateTime DateTime { get; set; }
        public string ExperimentID { get; set; } = string.Empty;
        public string ScientistID { get; set; } = string.Empty;

        public string TextLogID { get; set; } = string.Empty;


        public string TextID { get; set; } = string.Empty;

        public string DataScientistID { get; set; } = string.Empty;

        public DateTime DataDateTime { get; set; }      

        public string Text { get; set; } = string.Empty;

        public string ImageLogID { get; set; } = string.Empty;

        public string ImageID { get; set; } = string.Empty;

        public string ThumbnailBlobName { get; set; } = string.Empty;

        public string TranscriptionLogID { get; set; } = string.Empty;
        public string WeightLogID { get; set; } = string.Empty;



    }
}
