using SitecoreDynamicStorage.Cache;
using System;

namespace SitecoreDynamicStorage.Core.Extensions
{
	public static class DynamicStorageExtensions
	{
		public static string GetDynamicStorageValue(this Sitecore.Sites.SiteContext context, string key)
		{
			return GetFromDB(key);
		}

		public static void SetDynamicStorageValue(this Sitecore.Sites.SiteContext context, string key, string value)
		{
			SetToDB(key, value);
		}

		public static void SetDynamicStorageValue(this Sitecore.Sites.SiteContext context, string key, int value)
		{
			SetToDB(key, value.ToString());
		}

		public static void SetDynamicStorageValue(this Sitecore.Sites.SiteContext context, string key, long value)
		{
			SetToDB(key, value.ToString());
		}

		public static void SetDynamicStorageValue(this Sitecore.Sites.SiteContext context, string key, DateTime value)
		{
			SetToDB(key, value.ToString());
		}

		public static int GetDynamicStorageInt32Value(this Sitecore.Sites.SiteContext context, string key)
		{
			return int.Parse(GetFromDB(key));
		}

		public static long GetDynamicStorageLongValue(this Sitecore.Sites.SiteContext context, string key)
		{
			return long.Parse(GetFromDB(key));
		}

		public static DateTime GetDynamicStorageDateTimeValue(this Sitecore.Sites.SiteContext context, string key)
		{
			return DateTime.Parse(GetFromDB(key));
		}

		private static string GetFromDB(string key)
		{
			DynamicStorageService svc = new DynamicStorageService();
			DynamicStorageCache cache = new DynamicStorageCache();

			var value = cache.GetValue(key, svc.GetFromDB);

			if (value != null)
				return value.ToString();
			else
				return null;
		}

		private static void SetToDB(string key, string value)
		{
			DynamicStorageService svc = new DynamicStorageService();
			DynamicStorageCache cache = new DynamicStorageCache();

			svc.SetToDB(key, value);
			cache.ClearCacheEntry(key);
		}
	}
}