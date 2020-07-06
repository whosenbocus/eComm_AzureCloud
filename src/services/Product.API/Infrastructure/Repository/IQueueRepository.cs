using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Model;

namespace Product.API.Infrastructure.Repository
{
    public interface IQueueRepository
    {
        Task<QueueMessage> GetMessage();
        void SaveMessage(QueueMessage message);
        void UpdateMessage(QueueMessage message);

        void DeleteMessage();
    }
}
