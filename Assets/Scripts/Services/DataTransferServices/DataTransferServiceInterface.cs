using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{
    public interface DataTransferServiceInterface<OBJ, DTO>
    {
        public Task<DTO> Create(OBJ obj);
        public Task<DTO> Update(OBJ obj);
        public Task<bool> Delete(OBJ obj);
        public Task<DTO> Read(int id);

    }
}