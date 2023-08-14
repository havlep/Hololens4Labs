using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories
{


    public interface RepositoryInterface<DTO, CDTO>
    {

        public Task<DTO> Create(CDTO obj);
        public Task<bool> Update(DTO obj);
        public Task<bool> Delete(DTO obj);
        public Task<DTO> Read(string id);

    }
}