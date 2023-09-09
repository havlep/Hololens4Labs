using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;//TODO look into how to do this for the whole project

using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Model.Logs;


namespace HoloLens4Labs.Scripts.Managers
{


    public class DataManager : MonoBehaviour, RepositoryInterface
    {

        [Header("Repository")]
        [SerializeField]
        private GameObject repositoryObject;
  


        [Header("Events")]
        [SerializeField]
        private UnityEvent onRepoReadyReady = default;

        private RepositoryInterface repo = default;

        private void Awake()
        {
            if (!repositoryObject.TryGetComponent<RepositoryInterface>(out repo)) {

                Debug.Log($"Repository object does not implement the Repository Interface.");
                throw new MissingReferenceException("Unity project is not setup correctly");
            }
            if (repo.IsReady())
                Debug.Log($"Repository object has been setup properly.");
            else
                Debug.Log($"Repository object has not been initialized.");


        }

        /// <summary>
        /// Check if the repository has been initialized
        /// </summary>
        public void Init()
        {
             // Check that the right repository was intialized and propagate the info
             if (IsReady())
                 onRepoReadyReady?.Invoke();

        }


        /// <summary>
        /// Save a new experiment
        /// </summary>
        /// <param name="experiment">The experiment</param>
        /// <returns>A new experiment with an ID in the repo</returns>
        public Task<Experiment> CreateExperiment(Experiment experiment)
        {
            return repo.CreateExperiment(experiment);
        }

        /// <summary>
        /// Create a new log
        /// </summary>
        /// <param name="log">The log</param>
        /// <returns>A new log with an ID in the repo</returns>
        public Task<Log> CreateLog(Log log)
        {
            return repo.CreateLog(log);
        }

        /// <summary>
        /// Create a new scientist
        /// </summary>
        /// <param name="scientist">The scientist</param>
        /// <returns>A new scientist object with an ID in the repo</returns>
        public Task<Scientist> CreateScientist(Scientist scientist)
        {
            return repo.CreateScientist(scientist);
        }

        /// <summary>
        /// Delete an experiment
        /// </summary>
        /// <param name="experiment"> The Experiment that will be deleted </param>
        /// <returns>true on success</returns>
        public Task<bool> DeleteExperiment(Experiment experiment)
        {
            return repo.DeleteExperiment(experiment);
        }

        /// <summary>
        /// Delete a log
        /// </summary>
        /// <param name="log"> The Log that will be deleted </param>
        /// <returns>true on success</returns>
        public Task<bool> DeleteLog(Log log)
        {
            return repo.DeleteLog(log);
        }

        /// <summary>
        /// Delete a scientist
        /// </summary>
        /// <param name="scientist">The Scientist that will be deleted</param>
        /// <returns>true on success</returns>
        public Task<bool> DeleteScientist(Scientist scientist)
        {
            return repo.DeleteScientist(scientist);
        }

        /// <summary>
        /// Update an Experiment that is already in the repo 
        /// </summary>
        /// <param name="experiment">The Experiment that will be updated</param>
        /// <returns>true on success</returns>
        public Task<bool> UpdateExperiment(Experiment experiment)
        {
            return repo.UpdateExperiment(experiment);
        }

        /// <summary>
        /// Update a Log that is already in the repo
        /// </summary>
        /// <param name="log">The Log that will be updated</param>
        /// <returns>true on success</returns>  
        public Task<bool> UpdateLog(Log log)
        {
            return repo.UpdateLog(log);
        }

        /// <summary>
        /// Update a Scientist that is already in the repo
        /// </summary>
        /// <param name="scientist">The Scientist that will be updated</param>
        /// <returns>true on success</returns>
        public Task<bool> UpdateScientist(Scientist scientist)
        {
            return repo.UpdateScientist(scientist);
        }

        /// <summary>
        /// Create or update a Scientist
        /// </summary>
        /// <param name="scientist">The Scientist that will be created or updated</param>
        /// <returns>The Scientist that was created or updated</returns>
        public async Task<Scientist> CreateOrUpdateScientist(Scientist scientist)
        {

            if (scientist.Id == string.Empty)
                return await CreateScientist(scientist);

            if (await UpdateScientist(scientist))
                return scientist;

            return null;


        }

        /// <summary>
        /// Create or update an Experiment
        ///         /// <
        /// </summary>
        /// <param name="experiment">The Experiment that will be created or updated</param>
        /// <returns>The Experiment that was created or updated</returns>
        public async Task<Experiment> CreateOrUpdateExperiment(Experiment experiment)
        {

            if (experiment.Id == string.Empty)
                return await CreateExperiment(experiment);

            if (await UpdateExperiment(experiment))
                return experiment;

            return null;


        }

        /// <summary>
        /// Create or update a Log
        /// </summary>
        /// <param name="log"> The Log that will be created or updated</param>
        /// <returns>The Log that was created or updated</returns>
        public async Task<Log> CreateOrUpdateLog(Log log)
        {

            if (log.Id == string.Empty)
                return await CreateLog(log);
            if (await UpdateLog(log))
                return log;
            return null;

        }

        
        public bool IsReady()
        {
            return repo.IsReady();
        }

        /// <summary>
        /// Get all logs for experiment
        /// </summary>
        /// <param name="experimentID"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<Log[]> GetLogsForExperiment(string experimentID)
        {
            return repo.GetLogsForExperiment(experimentID);
        }
    }
}
