using SitecoreDynamicStorage.Core.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SitecoreDynamicStorage.Core
{
	public class DynamicModel: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public DynamicModel()
		{
			PropertyChanged += DynamicModel_PropertyChanged;

			foreach (var prop in this.GetType().GetProperties())
			{
				var value = DynamicStorageExtensions.GetDynamicStorageValue(null, prop.Name);
				if(prop.PropertyType == typeof(Int32))
				{
					prop.SetValue(this, int.Parse(value));
				}
				else if (prop.PropertyType == typeof(long))
				{
					prop.SetValue(this, long.Parse(value));
				}
				else if (prop.PropertyType == typeof(DateTime))
				{
					prop.SetValue(this, DateTime.Parse(value));
				}
				else
				{
					prop.SetValue(this, value);
				}
			}
		}

		private void DynamicModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			DynamicStorageExtensions.SetDynamicStorageValue(null, e.PropertyName, this.GetType().GetProperty(e.PropertyName).GetValue(this)?.ToString());
		}

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}