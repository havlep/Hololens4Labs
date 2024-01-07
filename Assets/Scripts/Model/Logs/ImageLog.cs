using System;
using HoloLens4Labs.Scripts.Model.DataTypes;

namespace HoloLens4Labs.Scripts.Model.Logs
{

    /// <summary>
    /// Log class for storing image data
    /// </summary>
    public class ImageLog : Log
    {

        /// <summary>
        /// The image data of the log
        /// </summary>
        public ImageData Data
        {
            get; set;
        }

        /// <summary>
        /// Constructor used when the ImageLog is not yet in the database and the Scientist and Experiment exist as data model objects
        /// </summary>
        /// <param name="at">The timestamp of when the ImageLog was created at</param>
        /// <param name="createdBy">The Scientist that created the ImageLog</param>
        /// <param name="doneWithin">The Experiment that the ImageLog was created within</param>
        public ImageLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at, createdBy, doneWithin)
        { }

        /// <summary>
        /// Constructor used when the ImageLog is already in the database and the Scientist and Experiment exist as data model objects
        /// </summary>
        /// <param name="id">The id of the ImageLog in the database</param>
        /// <param name="at">The timestamp of when the ImageLog was created at</param>
        /// <param name="createdBy">The Scientist that created the ImageLog</param>
        /// <param name="doneWithin">The Experiment that the ImageLog was created within</param>
        public ImageLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        { }

        /// <summary>
        /// Constructor used when the ImageLog is already in the database and the Scientist and Experiment do not exist as data model objects at the moment
        /// </summary>
        /// <param name="id">The Id of the ImageLog in the database</param>
        /// <param name="at">The timestamp of when the ImageLog was created at</param>
        /// <param name="createdByID">The id of the Scientist that created the ImageLog</param>
        /// <param name="doneWithinID">The Experiment that the ImageLog was created withi</param>
        /// <param name="data"></param>
        public ImageLog(string id, DateTime at, string createdByID, string doneWithinID, ImageData data) : base(id, at, createdByID, doneWithinID)
        {
            Data = data;

        }


        /// <summary>
        /// Static method for getting the type name of the Log
        /// </summary>
        public static new string GetTypeName()
        {
            return "Image";
        }

    }
}
