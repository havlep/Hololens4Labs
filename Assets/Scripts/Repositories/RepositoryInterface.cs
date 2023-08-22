using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories
{


    public interface RepositoryInterface
    {

        public Task<Experiment> CreateExperiment(Experiment experiment);

        public Task<bool> UpdateExperiment(Experiment experiment);

        public Task<bool> DeleteExperiment(Experiment experiment);

        public Task<Scientist> CreateScientist(Scientist scientist);
        
        public Task<bool> UpdateScientist(Scientist scientist);

        public Task<bool> DeleteScientist(Scientist scientist);

        public Task<Log> CreateLog(Log log);

        public Task<bool> UpdateLog(Log log);

        public Task<bool> DeleteLog(Log log);

        bool IsReady();


    }
}