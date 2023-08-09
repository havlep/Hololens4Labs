using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.DTOs;



namespace HoloLens4Labs.Scripts.Mappers
{
    public class TextDataMapper : MapperInterface<TextData, TextDataDTO, TextDataDTO>
    {
        public TextDataDTO CreateDTO(TextData obj)
        {
            return ToDTO(obj);
        }

        public TextDataDTO ToDTO(TextData obj)
        {
            TextDataDTO dto = new TextDataDTO();

            dto.TextID = obj.Id.ToString();
            dto.TextLogID = obj.DoneWithinLogID.ToString();
            dto.ScientistID = obj.CreatedById.ToString();
            dto.Created = obj.DateTime;
            dto.Text = obj.Text;

            return dto;

        }

    }
}