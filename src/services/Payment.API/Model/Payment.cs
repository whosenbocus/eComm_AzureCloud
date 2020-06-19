using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Model
{
    public class Payment : TableEntity
    {
        public Payment(string description)
        {
            this.PartitionKey = description;
            this.RowKey = Guid.NewGuid().ToString();
        }
        public Payment()
        {

        }
        //public int Id { get; set; }
        //public string Description { get; set; }
        public double Amount { get; set; }
        public new DateTime Timestamp { get; set; }
    }
}
