using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace GTL.Application.Helper
{
    public class CacheHelper
    {
        public static MemoryCacheEntryOptions CacheOptions()
        {
            return new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(1),
                AbsoluteExpiration = DateTimeOffset.Now + TimeSpan.FromHours(8),
            };
        }
    }
}
