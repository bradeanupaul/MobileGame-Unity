using System.Collections.Generic;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	public class AdLoaderClientArgs
	{
		public string AdUnitId { get; set; }

		public HashSet<NativeAdType> AdTypes { get; set; }

		internal Dictionary<string, bool> TemplateIds { get; set; }
	}
}
