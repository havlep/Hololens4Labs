using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;
using HoloLens4Labs.Scripts.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories
{
    public class ExperimentRepository : NoSQLRepository<ExperimentDTO, ExperimentDTO>
    {

        public ExperimentRepository(CloudTable table, string partitionKey) : base(table, partitionKey) { }

        /// <summary>
        /// Create an experiment of ID equal to objID if it does not exist.
        /// </summary>
        /// <returns>Experiment instance from database.</returns>
        public override async Task<ExperimentDTO> Create(ExperimentDTO obj)
        {

            // See if there is an experiment with a similiar id
            var query = new TableQuery<ExperimentDTO>().Where(
            TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, this.partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, obj.ExperimentID)));
            var segment = await this.table.ExecuteQuerySegmentedAsync(query, null);

            var experiment = segment.Results.FirstOrDefault();
            if (experiment != null)
            {
                // Experiment with a given id already exists throw an exception
                throw new ObjectDataBaseException("Object od given ID already exists");
            }

            // Insert the object into the database
            var insertOrMergeOperation = TableOperation.InsertOrMerge(obj);
            await this.table.ExecuteAsync(insertOrMergeOperation);

            return experiment;

        }

        public override Task<bool> Delete(ExperimentDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public override Task<ExperimentDTO> Read(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<ExperimentDTO> Update(ExperimentDTO obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
