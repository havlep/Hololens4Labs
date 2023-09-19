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

    }
}
