using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{

    public class LogTransferService : DataTransferService<Log, LogDTO, LogDTO>
    {
        public LogTransferService(RepositoryInterface<LogDTO, LogDTO> repository, MapperInterface<Log, LogDTO, LogDTO> mapper) : base(repository, mapper) { }
    }
}
