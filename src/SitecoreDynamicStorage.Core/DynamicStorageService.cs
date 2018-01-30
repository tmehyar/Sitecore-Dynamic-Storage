using SitecoreDynamicStorage.Core.Events;
using SitecoreDynamicStorage.DataAccess;
using System;
using System.Linq;

namespace SitecoreDynamicStorage.Core
{
	public class DynamicStorageService
	{
		public string GetFromDB(string key)
		{
			string result = null;
			using (DynamicStorageDataContext context = new DynamicStorageDataContext())
			{
				result = context.DynamicStorageRecords.FirstOrDefault(w => w.Name == key)?.Value;
			}

			DynamicStorageEventsManager.InvokeRetrievedEvent(key, result);
			return result;
		}

		public void SetToDB(string key, string value)
		{
			using (DynamicStorageDataContext context = new DynamicStorageDataContext())
			{
				var currentRecord = context.DynamicStorageRecords.FirstOrDefault(w => w.Name == key);

				if (currentRecord == null)
				{
					context.DynamicStorageRecords.InsertOnSubmit(new DynamicStorageRecord() { Name = key, Value = value, LastModified = DateTime.Now });
				}
				else
				{
					currentRecord.Value = value;
					currentRecord.LastModified = DateTime.Now;
				}

				context.SubmitChanges();
				if (currentRecord == null)
					DynamicStorageEventsManager.InvokeAddedEvent(key, value);
				else
					DynamicStorageEventsManager.InvokeUpdatedEvent(key, value);
			}
		}
	}
}