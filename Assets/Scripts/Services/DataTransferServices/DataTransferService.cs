


using HoloLens4Labs.Scripts.Repositories;
using HoloLens4Labs.Scripts.Mappers;


namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{

    public class DataTransferService<OBJ, DTO, CDTO> : DataTransferServiceInterface<OBJ, DTO>
    {

        RepositoryInterface<DTO, CDTO> _repo;
        MapperInterface<OBJ, DTO, CDTO> _mapper;

        public DataTransferService(RepositoryInterface<DTO, CDTO> repository,  MapperInterface<OBJ, DTO, CDTO> mapper)
        {

            _repo = repository;
            _mapper = mapper;

        }

        public DTO Create(OBJ obj)
        {

            CDTO cdto = _mapper.CreateDTO(obj);
            return _repo.Create(cdto);
            

        }

        public DTO Update(OBJ obj)
        {

            return _repo.Update(_mapper.ToDTO(obj));

        }

        public bool Delete(OBJ obj)
        {

            return _repo.Delete(_mapper.ToDTO(obj));

        }

        public DTO Read(int id)
        {

            return _repo.Read(id);

        }

    }
}



