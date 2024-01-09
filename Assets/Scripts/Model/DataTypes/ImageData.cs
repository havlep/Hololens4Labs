using HoloLens4Labs.Scripts.Model.Logs;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    /// <summary>
    /// Class that represents image data
    /// </summary>
    public class ImageData : DataType
    {

        private readonly Func<string,Task<byte[]>> getImageData;
        private Task<byte[]> data;


        /// <summary>
        /// The image data of the log
        /// </summary>
        public Task<byte[]> getData()
        {
            return data == null ? data = getImageData(this.Id) : data;
        }

        /// <summary>
        /// The thumbnail of the image data
        /// </summary>
        public Texture2D Texture { get; set; }


        /// <summary>
        /// Constructor used when the ImageData is not yet in the database and scientist and log exist as data model objects
        /// </summary>
        /// <param name="createdOn">The timestamp of when the ImageData was created</param>
        /// <param name="createdBy">The Scientist that created the ImageData</param>
        /// <param name="doneWithinLog">The Log that the ImageData was created within</param>
        /// <param name ="data">The byte array of the image data</param>
        /// <pa
        public ImageData(DateTime createdOn, Scientist createdBy, Log doneWithinLog, byte[] data, Texture2D texture) : base(createdOn, createdBy, doneWithinLog)
        {
            this.data = Task.FromResult(data);
            this.Texture = texture;
        }

        /// <summary>
        /// Constructor used when the ImageData is already in the database and scientist and log don't exist as data model objects
        /// </summary>
        /// <param name="id">The id of the ImageData in the database</param>
        /// <param name="createdOn">The timestamp of when the ImageData was created</param>
        /// <param name="createdByID">The id of the Scientist that created the ImageData</param>
        /// <param name="doneWithinLogID">The id of the Log that the ImageData was created within</param>
        public ImageData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, Func<string, Task<byte[]>> getImageData) : base(id, createdOn, createdByID, doneWithinLogID)
        {
            this.getImageData = getImageData;
        }

        /// <summary>
        /// Constructor used when the ImageData is already in the database and scientist and log exist as data model objects
        /// </summary>
        /// <param name="id">The id of the ImageData in the database</param>
        /// <param name="createdOn">The timestamp of when the ImageData was created</param>
        /// <param name="createdBy">The Scientist that created the ImageData</param>
        /// <param name="doneWithinLog">The Log that the ImageData was created within</param>
        public ImageData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog, Func<string, Task<byte[]>> getImageData) : base(id, createdOn, createdBy, doneWithinLog)
        {
            this.getImageData = getImageData;
        }
    }
}
