using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class ExperimentDTO : TableEntity
    {
        public string Name { get; set; }
        public string ExperimentID { get; set; }
        public string ScientistsID { get; set; }

    }
}
