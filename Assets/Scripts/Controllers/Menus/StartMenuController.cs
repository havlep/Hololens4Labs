using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the start menu
    /// </summary>

    public class StartMenuController : MonoBehaviour
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


        /// <summary>
        /// Initialize the view with the last experiment data
        /// </summary>
        void Start()
        {

            if (sceneController == null)
            {
                sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            }

            /*
            if (ExperimentManager.Instance.LastExperiment != null)
            {
                lastExpEditDateLabel.text = ExperimentManager.Instance.LastExperiment.LastEditDate.ToShortDateString();
                lastExpEditNameLabel.text = ExperimentManager.Instance.LastExperiment.Name;
                lastExpUsedLabel.text = ExperimentManager.Instance.LastExperiment.LastUseDate.ToShortDateString();
            }
            else
            {
                ContinueLastExpButton.SetActive(false);
            }
            */
        }

        // Select From Existing Experiments

        /// <summary>
        /// Open the create experiment view
        /// </summary>
        public void CreateNewExperiment()
        {

            ExperimentInfoViewController view = Instantiate(experimentInfoView, this.transform.position, Quaternion.identity);
            view.InitNew(gameObject);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// Open the list all experiments menu
        /// </summary>
        public void ListAllExperiments()
        {
            sceneController.OpenExperimentListMenu();
            gameObject.SetActive(false);

        }

    }
}