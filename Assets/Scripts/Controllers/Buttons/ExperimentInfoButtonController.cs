// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using HoloLens4Labs.Scripts.Model;
using TMPro;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Controllers
{
    /// <summary>
    /// Controller for the experiment info button used in the experiment list
    /// </summary>
    public class ExperimentInfoButtonController : MonoBehaviour
    {

        [Header("UI Elements")]
        [SerializeField]
        protected TMP_Text experimentDateLabel = default;
        [SerializeField]
        protected TMP_Text experimentNameLabel = default;


        protected ExperimentListMenuController parent = default;

        protected Experiment experiment = null;

        /// <summary>
        /// Initialize the button with the experiment data
        /// </summary>
        /// <param name="experiment">The experiment that will be shown in the button</param>
        public void Init(Experiment experiment, ExperimentListMenuController parent)
        {

            experimentDateLabel.text = experiment.CreatedOn.ToString("dd/MM/yyyy HH:mm:ss");
            experimentNameLabel.text = experiment.Name;
            this.experiment = experiment;
            this.parent = parent;

        }

        public void OnClick()
        {
            parent.OnExperimentInfoButtonClick(experiment);
        }

    }
}