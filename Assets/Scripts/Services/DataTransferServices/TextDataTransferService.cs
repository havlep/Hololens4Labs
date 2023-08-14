using HoloLens4Labs.Scripts.DTOs;
using HoloLens4Labs.Scripts.Model;
using HoloLens4Labs.Scripts.Model.DataTypes;
using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{

    public class TextDataTransferService : DataTransferService<TextData, TextDataDTO, TextDataDTO>
    {
       public TextDataTransferService(RepositoryInterface<TextDataDTO, TextDataDTO> repository, MapperInterface<TextData, TextDataDTO, TextDataDTO> mapper): base(repository, mapper) { }
    }
}