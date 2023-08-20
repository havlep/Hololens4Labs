using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Mappers;
using HoloLens4Labs.Scripts.Model;
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    public class ATScientistRepository : AzureTableObjectRepository<Scientist, ScientistDTO>
    {
        public ATScientistRepository(CloudTable table, string partitionKey) : base(new ScientistMapper(), table, partitionKey) { }

    }
}