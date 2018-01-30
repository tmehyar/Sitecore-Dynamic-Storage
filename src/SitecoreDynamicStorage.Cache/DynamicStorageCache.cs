using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDynamicStorage.Cache
{
	public class DynamicStorageCache
	{
		private const string _globalKey = "DynamicStorageCacheKey_";
		public object GetValue(string key, Func<string, string> callback)
		{
			var current = HttpContext.Current.Cache[_globalKey + key];

			if (current == null)
			{
				current = callback(key);
				if (current != null && string.IsNullOrEmpty(current.ToString()) == false)
					HttpContext.Current.Cache.Add(_globalKey + key, current, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
			}

			return current;
		}

		public void ClearCacheEntry(string key)
		{
			HttpContext.Current.Cache.Remove(_globalKey + key);
		}
	}
}