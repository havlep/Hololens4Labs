using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Repositories;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace HoloLens4Labs.Scripts.Managers
{
    /// <summary>
    /// A facade class that handles the communication with the repositories
    /// </summary>
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

            if (!repositoryObject.TryGetComponent<RepositoryInterface>(out repo))
            {

                Debug.Log($"Repository object does not implement the Repository Interface.");
                throw new MissingReferenceException("Unity project is not setup correctly");
            }

        }

        /// <summary>
        /// Check if the repository has been initialized
        /// </summary>
        public void Init()
        {
            // Check that the right repository was intialized and propagate the info
            if (IsReady())
            {
                Debug.Log($"Repository object has been setup properly.");
                onRepoReadyReady?.Invoke();
            }
            else
            {
                Debug.Log($"Repository object has not been initialized.");
            }
        }


        /// <summary>
        /// Save a new experiment to the repository
        /// </summary>
        /// <param name="experiment">The experiment</param>
        /// <returns>A new experiment with an ID in the repo</returns>
        public Task<Experiment> CreateExperiment(Experiment experiment)
        {
            return repo.CreateExperiment(experiment);
        }

        /// <summary>
        /// Create a new log inside of the repository
        /// </summary>
        /// <param name="log">The log</param>
        /// <returns>A new log with an ID in the repo</returns>
        public Task<Log> CreateLog(Log log)
        {
            return repo.CreateLog(log);
        }

        /// <summary>
        /// Create a new scientist within the repository
        /// </summary>
        /// <param name="scientist">The scientist</param>
        /// <returns>A new scientist object with an ID in the repo</returns>
        public async Task<Scientist> CreateScientist(Scientist scientist)
        {
            return await repo.CreateScientist(scientist);
        }

        /// <summary>
        /// Get Scientist by ID from the repository
        /// </summary>
        /// <param name="scientistId">The id of the scientist that we want to return</param>
        /// <returns>The scientist with the given ID if he/she exist</returns>
        public async Task<Scientist> GetScientistById(string scientistId)
        {
            return await repo.GetScientistById(scientistId);
        }

        /// <summary>
        /// Delete an experiment from the repository
        /// </summary>
        /// <param name="experiment"> The Experiment that will be deleted </param>
        /// <returns> True on success </returns>
        public Task<bool> DeleteExperiment(Experiment experiment)
        {
            return repo.DeleteExperiment(experiment);
        }

        /// <summary>
        /// Delete a log from the repository 
        /// </summary>
        /// <param name="log"> The Log that will be deleted </param>
        /// <returns> True on success </returns>
        public Task<bool> DeleteLog(Log log)
        {
            return repo.DeleteLog(log);
        }

        /// <summary>
        /// Delete a scientist
        /// </summary>
        /// <param name="scientist">The Scientist that will be deleted</param>
        /// <returns> True on success </returns>
        public Task<bool> DeleteScientist(Scientist scientist)
        {
            return repo.DeleteScientist(scientist);
        }

        /// <summary>
        /// Update an Experiment that is already in the repo 
        /// </summary>
        /// <param name="experiment">The Experiment that will be updated</param>
        /// <returns> True on success </returns>
        public Task<bool> UpdateExperiment(Experiment experiment)
        {
            return repo.UpdateExperiment(experiment);
        }

        /// <summary>
        /// Update a Log that is already in the repo
        /// </summary>
        /// <param name="log">The Log that will be updated</param>
        /// <returns> True on success </returns>  
        public Task<bool> UpdateLog(Log log)
        {
            return repo.UpdateLog(log);
        }

        /// <summary>
        /// Update a Scientist that is already in the repo
        /// </summary>
        /// <param name="scientist">The Scientist that will be updated</param>
        /// <returns> True on success </returns>
        public Task<bool> UpdateScientist(Scientist scientist)
        {
            return repo.UpdateScientist(scientist);
        }

        /// <summary>
        /// Create or update a Scientist in the repository
        /// </summary>
        /// <param name="scientist"> The Scientist that will be created or updated </param>
        /// <returns> The Scientist that was created or updated </returns>
        public async Task<Scientist> CreateOrUpdateScientist(Scientist scientist)
        {

            if (scientist.Id == string.Empty)
                return await CreateScientist(scientist);

            if (await UpdateScientist(scientist))
                return scientist;

            return null;


        }

        /// <summary>
        /// Create or update an Experiment in the repository
        /// </summary>
        /// <param name="experiment"> The Experiment that will be created or updated </param>
        /// <returns> The Experiment that was created or updated </returns>
        public async Task<Experiment> CreateOrUpdateExperiment(Experiment experiment)
        {

            if (experiment.Id == string.Empty)
                return await CreateExperiment(experiment);

            if (await UpdateExperiment(experiment))
                return experiment;

            return null;


        }

        /// <summary>
        /// Create or update a Log in the repository
        /// </summary>
        /// <param name="log"> The Log that will be created or updated </param>
        /// <returns> The Log that was created or updated </returns>
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
        /// Get all logs for experiment from the repository
        /// </summary>
        /// <param name="experimentID"> The Id of the Experiment </param>
        /// <returns> A list of logs that were created under the Experiment </returns>
        public Task<Log[]> GetLogsForExperiment(string experimentID)
        {
            return repo.GetLogsForExperiment(experimentID);
        }

        /// <summary>
        /// Get all Experiments from the repository
        /// </summary>
        /// <returns> List of all experiments </returns>
        public async Task<Experiment[]> GetAllExperiments()
        {
            return await repo.GetAllExperiments();
        }

        /// <summary>
        /// Get experiment by ID from the repository
        /// </summary>
        /// <param name="experimentID"> The Id of the Experiment </param>
        /// <returns> The Experiment with the given ID </returns>
        public async Task<Experiment> GetExperimentByID(string experimentID)
        {
            return await repo.GetExperimentByID(experimentID);
        }
    }
}
