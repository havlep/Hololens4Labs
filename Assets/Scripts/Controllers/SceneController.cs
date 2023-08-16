using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
#if UNITY_WSA
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;
#endif

using HoloLens4Labs.Scripts.Managers;
using HoloLens4Labs.Scripts.DTOs;

namespace HoloLens4Labs.Scripts.Controllers
{
    public class SceneController : MonoBehaviour
    {

        public bool IsCameraActive { private set; get; }
        public ExperimentDTO CurrentExperiment { get; private set; }

        public DataManager DataManager => dataManager;


        [Header("Managers")]
        [SerializeField]
        private DataManager dataManager = default;

        [Header("Misc Settings")]
        [SerializeField]
        private GameObject mainMenu = default;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onCameraStarted = default;
        [SerializeField]
        private UnityEvent onCameraStopped = default;

#if UNITY_WSA
    private UnityEngine.Windows.WebCam.PhotoCapture photoCapture;
#else
        private WebCamTexture webCamTexture;
#endif


        // Start is called before the first frame update
        void Start()
        {
            OpenMainMenu();
        }

        // Should be called from DataManager ready callback to ensure DB is ready.
        public async void Init()
        {
            if (CurrentExperiment == null)
            {
               // CurrentExperiment = await dataManager.CreateExperiment();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenMainMenu()
        {
            mainMenu?.SetActive(true);
        }
    }
}