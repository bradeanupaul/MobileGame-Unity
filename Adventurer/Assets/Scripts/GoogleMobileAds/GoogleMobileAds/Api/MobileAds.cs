using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class MobileAds
	{
		public static class Utils
		{
			public static float GetDeviceScale()
			{
				return Instance.client.GetDeviceScale();
			}

			public static int GetDeviceSafeWidth()
			{
				return Instance.client.GetDeviceSafeWidth();
			}
		}

		private readonly IMobileAdsClient client = GetMobileAdsClient();

		private static MobileAds instance;

		public static MobileAds Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MobileAds();
				}
				return instance;
			}
		}

		[Obsolete("Initialize(string appId) is deprecated, use Initialize(Action<InitializationStatus> initCompleteAction) instead.")]
		public static void Initialize(string appId)
		{
			Instance.client.Initialize(appId);
			MobileAdsEventExecutor.Initialize();
		}

		public static void Initialize(Action<InitializationStatus> initCompleteAction)
		{
			Instance.client.Initialize(delegate(IInitializationStatusClient initStatusClient)
			{
				if (initCompleteAction != null)
				{
					initCompleteAction(new InitializationStatus(initStatusClient));
				}
			});
			MobileAdsEventExecutor.Initialize();
		}

		public static void SetApplicationMuted(bool muted)
		{
			Instance.client.SetApplicationMuted(muted);
		}

		public static void SetRequestConfiguration(RequestConfiguration requestConfiguration)
		{
			Instance.client.SetRequestConfiguration(requestConfiguration);
		}

		public static RequestConfiguration GetRequestConfiguration()
		{
			return Instance.client.GetRequestConfiguration();
		}

		public static void SetApplicationVolume(float volume)
		{
			Instance.client.SetApplicationVolume(volume);
		}

		public static void SetiOSAppPauseOnBackground(bool pause)
		{
			Instance.client.SetiOSAppPauseOnBackground(pause);
		}

		private static IMobileAdsClient GetMobileAdsClient()
		{
			return GoogleMobileAdsClientFactory.MobileAdsInstance();
		}
	}
}
