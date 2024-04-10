using GoogleMobileAds.Android;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds
{
	public class GoogleMobileAdsClientFactory
	{
		public static IBannerClient BuildBannerClient()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return new BannerClient();
			}
			return new DummyClient();
		}

		public static IInterstitialClient BuildInterstitialClient()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return new InterstitialClient();
			}
			return new DummyClient();
		}

		public static IRewardBasedVideoAdClient BuildRewardBasedVideoAdClient()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return new RewardBasedVideoAdClient();
			}
			return new DummyClient();
		}

		public static IRewardedAdClient BuildRewardedAdClient()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return new RewardedAdClient();
			}
			return new RewardedAdDummyClient();
		}

		public static IAdLoaderClient BuildAdLoaderClient(AdLoaderClientArgs args)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return new AdLoaderClient(args);
			}
			return new DummyClient();
		}

		public static IMobileAdsClient MobileAdsInstance()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return MobileAdsClient.Instance;
			}
			return new DummyClient();
		}
	}
}
