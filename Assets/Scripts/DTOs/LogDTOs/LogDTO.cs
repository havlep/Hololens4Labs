using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace HoloLens4Labs.Scripts.DTOs
{
    /// <summary>
    /// Log DTO for transfering logss to the Azure Table service
    /// </summary>
    /// <remarks>
    /// Rowkey is used for Log ID
    /// </remarks>
    public class LogDTO : TableEntity
    {

        // Common properties
        // Note: RowKey is used for Log ID

        /// <summary>
        /// The timestamp of when the log was created
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// The id of the experiment that the log was created within
        /// <!-- Note: PartitionKey is used for Experiment ID -->
        /// </summary>
        public string ExperimentID { get; set; } = string.Empty;

        /// <summary>
        /// The id of the scientist that created the log
        /// </summary>
        public string ScientistID { get; set; } = string.Empty;

        // Properties related to the LogType

        /// <summary>
        /// The ID of the log if it is a TextLog
        /// <!-- Will be empty if of any other Log type -->
        /// </summary>
        public string TextLogID { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the log if it is a ImageLog
        /// <!-- Will be empty if of any other Log type -->
        /// </summary>
        public string ImageLogID { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the log if it is a TranscriptionLog
        /// <!-- Will be empty if of any other Log type -->
        /// </summary>
        public string TranscriptionLogID { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the log if it is a WeightLog
        /// <!-- Will be empty if of any other Log type -->
        /// <!--WeightLog handeling is currently not implement this is only for future functionality-->
        /// </summary>
        public string WeightLogID { get; set; } = string.Empty;

        // Properties related to the DataType element associated with the log 

        /// <summary>
        /// The Id of the scientist who created the DataType element
        /// </summary>
        /// 
        public string DataScientistID { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the DataType element    
        /// </summary>
        public string DataID { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of when the DataType element was created
        /// </summary>
        public DateTime DataDateTime { get; set; }

        /// <summary>
        /// The text associated with the DataType element
        /// <!-- Will be empty for ImageLogs -->
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// The Image associated with the DataType element
        /// <!-- Will be empty for TextLogs -->
        /// </summary>
        public string ThumbnailBlobName { get; set; } = string.Empty;


    }
}
