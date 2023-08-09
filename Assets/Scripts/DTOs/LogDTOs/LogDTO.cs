using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class LogDTO : TableEntity
    {

        public string LogID { get; set; }
        public string TimeStamp { get; set; }
        public string ExperimentID { get; set; }
        public string ScientistID { get; set; }

        public string TextLogID { get; set; }
        public string PhotoLogID { get; set; }
        public string TranscriptionLogID { get; set; }
        public string WeightLogID { get; set; }

    }
}
