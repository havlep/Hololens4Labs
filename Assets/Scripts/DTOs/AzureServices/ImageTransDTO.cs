namespace HoloLens4Labs.Scripts.DTOs
{
    /// <summary>
    /// DTO for the ReadResult object returned by the Image Vision service. The data model is on purpose simplified as most of the data is not used.
    /// </summary>
    public class ReadResultDTO
    {
        /// <summary>
        /// The text content of the image
        /// </summary>
        public string content { get; set; }

    }
    public class ImageTransDTO
    {
        /// <summary>
        /// The result as returend by the Image Vision service
        /// </summary>
        public ReadResultDTO readResult { get; set; }

    }
}