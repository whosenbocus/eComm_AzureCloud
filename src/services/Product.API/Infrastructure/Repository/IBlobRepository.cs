using System.Collections.Generic;
using System.Threading.Tasks;
using Product.API.Model;

namespace Product.API.Infrastructure.Repository
{
    public interface IBlobRepository
    {
        void SaveBlob (string url);

        IEnumerable<string> RetrieveBlob();

        void DeleteBlob();
    }
}