using HoloLens4Labs.Scripts.Model.Logs;
using System;


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
        public TranscriptionData(DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(createdOn, createdBy, doneWithinLog)
        {

        }

        /// <summary>
        /// Constructor used when the TranscriptionData is already in the database and the Scientist and Log don't exist as data model objects
        /// </summary>
        /// <param name="id">The id of the TranscriptionData in the database</param>
        /// <param name="createdOn">The date that the TranscriptionData was create on</param>
        /// <param name="createdByID">The id of the Scientist who create the TranscripitonData</param>
        /// <param name="doneWithinLogID">The id of the Log that the TranscriptionData was created within</param>
        /// <param name="thumbnailBlobName">The blob name of the thumbnail in the blob repository</param>
        /// <param name="text">The textual representation of the transcription</param>

        public TranscriptionData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string thumbnailBlobName, string text) : base(id, createdOn, createdByID, doneWithinLogID, thumbnailBlobName)
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
        public TranscriptionData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog) : base(id, createdOn, createdBy, doneWithinLog)
        {

        }


    }

}
