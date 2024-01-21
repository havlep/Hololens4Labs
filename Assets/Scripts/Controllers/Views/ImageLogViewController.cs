// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System.Threading.Tasks;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using UnityEngine;
using UnityEngine.UI;


namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the image log view
    /// </summary>

    public class ImageLogViewController : LogViewController
    {


        [Header("UI Elements")]
        [SerializeField]
        protected Image imageCanvas = default;

        [SerializeField]
        private Sprite placeHolderImage = default;

        [SerializeField]
        private PhotoCameraController photoCapturePanelPrefab;


        /// <summary>
        /// Initialize the view with the existing image log data
        /// </summary>
        /// <param name="log">Existing image log</param>
        /// <param name="parentObj">The object that called this image log</param>
        /// <exception cref="System.Exception"></exception>
        public void InitWithExisting(ImageLog log, GameObject parentObj)
        {

            if (log.Data == null)
                throw new System.Exception("No image in existing image imagelog");

            lastModifiedLabel.SetText(log.Data.CreatedOn.ToString("dd/MM/yyyy HH:mm:ss"));
            setImageCanvas(log.Data);
            

            base.Init(log, parentObj);

        }

        public async void setImageCanvas(ImageData imageData)
        {
            imageCanvas.sprite = await SpriteFromImage(imageData);
        }

        /// <summary>
        /// Initialize the view for a new image log 
        /// </summary>
        /// <param name="log">The new image log</param>
        /// <param name="parentObj">The parent object that called this image log</param>
        public void InitNew(ImageLog log, GameObject parentObj)
        {

            gameObject.SetActive(false);
            this.log = log;
            var photoCaptureView = Instantiate(photoCapturePanelPrefab, this.transform.position, Quaternion.identity);
            photoCaptureView.transform.localScale = parentObj.transform.localScale;
            photoCaptureView.Init(log, this);
            base.Init(log, parentObj);

        }

        /// <summary>
        /// Create a sprite from the image data
        /// </summary>
        /// <param name="imageData">The data of the image</param>
        /// <returns>A sprite created from the image data</returns>
        private async Task<Sprite> SpriteFromImage(ImageData imageData)
        {
            if (imageData == null)
                return placeHolderImage;

            if (imageData.Texture == null)
            {
                imageData.Texture = new Texture2D(2, 2);
                imageData.Texture.LoadImage( await imageData.getData());
            }

            return Sprite.Create(imageData.Texture, new Rect(0, 0, imageData.Texture.width, imageData.Texture.height), new Vector2(0.5f, 0.5f));
        }

        /// <summary>
        /// Called when the image is captured
        /// </summary>
        /// <param name="data">Called by the cameera controller when an image is captured</param>
        /// <exception cref="System.Exception"></exception>

        override public void ImageCaptured(DataType data)
        {
            if (!(data is ImageData))
                throw new System.Exception("Wrong data type call for image log");

            var imagelog = log as ImageLog;
            imagelog.Data = (ImageData)data;
            setImageCanvas(imagelog.Data);
            gameObject.SetActive(true);

        }

    }
}