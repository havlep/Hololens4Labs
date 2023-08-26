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
            {
                var textLog = (TextLog)log;

                dto.TextLogID = textLog.Id;
                if (textLog.TextData != null)
                {
                    dto.TextID = textLog.TextData.Id;
                    dto.DataDateTime = textLog.TextData.CreatedOn;
                    dto.DataScientistID = textLog.TextData.CreatedById;
                    dto.Text = textLog.TextData.Text;
                }

            }if (log is ImageLog)
            {
                var imageLog = (ImageLog)log;

                dto.ImageLogID = imageLog.Id;
                if (imageLog.ImageData != null)
                {
                    dto.ImageID = imageLog.ImageData.Id;
                    dto.DataDateTime = imageLog.ImageData.CreatedOn;
                    dto.DataScientistID = imageLog.ImageData.CreatedById;
                    dto.ThumbnailBlobName = imageLog.ImageData.ThumbnailBlobName;

                }
            }
            else
            {

                throw new System.NotImplementedException("No yet implemented log type");

            }
            


            return dto;

        }

        public Log ToOBJ(LogDTO dto)
        {
            if (dto.TextLogID != null)
            {

                TextData data = null;
                if(dto.TextID != null)    
                    data = new TextData(dto.TextID, dto.DataDateTime, dto.DataScientistID, dto.RowKey ,dto.Text );

                TextLog textLog = new TextLog(dto.RowKey, dto.DateTime, dto.ScientistID, dto.ExperimentID, data);

                return textLog;


            } else if (dto.ImageLogID != null)
            {
                ImageData data = null;
                if(dto.ImageID != null)    
                    data = new ImageData(dto.TextID, dto.DataDateTime, dto.DataScientistID, dto.RowKey ,dto.ThumbnailBlobName );

                ImageLog imageLog = new ImageLog(dto.RowKey, dto.DateTime, dto.ScientistID, dto.ExperimentID, data);

                return imageLog;
            }
            else {
                throw new System.NotImplementedException("No yet implemented log type");
            }
        }
    }
}