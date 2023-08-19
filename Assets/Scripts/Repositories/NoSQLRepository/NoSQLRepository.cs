using HoloLens4Labs.Scripts.DTOs;
using Microsoft.WindowsAzure.Storage.Table;
using HoloLens4Labs.Scripts.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System;

namespace HoloLens4Labs.Scripts.Repositories
{
    public abstract class NoSQLRepository<DTO> : RepositoryInterface<DTO, DTO> where DTO : ITableEntity, new() 
    {
        protected CloudTable table;
        protected string partitionKey;

        public NoSQLRepository(CloudTable table, string partitionKey)
        {
            this.table = table;
            this.partitionKey=partitionKey;
        }
        public async Task<DTO> Create(DTO obj) {

            // If the object does not have a partiation key then create a new one
            if (obj.RowKey == string.Empty)
                obj.RowKey = Guid.NewGuid().ToString();

            // See if there is an experiment with a similiar id
            var query = new TableQuery<DTO>().Where(
            TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, this.partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, obj.RowKey)));
            var segment = await this.table.ExecuteQuerySegmentedAsync(query, null);

            var experiment = segment.Results.FirstOrDefault();
            if (experiment != null)
            {
                // Experiment with a given id already exists throw an exception
                throw new ObjectDataBaseException("Object of given ID" + this.partitionKey +" already exists");
            }

            // Insert the object into the database
            var insertOrMergeOperation = TableOperation.InsertOrMerge(obj);
            var res = await this.table.ExecuteAsync(insertOrMergeOperation);

            if (res.Result == null)
            {

                throw new ObjectDataBaseException();

            }

            return (DTO)res.Result;

        }
        public async Task<DTO> Read(string id) {

            // See if there is an experiment with a similiar id
            var query = new TableQuery<DTO>().Where(
            TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, this.partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id)));
            var segment = await this.table.ExecuteQuerySegmentedAsync(query, null);

            var experiment = segment.Results.FirstOrDefault();
            if (experiment != null)
            {
                return experiment;
            }
            throw new ObjectDataBaseException("Object of given ID does not exist");

        }
        public async Task<bool> Delete(DTO obj) {

            var deleteOperation = TableOperation.Delete(obj);
            var result = await this.table.ExecuteAsync(deleteOperation);

            return result.HttpStatusCode == (int)HttpStatusCode.OK;

        }
        public async Task<bool> Update(DTO obj) {

            var insertOrMergeOperation = TableOperation.InsertOrMerge(obj);
            var result = await this.table.ExecuteAsync(insertOrMergeOperation);

            return result.Result != null;

        }
    }
}