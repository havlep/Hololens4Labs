using HoloLens4Labs.Scripts.Model.DataTypes;
using System;



namespace HoloLens4Labs.Scripts.Model.Logs
{
    public class TranscriptionLog : Log
    {


        public TranscriptionData Data
        {
            get; set;
        }
        public TranscriptionLog(DateTime at, Scientist createdBy, Experiment doneWithin) : base(at, createdBy, doneWithin)
        { }

        public TranscriptionLog(string id, DateTime at, Scientist createdBy, Experiment doneWithin) : base(id, at, createdBy, doneWithin)
        { }

        public TranscriptionLog(string id, DateTime at, string createdByID, string doneWithinID, TranscriptionData data) : base(id, at, createdByID, doneWithinID)
        {
          Data = data;
        }

        public TranscriptionLog(Scientist createdBy, Experiment doneWithin) : base(createdBy, doneWithin)
        { }

    }
}