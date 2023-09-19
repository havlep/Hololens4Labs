using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.DTOs;

namespace HoloLens4Labs.Scripts.Mappers
{

    /// <summary>
    /// Mapper class for mapping between the Experiment data model and the ExperimentDTO
    /// </summary>
    public class ExperimentMapper : MapperInterface<Experiment, ExperimentDTO, ExperimentDTO>
    {
    
        /// <summary>
        /// Method for mapping from an Experiment to the DTO when creating in a database for the first time 
        /// </summary>
        /// <param name="obj">The Experiment being mapped</param>
        /// <returns>An ExperimentDTO</returns>
        public ExperimentDTO CreateDTO(Experiment obj)
        {
            return ToDTO(obj);
        }


        /// <summary>
        /// Method for mapping from an Experiment to the DTO
        /// </summary>
        /// <param name="obj">The Experiment being mapped</param>
        /// <returns>An ExperimentDTO</returns>

        public ExperimentDTO ToDTO(Experiment obj)
        {
            ExperimentDTO dto = new ExperimentDTO();

            dto.RowKey = dto.ExperimentID  = obj.Id;
            dto.Name = obj.Name;
            dto.ScientistsID = obj.CreatedByID;
            dto.DateTime = obj.CreatedOn;
            dto.ETag = "*";

            return dto;

        }

        /// <summary>
        /// Method for mapping from an ExperimentDTO to the Experiment data model object
        /// </summary>
        /// <param name="dto">The DTO being mapped</param>
        /// <returns>Experiment data model object</returns>
        public Experiment ToOBJ(ExperimentDTO dto)
        {
            return new Experiment(dto.RowKey, dto.Name, dto.ScientistsID, dto.DateTime);
        }
    }
}