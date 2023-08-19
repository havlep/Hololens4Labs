using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Controllers
{
    public class TextLogViewController : MonoBehaviour
    {

        [Header("Managers")]
        [SerializeField]
        private SceneController sceneController;

        [Header("UI Elements")]
        [SerializeField]
        private TMP_Text logNameLabel = default;
        [SerializeField]
        private TMP_Text createdOnLabel = default;
        [SerializeField]
        private TMP_Text createdByLabel = default;
        [SerializeField]
        private TMP_Text lastModifiedLabel = default;

        [SerializeField]
        private TMP_Text messageLabel = default;
        [SerializeField]
        private TMP_InputField descriptionInputField = default;
        [SerializeField]
        private Interactable[] buttons = default;


        private TextLog textLog;


        private void Awake()
        {
            if (sceneController == null)
            {
                sceneController = FindObjectOfType<SceneController>();
            }
        }

        private void OnDisable()
        {
            sceneController.OpenMainMenu();
        }

        public void CloseCard()
        {
            sceneController.OpenMainMenu();
            Destroy(gameObject);
        }


        /// <summary>
        /// Init the menu with the given TextLogDTO.
        /// Should be called from the previous menu.
        /// </summary>
        /// <param name="source">TextLogDTO source</param>
        public void Init(TextLog source)
        {

            textLog = source;
            logNameLabel.SetText(textLog.Id);
            createdOnLabel.SetText(textLog.CreatedOn.ToShortTimeString());
            createdByLabel.SetText(textLog.CreatedBy.Name);
            if (textLog.TextData != null)
            {
                lastModifiedLabel.SetText(textLog.TextData.CreatedOn.ToShortTimeString());
                descriptionInputField.text = textLog.TextData.Text;
            }
            else {
                lastModifiedLabel.SetText(string.Empty);
                descriptionInputField.text = string.Empty;
            }
            SetButtonsInteractiveState(true);

        }

        /// <summary>
        /// Save changes for the TextLogDTO into the azure table storage.
        /// </summary>
        public async void SaveChanges()
        {
            // TODO - createdBy should be changed to the current user 
            textLog.TextData = new TextData(DateTime.Now, textLog.CreatedBy, textLog, descriptionInputField.text);

            SetButtonsInteractiveState(false);

            messageLabel.text = "Updating data, please wait ...";
            try
            {
                var success = await sceneController.DataManager.CreateOrUpdateLog(textLog);
                messageLabel.text = "Updated data in the database.";

            }
            catch (Exception e)  { 
            
                messageLabel.text = "Failed to update database." +  e.Message;
            }
     

            SetButtonsInteractiveState(true);

        }

        private void SetButtonsInteractiveState(bool state)
        {
            foreach (var interactable in buttons)
            {
                interactable.IsEnabled = state;
            }
        }


    }
}