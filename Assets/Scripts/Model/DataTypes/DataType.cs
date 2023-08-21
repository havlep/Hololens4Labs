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
        
        private string logID = string.Empty;

        public string Id { get => id; set => id = value; }
        public DateTime CreatedOn { get => dateTime; }
        public Scientist CreatedBy { get => scientist; set => scientist = value; }
        public Log DoneWithinLog { get => log; set => log = value; }
        public string CreatedById { get => scientist != null ? scientist.Id : scientistID == string.Empty ? throw new ObjectDataBaseException() : scientistID; }
      
        public string DoneWithinLogID { get => log != null ? log.Id : logID == string.Empty ? throw new ObjectDataBaseException() : logID; }

        public DataType(string dataTypeId, DateTime createdOn, string createdById, string doneWithinLogId)
        {

            this.dateTime = createdOn;
            this.id = dataTypeId;
            this.scientistID = createdById;
            
            this.logID = doneWithinLogId;

        }

        public DataType(DateTime createdOn, Scientist createdBy, Log doneWithinLog) { 
            this.dateTime = createdOn; 
            this.scientist = createdBy;
            this.log = doneWithinLog;
        }

        public DataType(Scientist createdBy, Log doneWithinLog): this(DateTime.Now, createdBy, doneWithinLog){ }

        public DataType(string dataTypeId, DateTime createdOn, Scientist createdBy, Log doneWithinLog): this(createdOn, createdBy, doneWithinLog)
        { this.id = dataTypeId; }

    }
}