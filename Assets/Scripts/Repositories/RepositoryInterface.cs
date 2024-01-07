using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories
{

    /// <summary>
    /// CRUD interface for all of the application model objects that are stored in a database
    /// </summary>
    public interface RepositoryInterface
    {
        /// <summary>
        /// Creates a new Experiment in the database
        /// </summary>
        /// <param name="experiment">The Experiment to be created</param>
        /// <returns>The created Experiment</returns>
        public Task<Experiment> CreateExperiment(Experiment experiment);

        /// <summary>
        /// Updates an existing Experiment in the database
        /// </summary>
        /// <param name="experiment">The Experiment to be updated</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        public Task<bool> UpdateExperiment(Experiment experiment);

        /// <summary>
        /// Delete an existing Experiment from the database
        /// </summary>
        /// <param name="experiment">The Experiment to be deleted</param>
        /// <returns>True if the deletion was successful, false otherwise</returns>
        public Task<bool> DeleteExperiment(Experiment experiment);

        /// <summary>
        /// Create a new Scientist in the database
        /// </summary>
        /// <param name="scientist">The Scientist to be created</param>
        /// <returns>The created Scientist</returns>
        public Task<Scientist> CreateScientist(Scientist scientist);

        /// <summary>
        /// Update an existing Scientist in the database
        /// </summary>
        /// <param name="scientist">The Scientist to be updated</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        public Task<bool> UpdateScientist(Scientist scientist);

        /// <summary>
        /// Delete an existing Scientist from the database
        /// </summary>
        public Task<bool> DeleteScientist(Scientist scientist);

        /// <summary>
        /// Create a log in the database
        /// </summary>
        /// <param name="log">The log to be created</param>
        /// <returns>The created log</returns>
        public Task<Log> CreateLog(Log log);

        /// <summary>
        /// Update an existing log in the database
        /// </summary>
        /// <param name="log">The Log that will be updated</param>
        /// <returns>True if update was succefull</returns>
        public Task<bool> UpdateLog(Log log);

        /// <summary>
        /// Delete an existing log from the database
        /// </summary>
        /// <param name="log">The Log that will be deleted</param>
        /// <returns>True if deletion was succefull</returns>
        public Task<bool> DeleteLog(Log log);

        /// <summary>
        /// Get all logs for a given Experiment
        /// </summary>
        /// <param name="experimentID">The id of the Experiment</param>
        /// <returns>An array of all the logs for the given Experiment</returns>
        public Task<Log[]> GetLogsForExperiment(string experimentID);

        /// <summary>
        /// Get all Experiments from the database
        /// </summary>
        /// <returns>A list of all experiment in the database</returns>
        public Task<Experiment[]> GetAllExperiments();

        bool IsReady();


    }
}