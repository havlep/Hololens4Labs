
namespace HoloLens4Labs.Scripts.Repositories
{
    public abstract class NoSQLRepository<DTO, CDTO> : RepositoryInterface<DTO, CDTO> 
    {
       public abstract DTO Create(CDTO obj);
       public abstract DTO Read(int id);
       public abstract bool Delete(DTO obj);
       public abstract DTO Update(DTO obj);
    }
}