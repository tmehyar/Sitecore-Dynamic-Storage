using System;

namespace SitecoreDynamicStorage.Core.Events
{
	public class DynamicStorageEventArgs : EventArgs
	{
		internal DynamicStorageEventArgs(DynamicStorageEventType eventType, string key, string value)
		{
			EventType = eventType;
			Key = key;
			Value = value;
		}

		public DynamicStorageEventType EventType { get; private set; }
		public string Key { get; private set; }
		public string Value { get; private set; }
	}

	public enum DynamicStorageEventType
	{
		Added,
		Updated,
		Retrieved
	}
}