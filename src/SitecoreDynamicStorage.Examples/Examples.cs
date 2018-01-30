using SitecoreDynamicStorage.Core.Events;
using SitecoreDynamicStorage.Core.Extensions;
using System;

namespace SitecoreDynamicStorage.Examples
{
	public class Examples
	{
		public void RunAllExamples()
		{
			UsingLibraryDirectly();
			UsingExtensionsMethods();
			WorkingWithOtherTypes();
			StaticallyTypedAndNamed();
		}

		public void UsingLibraryDirectly()
		{
			//SiteContext is only used as a shortcut extensions method when using Sitecore.Context.Site.
			//So you can pass it null
			//The following key-value pair will get saved to Core DB
			DynamicStorageExtensions.SetDynamicStorageValue(null, "Name", "TamerMehyar");

			//Gets retrieved from CoreDB since cache will still be empty
			var myName = DynamicStorageExtensions.GetDynamicStorageValue(null, "Name");
			//Gets retrieved from Cache 
			var myNameCached = DynamicStorageExtensions.GetDynamicStorageValue(null, "Name");
		}

		public void UsingExtensionsMethods()
		{
			Sitecore.Context.Site.SetDynamicStorageValue("PageTitle", "Hello World");

			var title = Sitecore.Context.Site.GetDynamicStorageValue("PageTitle");
			var titleCached = Sitecore.Context.Site.GetDynamicStorageValue("PageTitle");
		}

		public void WorkingWithOtherTypes()
		{
			Sitecore.Context.Site.SetDynamicStorageValue("TypeString", "This is a string");
			Sitecore.Context.Site.SetDynamicStorageValue("TypeInt32", 50);
			Sitecore.Context.Site.SetDynamicStorageValue("Typelong", long.MaxValue);
			Sitecore.Context.Site.SetDynamicStorageValue("TypeDateTime", DateTime.Now);

			var typeString = Sitecore.Context.Site.GetDynamicStorageValue("TypeString");
			var typeInt32 = Sitecore.Context.Site.GetDynamicStorageInt32Value("TypeInt32");
			var typeLong = Sitecore.Context.Site.GetDynamicStorageLongValue("Typelong");
			var typeDateTime = Sitecore.Context.Site.GetDynamicStorageDateTimeValue("TypeDateTime");
		}

		public void Events()
		{
			DynamicStorageEventsManager.SubscribeToAddedEvent("Name", Name_Added);
			DynamicStorageEventsManager.SubscribeToUpdatedEvent("Name", Name_Updated);
			DynamicStorageEventsManager.SubscribeToRetrievedEvent("Name", Name_Retrieved);
		}

		private void Name_Added(DynamicStorageEventArgs args)
		{
			string result = $"{args.EventType.ToString()} - {args.Key}:{args.Value}";
		}

		private void Name_Updated(DynamicStorageEventArgs args)
		{
			string result = $"{args.EventType.ToString()} - {args.Key}:{args.Value}";
		}

		private void Name_Retrieved(DynamicStorageEventArgs args)
		{
			string result = $"{args.EventType.ToString()} - {args.Key}:{args.Value}";
		}

		public void StaticallyTypedAndNamed()
		{
			MyStoredModel model = new MyStoredModel();
			model.Name = "Tamer";
			model.DOB = new DateTime(2018, 1, 1);
			model.Height = 177;

			DynamicStorageEventsManager.SubscribeToUpdatedEvent(nameof(model.Name), Name_Updated);
			//Can't subscribe to RetrievedEvent using the DynamicModel at this time
			//DynamicStorageEventsManager.SubscribeToRetrievedEvent(nameof(model.Name), Name_Retrieved);

			string me = $"{model.Name} - {model.DOB.ToString()} - {model.Height}cm";
		}
	}
}