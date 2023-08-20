using System.Threading.Tasks;


namespace HoloLens4Labs.Scripts.Repositories
{

    public interface ObjectRepositoryInterface<T>
    {
        public Task<T> Create(T obj);
        public Task<bool> Update(T obj);
        public Task<bool> Delete(T obj);
        public Task<T> Read(string id);

    }
}
