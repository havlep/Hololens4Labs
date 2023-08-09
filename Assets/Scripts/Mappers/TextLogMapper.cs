using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.DTOs;


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

            dto.TextID = obj.TextData.Id.ToString();
            dto.LogID = obj.Id.ToString();
            dto.TextLogID = obj.Id.ToString();

            return dto;

        }

    }
}