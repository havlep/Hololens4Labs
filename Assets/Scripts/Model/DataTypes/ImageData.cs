using HoloLens4Labs.Scripts.Model.Logs;
using System;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    /// <summary>
    /// Class that represents image data
    /// </summary>
    public class ImageData : DataType
    {

        /// <summary>
        /// The image data of the log
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// The thumbnail of the image data
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The blob name of the thumbnail in the blob repository 
        /// </summary>
        public string ThumbnailBlobName { get; set; }

        /// <summary>
        /// Constructor used when the ImageData is not yet in the database and scientist and log exist as data model objects
        /// </summary>
        /// <param name="createdOn">The timestamp of when the ImageData was created</param>
        /// <param name="createdBy">The Scientist that created the ImageData</param>
        /// <param name="doneWithinLog">The Log that the ImageData was created within</param>
        public ImageData(DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(createdOn, createdBy, doneWithinLog)
        {

        }

        /// <summary>
        /// Constructor used when the ImageData is already in the database and scientist and log don't exist as data model objects
        /// </summary>
        /// <param name="id">The id of the ImageData in the database</param>
        /// <param name="createdOn">The timestamp of when the ImageData was created</param>
        /// <param name="createdByID">The id of the Scientist that created the ImageData</param>
        /// <param name="doneWithinLogID">The id of the Log that the ImageData was created within</param>
        /// <param name="thumbnailBlobName">The blob name of the thumbnail in the blob repository</param>
        public ImageData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string thumbnailBlobName) : base(id, createdOn, createdByID, doneWithinLogID)
        {
            ThumbnailBlobName = thumbnailBlobName;
        }

        /// <summary>
        /// Constructor used when the ImageData is already in the database and scientist and log exist as data model objects
        /// </summary>
        /// <param name="id">The id of the ImageData in the database</param>
        /// <param name="createdOn">The timestamp of when the ImageData was created</param>
        /// <param name="createdBy">The Scientist that created the ImageData</param>
        /// <param name="doneWithinLog">The Log that the ImageData was created within</param>
        public ImageData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(id, createdOn, createdBy, doneWithinLog)
        {

        }
    }
}
