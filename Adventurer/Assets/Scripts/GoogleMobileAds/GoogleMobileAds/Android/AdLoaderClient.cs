using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class AdLoaderClient : AndroidJavaProxy, IAdLoaderClient
	{
		private AndroidJavaObject adLoader;

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		public event EventHandler<CustomNativeClientEventArgs> OnCustomNativeTemplateAdLoaded;

		public event EventHandler<CustomNativeClientEventArgs> OnCustomNativeTemplateAdClicked;

		public AdLoaderClient(AdLoaderClientArgs args)
			: base("com.google.unity.ads.UnityAdLoaderListener")
		{
			AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			adLoader = new AndroidJavaObject("com.google.unity.ads.NativeAdLoader", @static, args.AdUnitId, this);
			bool flag = false;
			if (args.AdTypes.Contains(NativeAdType.CustomTemplate))
			{
				flag = false;
				foreach (KeyValuePair<string, bool> templateId in args.TemplateIds)
				{
					string key = templateId.Key;
					bool value = templateId.Value;
					adLoader.Call("configureCustomNativeTemplateAd", key, value);
				}
			}
			if (flag)
			{
				adLoader.Call("configureReturnUrlsForImageAssets");
			}
			adLoader.Call("create");
		}

		public void LoadAd(AdRequest request)
		{
			adLoader.Call("loadAd", Utils.GetAdRequestJavaObject(request));
		}

		public void onCustomTemplateAdLoaded(AndroidJavaObject ad)
		{
			if (this.OnCustomNativeTemplateAdLoaded != null)
			{
				CustomNativeClientEventArgs e = new CustomNativeClientEventArgs
				{
					nativeAdClient = new CustomNativeTemplateClient(ad),
					assetName = null
				};
				this.OnCustomNativeTemplateAdLoaded(this, e);
			}
		}

		private void onAdFailedToLoad(string errorReason)
		{
			AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
			{
				Message = errorReason
			};
			this.OnAdFailedToLoad(this, e);
		}

		public void onCustomClick(AndroidJavaObject ad, string assetName)
		{
			if (this.OnCustomNativeTemplateAdClicked != null)
			{
				CustomNativeClientEventArgs e = new CustomNativeClientEventArgs
				{
					nativeAdClient = new CustomNativeTemplateClient(ad),
					assetName = assetName
				};
				this.OnCustomNativeTemplateAdClicked(this, e);
			}
		}
	}
}
