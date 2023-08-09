using System;
using System.Collections;
using System.Collections.Generic;

using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model.Logs;


namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public abstract class DataType
    {

        private int id = -1;
        private DateTime dateTime;
        private Log log = null;
        private Scientist scientist = null;
        private int scientistID = -1;
        private string scientistName = null;
        private int logID = -1;

        public int Id { get => id < 0 ? throw new ObjectDataBaseException() : id; }
        public DateTime DateTime { get => dateTime; }
        public Scientist CreatedBy { get => scientist; set => scientist = value; }
        public Log DoneWithinLog { get => log; set => log = value; }
        public int CreatedById { get => scientist != null ? scientist.Id : scientistID < 0 ? throw new ObjectDataBaseException() : scientistID; }
        public string CreatedByName { get => scientist != null ? scientist.Name : scientistName == null ? throw new ObjectDataBaseException() : scientistName; }
        public int DoneWithinLogID { get => log != null ? log.Id : logID == -1 ? throw new ObjectDataBaseException() : logID; }

        public DataType(int dataTypeId, DateTime creationTime, int createdById, string createdByName, int doneWithinLogId)
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

        public DataType(int dataTypeId, DateTime creationTime, Scientist createdBy, Log doneWithinLog): this(creationTime, createdBy, doneWithinLog)
        { this.id = dataTypeId; }

    }
}