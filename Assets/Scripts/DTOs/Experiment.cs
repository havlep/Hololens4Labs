using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class Experiment : TableEntity
    {
        public string Name { get; set; }

    }
}
