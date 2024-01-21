// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System.Threading.Tasks;


namespace HoloLens4Labs.Scripts.Repositories
{

    /// <summary>
    /// CRUD interface for handling one structured objects
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    public interface ObjectRepositoryInterface<T>
    {
        /// <summary>
        /// Create a new object
        /// </summary>
        /// <param name="obj">The object that will be created in the database</param>
        /// <returns>A task holding the newly created object</returns>
        public Task<T> Create(T obj);

        /// <summary>
        /// Update an existing object
        /// </summary>
        /// <param name="obj">The object that will be updated in the database</param>
        /// <returns>A task holding a boolean indicating whether the update was successful</returns>
        public Task<bool> Update(T obj);

        /// <summary>
        /// Delete an existing object from the repository
        /// </summary>
        /// <param name="obj">The object that will be deleted from the database</param>
        /// <returns>A task holding a boolean indicating whether the deletion was successful</returns>
        public Task<bool> Delete(T obj);

        /// <summary>
        /// Read an object from the repository
        /// </summary>
        /// <param name="id">The id of the object to be read</param>
        /// <returns>A task holding the object</returns>
        public Task<T> Read(string id);

    }
}
