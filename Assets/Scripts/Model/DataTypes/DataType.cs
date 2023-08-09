using System;
using System.Collections;
using System.Collections.Generic;

using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model;


namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public abstract class DataType
    {

        private int id = -1;
        private DateTime dateTime;
        private Experiment experiment = null;
        private Scientist scientist = null;
        private int scientistID = -1;
        private string scientistName = null;
        private int experimentID = -1;
        private string experimentName = null;

        public int Id { get => id < 0 ? throw new ObjectDataBaseException() : id; }
        public DateTime DateTime { get => dateTime; }
        public Scientist CreatedBy { get => scientist; set => scientist = value; }
        public Experiment DoneWithin { get => experiment; set => experiment = value; }
        public int CreatedById { get => scientist != null ? scientist.Id : scientistID < 0 ? throw new ObjectDataBaseException() : scientistID; }
        public int DoneWithinId { get => experiment != null ? experiment.Id : experimentID < 0 ? throw new ObjectDataBaseException() : experimentID; }
        public string CreatedByName { get => scientist != null ? scientist.Name : scientistName == null ? throw new ObjectDataBaseException() : scientistName; }
        public string DoneWithinName { get => experiment != null ? experiment.Name : experimentName == null ? throw new ObjectDataBaseException() : experimentName; }

        public DataType(int dataTypeId, DateTime creationTime, int createdById, string createdByName, int doneWithinId, string doneWithinName)
        {

            this.dateTime = creationTime;
            this.id = dataTypeId;
            this.scientistID = createdById;
            this.scientistName = createdByName;
            this.experimentID = doneWithinId;
            this.experimentName = doneWithinName;

        }

        public DataType(DateTime creationTime, Scientist createdBy, Experiment doneWithin) { 
            this.dateTime = creationTime; 
        }

        public DataType(Scientist createdBy, Experiment doneWithin): this(DateTime.Now, createdBy, doneWithin){ }

        public DataType(int dataTypeId, DateTime creationTime, Scientist createdBy, Experiment doneWithin): this(creationTime, createdBy, doneWithin)
        { this.id = dataTypeId; }

    }
}