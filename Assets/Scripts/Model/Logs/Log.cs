using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Exceptions;

namespace HoloLens4Labs.Scripts.Model.Logs
{
    public abstract class Log 
    {
        private int id = -1;
        private DateTime dateTime;
        private Scientist scientist;
        private Experiment experiment;

        public DateTime DateTime { get => dateTime; }
        public Scientist CreatedBy { get => scientist; }
        public Experiment DoneWithin { get => experiment; }

        public int Id { get => id < 0 ? throw new ObjectDataBaseException() : id; }

        public Log(DateTime creationDateTime, Scientist createdBy, Experiment doneWithin) 
        {
            this.scientist = createdBy;
            this.experiment = doneWithin;
            this.dateTime = creationDateTime; 
        }
        public Log(Scientist createdBy, Experiment doneWithin) : this(DateTime.Now,createdBy,doneWithin) { }
        public Log(int logId, DateTime creationDateTime, Scientist createdBy, Experiment doneWithin) : this (creationDateTime, createdBy, doneWithin)
        {
            id = logId;
        }

    }
}