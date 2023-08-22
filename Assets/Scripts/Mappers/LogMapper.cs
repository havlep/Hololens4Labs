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

        public LogDTO ToDTO(Log obj)
        {
            LogDTO dto = new LogDTO();

            dto.RowKey = obj.Id;
            dto.DateTime = obj.CreatedOn;
            dto.ExperimentID = obj.DoneWithin.Id;
            dto.ScientistID = obj.CreatedBy.Id;
            dto.PartitionKey = dto.ExperimentID;
            dto.ETag = "*";

            if (obj is TextLog)
            {
                var textLog = (TextLog)obj;

                dto.TextLogID = textLog.Id;
                if (textLog.TextData != null)
                {
                    dto.TextID = textLog.TextData.Id;
                    dto.DataDateTime = textLog.TextData.CreatedOn;
                    dto.DataScientistID = textLog.TextData.CreatedById;
                    dto.Text = textLog.TextData.Text;
                }

            }
            else {

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


            }
            else {
                throw new System.NotImplementedException("No yet implemented log type");
            }
        }
    }
}