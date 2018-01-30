using System;
using System.Collections.Generic;
using System.Web;

namespace SitecoreDynamicStorage.Core.Events
{
	public static class DynamicStorageEventsManager
	{
		public delegate void DynamicStorageEventHandler(DynamicStorageEventArgs args);
		private static string _added_dictionaryKey = "SubscriptionsDictionary_Added";
		private static Dictionary<string, DynamicStorageEventHandler> SubscriptionsDictionary_Added
		{
			get
			{
				var dictionary = HttpContext.Current.Cache[_added_dictionaryKey];

				if (dictionary == null || (dictionary is Dictionary<string, DynamicStorageEventHandler>) == false)
				{
					HttpContext.Current.Cache.Remove(_added_dictionaryKey);
					HttpContext.Current.Cache.Add(_added_dictionaryKey, new Dictionary<string, DynamicStorageEventHandler>(), null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.AboveNormal, null);
				}

				return (Dictionary<string, DynamicStorageEventHandler>)HttpContext.Current.Cache[_added_dictionaryKey];
			}
		}

		private static string _updated_dictionaryKey = "SubscriptionsDictionary_Updated";
		private static Dictionary<string, DynamicStorageEventHandler> SubscriptionsDictionary_Updated
		{
			get
			{
				var dictionary = HttpContext.Current.Cache[_updated_dictionaryKey];

				if (dictionary == null || (dictionary is Dictionary<string, DynamicStorageEventHandler>) == false)
				{
					HttpContext.Current.Cache.Remove(_updated_dictionaryKey);
					HttpContext.Current.Cache.Add(_updated_dictionaryKey, new Dictionary<string, DynamicStorageEventHandler>(), null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.AboveNormal, null);
				}

				return (Dictionary<string, DynamicStorageEventHandler>)HttpContext.Current.Cache[_updated_dictionaryKey];
			}
		}

		private static string _retrieved_dictionaryKey = "SubscriptionsDictionary_Retrieved";
		private static Dictionary<string, DynamicStorageEventHandler> SubscriptionsDictionary_Retrieved
		{
			get
			{
				var dictionary = HttpContext.Current.Cache[_retrieved_dictionaryKey];

				if (dictionary == null || (dictionary is Dictionary<string, DynamicStorageEventHandler>) == false)
				{
					HttpContext.Current.Cache.Remove(_retrieved_dictionaryKey);
					HttpContext.Current.Cache.Add(_retrieved_dictionaryKey, new Dictionary<string, DynamicStorageEventHandler>(), null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.AboveNormal, null);
				}

				return (Dictionary<string, DynamicStorageEventHandler>)HttpContext.Current.Cache[_retrieved_dictionaryKey];
			}
		}

		public static void SubscribeToAddedEvent(string key, DynamicStorageEventHandler eventHandler)
		{
			if (SubscriptionsDictionary_Added.ContainsKey(key))
				SubscriptionsDictionary_Added[key] += eventHandler;
			else
				SubscriptionsDictionary_Added.Add(key, eventHandler);
		}

		public static void SubscribeToUpdatedEvent(string key, DynamicStorageEventHandler eventHandler)
		{
			if (SubscriptionsDictionary_Updated.ContainsKey(key))
				SubscriptionsDictionary_Updated[key] += eventHandler;
			else
				SubscriptionsDictionary_Updated.Add(key, eventHandler);
		}

		public static void SubscribeToRetrievedEvent(string key, DynamicStorageEventHandler eventHandler)
		{
			if (SubscriptionsDictionary_Retrieved.ContainsKey(key))
				SubscriptionsDictionary_Retrieved[key] += eventHandler;
			else
				SubscriptionsDictionary_Retrieved.Add(key, eventHandler);
		}

		internal static void InvokeAddedEvent(string key, string value)
		{
			if (SubscriptionsDictionary_Added.ContainsKey(key))
				SubscriptionsDictionary_Added[key](new DynamicStorageEventArgs(DynamicStorageEventType.Added, key, value));

			if (SubscriptionsDictionary_Added.ContainsKey(string.Empty))
				SubscriptionsDictionary_Added[string.Empty](new DynamicStorageEventArgs(DynamicStorageEventType.Added, key, value));
		}

		internal static void InvokeUpdatedEvent(string key, string value)
		{
			if (SubscriptionsDictionary_Updated.ContainsKey(key))
				SubscriptionsDictionary_Updated[key](new DynamicStorageEventArgs(DynamicStorageEventType.Updated, key, value));

			if (SubscriptionsDictionary_Updated.ContainsKey(string.Empty))
				SubscriptionsDictionary_Updated[string.Empty](new DynamicStorageEventArgs(DynamicStorageEventType.Updated, key, value));
		}

		internal static void InvokeRetrievedEvent(string key, string value)
		{
			if (SubscriptionsDictionary_Retrieved.ContainsKey(key))
				SubscriptionsDictionary_Retrieved[key](new DynamicStorageEventArgs(DynamicStorageEventType.Retrieved, key, value));

			if (SubscriptionsDictionary_Retrieved.ContainsKey(string.Empty))
				SubscriptionsDictionary_Retrieved[string.Empty](new DynamicStorageEventArgs(DynamicStorageEventType.Retrieved, key, value));
		}
	}
}