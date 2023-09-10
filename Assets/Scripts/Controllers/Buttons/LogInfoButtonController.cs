using HoloLens4Labs.Scripts.Model.Logs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
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

                throw new System.Exception("Not implemented yet");
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