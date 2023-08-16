using System;
using System.Collections;
using System.Collections.Generic;

using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model.Logs;


namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public abstract class DataType
    {

        private string id = string.Empty;
        private DateTime dateTime;
        private Log log = null;
        private Scientist scientist = null;
        private string scientistID = string.Empty;
        private string scientistName = null;
        private string logID = string.Empty;

        public string Id { get => id; set => id = value; }
        public DateTime DateTime { get => dateTime; }
        public Scientist CreatedBy { get => scientist; set => scientist = value; }
        public Log DoneWithinLog { get => log; set => log = value; }
        public string CreatedById { get => scientist != null ? scientist.Id : scientistID == string.Empty ? throw new ObjectDataBaseException() : scientistID; }
        public string CreatedByName { get => scientist != null ? scientist.Name : scientistName == null ? throw new ObjectDataBaseException() : scientistName; }
        public string DoneWithinLogID { get => log != null ? log.Id : logID == string.Empty ? throw new ObjectDataBaseException() : logID; }

        public DataType(string dataTypeId, DateTime creationTime, string createdById, string createdByName, string doneWithinLogId)
        {

            this.dateTime = creationTime;
            this.id = dataTypeId;
            this.scientistID = createdById;
            this.scientistName = createdByName;
            this.logID = doneWithinLogId;

        }

        public DataType(DateTime creationTime, Scientist createdBy, Log doneWithinLog) { 
            this.dateTime = creationTime; 
            this.scientist = createdBy;
            this.log = doneWithinLog;
        }

        public DataType(Scientist createdBy, Log doneWithinLog): this(DateTime.Now, createdBy, doneWithinLog){ }

        public DataType(string dataTypeId, DateTime creationTime, Scientist createdBy, Log doneWithinLog): this(creationTime, createdBy, doneWithinLog)
        { this.id = dataTypeId; }

    }
}