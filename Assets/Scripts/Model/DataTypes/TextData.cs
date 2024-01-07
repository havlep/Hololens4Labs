using HoloLens4Labs.Scripts.Model.Logs;
using System;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    /// <summary>
    /// Class that holds the text data of a Log
    /// </summary>
    public class TextData : DataType
    {

        /// <summary>
        /// Property for setting the text data of the log
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor used when creatign new TextData and scientist and log exist as data model objects
        /// </summary>
        /// <param name="createdOn">The timestamp of when the TextData was created</param>
        /// <param name="createdBy">The Scientist that created the TextData</param>
        /// <param name="doneWithinLog">The Log that the TextData was created within</param>
        /// <param name="textValue">The text data of the log</param>
        public TextData(DateTime createdOn, Scientist createdBy, Log doneWithinLog, string textValue) : base(createdOn, createdBy, doneWithinLog)
        {
            Text = textValue;
        }

        /// <summary>
        /// Constructor used when the TextData is already in the database and scientist and log don't exist as data model objects
        /// </summary>
        /// <param name="id">The databasde ID of the TextData</param>
        /// <param name="createdOn">The timestamp of when the TextData was created</param>
        /// <param name="createdByID">The ID of the Scientist that created the TextData</param>
        /// <param name="doneWithinLogID">The ID of the Log that the TextData was created within</param>
        /// <param name="textValue">The text data of the log</param>
        public TextData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string textValue) : base(id, createdOn, createdByID, doneWithinLogID)
        {
            Text = textValue;
        }

        /// <summary>
        /// Constructor used when the TextData is already in the database and scientist and log exist as data model objects
        /// </summary>
        /// <param name="id">The database ID of the TextData</param>
        /// <param name="createdOn">The timestamp of when the TextData was created</param>
        /// <param name="createdBy">The Scientist that created the TextData</param>
        /// <param name="doneWithinLog">The Log that the TextData was created within</param>
        /// <param name="textValue">The text data of the log</param>
        public TextData(string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog, string textValue) : base(id, createdOn, createdBy, doneWithinLog)
        {
            Text = textValue;
        }

    }
}
