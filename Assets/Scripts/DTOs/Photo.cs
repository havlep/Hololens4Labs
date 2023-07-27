using Microsoft.WindowsAzure.Storage.Table;


public class Photo : TableEntity{

        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailBlobName { get; set; }


        public Photo() { }
    
        public Photo(string name)
        {
           Name = name;
           RowKey = name;
         }

}

