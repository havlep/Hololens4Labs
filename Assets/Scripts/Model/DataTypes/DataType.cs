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
        private Experiment experiment;
        private Scientist scientist;

        public int Id { get => id < 0 ? throw new ObjectDataBaseException() : id; }
        public DateTime DateTime { get => dateTime; }
        public Scientist CreatedBy { get => scientist; }
        public Experiment DoneWithin { get => experiment; }

        public DataType(DateTime creationTime, Scientist createdBy, Experiment doneWithin) { 
            this.dateTime = creationTime; 
        }

        public DataType(Scientist createdBy, Experiment doneWithin): this(DateTime.Now, createdBy, doneWithin){ }

        public DataType(int DataTypeId, DateTime creationTime, Scientist createdBy, Experiment doneWithin): this(creationTime, createdBy, doneWithin)
        { this.id = DataTypeId; }

    }
}