
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using System;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the log view
    /// </summary>
    public class TextLogViewController : LogViewController
    {

        [Header("UI Elements")]
        [SerializeField]
        protected TMP_InputField descriptionInputField = default;

        /// <summary>
        /// Initialize the view with the existing text log data
        /// </summary>
        /// <param name="log"> The exisignt log </param>
        /// <param name="parentObj"> The calling object </param>
        public void InitWithExisting(TextLog log, GameObject parentObj)
        {
            if (log.TextData != null)
            {
                lastModifiedLabel.SetText(log.TextData.CreatedOn.ToString("dd/MM/yyyy HH:mm:ss"));
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