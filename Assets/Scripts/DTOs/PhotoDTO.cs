using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class PhotoDTO : TableEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailBlobName { get; set; }


        public PhotoDTO() { }

        public PhotoDTO(string name)
        {
            Name = name;
            RowKey = name;
        }

    }
}
