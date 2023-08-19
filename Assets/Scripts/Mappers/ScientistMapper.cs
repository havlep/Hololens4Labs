using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.DTOs;

namespace HoloLens4Labs.Scripts.Mappers
{
    public class ScientistMapper : MapperInterface<Scientist, ScientistDTO, ScientistDTO>
    {
        public ScientistDTO CreateDTO(Scientist obj)
        {
            return ToDTO(obj);
        }

        public ScientistDTO ToDTO(Scientist obj)
        {
            ScientistDTO dto = new ScientistDTO();

            dto.ScientistID   = obj.Id;
            dto.Name = obj.Name;
            dto.RowKey = dto.ScientistID;

            return dto;

        }

    }
}