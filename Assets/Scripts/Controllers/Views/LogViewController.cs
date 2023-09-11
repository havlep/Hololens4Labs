using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using TMPro;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the log view
    /// </summary>
    public class LogViewController
        : MonoBehaviour
    {

        [Header("Managers")]
        [SerializeField]
        protected SceneController sceneController;

        [Header("UI Elements")]
        [SerializeField]
        protected TMP_Text logNameLabel = default;
        [SerializeField]
        protected TMP_Text createdOnLabel = default;
        [SerializeField]
        protected TMP_Text createdByLabel = default;
        [SerializeField]
        protected TMP_Text lastModifiedLabel = default;

        [SerializeField]
        protected TMP_Text messageLabel = default;
        
        [SerializeField]
        protected Interactable[] buttons = default;


        protected GameObject parentObject = default;
        protected Log log = default;

        /// <summary>
        /// Connect the scenecontroller if it is not set
        /// </summary>
        protected void Awake()
        {
            if (sceneController == null)
            {
                sceneController = FindObjectOfType<SceneController>();
            }
        }

        public void CloseCard()
        {
            parentObject.SetActive(true);
            Destroy(gameObject);
        }


        /// <summary>
        /// InitWithExisting the menu with the given TextLogDTO.
        /// Should be called from the previous menu.
        /// </summary>
        /// <param name="source">TextLogDTO source</param>
        protected void Init(Log source, GameObject parent)
        {
            Debug.Log($"Initializing textlog");
            log = source;
            logNameLabel.SetText(log.Id);
            createdOnLabel.SetText(log.CreatedOn.ToShortTimeString());
            if (log.CreatedBy != null)
                createdByLabel.SetText(log.CreatedBy.Name);
            else
                createdByLabel.SetText("User not found!");

            
            parentObject = parent;
            SetButtonsInteractiveState(true);

        }

        /// <summary>
        /// Set the buttons interactive state
        /// </summary>
        /// <param name="state"></param>
        protected void SetButtonsInteractiveState(bool state)
        {
            foreach (var interactable in buttons)
            {
                interactable.IsEnabled = state;
            }
        }

        /// <summary>
        /// Save changes for the log into the azure table storage.
        /// </summary>
        virtual public async void SaveChanges() 
        {

            Debug.Log($"Saving Log.");
            SetButtonsInteractiveState(false);

            messageLabel.text = "Updating data, please wait ...";
            try
            {
                var success = await sceneController.DataManager.CreateOrUpdateLog(log);
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

        /// <summary>
        /// Callback for when the image is captured
        /// </summary>
        public virtual void ImageCaptured(DataType data)
        {
        }


    }
}