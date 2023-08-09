using System.Threading.Tasks;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using HoloLens4Labs.Scripts.DTOs;



namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Handles UI/UX for creation of a new tracked object.
    /// </summary>

    public class TextLogEditController : MonoBehaviour
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

        private TextLogDTO textLog;

        private void Awake()
        {
            if (sceneController == null)
            {
                sceneController = FindObjectOfType<SceneController>();
            }
        }


        /// <summary>
        /// Init the menu with the given TextLogDTO.
        /// Should be called from the previous menu.
        /// </summary>
        /// <param name="source">TextLogDTO source</param>
        public void Init(TextLogDTO source)
        {

            /*
            textLog = source;
            logNameLabel.SetText(textLog.Name);
            createdOnLabel.SetText(textLog.CreatedOn);
            createdByLabel.SetText(textLog.CreatedBy);
            lastModifiedLabel.SetText(textLog.LastModifiedOn);
            descriptionInputField.text = textLog.Description;
            SetButtonsInteractiveState(true);
            */

        }

        /// <summary>
        /// Save changes for the TextLogDTO into the azure table storage.
        /// </summary>
        public async void SaveChanges()
        {
            /*
            SetButtonsInteractiveState(false);
            textLog.Description = descriptionInputField.text;
            textLog.CreatedOn = createdOnLabel.text;
            textLog.CreatedBy = createdByLabel.text;
            textLog.LastModifiedOn = lastModifiedLabel.text;

            messageLabel.text = "Updating data, please wait ...";
            var success = await sceneController.DataManager.UploadOrUpdate(textLog);
            messageLabel.text = success ? "Updated data in database." : "Failed to update database.";
            SetButtonsInteractiveState(true);
            */
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
