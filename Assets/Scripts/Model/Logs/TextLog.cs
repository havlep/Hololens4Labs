using System;
using HoloLens4Labs.Scripts.Model.DataTypes;

namespace HoloLens4Labs.Scripts.Model.Logs
{
    /// <summary>
    /// A Log that contains text data
    /// </summary>
    public class TextLog : Log
    {

        /// <summary>
        /// The text data of the log
        /// </summary
        public TextData TextData
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor used when the TextLog is not yet in the database
        /// </summary>
        /// <param name="at">The timestamp of when the TextLog was created</param>
        /// <param name="createdBy">The Scientist who created the log</param>
        /// <param name="doneWithin">The Experiment under which the log was created</param>

        public TextLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at,createdBy,doneWithin)
        { }

        /// <summary>
        /// Constructor used when the TextLog is already in the database and the Scientist and Experiment exist as data model objects
        /// </summary>
        /// <param name="id">The id of the TextLog in the database</param>
        /// <param name="at">The timestamp of when the TextLog was created</param>
        /// <param name="createdBy">The Scientist who created the TextLog</param>
        /// <param name="doneWithin">The Experiment under which the TextLog was created</param>
        public TextLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        {}

        /// <summary>
        /// Constructor used when the TextLog is already in the database and the Scientist and Experiment do not exist as data model objects at the moment
        /// </summary>
        /// <param name="id">The id of the TextLog in the database</param>
        /// <param name="at">The timestamp of when the TextLog was created</param>
        /// <param name="createdByID">The id of the Scientist who created the TextLog</param>
        /// <param name="doneWithinID">The id of the Experiment under which the TextLog was created</param>
        /// <param name="data">The text data of the log</param>
        public TextLog(string id, DateTime at, string createdByID, string doneWithinID, TextData data) : base(id, at, createdByID, doneWithinID)
        { 
            TextData = data;
        
        }

        /// <summary>
        /// Constructor used to create a new TextLog when the Scientist and Experiment exist as data model objects
        /// </summary>
        /// <param name="createdBy">The Scientist who created the TextLog</param>
        /// <param name="doneWithin">The Experiment under which the TextLog was created</param>
        public TextLog(Scientist createdBy, Experiment doneWithin) : base(createdBy, doneWithin)
        { }

        /// <summary>
        /// Static method for getting the name of the Log
        /// </summary>
        public static new string GetTypeName()
        {
            return "Text";
        }


    }
}