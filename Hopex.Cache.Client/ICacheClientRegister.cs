//------------------------------------------------------------
// <copyright file="ICacheClientRegister.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Interface to implement a cache client register
// </summary>
//------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hopex.Cache.Client
{
    public interface ICacheClientRegister
    {
        void Register(CacheServiceOptions cacheConfiguration, ILogger logger, IServiceCollection services);
    }
}
