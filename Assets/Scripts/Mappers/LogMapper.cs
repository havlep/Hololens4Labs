using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model.DataTypes;


namespace HoloLens4Labs.Scripts.Mappers
{
    public class LogMapper : MapperInterface<Log, LogDTO, LogDTO>
    {
        public LogDTO CreateDTO(Log obj)
        {
            return ToDTO(obj);
        }

        public LogDTO ToDTO(Log log)
        {
            LogDTO dto = new LogDTO();

            dto.RowKey = log.Id;
            dto.DateTime = log.CreatedOn;
            dto.ExperimentID = log.DoneWithin.Id;
            dto.ScientistID = log.CreatedBy.Id;
            dto.PartitionKey = dto.ExperimentID;
            dto.ETag = "*";

            if (log is TextLog)
                dto = ToDTO((TextLog)log, dto);
            else if (log is TranscriptionLog)
                dto = ToDTO((TranscriptionLog)log, dto);
            else if (log is ImageLog)
                dto = ToDTO((ImageLog)log, dto);
            else
                throw new System.NotImplementedException("No yet implemented log type");

            return dto;

        }

        private LogDTO ToDTO(TextLog textLog, LogDTO dto)
        {

            dto.TextLogID = textLog.Id;

            dto = LogDTO(textLog.TextData, dto);

            return dto;

        }

        private LogDTO LogDTO(TextData textData, LogDTO dto)
        {
            if (textData != null)
            {
                dto.DataID = textData.Id;
                dto.DataDateTime = textData.CreatedOn;
                dto.DataScientistID = textData.CreatedById;
                dto.Text = textData.Text;
            }

            return dto;

        }

        private LogDTO ToDTO(ImageLog imageLog, LogDTO dto)
        {

            dto.ImageLogID = imageLog.Id;

            dto = ToDTO(imageLog.Data, dto);

            return dto;

        }

        private LogDTO ToDTO(TranscriptionLog transcriptionLog, LogDTO dto) {

            dto.TranscriptionLogID = transcriptionLog.Id;

            dto = ToDTO(transcriptionLog.Data, dto);

            return dto;

        }

        private LogDTO ToDTO(TranscriptionData transcriptionData, LogDTO dto)
        {
            if (transcriptionData != null)
            {
                dto.DataID = transcriptionData.Id;
                dto.DataDateTime = transcriptionData.CreatedOn;
                dto.DataScientistID = transcriptionData.CreatedById;
                dto.ThumbnailBlobName = transcriptionData.ThumbnailBlobName;
            }
            return dto;

        }

        private LogDTO ToDTO(ImageData imageData, LogDTO dto)
        {

            if (imageData != null)
            {
                dto.DataID = imageData.Id;
                dto.DataDateTime = imageData.CreatedOn;
                dto.DataScientistID = imageData.CreatedById;
                dto.ThumbnailBlobName = imageData.ThumbnailBlobName;
            }
            return dto;

        }

        public Log ToOBJ(LogDTO dto)
        {
            if (dto.TextLogID != null && dto.TextLogID != string.Empty )
            {

                TextData data = null;
                if (dto.DataID != null && dto.DataID != string.Empty)
                    data = new TextData(dto.DataID, dto.DataDateTime, dto.DataScientistID, dto.RowKey, dto.Text);

                return new TextLog(dto.RowKey, dto.DateTime, dto.ScientistID, dto.ExperimentID, data);

            }
            else if (dto.ImageLogID != null && dto.ImageLogID != string.Empty)
            {
                ImageData data = null;
                if (dto.DataID != null && dto.DataID != string.Empty)
                    data = new ImageData(dto.DataID, dto.DataDateTime, dto.DataScientistID, dto.RowKey, dto.ThumbnailBlobName);

                return new ImageLog(dto.RowKey, dto.DateTime, dto.ScientistID, dto.ExperimentID, data);

            }
            else if (dto.TranscriptionLogID != null && dto.TranscriptionLogID != string.Empty) {
                TranscriptionData data = null;
                if (dto.DataID != null && dto.DataID != string.Empty)
                    data = new TranscriptionData(dto.DataID, dto.DataDateTime, dto.DataScientistID, dto.RowKey, dto.ThumbnailBlobName, dto.Text);

                return new TranscriptionLog(dto.RowKey, dto.DateTime, dto.ScientistID, dto.ExperimentID, data);

            }
            else
            {
                throw new System.NotImplementedException("No yet implemented log type");
            }
        }
    }
}