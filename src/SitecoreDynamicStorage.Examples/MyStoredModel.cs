using SitecoreDynamicStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreDynamicStorage.Examples
{
	internal class MyStoredModel : DynamicModel
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (value != this._name)
				{
					this._name = value;
					NotifyPropertyChanged();
				}
			}
		}

		private string _height;
		public int Height
		{
			get
			{ return int.Parse(_height); }
			set
			{
				if (value.ToString() != this._height)
				{
					this._height = value.ToString();
					NotifyPropertyChanged();
				}
			}
		}

		private string _dob;
		public DateTime DOB
		{
			get { return DateTime.Parse(_dob); }
			set
			{
				if (value.ToString() != this._dob)
				{
					this._dob = value.ToString();
					NotifyPropertyChanged();
				}
			}
		}
	}
}