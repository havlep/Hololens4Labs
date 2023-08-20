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
        private RepositoryInterface repo = default;

        public Task<Experiment> CreateExperiment(Experiment experiment)
        {
            return repo.CreateExperiment(experiment);
        }

        public Task<Log> CreateLog(Log log)
        {
            return repo.CreateLog(log);
        }

        public Task<Scientist> CreateScientist(Scientist scientist)
        {
            return repo.CreateScientist(scientist);
        }

        public Task<bool> DeleteExperiment(Experiment experiment)
        {
            return repo.DeleteExperiment(experiment);
        }

        public Task<bool> DeleteLog(Log log)
        {
            return repo.DeleteLog(log);
        }

        public Task<bool> DeleteScientist(Scientist scientist)
        {
            return repo.DeleteScientist(scientist);
        }

        public Task<bool> UpdateExperiment(Experiment experiment)
        {
            return repo.UpdateExperiment(experiment);
        }

        public Task<bool> UpdateLog(Log log)
        {
            return repo.UpdateLog(log);
        }

        public Task<bool> UpdateScientist(Scientist scientist)
        {
            return repo.UpdateScientist(scientist);
        }

        public async Task<Scientist> CreateOrUpdateScientist(Scientist scientist)
        {

            if (scientist.Id == string.Empty)
                await CreateScientist(scientist);

            if (await UpdateScientist(scientist))
                return scientist;

            return null;


        }

        public async Task<Experiment> CreateOrUpdateExperiment(Experiment experiment)
        {

            if (experiment.Id == string.Empty)
                await CreateExperiment(experiment);

            if (await UpdateExperiment(experiment))
                return experiment;

            return null;


        }

        public async Task<Log> CreateOrUpdateLog(Log log)
        {

            if (log.Id == string.Empty)
                return await CreateLog(log);
            if (await UpdateLog(log))
                return log;
            return null;

        }

    }
}
