using System.Threading.Tasks;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HoloLens4Labs.Scripts.Controllers
{

    /// <summary>
    /// Controller for the transcription log view prefab
    /// </summary>
    public class TranscriptionLogViewController : LogViewController
    {


        [Header("UI Elements")]
        [SerializeField]
        protected Image imageCanvas = default;
        [SerializeField]
        protected TMP_InputField descriptionInputField = default;

        [SerializeField]
        private Sprite placeHolderImage = default;

        [SerializeField]
        private PhotoCameraController photoCapturePanelPrefab;

        /// <summary>
        /// Initializes the view with an existing transcription log
        /// </summary>
        /// <param name="log">The transcription log</param>
        /// <param name="parentObj">The parent object</param>
        public void InitWithExisting(TranscriptionLog log, GameObject parentObj)
        {

            if (log.Data == null)
                throw new System.Exception("No image in existing image transcriptionLog");

            lastModifiedLabel.SetText(log.Data.CreatedOn.ToShortTimeString());
            imageCanvas.sprite = spriteFromImage(log.Data);

            base.Init(log, parentObj);

        }

        ///<summary>
        /// Initializes the view with a new transcription log
        ///</summary>
        ///<param name="log">The transcription log</param>
        ///<param name="parentObj">The parent object</param>
        public void InitNew(TranscriptionLog log, GameObject parentObj)
        {

            this.log = log;
            captureImage();
            base.Init(log, parentObj);

        }

        /// <summary>
        /// Instatiate the capture image panel and hide this panel
        /// </summary>  
        public void captureImage()
        {
            gameObject.SetActive(false);
            var photoCaptureView = Instantiate(photoCapturePanelPrefab, this.transform.position, Quaternion.identity);
            photoCaptureView.transform.localScale = gameObject.transform.localScale;
            photoCaptureView.Init(log, this);
        }

        /// <summary>
        /// Converts the image data to a sprite for display
        /// </summary>    
        /// <param name="data">The image data</param>
        /// <returns>The sprite</returns>
        private Sprite spriteFromImage(TranscriptionData data)
        {
            return Sprite.Create(data.Texture, new Rect(0, 0, data.Texture.width, data.Texture.height), new Vector2(0.5f, 0.5f));
        }

        /// <summary>
        ///  Called by the PhotoCameraController when the image is captured
        /// </summary>
        /// <param name="data">The image data</param>
        public async override void ImageCaptured(DataType data)
        {
            if (!(data is ImageData))
                throw new System.Exception("Wrong data type call for transcription log");

            var imageData = data as ImageData;

            var text = await Transcribe(imageData);
            var transData = new TranscriptionData(imageData, text);

            if (transData.Text == null || transData.Text == string.Empty)
                messageLabel.text = "No transcription available";

            var transLog = log as TranscriptionLog;
            transLog.Data = transData;
            imageCanvas.sprite = spriteFromImage(transLog.Data);

            gameObject.SetActive(true);

        }

        /// <summary>
        /// Clears the description input field
        /// </summary>
        public void ClearDescription()
        {

            descriptionInputField.text = string.Empty;
        }

        /// <summary>
        /// Save the changes to the repository 
        /// </summary>
        public override void SaveChanges()
        {
            var transLog = log as TranscriptionLog;
            transLog.Data.Text = descriptionInputField.text;
            base.SaveChanges();

        }

        /// <summary>
        /// Transcribes the image that was captured
        /// </summary>
        /// <param name="imageData">An image data object that will be transcribed</param>
        /// <returns>The transcription</returns>
        private Task<string> Transcribe(ImageData imageData)
        {
            return  sceneController.ImageAnalysisManager.TranscribeImage(imageData);
        }

    }
}