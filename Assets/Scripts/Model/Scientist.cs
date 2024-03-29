// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

namespace HoloLens4Labs.Scripts.Model
{

    /// <summary>
    /// The Scientist data model class that represents users
    /// </summary>
    public class Scientist
    {
        /// <summary>
        /// The Id of the scientist in the database
        /// </summary>
        string id = string.Empty;

        /// <summary>
        /// The name of the scientist
        /// </summary>
        string name = string.Empty;
        
        /// <summary>
        /// The id of the last experiment that the scientist worked on
        /// </summary>
        string lastExperimentId = string.Empty;

        /// <summary>
        /// Property for getting and setting the name of the scientist
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Property for getting and setting the id of the last experiment that the scientist worked on
        /// </summary>
        public string LastExperimentId
        {
            get => lastExperimentId; 
            set => lastExperimentId = value;
        }

        /// <summary>
        /// Property for getting and setting the database id of the scientist 
        /// </summary>
        public string Id { get => id; set => id = value; }

        /// <summary>
        /// Constructor used when scientist name is not yet known and the scientist is not in the database
        /// </summary>
        public Scientist() { }

        /// <summary>
        /// Constructor used when scientist name is known and the scientist is not in the database
        /// </summary>
        /// <param name="name">The name of the scientist</param>
        public Scientist(string name) { this.Name = name; }

        /// <summary>
        /// Constructor used when scientist name and id is known and the scientist is in the database
        /// </summary>
        /// <param name="id">The database ID of the scientist</param>
        /// <param name="name">The name of the scientist</param>
        /// <param name="lastExperimentId">The Id of the last experiment used by the scientist</param>
        public Scientist(string id, string name, string lastExperimentId) : this(name)
        {
            this.id = id;
            this.LastExperimentId = lastExperimentId;
        }

    }
}