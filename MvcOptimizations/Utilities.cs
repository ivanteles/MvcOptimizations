﻿using System;
using System.Web;
using System.Web.Caching;

namespace MvcOptimizations
{
    public class Utilities
    {
        public static T Cache<T>(string key, TimeSpan duration, Func<T> action, CacheItemPriority priority = CacheItemPriority.Normal) where T : class
        {
            var value = HttpRuntime.Cache[key] as T;

            if (value == null)
            {
                value = action();

                if (value != null)
                {
                    HttpRuntime.Cache.Insert(key,
                                      value,
                                      null,
                                      DateTime.Now + duration,
                                      System.Web.Caching.Cache.NoSlidingExpiration,
                                      priority,
                                      null);
                }                
            }

            return value;
        }
    }
}
