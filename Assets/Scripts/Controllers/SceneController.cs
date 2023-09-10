using UnityEngine;
using HoloLens4Labs.Scripts.Managers;
using HoloLens4Labs.Scripts.Model;


namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Singleton class that manages the scene, 
    /// </summary>
    public class SceneController : MonoBehaviour
    {

        [Header("Managers")]
        [SerializeField]
        private DataManager dataManager = default;
        [SerializeField]
        private ImageAnalysisManager imageAnalysisManager = default;


        [Header("Menus")]
        [SerializeField]
        private GameObject loadingMenu = default;
        [SerializeField]
        private GameObject startMenu = default;
        [SerializeField]
        private GameObject experimentListMenu = default;
        [SerializeField]
        private GameObject logSelectionMenu = default;


        /// <summary>
        /// The experiment that is currently being worked on
        /// </summary>
        public Experiment CurrentExperiment { get; set; }

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

        private GameObject currentMenu = default;

        /// <summary>
        /// Set the loading menu as the current menu and turn all other menus off
        /// </summary>
        void Start()
        {
            loadingMenu?.SetActive(true);
            currentMenu = loadingMenu;
            startMenu?.SetActive(false);
            experimentListMenu?.SetActive(false);
            logSelectionMenu?.SetActive(false);
        }

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
        /// Initialize the scene controller called from the databasemanager once it is ready
        /// </summary>
        public async void Init()
        {

            CurrentUser = new Scientist("11","Rutherford");
            CurrentUser = await dataManager.CreateOrUpdateScientist(CurrentUser);

            OpenStartMenu();

        }

        /// <summary>
        /// Open the start menu
        /// </summary>
        public void OpenStartMenu()
        {
            currentMenu?.SetActive(false);
            startMenu?.SetActive(true);
            currentMenu = startMenu;
        }

        /// <summary>
        /// Open the experiment list menu
        /// </summary>
        public void OpenExperimentListMenu()
        {
            currentMenu?.SetActive(false);
            experimentListMenu?.SetActive(true);
            currentMenu = experimentListMenu;
        }

        /// <summary>
        /// Open the log selection menu
        /// </summary>
        public void OpenLogSelectionMenu()
        {
            currentMenu?.SetActive(false);
            logSelectionMenu?.SetActive(true);
            currentMenu = logSelectionMenu;
        }

    }
}