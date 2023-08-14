using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{

    public class ScientistTransferService : DataTransferService<Scientist, ScientistDTO, ScientistDTO>
    {
        public ScientistTransferService(RepositoryInterface<ScientistDTO, ScientistDTO> repository, MapperInterface<Scientist, ScientistDTO, ScientistDTO> mapper) : base(repository, mapper) { }
    }
}