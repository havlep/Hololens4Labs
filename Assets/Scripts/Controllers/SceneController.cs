using UnityEngine;
using UnityEngine.Events;
#if UNITY_WSA
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.WebCam;
#endif

using HoloLens4Labs.Scripts.Managers;
using HoloLens4Labs.Scripts.Model;
using System.Threading.Tasks;
using System;

namespace HoloLens4Labs.Scripts.Controllers
{
    public class SceneController : MonoBehaviour
    {


        public Experiment CurrentExperiment { get; private set; }
        public Scientist CurrentUser { get; private set; }

        public DataManager DataManager => dataManager;

        public PhotoCameraController PhotoCameraController => cameraController;


        [Header("Managers")]
        [SerializeField]
        private DataManager dataManager = default;
        [SerializeField]
        private PhotoCameraController cameraController = default;

        [Header("Misc Settings")]
        [SerializeField]
        private GameObject mainMenu = default;



        // Start is called before the first frame update
        void Start()
        {
            OpenMainMenu();
        }

        // Should be called from DataManager ready callback to ensure DB is ready.
        public async void Init()
        {
            CurrentUser = new Scientist("11","Rutherford");
            CurrentUser = await dataManager.CreateOrUpdateScientist(CurrentUser);

            if (CurrentExperiment == null)
            {
                CurrentExperiment = await dataManager.CreateOrUpdateExperiment(new Experiment("12", "Electron 1", CurrentUser));
            }
        }


        public void OpenMainMenu()
        {
            mainMenu?.SetActive(true);
        }
    }
}