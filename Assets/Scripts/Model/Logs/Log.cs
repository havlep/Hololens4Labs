using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Exceptions;

namespace HoloLens4Labs.Scripts.Model.Logs
{
    public abstract class Log 
    {
        private string id = string.Empty;
        private DateTime creationDateTime;
   
        private Scientist scientist;
        private string scientistID;
        private Experiment experiment;
        private string experimentID;

        public DateTime CreatedOn { get => creationDateTime; }
        public Scientist CreatedBy { get => scientist; }

        public string CreatedByID { get => scientist != null ? scientist.Id : scientistID; }


        public Experiment DoneWithin { get => experiment; }
        public string DoneWithinID { get => experiment != null ? experiment.Id : experimentID; }

        public string Id { get => id; set => id = value; }

        public Log(DateTime createdOn, Scientist createdBy, Experiment doneWithin) 
        {
            this.scientist = createdBy;
            this.experiment = doneWithin;
            this.creationDateTime = createdOn; 
        }
        public Log(Scientist createdBy, Experiment doneWithin) : this(DateTime.Now,createdBy,doneWithin) { }
        public Log(string logId, DateTime createdOn, Scientist createdBy, Experiment doneWithin) : this (createdOn, createdBy, doneWithin)
        {
            id = logId;
        }

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