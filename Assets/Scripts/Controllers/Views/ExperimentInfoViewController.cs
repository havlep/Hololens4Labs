using HoloLens4Labs.Scripts.Model;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the experiment info menu
    /// </summary>
    public class ExperimentInfoViewController : MonoBehaviour
    {

        [Header("Managers")]
        [SerializeField]
        private SceneController sceneController = default;

        [Header("UI Elements")]
        [SerializeField]
        protected TMP_Text actionLabel = default;
        [SerializeField]
        protected TMP_Text createdByLabel = default;
        [SerializeField]
        protected TMP_Text createdOnLabel = default;
        [SerializeField]
        protected TMP_InputField nameField = default;
        

        protected GameObject parent;

        /// <summary>
        /// Initialize the view
        /// </summary>
        void Awake()
        {
            if (sceneController == null)
            {
                sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            }
        }

        /// <summary>
        /// Initialize the view with the existing experiment data
        /// </summary>
        /// <param name="experiment">The experiment that will be modified</param>
        /// <param name="parent">The parrent object</param>
        public void InitExisting(Experiment experiment, GameObject parent) {

            nameField.text = experiment.Name;
            createdByLabel.text = experiment.CreatedBy.Name;
            //createdOnLabel.text = experiment.CreatedOn.ToShortDateString();
            actionLabel.text = "Create New Experiment";
            this.parent = parent;
            gameObject.SetActive(true);

        }
        
        /// <summary>
        /// Intialize the view for a new experiment
        /// </summary>
        public void InitNew(GameObject parent)
        {
            createdByLabel.text = sceneController.CurrentUser.Name;
            actionLabel.text = "Create New Experiment";
            gameObject.SetActive(true);
            this.parent = parent;
        
        }

        /// <summary>
        /// Save the experiment and open the log selection menu
        /// </summary>
        public async void SaveExperiment() { 
        
            var experiment = new Experiment(nameField.text, sceneController.CurrentUser.Id);
            experiment = await sceneController.DataManager.CreateOrUpdateExperiment(experiment);
            sceneController.CurrentExperiment = experiment;
            sceneController.OpenLogSelectionMenu();
            Destroy(gameObject);

        }

        /// <summary>
        /// Close the current View and return to parent menu
        /// </summary>
        public void CloseView()
        {
        
            parent.SetActive(true);
            Destroy(gameObject);

        }
    
    }

}