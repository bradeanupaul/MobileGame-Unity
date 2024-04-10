using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	public interface IAdLoaderClient
	{
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		event EventHandler<CustomNativeClientEventArgs> OnCustomNativeTemplateAdLoaded;

		event EventHandler<CustomNativeClientEventArgs> OnCustomNativeTemplateAdClicked;

		void LoadAd(AdRequest request);
	}
}
