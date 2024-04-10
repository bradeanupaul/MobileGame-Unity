using System.Collections.Generic;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	public interface IInitializationStatusClient
	{
		AdapterStatus getAdapterStatusForClassName(string className);

		Dictionary<string, AdapterStatus> getAdapterStatusMap();
	}
}
