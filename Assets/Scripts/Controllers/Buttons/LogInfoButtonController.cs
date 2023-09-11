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

        /// <summary>
        /// Initialize the button with the log data
        /// </summary>
        /// <param name="log"></param>
        /// <exception cref="NotImplementedException">If the log type in the DTO is not supported</exception>
        public void Init(Log log)
        {
            logDateLabel.text = log.CreatedOn.ToShortTimeString();
            logIDLabel.text = log.Id;

            if (log is TextLog)
            {

                TextLog textLog = log as TextLog;
                logTextLabel.text = textLog.TextData.Text;
                logTypeLabel.text = TextLog.GetTypeName();

            }

            else if (log is ImageLog)
            {

                ImageLog imageLog = log as ImageLog;
                logTextLabel.text = "";
                logTypeLabel.text = ImageLog.GetTypeName();

            }
            else if (log is TranscriptionLog)
            {

                TranscriptionLog transcriptionLog = log as TranscriptionLog;
                if (transcriptionLog.Data != null)
                    logTextLabel.text = transcriptionLog.Data.Text;
                logTypeLabel.text = TranscriptionLog.GetTypeName();

            }
            else if (log is WheightLog)
            {

                throw new NotImplementedException("Not implemented yet");
                /*
                Tran transcriptionLog = log as WheightLog;
                if (transcriptionLog.Data != null)
                    logTextLabel.text = transcriptionLog.Data.Text;
                logTypeLabel.text = TranscriptionLog.GetTypeName();
                */

            }

        }

    }
}