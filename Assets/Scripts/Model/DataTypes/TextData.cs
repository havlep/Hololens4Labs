using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Model.DataTypes
{
    public class TextData: DataType
    {

        private string text = String.Empty;
        public string Text { get => text; }

        public TextData(Scientist createdBy, Experiment doneWithin, string textValue) : base(createdBy, doneWithin)
        {
            text = textValue;
        }

        public TextData( int id, DateTime dateTime, Scientist createdBy, Experiment doneWithin, string textValue ) : base(id, dateTime, createdBy, doneWithin )
        {
            text = textValue;
        }

    }
}
