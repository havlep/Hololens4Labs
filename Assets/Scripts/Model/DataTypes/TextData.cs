using System;
using HoloLens4Labs.Scripts.Model.Logs;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public class TextData: DataType
    {

        private string text = String.Empty;
        public string Text { get => text; }

        public TextData(Scientist createdBy, Log doneWithinLog, string textValue) : base(createdBy, doneWithinLog)
        {
            text = textValue;
        }

        public TextData( int id, DateTime dateTime, Scientist createdBy, Log doneWithinLog, string textValue ) : base(id, dateTime, createdBy, doneWithinLog )
        {
            text = textValue;
        }

    }
}
