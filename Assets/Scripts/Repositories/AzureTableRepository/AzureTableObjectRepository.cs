using HoloLens4Labs.Scripts.Exceptions;
using HoloLens4Labs.Scripts.Mappers;
using Microsoft.WindowsAzure.Storage.Table;
// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Repositories.AzureTables
{
    /// <summary>
    /// Abstract class for all Azure Table object repositories
    /// </summary>
    /// <typeparam name="DTO">The DTO type for the repository and data model class</typeparam>
    /// <typeparam name="OBJ">The data model class stored in the repository</typeparam>
    public abstract class AzureTableObjectRepository
        <OBJ, DTO> : ObjectRepositoryInterface<OBJ> where DTO : ITableEntity, new()
    {
        /// <summary>
        /// A Azure Table object repository
        /// </summary>
        protected CloudTable table = null;

        /// <summary>
        /// The mapper between the data model class and the DTO
        /// </summary>
        protected MapperInterface<OBJ, DTO, DTO> mapper;

        /// <summary>
        /// The default partition key for the repository
        /// </summary>
        protected string defaultPartitionKey = null;

        /// <summary>
        /// Constructor for the Azure Table object repository when the table is not known at the time of construction
        /// </summary>
        /// <param name="mapper">Mapper for mapping DTOs and OBJs</param>
        /// <param name="defaultPartitionKey">The default partition key</param>
        public AzureTableObjectRepository(MapperInterface<OBJ, DTO, DTO> mapper, string defaultPartitionKey)
        {
            this.mapper = mapper;
            this.defaultPartitionKey=defaultPartitionKey;
        }

        /// <summary>
        /// Constructor for the Azure Table object repository when the table is known at the time of construction
        /// </summary>
        /// <param name="mapper">Mapper for mapping DTOs and OBJs</param>
        /// <param name="table">The Azure Table object repository</param>
        /// <param name="defaultPartitionKey">The default partition key</param>
        public AzureTableObjectRepository(MapperInterface<OBJ, DTO, DTO> mapper, CloudTable table, string defaultPartitionKey) : this(mapper, defaultPartitionKey)
        {
            this.table = table;
        }

        /// <summary>
        /// Create an object in the repository
        /// </summary>
        /// <param name="obj">The OBJ that will be created in the repository</param>
        /// <returns>The OBJ that was created in the repository</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDataBaseException"></exception>
        public async Task<OBJ> Create(OBJ obj)
        {

            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var dto = mapper.CreateDTO(obj);

            if (dto.RowKey == null || dto.RowKey == string.Empty)
                dto.RowKey = Guid.NewGuid().ToString();

            if (dto.PartitionKey == null || dto.PartitionKey == string.Empty)
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

        /// <summary>
        /// Read an object from the repository using the default partition key
        /// </summary>
        /// <returns>The OBJ that was read from the repository</returns>
        public async Task<OBJ> Read(string id)
        {

            return await Read(id, defaultPartitionKey);
        }

        /// <summary>
        /// Read an object from the repository using the provided partition key
        /// </summary>
        /// <param name="id">The id of the object to be read</param>
        /// <param name="partitionKey">The partition key of the object to be read</param>
        /// <returns>The OBJ that was read from the repository</returns>
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

        /// <summary>
        /// Delete an object from the repository
        /// </summary>
        /// <param name="obj">The OBJ that will be deleted from the repository</param>
        /// <returns>True if the object was deleted, false otherwise</returns>
        public async Task<bool> Delete(OBJ obj)
        {

            var dto = mapper.ToDTO(obj);
            if (dto.PartitionKey == string.Empty)
                dto.PartitionKey = defaultPartitionKey;

            var deleteOperation = TableOperation.Delete(dto);
            var result = await this.table.ExecuteAsync(deleteOperation);

            if (result.HttpStatusCode == (int)HttpStatusCode.OK)
                Debug.Log($"Object {dto.RowKey} deleted from the repo");
            else
                Debug.LogError($"Failed to delete Object {dto.RowKey} from repo with status code" + result.HttpStatusCode);

            return result.HttpStatusCode == (int)HttpStatusCode.OK;

        }

        /// <summary>
        /// Update an object in the repository
        /// </summary>
        /// <param name="obj">The OBJ that will be updated in the repository</param>
        /// <returns>True if the object was updated, false otherwise</returns>
        public async Task<bool> Update(OBJ obj)
        {
            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var dto = mapper.ToDTO(obj);


            if (dto.PartitionKey == null || dto.PartitionKey == string.Empty)
                dto.PartitionKey = defaultPartitionKey;

            var insertOrMergeOperation = TableOperation.InsertOrMerge(dto);
            var result = await this.table.ExecuteAsync(insertOrMergeOperation);

            if (result.Result != null)
                Debug.Log($"Object {dto.RowKey} updated in the repo");
            else
                Debug.Log($"Failed to update Object {dto.RowKey} in repo with status code" + result.HttpStatusCode);

            return result.Result != null;

        }
        /// <summary>
        /// Read one page of objects from the repository
        /// </summary>
        /// <param name="pageSize">The number of objects to be read in one page</param>
        /// <param name="continuationToken">The continuation token for the pagination session</param>
        /// <returns>A list of objects and a session continuation token</returns>
        public async Task<(OBJ[], TableContinuationToken)> ReadOnePageAsync(int pageSize, TableContinuationToken continuationToken = null)
        {
            if (table == null)
                throw new NotSupportedException("Table was not initialized for this type");

            var query = new TableQuery<DTO>().Take(pageSize);
            var segment = await table.ExecuteQuerySegmentedAsync(query, continuationToken);

            var results = segment.Results.Select(x => mapper.ToOBJ(x)).ToArray();
            return (results, segment.ContinuationToken);
        }
    }
}