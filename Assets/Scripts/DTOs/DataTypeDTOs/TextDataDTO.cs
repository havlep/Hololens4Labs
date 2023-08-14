using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    public class TextDataDTO: TableEntity
    {

        public string TextDataID { get; set; }

        
        public string ScientistID { get; set; }

     
        public string TextLogID { get; set; }

   
        public string Text { get; set; }


        public DateTime Created { get; set; }

    }
}