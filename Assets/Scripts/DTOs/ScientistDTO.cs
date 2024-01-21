// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.
using Microsoft.WindowsAzure.Storage.Table;

namespace HoloLens4Labs.Scripts.DTOs
{
    /// <summary>
    /// Scientist DTO for transfering user info to the Azure Table service
    /// </summary>
    public class ScientistDTO : TableEntity
    {
        /// <summary>
        /// The name of the scientist
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The app id of the scientist
        /// </summary>
        public string ScientistID { get; set; }

        /// <summary>
        /// The id of the last experiment that the scientist worked on
        /// </summary>
        public string LastExperimentId { get; set; }

    }
}
