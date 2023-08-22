
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
        public async void SaveChanges()
        {
            // TODO - createdBy should be changed to the current user 
            Debug.Log($"Saving Log.");
            var textLog  = log as TextLog;
            textLog.TextData = new TextData(DateTime.Now, textLog.CreatedBy, textLog, descriptionInputField.text);

            SetButtonsInteractiveState(false);

            messageLabel.text = "Updating data, please wait ...";
            try
            {
                var success = await sceneController.DataManager.CreateOrUpdateLog(textLog);
                messageLabel.text = "Updated data in the database.";

            }
            catch (Exception e)
            {

                messageLabel.text = "Failed to update database." +  e.Message;
                throw e;
            }

            Debug.Log($"Log saved.");
            parentObject.SetActive(true);
            Destroy(gameObject);

        }
    }
}