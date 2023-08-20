using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.DTOs;

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

            dto.LogID   = obj.Id;
            dto.DateTime = obj.CreatedOn;
            dto.ExperimentID = obj.DoneWithin.Id.ToString();
            dto.ScientistID = obj.CreatedBy.Id.ToString();

            if (obj is TextLog)
            {
                dto.TextLogID = obj.Id.ToString();
            }
            else {

                throw new System.Exception("No yet implemented log type");
            
            }
            dto.RowKey = dto.LogID;


            return dto;

        }

        public Log ToOBJ(LogDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}