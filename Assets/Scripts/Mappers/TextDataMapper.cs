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

            dto.TextDataID = obj.Id;
            dto.TextLogID = obj.DoneWithinLogID;
            dto.ScientistID = obj.CreatedById;
            dto.Created = obj.CreatedOn;
            dto.Text = obj.Text;
            dto.RowKey = obj.Id;

            return dto;

        }

    }
}