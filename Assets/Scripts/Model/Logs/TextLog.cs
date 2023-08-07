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
                
        public TextData Text
        {
            get => text;
        }


        public TextLog(Scientist createdBy, Experiment doneWithin, TextData textData) : base(createdBy, doneWithin)
        {
            text = textData;
        }


        public TextLog(DateTime at, Scientist createdBy, Experiment doneWithin, TextData textData) : base(at,createdBy,doneWithin)
        {
            text = textData;
        }



        public TextLog(int id, DateTime at, Scientist createdBy, Experiment doneWithin, TextData textData) : base(id, at, createdBy, doneWithin)
        {
            text = textData;
        }
        
    }
}