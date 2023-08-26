using HoloLens4Labs.Scripts.Model.Logs;
using System;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public class TranscriptionData : DataType
    {
        public string Text { get; set; }

        public byte[] Data { get; set; }

        public Texture2D Texture { get; set; }

        public string ThumbnailBlobName { get; set; }

        public TranscriptionData(DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(createdOn, createdBy, doneWithinLog)
        {

        }

        public TranscriptionData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string thumbnailBlobName, string text) : base(id, createdOn, createdByID, doneWithinLogID)
        {
            this.ThumbnailBlobName = thumbnailBlobName;
            this.Text = text;
        }

        public TranscriptionData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(id, createdOn, createdBy, doneWithinLog)
        {

        }

        
    }
  
}
