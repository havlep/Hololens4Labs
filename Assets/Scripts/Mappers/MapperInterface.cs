
using System;
using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Mappers
{

    /// <summary>
    /// An interface for the mappers for mapping between the data model and the DTOs
    /// </summary>
    public interface MapperInterface<OBJ, DTO, CDTO>
    {

        /// <summary>
        /// The method for mapping from the data model to the DTO
        /// </summary>
        /// <param name="obj">The object to be mapped</param>
        /// <returns>The DTO for the data model object</returns>
        DTO ToDTO(OBJ obj);

        /// <summary>
        /// The method for creating a DTO from the data model
        /// </summary>
        /// <param name="obj">The object to be mapped</param>
        /// <returns>The Create DTO for the data model object</returns>
        CDTO CreateDTO(OBJ obj);

        /// <summary>
        /// The method for mapping from the DTO to the data model
        /// </summary>
        /// <param name="dto">The DTO to be mapped</param>
        /// <returns>The data model object</returns>
        OBJ ToOBJ(DTO dto);

    }
}