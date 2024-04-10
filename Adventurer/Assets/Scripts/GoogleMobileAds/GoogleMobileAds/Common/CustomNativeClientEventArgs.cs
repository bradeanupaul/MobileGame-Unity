using System;

namespace GoogleMobileAds.Common
{
	public class CustomNativeClientEventArgs : EventArgs
	{
		internal ICustomNativeTemplateClient nativeAdClient { get; set; }

		internal string assetName { get; set; }
	}
}
