using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class ScientistDTO : TableEntity
    {
        public string Name { get; set; }
        public string ScientistID { get; set; }

    }
}
