using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model.Logs;
using System;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    /// <summary>
    /// Abstract class for all data types
    /// </summary>
    public abstract class DataType
    {

        /// <summary>
        /// The id of the data type in the database
        /// </summary>
        private string id = string.Empty;

        /// <summary>
        /// The timestamp of when the data type was created
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// The Log that the data type was created within
        /// </summary>
        private Log log = null;

        /// <summary>
        /// The Scientist that created the data type
        /// </summary>
        private Scientist scientist = null;

        /// <summary>
        /// The database id of the scientist that created the data type
        /// </summary>
        private string scientistID = string.Empty;

        /// <summary>
        /// The database id of the log that the data type was created within
        /// <summary>
        private string logID = string.Empty;

        /// <summary>
        /// Property for getting and setting the database id of the data type
        /// </summary>
        public string Id { get => id; set => id = value; }

        /// <summary>
        /// Property for getting and setting the timestamp of when the data type was created
        /// </summary>
        public DateTime CreatedOn { get => dateTime; }

        /// <summary>
        /// Property for getting and setting the Scientist that created the datatype
        /// </summary>
        public Scientist CreatedBy { get => scientist; set => scientist = value; }

        /// <summary>
        /// Property for getting and setting the Log that the data type was created within
        /// </summary>
        public Log DoneWithinLog { get => log; set => log = value; }

        /// <summary>
        /// Property for getting and setting the database id of the scientist that created the data type
        /// </summary>
        public string CreatedById { get => scientist != null ? scientist.Id : scientistID == string.Empty ? throw new ObjectDataBaseException() : scientistID; }

        /// <summary>
        /// Property for getting and setting the database id of the log that the data type was created within
        /// </summary>
        public string DoneWithinLogID { get => log != null ? log.Id : logID == string.Empty ? throw new ObjectDataBaseException() : logID; }

        ///<summary>
        /// Constructor used when the data type is in the database and the Scientist and Log don't exist as data model objects
        ///</summary>
        ///<param name="dataTypeId">The id of the data type in the database</param>
        ///<param name="createdOn">The timestamp of when the data type was created</param> 
        ///<param name="createdById">The id of the Scientist that created the data type</param>
        ///<param name="doneWithinLogId">The id of the Log that the data type was created within</param>
        public DataType(string dataTypeId, DateTime createdOn, string createdById, string doneWithinLogId)
        {

            this.dateTime = createdOn;
            this.id = dataTypeId;
            this.scientistID = createdById;

            this.logID = doneWithinLogId;

        }

        ///<summary>
        /// Constructor used when the data type is not yet in the database and the Scientist and Log exist as data model objects
        ///</summary>
        ///<param name="createdOn">The timestamp of when the data type was created</param>
        ///<param name="createdBy">The Scientist that created the data type</param>
        ///<param name="doneWithinLog">The Log that the data type was created within</param>
        public DataType(DateTime createdOn, Scientist createdBy, Log doneWithinLog)
        {
            this.dateTime = createdOn;
            this.scientist = createdBy;
            this.log = doneWithinLog;
        }

        ///<summary>
        /// Constructor used when to create a new data type at the current moment and the Scientist and Log exist as data model objects
        ///</summary>
        ///<param name="createdBy">The Scientist that created the data type</param> 
        ///<param name="doneWithinLog">The Log that the data type was created within</param>
        public DataType(Scientist createdBy, Log doneWithinLog) : this(DateTime.Now, createdBy, doneWithinLog) { }

        ///<summary>
        ///Constructor used when the data type is already in the database and the Scientist and Log exist as data model objects
        ///</summary>
        ///<param name="dataTypeId">The id of the data type in the database</param>
        ///<param name="createdOn">The timestamp of when the data type was created</param>
        ///<param name="createdBy">The Scientist that created the data type</param>
        ///<param name="doneWithinLog">The Log that the data type was created within</param>
        public DataType(string dataTypeId, DateTime createdOn, Scientist createdBy, Log doneWithinLog) : this(createdOn, createdBy, doneWithinLog)
        { this.id = dataTypeId; }

    }
}