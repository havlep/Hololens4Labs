// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Utils;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the log selection menu
    /// </summary>
    public class LogSelectionController : MenuController
    {

        [Header("Managers")]
        [SerializeField]
        private SceneController sceneController = default;


        [Header("UI Elements")]
        [SerializeField]
        private TextLogViewController textLogViewPrefab = default;
        [SerializeField]
        private ImageLogViewController photoLogViewPrefab = default;
        [SerializeField]
        private TranscriptionLogViewController transcriptionLogViewPrefab = default;
        [SerializeField]
        private ScrollableListPopulator scrollableListPopulator = default;

      

        /// <summary>
        ///  Initialize the scene controller and create the scrolling list and populate it with the logs
        /// </summary>
        void OnEnable()
        {
            if (sceneController == null)
            {
                sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            }
            scrollableListPopulator.MakeScrollingList();
            PopulateList(sceneController.DataManager.GetLogsForExperiment(sceneController.CurrentExperiment.Id));

        }

        /// <summary>
        /// Populate the scrollable list populator with the list of logs
        /// </summary>  
        /// <param name="logs">A list of logs</param>
        private async void PopulateList(Task<Log[]> logs)
        {
            var logsArray = await logs;

            foreach (Log log in logsArray)
            {
                scrollableListPopulator.AddItem(log);
            }
        }


        /// <summary>
        /// Populate the list item with the log data
        /// </summary>
        /// <param name="itemInstance">The GameObject that represents the item in a list</param>
        /// <param name="data">The Log that contains the data</param>
        public void PopulateItemAction(GameObject itemInstance, object data)
        {

            var logInfoButtonController = itemInstance.GetComponent<LogInfoButtonController>();

            if (logInfoButtonController == null)
            {

                Debug.LogError("PopulateItemAction: obj is not a LogInfoButtonController");
                throw new NotSupportedException("PopulateItemAction: obj is not a LogInfoButtonController");
            }

            if ((data is not Log))
            {

                Debug.LogError("PopulateItemAction: data is not a Log");
                throw new NotSupportedException("PopulateItemAction: data is not a Log");
            }

            var log = data as Log;

            logInfoButtonController.Init(log, this);

        }

        /// <summary>
        /// Create a new text log
        /// </summary>
        public void CreateNewTextLog()
        {

            var textLogView = Instantiate(textLogViewPrefab, this.transform.position, Quaternion.identity);
            textLogView.transform.localScale = gameObject.transform.localScale;
            var textLog = new TextLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
            textLogView.InitWithExisting(textLog, gameObject);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// Create a new image log
        /// </summary>
        public void CreateNewImageLog()
        {

            var imageLogView = Instantiate(photoLogViewPrefab, this.transform.position, Quaternion.identity);
            imageLogView.transform.localScale = gameObject.transform.localScale;
            var imageLog = new ImageLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
            imageLogView.InitNew(imageLog, gameObject);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// Create a new transcription log
        /// </summary>
        public void CreateNewTranscriptionLog()
        {

            var transcriptionLogView = Instantiate(transcriptionLogViewPrefab, this.transform.position, Quaternion.identity);
            transcriptionLogView.transform.localScale = gameObject.transform.localScale;
            TranscriptionLog transcriptionLog = new TranscriptionLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
            transcriptionLogView.InitNew(transcriptionLog, gameObject);
            gameObject.SetActive(false);

        }


        /// <summary>
        /// Called when the log item button is clicked
        /// </summary>
        /// <param name="log">The log that was clicked</param>
        public void OnLogItemSelected(Log log)
        {

            switch (log)
            {
                case TextLog textLog:
                    var textLogView = Instantiate(textLogViewPrefab, this.transform.position, Quaternion.identity);
                    textLogView.InitWithExisting(textLog, gameObject);
                    textLogView.transform.localScale = gameObject.transform.localScale;
                    break;
                case TranscriptionLog transcriptionLog:
                    var transcriptionLogView = Instantiate(transcriptionLogViewPrefab, this.transform.position, Quaternion.identity);
                    transcriptionLogView.transform.localScale = gameObject.transform.localScale;
                    transcriptionLogView.InitWithExisting(transcriptionLog, gameObject);
                    break;
                case ImageLog imageLog:
                    var imageLogView = Instantiate(photoLogViewPrefab, this.transform.position, Quaternion.identity);
                    imageLogView.transform.localScale = gameObject.transform.localScale;
                    imageLogView.InitWithExisting(imageLog, gameObject);
                    break;
                default:
                    throw new NotImplementedException("Not implemented log type");
            }
            
            gameObject.SetActive(false);

        }

    }
}