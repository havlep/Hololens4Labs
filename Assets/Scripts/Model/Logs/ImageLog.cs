using System;
using HoloLens4Labs.Scripts.Model.DataTypes;

namespace HoloLens4Labs.Scripts.Model.Logs
{
    public class ImageLog : Log
    {

        public ImageData Data
        {
            get; set;
        }


        public ImageLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at, createdBy, doneWithin)
        { }

        public ImageLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        { }

        public ImageLog(string id, DateTime at, string createdByID, string doneWithinID, ImageData data) : base(id, at, createdByID, doneWithinID)
        {
            Data = data;

        }

        public ImageLog(Scientist createdBy, Experiment doneWithin) : base(createdBy, doneWithin)
        { }


    }
}
