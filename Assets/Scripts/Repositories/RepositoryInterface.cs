
namespace HoloLens4Labs.Scripts.Repositories { 


    public interface RepositoryInterface<DTO, CDTO> 
    {

        public DTO Create(CDTO obj);
        public DTO Update(DTO obj);
        public bool Delete(DTO obj);
        public DTO Read(int i);

    }
}