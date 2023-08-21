using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class LogDTO : TableEntity
    {

        // RowKey is the Log ID
        public DateTime DateTime { get; set; }
        public string ExperimentID { get; set; }
        public string ScientistID { get; set; }

        public string TextLogID { get; set; }

        public string TextID { get; set; }

        public string DataScientistID { get; set; }

        public DateTime DataDateTime { get; set; }

        public string Text { get; set; }

        public string PhotoLogID { get; set; }
        public string TranscriptionLogID { get; set; }
        public string WeightLogID { get; set; }



    }
}
