using Payment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;



namespace Payment.API.Infrastructure.Repositories
{
    public class AzureTablePaymentRepository : IPaymentRepository
    {
        private readonly CloudTable table;

        public AzureTablePaymentRepository()
        {
            var account = CloudStorageAccount.Parse("[connection string]");
            var client = account.CreateCloudTableClient();
            table = client.GetTableReference("Payment");
            table.CreateIfNotExists();
        }

        public async Task<bool> DeletePaymentAsync(Model.Payment payment)
        {
            TableOperation deleteOperation = TableOperation.Delete(payment);
            TableResult result = await table.ExecuteAsync(deleteOperation);
            if (result.RequestCharge.HasValue)
            {
                return true;
            }
            return false;
        }

        public async Task<Model.Payment> GetPaymentAsync(string desc, string Id)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Model.Payment>(desc, Id);
            TableResult result = await table.ExecuteAsync(retrieveOperation);
            Model.Payment customer = result.Result as Model.Payment;


            return customer;
        }

        public Task<IEnumerable<Model.Payment>> GetPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Model.Payment> SavePaymentAsync(Model.Payment payment)
        {
            // Create the InsertOrReplace table operation
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(payment);

            // Execute the operation.
            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
            Model.Payment insertedPayment = result.Result as Model.Payment;

            // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure Cosmos DB

            return insertedPayment;
        }

        public async Task<Model.Payment> UpdatePaymentAsync(Model.Payment payment)
        {
            // Create the InsertOrReplace table operation
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(payment);

            // Execute the operation.
            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
            Model.Payment insertedPayment = result.Result as Model.Payment;

            // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure Cosmos DB

            return insertedPayment;
        }
    }
}
