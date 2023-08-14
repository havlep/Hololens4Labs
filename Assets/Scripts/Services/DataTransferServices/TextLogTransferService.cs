using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.Logs;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{

    public class TextLogTransferService : DataTransferService<TextLog, TextLogDTO, TextLogDTO>
    {
        TextLogTransferService(RepositoryInterface<TextLogDTO, TextLogDTO> repository, MapperInterface<TextLog, TextLogDTO, TextLogDTO> mapper) : base(repository, mapper) { }
    }
}
