using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;

namespace HoloLens4Labs.Scripts.Mappers
{
    /// <summary>
    /// Mapper class for mapping between the Scientist data model and the ScientistDTO
    /// </summary>
    public class ScientistMapper : MapperInterface<Scientist, ScientistDTO, ScientistDTO>
    {
        /// <summary>
        /// Method for mapping from an Scientist to the ScientistDTO when creating in a database for the first time 
        /// </summary>
        /// <param name="obj">The Scientist object being mapped</param>
        /// <returns>An ScientistDTO</returns>
        public ScientistDTO CreateDTO(Scientist obj)
        {
            return ToDTO(obj);
        }

        /// <summary>
        /// Method for mapping from an Scientist to the DTO
        /// </summary>
        /// <param name="obj">The Scientist being mapped</param>
        /// <returns>An ScientistDTO</returns>
        public ScientistDTO ToDTO(Scientist obj)
        {
            ScientistDTO dto = new ScientistDTO();

            dto.RowKey = dto.ScientistID = obj.Id;
            dto.Name = obj.Name;
            dto.ETag = "*";

            return dto;

        }

        /// <summary>
        /// Method for mapping from an ScientistDTO to the Scientist data model object
        /// </summary>
        /// <param name="dto">The DTO being mapped</param>
        /// <returns>Scientist data model object</returns>
        public Scientist ToOBJ(ScientistDTO dto)
        {
            return new Scientist(dto.RowKey, dto.Name);
        }
    }
}