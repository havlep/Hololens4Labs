using HoloLens4Labs.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
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
        protected GameObject ExperimentDetailMenu = default;
        [SerializeField]
        protected GameObject ListExperimentsMenu = default;
        [SerializeField]
        protected GameObject CreateExperimentMenu = default;

        // Start is called before the first frame update
        void Start()
        {

            if (sceneController == null)
            {
                sceneController = FindObjectOfType<SceneController>();
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
        /// Create a new experiment
        /// </summary>
        public void CreateNewExperiment()
        {
            Instantiate(CreateExperimentMenu);
            Destroy(gameObject);

        }

        /// <summary>
        /// List all experiments
        /// </summary>
        public void ListAllExperiments()
        {
            Instantiate(ListExperimentsMenu);
            Destroy(gameObject);
        }

    }
}