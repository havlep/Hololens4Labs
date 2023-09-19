using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the experiment list menu
    /// </summary>
    public class ExperimentListMenuController : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField]
        private SceneController sceneController = default;

        [SerializeField]
        private ScrollableListPopulator scrollableListPopulator = default;

        
        [Header("Views")]
        [SerializeField]
        private ExperimentInfoViewController experimentInfoViewPrefab = default;

        // Todo implement the creation of a new experiment
        //private TextLogViewController textLogViewPrefab = default;


        private void Start()
        {
            if (sceneController == null)
            {
                sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            }

        }

        /// <summary>
        /// Initialize the button with the experiment data
        /// </summary>
        private void OnEnable()
        {

            scrollableListPopulator.MakeScrollingList();
            PopulateList(sceneController.DataManager.GetAllExperiments());

        }

        /// <summary>
        /// Populate the list with the experiments
        /// </summary>
        /// <param name="experiments">Task that returns an array of experiments</param>
        private async void PopulateList(Task<Experiment[]> experiments)
        {
            var experimentsArray = await experiments;

            foreach (Experiment experiment in experimentsArray)
            {
                scrollableListPopulator.AddItem(experiment);
            }
        }

        /// <summary>
        /// Populate the butto, item with the experiment data
        /// </summary>
        /// <param name="itemInstance">Experiment info button instance</param>
        /// <param name="data">Experiment data</param>
        /// <exception cref="NotSupportedException"></exception>
        public void PopulateItemAction(GameObject itemInstance, object data)
        {

            var experimentInfoButtonController = itemInstance.GetComponent<ExperimentInfoButtonController>();

            if (experimentInfoButtonController == null)
            {

                Debug.LogError("PopulateItemAction: obj is not a LogInfoButtonController");
                throw new NotSupportedException("PopulateItemAction: obj is not a LogInfoButtonController");
            }

            if ((data is not Experiment))
            {

                Debug.LogError("PopulateItemAction: data is not a Experiment");
                throw new NotSupportedException("PopulateItemAction: data is not a Experiment");
            }

            var experiment = data as Experiment;

            experimentInfoButtonController.Init(experiment, this);

        }

        /// <summary>
        /// Open the Experiment Info view to create a new experiment
        /// </summary>
        public void CreateNewExperiment()
        {

            var experimentInfoView = Instantiate(experimentInfoViewPrefab, this.transform.position, Quaternion.identity);
            experimentInfoView.InitNew(gameObject);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// Open the Experiment Info view to edit an existing experiment
        /// </summary>
        /// <param name="experiment">The experiment that will be modified</param>
        public void OpenExperimentInfo(Experiment experiment)
        {

            var experimentInfoView = Instantiate(experimentInfoViewPrefab, this.transform.position, Quaternion.identity);
            experimentInfoView.InitExisting(experiment, gameObject);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// Close the list view and open the start menu
        /// </summary>
        public void Close()
        {

            sceneController.OpenStartMenu();

        }


        public void OnExperimentInfoButtonClick(Experiment experiment)
        {

            sceneController.CurrentExperiment = experiment;
            sceneController.OpenLogSelectionMenu();
            gameObject.SetActive(false);

        }

    }
}