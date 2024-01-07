using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model.Logs;


namespace HoloLens4Labs.Scripts.Mappers
{
    public class TextLogMapper : MapperInterface<TextLog, TextLogDTO, TextLogDTO>
    {
        public TextLogDTO CreateDTO(TextLog obj)
        {
            return ToDTO(obj);
        }

        public TextLogDTO ToDTO(TextLog obj)
        {
            TextLogDTO dto = new TextLogDTO();

            dto.TextID = obj.TextData.Id;
            dto.LogID = obj.Id;
            dto.TextLogID = obj.Id;
            dto.RowKey = obj.Id;

            return dto;

        }

        public TextLog ToOBJ(TextLogDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}