using System;
using System.Threading.Tasks;

namespace ECommerce.Cache.RemoteServer.Services
{
    public interface IRemoteCacheService
    {
        Task AddItem(string key, string value, TimeSpan? expiredIn = null);
        Task<string> GetItem(string key);
        Task RemoveItem(string key);
        Task UpdateItem(string key, string newValue, TimeSpan? expiredIn = null);
    }
}