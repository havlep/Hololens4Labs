using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.DTOs;

namespace HoloLens4Labs.Scripts.Mappers
{
    public class ExperimentMapper : MapperInterface<Experiment, ExperimentDTO, ExperimentDTO>
    {
        public ExperimentDTO CreateDTO(Experiment obj)
        {
            return ToDTO(obj);
        }

        public ExperimentDTO ToDTO(Experiment obj)
        {
            ExperimentDTO dto = new ExperimentDTO();

            dto.RowKey = dto.ExperimentID  = obj.Id;
            dto.Name = obj.Name;
            dto.ScientistsID = obj.CreatedByID;

            return dto;

        }

        public Experiment ToOBJ(ExperimentDTO dto)
        {
            return new Experiment(dto.RowKey, dto.Name, dto.ScientistsID);
        }
    }
}