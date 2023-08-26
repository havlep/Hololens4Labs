using HoloLens4Labs.Scripts.Model.Logs;
using System;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public class ImageData: DataType
    {

        public byte[] Data { get; set; }
        public Texture2D Texture { get; set; }

        public string ThumbnailBlobName { get; set; }

        public ImageData(DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(createdOn, createdBy, doneWithinLog)
        {
            
        }

        public ImageData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string thumbnailBlobName) : base(id, createdOn, createdByID, doneWithinLogID)
        {
            ThumbnailBlobName = thumbnailBlobName;
        }

        public ImageData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(id, createdOn, createdBy, doneWithinLog)
        {

        }
    }
}
