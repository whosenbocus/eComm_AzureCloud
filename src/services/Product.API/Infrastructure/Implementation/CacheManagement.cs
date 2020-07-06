using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Product.API.Infrastructure.Repository;
using Product.API.Model;
using StackExchange.Redis;

public class CacheManagement : ICacheRepository
{
    public IConfiguration Configuration { get; }

    private readonly IDatabase cache;
    public CacheManagement(IConfiguration configuration)
    {
        Configuration = configuration;
        string connectionString = Configuration["CacheConnectionString"];
        //"<cache name>.redis.cache.windows.net,abortConnect=false,ssl=true,password=<primary-access-key>"
        cache =  ConnectionMultiplexer.Connect(connectionString).GetDatabase();
    }

    public CacheObject GetCache(string Metadata)
    {
        string Value = cache.StringGet(Metadata).ToString();
        CacheObject obj = JsonConvert.DeserializeObject<CacheObject>(Value);
        return obj;
    }

    public void SaveCache(CacheObject message, string Metadata)
    {
        string jsonValue = JsonConvert.SerializeObject(message);
        cache.StringSet(Metadata,jsonValue);
    }
}