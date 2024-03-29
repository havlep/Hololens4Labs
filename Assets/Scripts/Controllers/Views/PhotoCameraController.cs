// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Utils;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.Input;
#if UNITY_WSA
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;
#endif


namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the log view
    /// </summary>
    public class PhotoCameraController : MonoBehaviour
    {
        public bool IsCameraActive { private set; get; }

        [Header("Managers")]
        [SerializeField]
        private SceneController SceneController = default;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onCameraStarted = default;
        [SerializeField]
        private UnityEvent onCameraStopped = default;

        [Header("UI Elements")]
        [SerializeField]
        protected TMP_Text logNameLabel = default;
        [SerializeField]
        protected TMP_Text messageLabel = default;
        [SerializeField]
        protected Interactable[] buttons = default;

        private Log log;
        LogViewController parentObj;

#if UNITY_WSA
        private PhotoCapture photoCapture;
#else
        private WebCamTexture webCamTexture;
#endif


        private bool isWaitingForAirtap;


        /// <summary>
        /// Setup the scenecontroller if it is not set
        /// </summary>
        void Awake()
        {
            if (SceneController == null)
            {
                SceneController = FindObjectOfType<SceneController>();
            }
        }

        /// <summary>
        /// Initialize the view with the log that will be captured
        /// </summary>
        /// <param name="log">The log that will be filled in with data</param>
        /// <param name="parentObj">The calling object</param>

        public void Init(Log log, LogViewController parentObj)
        {

            this.log = log;
            this.parentObj = parentObj;
            isWaitingForAirtap = true;

            StartCamera();
        }


        /// <summary>
        /// Cancel the capture and return to the previous menu
        /// </summary>
        public void CancelCapture()
        {

            StopCamera();
            gameObject.SetActive(false);
            parentObj.CloseCard();
            Destroy(gameObject);

        }

        /// <summary>
        /// Callback that is called when the photo capture is completed
        /// </summary>
        /// <param name="imageData"></param>

        public void CaptureCompleted(ImageData imageData)
        {

            StopCamera();
            gameObject.SetActive(false);
            parentObj.ImageCaptured(imageData);
            Destroy(gameObject);

        }


#if UNITY_WSA

        /// <summary>
        /// Start the camera
        /// </summary>
        public void StartCamera()
        {
            if (IsCameraActive)
                return;

            Debug.Log("Starting camera system.");
            if (photoCapture == null)
            {
                PhotoCapture.CreateAsync(false, captureObject =>
                {
                    photoCapture = captureObject;
                    StartPhotoMode();
                });
            }
            else
            {
                StartPhotoMode();
            }

            IsCameraActive = true;
        }

        /// <summary>
        /// Start the photo mode
        /// </summary>
        private void StartPhotoMode()
        {
            var cameraResolution = PhotoCapture.SupportedResolutions
                .OrderByDescending((res) => res.width * res.height)
                .First();

            var cameraParams = new CameraParameters()
            {
                hologramOpacity = 0f,
                cameraResolutionWidth = cameraResolution.width,
                cameraResolutionHeight = cameraResolution.height,
                pixelFormat = CapturePixelFormat.JPEG
            };

            photoCapture.StartPhotoModeAsync(cameraParams, startResult =>
            {
                Debug.Log($"Camera system start result = {startResult.resultType}.");
                IsCameraActive = startResult.success;
                onCameraStarted?.Invoke();
            });
        }


#else
        /// <summary>
        /// Start the camera
        /// </summary>
        public void StartCamera()
        {
            if (IsCameraActive)
                return;

            Debug.Log("Starting camera system.");

            if (webCamTexture == null)
            {
            
                webCamTexture = new WebCamTexture();
                var webcamRenderer = gameObject.AddComponent<MeshRenderer>();
                webcamRenderer.material = new Material(Shader.Find("Standard"));
                webcamRenderer.material.mainTexture = webCamTexture;
                webCamTexture.Play();
            
            }
            else if (!webCamTexture.isPlaying)
                webCamTexture.Play();

            IsCameraActive = true;
        }

#endif



        /// <summary>
        /// Stop camera the camera.
        /// </summary>
#if UNITY_WSA
        public void StopCamera()
        {
            if (!IsCameraActive)
            {
                return;
            }

            Debug.Log("Stopping camera system.");

            photoCapture.StopPhotoModeAsync(result =>
            {
                if (result.success)
                {
                    IsCameraActive = false;
                    onCameraStopped?.Invoke();
                }
            });

            IsCameraActive = false;

        }
#else
        public void StopCamera()
        {
            if (!IsCameraActive)
                return;

            Debug.Log("Stopping camera system.");

            webCamTexture.Stop();

            IsCameraActive = false;

        }
#endif

        /// <summary>
        /// Take a photo from the WebCam. Make sure the camera is active.
        /// </summary>
        /// <returns>Image data with a Texture for thumbnail.</returns>
        public Task<ImageData> TakePhotoWithThumbnail(Log log)
        {
            if (!IsCameraActive)
            {
                throw new CameraException("Can't take photo when camera is not ready.");
            }

            return Task.Run(() =>
            {
                var completionSource = new TaskCompletionSource<ImageData>();

                AppDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log("Starting photo capture.");

#if UNITY_WSA
                    photoCapture.TakePhotoAsync((photoCaptureResult, frame) =>
                    {
                        Debug.Log("Photo capture done.");

                        var buffer = new List<byte>();
                        frame.CopyRawImageDataIntoBuffer(buffer);
                        var texture = new Texture2D(2, 2);
                        var data = buffer.ToArray();
                        texture.LoadImage(data);
                        var imageData = new ImageData(DateTime.Now, SceneController.CurrentUser, log, data, texture);
                        
                        completionSource.TrySetResult(imageData);
                    });
#else
                    var tex = new Texture2D(webCamTexture.width, webCamTexture.height);
                    tex.SetPixels(webCamTexture.GetPixels());
                    tex.Apply();
                    var data = tex.EncodeToPNG();

                    var imageData = new ImageData(DateTime.Now, SceneController.CurrentUser, log);
                    imageData.Data = data;
                    imageData.Texture = tex;
                    
                    completionSource.TrySetResult(imageData);
#endif
                });

                return completionSource.Task;
            });
        }

        /// <summary>
        /// Create a sprite from the image data
        /// </summary>
        /// <param name="imageData">ImageData as captured by the camera</param>
        /// <returns></returns>
        private Sprite spriteFromImage(ImageData imageData)
        {
            return Sprite.Create(imageData.Texture, new Rect(0, 0, imageData.Texture.width, imageData.Texture.height), new Vector2(0.5f, 0.5f));
        }

        public void HandleOnPointerClick()
        {
            if (isWaitingForAirtap)
            {
                CapturePhotoAndReturnAsync();
            }
        }

        /// <summary>
        /// Capture the photo and return to the previous menu
        /// </summary>
        public async void CapturePhotoAndReturnAsync()
        {

            var imageData = await CapturePhoto();
            CaptureCompleted(imageData);

        }

        /// <summary>
        /// Capture the photo and return the ImageData
        /// </summary>
        /// <returns>The photo data bundled in Image data</returns>
        private async Task<ImageData> CapturePhoto()
        {
            isWaitingForAirtap = false;

            var imageData = await TakePhotoWithThumbnail(log);
            //imageCanvas.sprite =  spriteFromImage(log.Data);


            SetButtonsInteractiveState(true);
            return imageData;
        }


        /// <summary>
        /// Take a thumbnail photo for the TrackedObject and upload to azure blob storage.
        /// </summary>
        public void TakeThumbnailPhoto()
        {
            if (!IsCameraActive)
            {
                messageLabel.text = "Camera is not ready or accessible.";
                return;
            }

            SetButtonsInteractiveState(false);
            isWaitingForAirtap = true;
            messageLabel.text = "Look at object and do Airtap to take photo.";
        }

        /// <summary>
        /// Set the buttons interactive state
        /// </summary>
        /// <param name="state"></param>
        protected void SetButtonsInteractiveState(bool state)
        {
            foreach (var interactable in buttons)
            {
                interactable.IsEnabled = state;
            }
        }
    }
}
