using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the log selection menu
    /// </summary>
    public class LogSelectionController : MonoBehaviour
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

        [Header("Misc Settings")]
        [SerializeField]
        private GameObject mainMenu = default;

        /// <summary>
        /// Initialize the scene controller
        /// </summary>
        private void Awake()
        {
            if (sceneController == null)
            {
                sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            }
        }

        /// <summary>
        /// Create the scrolling list and populate it with the logs
        /// </summary>
        void OnEnable()
        {
            scrollableListPopulator.MakeScrollingList();

            var logs = new List<Log>();

            for (int i = 0; i < 10; i++)
            {
                var log = new TextLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
                log.Id = i.ToString();
                var textData = new TextData(DateTime.Now, sceneController.CurrentUser, log, "Testo di prova");
                log.TextData = textData;

                logs.Add(log);
            }


            //  PopulateList(Task<Log[]>.FromResult(logs.ToArray()));
            PopulateList(sceneController.DataManager.GetLogsForExperiment(sceneController.CurrentExperiment.Id));
        }

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
            TranscriptionLog transcriptionLog = new TranscriptionLog(DateTime.Now, sceneController.CurrentUser, sceneController.CurrentExperiment);
            transcriptionLogView.InitNew(transcriptionLog, gameObject);
            gameObject.SetActive(false);

        }

        
        /// <summary>
        /// Called when the log item button is clicked
        /// </summary>
        /// <param name="log">The log that was clicked</param>
        public void OnLogItemSelected(Log log) { 

            switch (log)
            {
                case TextLog textLog:
                    var textLogView = Instantiate(textLogViewPrefab, this.transform.position, Quaternion.identity);
                    textLogView.InitWithExisting(textLog, gameObject);
                    break;
                case ImageLog imageLog:
                    var imageLogView = Instantiate(photoLogViewPrefab, this.transform.position, Quaternion.identity);
                    imageLogView.InitWithExisting(imageLog, gameObject);
                    break;
                case TranscriptionLog transcriptionLog:
                    var tracsciptionLogView = Instantiate(transcriptionLogViewPrefab, this.transform.position, Quaternion.identity);
                    tracsciptionLogView.InitWithExisting(transcriptionLog, gameObject);
                    break;
                 default:
                    throw new NotImplementedException("Not implemented log type");
            }
            gameObject.SetActive(false);

        }

    }
}