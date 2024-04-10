using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class MobileAdsClient : AndroidJavaProxy, IMobileAdsClient
	{
		private static MobileAdsClient instance = new MobileAdsClient();

		private Action<IInitializationStatusClient> initCompleteAction;

		public static MobileAdsClient Instance => instance;

		private MobileAdsClient()
			: base("com.google.android.gms.ads.initialization.OnInitializationCompleteListener")
		{
		}

		public void Initialize(string appId)
		{
			AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			new AndroidJavaClass("com.google.android.gms.ads.MobileAds").CallStatic("initialize", @static, appId);
		}

		public void Initialize(Action<IInitializationStatusClient> initCompleteAction)
		{
			this.initCompleteAction = initCompleteAction;
			AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			new AndroidJavaClass("com.google.android.gms.ads.MobileAds").CallStatic("initialize", @static, this);
		}

		public void SetApplicationVolume(float volume)
		{
			new AndroidJavaClass("com.google.android.gms.ads.MobileAds").CallStatic("setAppVolume", volume);
		}

		public void SetApplicationMuted(bool muted)
		{
			new AndroidJavaClass("com.google.android.gms.ads.MobileAds").CallStatic("setAppMuted", muted);
		}

		public void SetRequestConfiguration(RequestConfiguration requestConfiguration)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			AndroidJavaObject androidJavaObject = RequestConfigurationClient.BuildRequestConfiguration(requestConfiguration);
			androidJavaClass.CallStatic("setRequestConfiguration", androidJavaObject);
		}

		public RequestConfiguration GetRequestConfiguration()
		{
			return RequestConfigurationClient.GetRequestConfiguration(new AndroidJavaClass("com.google.android.gms.ads.MobileAds").CallStatic<AndroidJavaObject>("getRequestConfiguration", Array.Empty<object>()));
		}

		public void SetiOSAppPauseOnBackground(bool pause)
		{
		}

		public float GetDeviceScale()
		{
			return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getResources", Array.Empty<object>()).Call<AndroidJavaObject>("getDisplayMetrics", Array.Empty<object>())
				.Get<float>("density");
		}

		public int GetDeviceSafeWidth()
		{
			return Utils.GetScreenWidth();
		}

		public void onInitializationComplete(AndroidJavaObject initStatus)
		{
			if (initCompleteAction != null)
			{
				IInitializationStatusClient obj = new InitializationStatusClient(initStatus);
				initCompleteAction(obj);
			}
		}
	}
}
