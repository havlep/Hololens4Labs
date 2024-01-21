using HoloLens4Labs.Scripts.Model;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the start menu
    /// </summary>

    public class StartMenuController : MenuController
    {

        [Header("Managers")]
        [SerializeField]
        protected SceneController sceneController;


        [Header("UI Elements")]
        [SerializeField]
        protected TMP_Text lastExpEditDateLabel = default;
        [SerializeField]
        protected TMP_Text lastExpUsedLabel = default;
        [SerializeField]
        protected TMP_Text lastExpEditNameLabel = default;

        [SerializeField]
        protected GameObject ContinueLastExpButton = default;

        [Header("Prefabs")]
        [SerializeField]
        protected ExperimentInfoViewController experimentInfoView = default;

        private Experiment lastExperiment = default;


        /// <summary>
        /// Initialize the view with the last experiment data
        /// </summary>
        async void OnEnable()
        {

            if (sceneController == null)
            {
                sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            }

            if(sceneController.CurrentUser.LastExperimentId != null)
            {
                lastExperiment =  await sceneController.DataManager.GetExperimentByID(sceneController.CurrentUser.LastExperimentId);
                lastExpEditDateLabel.text = lastExperiment.CreatedOn.ToLongDateString();
                lastExpEditNameLabel.text = lastExperiment.Name;
                ContinueLastExpButton.SetActive(true);
            }
            else
            {
                ContinueLastExpButton.SetActive(false);
            }
           
            
        }


        /// <summary>
        /// Open the create experiment view
        /// </summary>
        public void CreateNewExperiment()
        {

            ExperimentInfoViewController view = Instantiate(experimentInfoView, this.transform.position, Quaternion.identity);
            view.transform.localScale = gameObject.transform.localScale;
            view.InitNew(gameObject);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// Open last experiment used by the current user
        /// </summary>
        public void OpenLastExperiment()
        {
            sceneController.SetExperiment(lastExperiment);
            sceneController.OpenLogSelectionMenu();
        }


        /// <summary>
        /// Open the list all experiments menu
        /// </summary>
        public void ListAllExperiments()
        {
            sceneController.OpenExperimentListMenu();

        }

    }
}