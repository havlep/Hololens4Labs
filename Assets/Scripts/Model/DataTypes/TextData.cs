using System;
using HoloLens4Labs.Scripts.Model.Logs;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public class TextData: DataType
    {

        private string text = String.Empty;
        public string Text { get => text; }

        public TextData(DateTime createdOn, Scientist createdBy, Log doneWithinLog, string textValue) : base(createdOn, createdBy, doneWithinLog)
        {
            text = textValue;
        }

        public TextData(string id, DateTime createdOn, string createdByID, string doneWithinLogID, string textValue) : base(id, createdOn, createdByID, doneWithinLogID)
        {
            text = textValue;
        }

        public TextData( string id, DateTime createdOn, Scientist createdBy, Log doneWithinLog, string textValue ) : base(id, createdOn, createdBy, doneWithinLog )
        {
            text = textValue;
        }

    }
}
