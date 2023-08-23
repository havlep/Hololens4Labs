using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Utils;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_WSA
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;
#endif


namespace HoloLens4Labs.Scripts.Controllers
{
    public class PhotoCameraController: MonoBehaviour
    {
        public bool IsCameraActive { private set; get; }

        [Header("Events")]
        [SerializeField]
        private SceneController SceneController = default;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onCameraStarted = default;
        [SerializeField]
        private UnityEvent onCameraStopped = default;

#if UNITY_WSA
        private PhotoCapture photoCapture;
#else
        private WebCamTexture webCamTexture;
#endif



#if UNITY_WSA
        /// With WSA
         
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
#else
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
        /// <returns>Image data encoded as jpg.</returns>
        public Task<byte[]> TakePhoto()
        {
            if (!IsCameraActive)
            {
                throw new CameraException("Can't take photo when camera is not ready.");
            }

            return Task.Run(() =>
            {
                var completionSource = new TaskCompletionSource<byte[]>();

                AppDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log("Starting photo capture.");

#if UNITY_WSA
                    photoCapture.TakePhotoAsync((photoCaptureResult, frame) =>
                    {
                        Debug.Log("Photo capture done.");
            
                        var buffer = new List<byte>();
                        frame.CopyRawImageDataIntoBuffer(buffer);
                        completionSource.TrySetResult(buffer.ToArray());
                    });
#else
                    var tex = new Texture2D(webCamTexture.width, webCamTexture.height);
                    tex.SetPixels(webCamTexture.GetPixels());
                    tex.Apply();
                    var data = tex.EncodeToPNG();
                    completionSource.TrySetResult(data);
#endif
                });

                return completionSource.Task;
            });
        }

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
                        var imageData = buffer.ToArray();
                        texture.LoadImage(imageData);
                        var imageThumbnail = new ImageData
                        {
                            ImageData = imageData,
                            Texture = texture
                        };
                        
                        completionSource.TrySetResult(imageThumbnail);
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
    }
}
