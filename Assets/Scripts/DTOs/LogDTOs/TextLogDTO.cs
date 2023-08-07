using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class TextLogDTO : TableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        public TextLogDTO() { }

        public TextLogDTO(string name)
        {
            Name = name;
            RowKey = name;
        }
    }
}
