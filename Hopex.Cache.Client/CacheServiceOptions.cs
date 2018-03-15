//------------------------------------------------------------
// <copyright file="CacheServiceOptions.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Cache service options
// </summary>
//------------------------------------------------------------
using Hopex.Quality.CodeQuality.Attributes;
using System;

namespace Hopex.Cache.Client
{
    [HopexAop]
    public class CacheServiceOptions : ContextBoundObject
    {

        /// <summary>
        /// Gets or sets the provider parameters
        /// </summary>
        public string CacheParameter { get; set; }

        [RequiredFieldValidator]
        /// <summary>
        /// Gets or sets the cache provider type name in format type;assembly
        /// </summary>
        public string CacheTypeName { get; set; }

        /// <summary>
        /// Gets or sets the environment to use
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        public string Language { get; set; }
    }
}
