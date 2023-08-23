using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model;


namespace HoloLens4Labs.Scripts.Model.Logs
{
    public class ImageLog : Log
    {

        private ImageData image;

        public ImageData ImageData
        {
            get => image;
            set => image = value;
        }


        public ImageLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at, createdBy, doneWithin)
        { }

        public ImageLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        { }

        public ImageLog(string id, DateTime at, string createdByID, string doneWithinID, ImageData data) : base(id, at, createdByID, doneWithinID)
        {
            ImageData = data;

        }

        public ImageLog(Scientist createdBy, Experiment doneWithin) : base(createdBy, doneWithin)
        { }


    }
}
