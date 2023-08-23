using HoloLens4Labs.Scripts.Model.Logs;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Controllers
{
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


        protected void Awake()
        {
            if (sceneController == null)
            {
                sceneController = FindObjectOfType<SceneController>();
            }
        }

        protected void OnDisable()
        {
            sceneController.OpenMainMenu();
        }

        public void CloseCard()
        {
            parentObject.SetActive(true);
            Destroy(gameObject);
        }


        /// <summary>
        /// Init the menu with the given TextLogDTO.
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

        protected void SetButtonsInteractiveState(bool state)
        {
            foreach (var interactable in buttons)
            {
                interactable.IsEnabled = state;
            }
        }


    }
}