using HoloLens4Labs.Scripts.Model.Logs;
using System;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the log info button used in the log selection menu
    /// </summary>
    public class LogInfoButtonController : MonoBehaviour
    {


        [Header("UI Elements")]
        [SerializeField]
        protected TMP_Text logDateLabel = default;
        [SerializeField]
        protected TMP_Text logTextLabel = default;
        [SerializeField]
        protected TMP_Text logIDLabel = default;
        [SerializeField]
        protected TMP_Text logTypeLabel = default;

        protected LogSelectionController parent = default;
        protected Log log = null;

        /// <summary>
        /// Initialize the button with the log data
        /// </summary>
        /// <param name="log"></param>
        /// <exception cref="NotImplementedException">If the log type in the DTO is not supported</exception>
        public void Init(Log log, LogSelectionController parent)
        {

            logDateLabel.text = log.CreatedOn.ToShortTimeString();
            logIDLabel.text = log.Id;

            switch (log)
            {

                case TextLog textLog:
                    logTextLabel.text = textLog.TextData.Text;
                    logTypeLabel.text = TextLog.GetTypeName();
                    break;
                case ImageLog:
                    logTextLabel.text = "";
                    logTypeLabel.text = ImageLog.GetTypeName();
                    break;
                case TranscriptionLog transcriptionLog:
                    if (transcriptionLog.Data != null)
                        logTextLabel.text = transcriptionLog.Data.Text;
                    logTypeLabel.text = TranscriptionLog.GetTypeName();
                    break;
                 default:
                    throw new NotImplementedException("Not implemented log type");
            
            }

            this.log = log;
            this.parent = parent;


        }

        /// <summary>
        /// Called when the item button is clicked
        /// </summary>
        public void OnClick()
        {

            parent.OnLogItemSelected(log);

        }

    }
}