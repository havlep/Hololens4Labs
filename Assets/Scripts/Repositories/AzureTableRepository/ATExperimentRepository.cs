using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    /// <summary>
    /// A concerete implementation of Azure Table object repository interface for the Experiment data model class
    /// </summary>
    public class ATExperimentRepository : AzureTableObjectRepository<Experiment, ExperimentDTO>
    {
        public ATExperimentRepository(CloudTable table, string partitionKey) : base(new ExperimentMapper(), table, partitionKey) { }

    }
}