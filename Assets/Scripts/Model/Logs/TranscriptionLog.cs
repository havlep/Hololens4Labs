using HoloLens4Labs.Scripts.Model.DataTypes;
using System;



namespace HoloLens4Labs.Scripts.Model.Logs
{
    /// <summary>
    /// Log class for storing transcription data
    /// </summary>
    public class TranscriptionLog : Log
    {

        /// <summary>
        /// The transcription data of the log
        /// </summary>
        public TranscriptionData Data
        {
            get; set;
        }

        /// <summary>
        /// Constructor used when the TranscriptionLog is not yet in the database and the Scientist and Experiment exist as data model objects
        /// </summary>
        /// <param name="at">The timestamp of when the TranscriptionLog was created at</param>
        /// <param name="createdBy">The Scientist that created the TranscriptionLog</param>
        /// <param name="doneWithin">The Experiment that the TranscriptionLog was done within</param>
        public TranscriptionLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at, createdBy, doneWithin)
        { }

        /// <summary>
        /// Constructor used when the TranscriptionLog is already in the database and the Scientist and Experiment exist as data model objects
        /// </summary>  
        /// <param name="id">The id of the TranscriptionLog in the database</param>
        /// <param name="at">The timestamp of when the TranscriptionLog was created at</param>
        /// <param name="createdBy">The Scientist that created the TranscriptionLog</param>
        /// <param name="doneWithin">The Experiment that the TranscriptionLog was done within</param>
        public TranscriptionLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        { }

        /// <summary>
        /// Constructor used when the TranscriptionLog is already in the database and the Scientist and Experiment do not exist as data model objects at the moment
        /// </summary>  
        /// <param name="id">The Id of the TranscriptionLog in the database</param>
        /// <param name="at">The timestamp of when the TranscriptionLog was created at</param>
        /// <param name="createdByID">The id of the Scientist that created the TranscriptionLog</param>
        /// <param name="doneWithinID">The Experiment that the TranscriptionLog was done within</param>
        /// <param name="data">The transcription data held within the TranscripitonLog </param>
        public TranscriptionLog(string id, DateTime at, string createdByID, string doneWithinID, TranscriptionData data) : base(id, at, createdByID, doneWithinID)
        {
            Data = data;
        }

        /// <summary>
        /// Constructor used to create a new TranscriptionLog when the Scientist and Experiment exist as data model objects
        /// </summary>
        /// <param name="createdBy">The id of the Scientist that created the TranscriptionLog</param>
        /// <param name="doneWithin">The Experiment that the TranscriptionLog was done within</param>
        public TranscriptionLog(Scientist createdBy, Experiment doneWithin) : base(createdBy, doneWithin)
        { }

        ///<summary>
        /// Static method for getting the type name of the log
        ///</summary>
        public static new string GetTypeName()
        {
            return "Transcription";
        }

    }
}