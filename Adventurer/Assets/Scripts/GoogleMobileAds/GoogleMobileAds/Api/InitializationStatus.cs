using System.Collections.Generic;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class InitializationStatus
	{
		private IInitializationStatusClient client;

		internal InitializationStatus(IInitializationStatusClient client)
		{
			this.client = client;
		}

		public AdapterStatus getAdapterStatusForClassName(string className)
		{
			return client.getAdapterStatusForClassName(className);
		}

		public Dictionary<string, AdapterStatus> getAdapterStatusMap()
		{
			return client.getAdapterStatusMap();
		}
	}
}
