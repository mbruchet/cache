using System;
using System.Threading.Tasks;
using Ecommerce.Data.RepositoryStore;
using ECommerce.Cache.RemoteServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Cache.RemoteServer.Controllers
{
    [Route("api/[controller]")]
    public class RemoteCacheController : Controller
    {
        private readonly IRemoteCacheService _remoteCacheService;

        public RemoteCacheController(IRemoteCacheService remoteCacheService)
        {
            _remoteCacheService = remoteCacheService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(string key, string value, TimeSpan? expiredIn = null)
        {
            await _remoteCacheService.AddItem(key, value, expiredIn);
            return Ok();
        }

        [HttpGet("{key}")]
        public async Task<string> GetItem(string key)
        {
            return await _remoteCacheService.GetItem(key);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItem(string key)
        {
            await _remoteCacheService.RemoveItem(key);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(string key, string newValue, TimeSpan? expiredIn = null)
        {
            await _remoteCacheService.UpdateItem(key, newValue, expiredIn);
            return Ok();
        }
    }
}