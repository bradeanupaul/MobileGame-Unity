using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class Utils
	{
		public const string AdListenerClassName = "com.google.android.gms.ads.AdListener";

		public const string AdRequestClassName = "com.google.android.gms.ads.AdRequest";

		public const string AdRequestBuilderClassName = "com.google.android.gms.ads.AdRequest$Builder";

		public const string AdSizeClassName = "com.google.android.gms.ads.AdSize";

		public const string AdMobExtrasClassName = "com.google.android.gms.ads.mediation.admob.AdMobExtras";

		public const string PlayStorePurchaseListenerClassName = "com.google.android.gms.ads.purchase.PlayStorePurchaseListener";

		public const string MobileAdsClassName = "com.google.android.gms.ads.MobileAds";

		public const string RequestConfigurationClassName = "com.google.android.gms.ads.RequestConfiguration";

		public const string RequestConfigurationBuilderClassName = "com.google.android.gms.ads.RequestConfiguration$Builder";

		public const string ServerSideVerificationOptionsClassName = "com.google.android.gms.ads.rewarded.ServerSideVerificationOptions";

		public const string ServerSideVerificationOptionsBuilderClassName = "com.google.android.gms.ads.rewarded.ServerSideVerificationOptions$Builder";

		public const string UnityAdSizeClassName = "com.google.unity.ads.UnityAdSize";

		public const string BannerViewClassName = "com.google.unity.ads.Banner";

		public const string InterstitialClassName = "com.google.unity.ads.Interstitial";

		public const string RewardBasedVideoClassName = "com.google.unity.ads.RewardBasedVideo";

		public const string UnityRewardedAdClassName = "com.google.unity.ads.UnityRewardedAd";

		public const string NativeAdLoaderClassName = "com.google.unity.ads.NativeAdLoader";

		public const string UnityAdListenerClassName = "com.google.unity.ads.UnityAdListener";

		public const string UnityRewardBasedVideoAdListenerClassName = "com.google.unity.ads.UnityRewardBasedVideoAdListener";

		public const string UnityRewardedAdCallbackClassName = "com.google.unity.ads.UnityRewardedAdCallback";

		public const string UnityAdapterStatusEnumName = "com.google.android.gms.ads.initialization.AdapterStatus$State";

		public const string OnInitializationCompleteListenerClassName = "com.google.android.gms.ads.initialization.OnInitializationCompleteListener";

		public const string UnityAdLoaderListenerClassName = "com.google.unity.ads.UnityAdLoaderListener";

		public const string UnityPaidEventListenerClassName = "com.google.unity.ads.UnityPaidEventListener";

		public const string PluginUtilsClassName = "com.google.unity.ads.PluginUtils";

		public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

		public const string BundleClassName = "android.os.Bundle";

		public const string DateClassName = "java.util.Date";

		public const string DisplayMetricsClassName = "android.util.DisplayMetrics";

		public static AndroidJavaObject GetAdSizeJavaObject(AdSize adSize)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.unity.ads.UnityAdSize");
			switch (adSize.AdType)
			{
			case AdSize.Type.SmartBanner:
				return androidJavaClass.CallStatic<AndroidJavaObject>("getSmartBannerAdSize", Array.Empty<object>());
			case AdSize.Type.AnchoredAdaptive:
			{
				AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				switch (adSize.Orientation)
				{
				case Orientation.Landscape:
					return androidJavaClass.CallStatic<AndroidJavaObject>("getLandscapeAnchoredAdaptiveBannerAdSize", new object[2] { @static, adSize.Width });
				case Orientation.Portrait:
					return androidJavaClass.CallStatic<AndroidJavaObject>("getPortraitAnchoredAdaptiveBannerAdSize", new object[2] { @static, adSize.Width });
				case Orientation.Current:
					return androidJavaClass.CallStatic<AndroidJavaObject>("getCurrentOrientationAnchoredAdaptiveBannerAdSize", new object[2] { @static, adSize.Width });
				default:
					throw new ArgumentException("Invalid Orientation provided for ad size.");
				}
			}
			case AdSize.Type.Standard:
				return new AndroidJavaObject("com.google.android.gms.ads.AdSize", adSize.Width, adSize.Height);
			default:
				throw new ArgumentException("Invalid AdSize.Type provided for ad size.");
			}
		}

		internal static int GetScreenWidth()
		{
			DisplayMetrics displayMetrics = new DisplayMetrics();
			return (int)((float)displayMetrics.WidthPixels / displayMetrics.Density);
		}

		public static AndroidJavaObject GetAdRequestJavaObject(AdRequest request)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.android.gms.ads.AdRequest$Builder");
			foreach (string keyword in request.Keywords)
			{
				androidJavaObject.Call<AndroidJavaObject>("addKeyword", new object[1] { keyword });
			}
			foreach (string testDevice in request.TestDevices)
			{
				if (testDevice == "SIMULATOR")
				{
					string @static = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<string>("DEVICE_ID_EMULATOR");
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[1] { @static });
				}
				else
				{
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[1] { testDevice });
				}
			}
			if (request.Birthday.HasValue)
			{
				DateTime valueOrDefault = request.Birthday.GetValueOrDefault();
				AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.util.Date", valueOrDefault.Year, valueOrDefault.Month, valueOrDefault.Day);
				androidJavaObject.Call<AndroidJavaObject>("setBirthday", new object[1] { androidJavaObject2 });
			}
			if (request.Gender.HasValue)
			{
				int? num = null;
				switch (request.Gender.GetValueOrDefault())
				{
				case Gender.Unknown:
					num = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_UNKNOWN");
					break;
				case Gender.Male:
					num = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_MALE");
					break;
				case Gender.Female:
					num = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_FEMALE");
					break;
				}
				if (num.HasValue)
				{
					androidJavaObject.Call<AndroidJavaObject>("setGender", new object[1] { num });
				}
			}
			if (request.TagForChildDirectedTreatment.HasValue)
			{
				androidJavaObject.Call<AndroidJavaObject>("tagForChildDirectedTreatment", new object[1] { request.TagForChildDirectedTreatment.GetValueOrDefault() });
			}
			androidJavaObject.Call<AndroidJavaObject>("setRequestAgent", new object[1] { "unity-5.1.0" });
			AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("android.os.Bundle");
			foreach (KeyValuePair<string, string> extra in request.Extras)
			{
				androidJavaObject3.Call("putString", extra.Key, extra.Value);
			}
			androidJavaObject3.Call("putString", "is_unity", "1");
			AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("com.google.android.gms.ads.mediation.admob.AdMobExtras", androidJavaObject3);
			androidJavaObject.Call<AndroidJavaObject>("addNetworkExtras", new object[1] { androidJavaObject4 });
			foreach (MediationExtras mediationExtra in request.MediationExtras)
			{
				AndroidJavaObject androidJavaObject5 = new AndroidJavaObject(mediationExtra.AndroidMediationExtraBuilderClassName);
				AndroidJavaObject androidJavaObject6 = new AndroidJavaObject("java.util.HashMap");
				foreach (KeyValuePair<string, string> extra2 in mediationExtra.Extras)
				{
					androidJavaObject6.Call<AndroidJavaObject>("put", new object[2] { extra2.Key, extra2.Value });
				}
				AndroidJavaObject androidJavaObject7 = androidJavaObject5.Call<AndroidJavaObject>("buildExtras", new object[1] { androidJavaObject6 });
				if (androidJavaObject7 != null)
				{
					androidJavaObject.Call<AndroidJavaObject>("addNetworkExtrasBundle", new object[2]
					{
						androidJavaObject5.Call<AndroidJavaClass>("getAdapterClass", Array.Empty<object>()),
						androidJavaObject7
					});
					androidJavaObject.Call<AndroidJavaObject>("addCustomEventExtrasBundle", new object[2]
					{
						androidJavaObject5.Call<AndroidJavaClass>("getAdapterClass", Array.Empty<object>()),
						androidJavaObject7
					});
				}
			}
			return androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
		}

		public static AndroidJavaObject GetJavaListObject(List<string> csTypeList)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.ArrayList");
			foreach (string csType in csTypeList)
			{
				androidJavaObject.Call<bool>("add", new object[1] { csType });
			}
			return androidJavaObject;
		}

		public static List<string> GetCsTypeList(AndroidJavaObject javaTypeList)
		{
			List<string> list = new List<string>();
			int num = javaTypeList.Call<int>("size", Array.Empty<object>());
			for (int i = 0; i < num; i++)
			{
				list.Add(javaTypeList.Call<string>("get", new object[1] { i }));
			}
			return list;
		}

		public static AndroidJavaObject GetServerSideVerificationOptionsJavaObject(ServerSideVerificationOptions serverSideVerificationOptions)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.android.gms.ads.rewarded.ServerSideVerificationOptions$Builder");
			androidJavaObject.Call<AndroidJavaObject>("setUserId", new object[1] { serverSideVerificationOptions.UserId });
			androidJavaObject.Call<AndroidJavaObject>("setCustomData", new object[1] { serverSideVerificationOptions.CustomData });
			return androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
		}
	}
}
