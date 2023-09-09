using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model;


namespace HoloLens4Labs.Scripts.Model.Logs
{
    public class TextLog : Log
    {

        private TextData text;
                
        public TextData TextData
        {
            get => text;
            set => text = value;
        }


        public TextLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at,createdBy,doneWithin)
        { }

        public TextLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        {}

        public TextLog(string id, DateTime at, string createdByID, string doneWithinID, TextData data) : base(id, at, createdByID, doneWithinID)
        { 
            TextData = data;
        
        }

        public TextLog(Scientist createdBy, Experiment doneWithin) : base(createdBy, doneWithin)
        { }

        public static new string GetTypeName()
        {
            return "Text";
        }


    }
}