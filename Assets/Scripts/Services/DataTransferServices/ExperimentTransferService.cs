using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{

    public class ExperimentTransferService : DataTransferService<Experiment, ExperimentDTO, ExperimentDTO>
    {
        public ExperimentTransferService(RepositoryInterface<ExperimentDTO, ExperimentDTO> repository, MapperInterface<Experiment, ExperimentDTO, ExperimentDTO> mapper) : base(repository, mapper) { }
    }
}