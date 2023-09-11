using HoloLens4Labs.Scripts.Exceptions;
using Microsoft.WindowsAzure.Storage.Table;
using HoloLens4Labs.Scripts.Mappers;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    public abstract class AzureTableObjectRepository
        <OBJ, DTO> : ObjectRepositoryInterface<OBJ> where DTO : ITableEntity, new()
    {
        protected CloudTable table = null;
        protected MapperInterface<OBJ, DTO, DTO> mapper;
        protected string defaultPartitionKey = null;


        public AzureTableObjectRepository(MapperInterface<OBJ, DTO, DTO> mapper, string defaultPartitionKey)
        {
            this.mapper = mapper;
            this.defaultPartitionKey=defaultPartitionKey;
        }


        public AzureTableObjectRepository(MapperInterface<OBJ, DTO, DTO> mapper, CloudTable table, string defaultPartitionKey) : this(mapper, defaultPartitionKey)
        {
            this.table = table;
        }
        public async Task<OBJ> Create(OBJ obj)
        {

            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var dto = mapper.CreateDTO(obj);

            if ( dto.RowKey == null || dto.RowKey == string.Empty )
                dto.RowKey = Guid.NewGuid().ToString();

            if ( dto.PartitionKey == null || dto.PartitionKey == string.Empty )
                dto.PartitionKey = defaultPartitionKey;

            // See if there is an object with a similiar id
            var query = new TableQuery<DTO>().Where(
            TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, dto.PartitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, dto.RowKey)));
            var segment = await this.table.ExecuteQuerySegmentedAsync(query, null);

            var respDTO = segment.Results.FirstOrDefault();
            if (respDTO != null)
                // Object with a given id already exists throw an exception
                throw new ObjectDataBaseException("Object of given ID" + dto.PartitionKey +" already exists");
            

            // Insert the object into the database
            var insertOrMergeOperation = TableOperation.InsertOrMerge(dto);
            var res = await this.table.ExecuteAsync(insertOrMergeOperation);

            if (res.Result == null)
                throw new ObjectDataBaseException();

            Debug.Log($"Object {dto.RowKey} created in repo");
            return mapper.ToOBJ((DTO)res.Result);

        }

        public async Task<OBJ> Read(string id)
        {

            return await Read(id, defaultPartitionKey);
        }
        public async Task<OBJ> Read(string id, string partitionKey)
        {
            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            // See if there is an resp with a similiar id
            var query = new TableQuery<DTO>().Where(
            TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id)));
            var segment = await this.table.ExecuteQuerySegmentedAsync(query, null);

            var resp = segment.Results.FirstOrDefault();
            if (resp != null)
            {
                return mapper.ToOBJ(resp);
            }
            throw new ObjectDataBaseException("Object of given ID does not exist");

        }
        public async Task<bool> Delete(OBJ obj)
        {

            var dto = mapper.ToDTO(obj);
            if (dto.PartitionKey == string.Empty)
                dto.PartitionKey = defaultPartitionKey;

            var deleteOperation = TableOperation.Delete(dto);
            var result = await this.table.ExecuteAsync(deleteOperation);

            if(result.HttpStatusCode == (int)HttpStatusCode.OK)
                Debug.Log($"Object {dto.RowKey} deleted from the repo");
            else 
                Debug.LogError($"Failed to delete Object {dto.RowKey} from repo with status code" + result.HttpStatusCode);

            return result.HttpStatusCode == (int)HttpStatusCode.OK;

        }
        public async Task<bool> Update(OBJ obj)
        {
            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var dto = mapper.ToDTO(obj);


            if (dto.PartitionKey == null || dto.PartitionKey == string.Empty)
                dto.PartitionKey = defaultPartitionKey;

            var insertOrMergeOperation = TableOperation.InsertOrMerge(dto);
            var result = await this.table.ExecuteAsync(insertOrMergeOperation);

            if(result.Result != null)
                Debug.Log($"Object {dto.RowKey} updated in the repo");
            else
                Debug.LogError($"Failed to update Object {dto.RowKey} in repo with status code" + result.HttpStatusCode);

            return result.Result != null;

        }

        //TODO : Implement pagination
        public async Task<(OBJ[], TableContinuationToken)> ReadOnePageAsync(int pageSize, TableContinuationToken continuationToken = null)
        {
            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var query = new TableQuery<DTO>().Take(pageSize);
            var segment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);

            var results = segment.Results.Select(x => mapper.ToOBJ(x)).ToList();
            return (results.ToArray(), segment.ContinuationToken);
        }
    }
}