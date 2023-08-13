
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace HoloLens4Labs.Scripts.Repositories
{
    public abstract class NoSQLRepository<DTO, CDTO> : RepositoryInterface<DTO, CDTO>
    {
        protected CloudTable table;
        protected string partitionKey;

        public NoSQLRepository(CloudTable table, string partitionKey)
        {
            this.table = table;
            this.partitionKey=partitionKey;
        }
        public abstract Task<DTO> Create(CDTO obj);
        public abstract Task<DTO> Read(int id);
        public abstract Task<bool> Delete(DTO obj);
        public abstract Task<DTO> Update(DTO obj);
    }
}