using HoloLens4Labs.Scripts.Model.Logs;
using System;
using System.Threading.Tasks;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    /// <summary>
    /// A data type that contains text data and image data for transcriptions
    /// </summary>
    public class TranscriptionData : ImageData
    {
        /// <summary>
        /// The textual representation of the transcription
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor used when the TranscriptionData is not yet in the database
        /// </summary>
        /// <param name="createdOn">The date that the TranscriptionData was create on</param>
        /// <param name="createdBy">The Scientist who create the transcripiton data</param>
        /// <param name="doneWithinLog">The Log that the TranscriptionData was created within</param>
        /// <param name="data">The image data that will be transcribed</param>
        /// <param name="texture">The texture of the image that will be transcribed</param>
        public TranscriptionData(DateTime createdOn, Scientist createdBy, Log doneWithinLog, byte[] data, Texture2D texture) : base(createdOn, createdBy, doneWithinLog, data, texture)
        {

        }

        /// <summary>
        /// Constructor used when transcription is done from an image
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="text"></param>
        public TranscriptionData(ImageData imageData, string text) : base(imageData.CreatedOn, imageData.CreatedBy,
            imageData.DoneWithinLog, imageData.getData().Result, imageData.Texture)
        {
            this.Text = text;
        }

        /// <summary>
        /// Constructor used when the TranscriptionData is already in the database and the Scientist and Log don't exist as data model objects
        /// </summary>
        /// <param name="id">The id of the TranscriptionData in the database</param>
        /// <param name="createdOn">The date that the TranscriptionData was create on</param>
        /// <param name="createdByID">The id of the Scientist who create the TranscripitonData</param>
        /// <param name="doneWithinLogID">The id of the Log that the TranscriptionData was created within</param>
        /// <param name="text">The textual representation of the transcription</param>

        public TranscriptionData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string text, Func<string, Task<byte[]>> getImageData) : base(id, createdOn, createdByID, doneWithinLogID, getImageData)
        {

            this.Text = text;
        }

        /// <summary>
        /// Constructor used when the TranscriptionData is already in the database and the Scientist and Log exist as data model objects
        /// </summary>
        /// <param name="id">The id of the TranscriptionData in the database</param>
        /// <param name="createdOn">The date that the TranscriptionData was create on</param>
        /// <param name="createdBy">The Scientist who create the transcripiton data</param>
        /// <param name="doneWithinLog">The Log that the TranscriptionData was created within</param>
        public TranscriptionData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog, Func<string, Task<byte[]>> getImageData) : base(id, createdOn, createdBy, doneWithinLog, getImageData)
        {

        }


    }

}
