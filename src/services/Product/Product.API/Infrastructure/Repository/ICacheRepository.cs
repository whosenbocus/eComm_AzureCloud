using System.Threading.Tasks;
using Product.API.Model;

namespace Product.API.Infrastructure.Repository
{
    public interface ICacheRepository
    {
        CacheObject GetCache(string Metadata);
        void SaveCache(CacheObject message,string Metadata);
    }
}