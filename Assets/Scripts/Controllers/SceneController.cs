// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Managers;
using HoloLens4Labs.Scripts.Model;
using UnityEngine;


namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Singleton class that manages the scene, data, image analysis and menus
    /// </summary>
    public class SceneController : MonoBehaviour
    {

        [Header("Managers")]
        [SerializeField]
        private DataManager dataManager = default;
        [SerializeField]
        private ImageAnalysisManager imageAnalysisManager = default;


        [Header("Menu Prefabs")]
        [SerializeField]
        private MenuController loadingMenu = default;
        [SerializeField] 
        protected StartMenuController startMenu = default;
        [SerializeField]
        private ExperimentListMenuController experimentListMenu = default;
        [SerializeField]
        private LogSelectionController logSelectionMenu = default;

        private Experiment currentExperiment = default;

        /// <summary>
        /// The experiment that is currently being worked on
        /// </summary>
        public Experiment CurrentExperiment
        {
            get => currentExperiment;
        }

        /// <summary>
        /// Set the current experiment
        /// </summary>
        /// <param name="experiment">The experiment that will be used as current</param>
        /// 
        public async void SetExperiment(Experiment experiment)
        {
                
            if (CurrentUser != null && CurrentUser.LastExperimentId != experiment.Id)
            { 
                CurrentUser.LastExperimentId = experiment.Id; 
                await dataManager.CreateOrUpdateScientist(CurrentUser);
            }

            currentExperiment = experiment;

        }
        

        /// <summary>
        /// The user currently using the app
        /// </summary>
        public Scientist CurrentUser { get; set; }

        /// <summary>
        /// DataManager instance
        /// </summary>
        public DataManager DataManager => dataManager;

        /// <summary>
        /// ImageAnalysisManager instance
        /// </summary>
        public ImageAnalysisManager ImageAnalysisManager => imageAnalysisManager;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static SceneController Instance { get; private set; }

        private MenuController currentMenu = default;

        /// <summary>
        /// Set the loading menu as the current menu and turn all other menus off
        /// </summary>
        void Start()
        {
            currentMenu = Instantiate(loadingMenu, this.transform.position, Quaternion.identity); ;
            currentMenu.transform.localScale = gameObject.transform.localScale;

        }

        /// <summary>
        /// Handling of singleton uniqueness on Awake
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

        }

        /// <summary>
        /// Initialize the scene controller called from the database manager once it is ready
        /// </summary>
        public async void Init()
        {

            try
            {
                CurrentUser = await dataManager.GetScientistById("11");
            }
            catch (ObjectDataBaseException)
            {
                CurrentUser = new Scientist("11", "Rutherford", "");
                CurrentUser = await dataManager.CreateOrUpdateScientist(CurrentUser);
            }

            OpenStartMenu();

        }

        /// <summary>
        /// Open the start menu
        /// </summary>
        public void OpenStartMenu()
        {
            currentMenu.CloseMenu();
            currentMenu = Instantiate(startMenu, gameObject.transform, false); 
            currentMenu.transform.localScale = gameObject.transform.localScale;
            currentMenu.enabled = true;
        }

        /// <summary>
        /// Open the experiment list menu
        /// </summary>
        public void OpenExperimentListMenu()
        {
            currentMenu.CloseMenu();
            currentMenu = Instantiate(experimentListMenu, gameObject.transform, false);
            currentMenu.transform.localScale = gameObject.transform.localScale;
            currentMenu.enabled = true;
        }

        /// <summary>
        /// Open the log selection menu
        /// </summary>
        public void OpenLogSelectionMenu()
        {
            currentMenu.CloseMenu();
            currentMenu = Instantiate(logSelectionMenu, gameObject.transform, false);
            currentMenu.transform.localScale = gameObject.transform.localScale;
            currentMenu.enabled = true;
        }

    }
}