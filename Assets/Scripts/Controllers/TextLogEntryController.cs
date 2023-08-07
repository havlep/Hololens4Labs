
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.DTOs;


namespace HoloLens4Labs.Scripts.Controllers
{
    public class TextLogEntryController : MonoBehaviour
    {
  
        [Header("References")]
        [SerializeField]
        private SceneController sceneController;

        [SerializeField]
        private TextLogEditController textLogEditController = default;

        private void Awake()
        {
            if (sceneController == null)
            {
                sceneController = FindObjectOfType<SceneController>();
            }
        }

        private void OnEnable()
        {
   //         inputField.text = "";
        }

        /*

        public async void newTextLog() { 
        
            var textLog = await CreateNewTextLog();

            if(textLog != null){

                textLogEditController.gameObject.setActive(true);
                textLogEditController.Init(textLog);

            }
        
        }

        
        private async Task<TextLog> CreateNewTextLog()
        {
            
            hintLabel.SetText(loadingText);
            hintLabel.gameObject.SetActive(true);
            var textLog = await sceneController.DataManager.FindTextLogByID(searchName);
            if (textLog == null)
            {
                textLog = new TextLog(searchName);
                var success = await sceneController.DataManager.UploadOrUpdate(textLog);
                if (!success)
                {
                    return null;
                }

                await sceneController.DataManager.UploadOrUpdate(textLog);
            }

            hintLabel.gameObject.SetActive(false);
            
            return textLog;
        }
        */

    }
}