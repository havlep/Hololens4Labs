

namespace HoloLens4Labs.Scripts.Services.DataTransferServices
{
    public interface DataTransferServiceInterface<OBJ, DTO>
    {
        public DTO Create(OBJ obj);
        public DTO Update(OBJ obj);
        public bool Delete(OBJ obj);
        public DTO Read(int id);

    }
}