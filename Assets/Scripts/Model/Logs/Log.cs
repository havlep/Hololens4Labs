using System;


namespace HoloLens4Labs.Scripts.Model.Logs
{
    /// <summary>
    /// The Log data model class that represents logs
    /// </summary>
    public abstract class Log 
    {
        /// <summary>
        /// The Id of the log in the database
        /// </summary>
        private string id = string.Empty;

        /// <summary>
        /// The timestamp of when the log was created
        /// </summary>
        private DateTime creationDateTime;
   
        /// <summary>
        /// The scientist who created the log
        /// </summary>
        private Scientist scientist = null;

        /// <summary>
        /// The scientist id of the scientist who created the log
        /// </summary>
        private string scientistID = string.Empty;

        /// <summary>
        /// The experiment under which the log was created
        /// </summary>
        private Experiment experiment = null;

        /// <summary>
        /// The id of the experiment under which the log was created
        /// </summary>
        private string experimentID;

        /// <summary>
        /// The data the that the log was created on 
        /// </summary>
        public DateTime CreatedOn { get => creationDateTime; }

        /// <summary>
        /// The Scientist that created the log
        /// </summary>
        public Scientist CreatedBy { get => scientist; }

        /// <summary>   
        /// The id of the scientist that created the log
        /// </summary>
        public string CreatedByID { get => scientist != null ? scientist.Id : scientistID; }


        /// <summary>
        /// The Experiment that the log was created within
        /// </summary>
        public Experiment DoneWithin { get => experiment; }

        /// <summary>
        /// The ID of the Experiment that the log was created within 
        /// </summary>
        public string DoneWithinID { get => experiment != null ? experiment.Id : experimentID; }

        /// <summary>
        /// Property of getting the log id
        /// </summary>
        public string Id { get => id; set => id = value; }

        /// <summary>
        /// Constructor for creating a new log when scientist and experiment is known
        /// </summary>
        /// <param name="createdOn">The timestamp of when the log was created</param>
        /// <param name="createdBy">The Scientist who create the log</param>
        /// <param name="doneWithin">The Experiment that the log was created within</param>
        public Log(DateTime createdOn, Scientist createdBy, Experiment doneWithin) 
        {
            this.scientist = createdBy;
            this.experiment = doneWithin;
            this.creationDateTime = createdOn; 
        }

        /// <summary>
        /// Constructor fore creating a new log at the current time when scientist and experiment is known
        /// </summary>
        /// <param name="createdBy">The Scientist who create the log</param>
        /// <param name="doneWithin">The Experiment that the log was created within</param>
        public Log(Scientist createdBy, Experiment doneWithin) : this(DateTime.Now,createdBy,doneWithin) { }

        /// <summary>
        /// Constructor for creating a data model representation of a log that is already present in the database when scientist and experiment are known
        /// </summary>
        /// <param name="logId">The ID of the log in the database</param>
        /// <param name="createdOn">The timestamp of when the log was created</param>
        /// <param name="createdBy">The Scientist who create the log</param>
        /// <param name="doneWithin">The Experiment that the log was created within</param>
        public Log(string logId, DateTime createdOn, Scientist createdBy, Experiment doneWithin) : this (createdOn, createdBy, doneWithin)
        {
            id = logId;
        }

        /// <summary>
        /// Constructor for creating a data model representation of a log that is already present in the database when scientist id and experiment id are known
        /// </summary>
        /// <param name="logId">The ID of the log in the database</param>
        /// <param name="createdOn">The timestamp of when the log was created</param>
        /// <param name="createdByID">The ID of the Scientist who create the log</param>
        /// <param name="doneWithinID">The ID of the Experiment that the log was created within</param>
        public Log(string logId, DateTime createdOn, string createdByID, string doneWithinID)
        {
            id = logId;
            creationDateTime = createdOn;
            scientistID = createdByID;
            experimentID = doneWithinID;
        }


        public static String GetTypeName()
        {
            return "Log";
        }


    }
}