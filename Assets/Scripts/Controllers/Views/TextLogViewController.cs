
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using System;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

namespace HoloLens4Labs.Scripts.Controllers
{
    public class TextLogViewController : LogViewController
    {

        [Header("UI Elements")]
        [SerializeField]
        protected TMP_InputField descriptionInputField = default;

        public void Init(TextLog log, GameObject parentObj)
        {
            if (log.TextData != null)
            {
                lastModifiedLabel.SetText(log.TextData.CreatedOn.ToShortTimeString());
                descriptionInputField.text = log.TextData.Text;
            }
            else
            {
                lastModifiedLabel.SetText(string.Empty);
                descriptionInputField.text = string.Empty;
            }
            base.Init(log, parentObj);
            
        }

        /// <summary>
        /// Save changes for the TextLogDTO into the azure table storage.
        /// </summary>
        override public void SaveChanges()
        {

            var textLog  = log as TextLog;
            textLog.TextData = new TextData(DateTime.Now, sceneController.CurrentUser, textLog, descriptionInputField.text);

            base.SaveChanges();

        }
    }
}