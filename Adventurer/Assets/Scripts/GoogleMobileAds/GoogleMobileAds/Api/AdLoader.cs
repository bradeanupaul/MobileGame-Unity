using System;
using System.Collections.Generic;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	public class AdLoader
	{
		public class Builder
		{
			internal string AdUnitId { get; private set; }

			internal HashSet<NativeAdType> AdTypes { get; private set; }

			internal HashSet<string> TemplateIds { get; private set; }

			internal Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers { get; private set; }

			public Builder(string adUnitId)
			{
				AdUnitId = adUnitId;
				AdTypes = new HashSet<NativeAdType>();
				TemplateIds = new HashSet<string>();
				CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>();
			}

			public Builder ForCustomNativeAd(string templateId)
			{
				TemplateIds.Add(templateId);
				AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			public Builder ForCustomNativeAd(string templateId, Action<CustomNativeTemplateAd, string> callback)
			{
				TemplateIds.Add(templateId);
				CustomNativeTemplateClickHandlers[templateId] = callback;
				AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			public AdLoader Build()
			{
				return new AdLoader(this);
			}
		}

		private IAdLoaderClient adLoaderClient;

		public Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers { get; private set; }

		public string AdUnitId { get; private set; }

		public HashSet<NativeAdType> AdTypes { get; private set; }

		public HashSet<string> TemplateIds { get; private set; }

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		private AdLoader(Builder builder)
		{
			AdUnitId = string.Copy(builder.AdUnitId);
			CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>(builder.CustomNativeTemplateClickHandlers);
			TemplateIds = new HashSet<string>(builder.TemplateIds);
			AdTypes = new HashSet<NativeAdType>(builder.AdTypes);
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (string templateId in TemplateIds)
			{
				dictionary[templateId] = false;
			}
			foreach (KeyValuePair<string, Action<CustomNativeTemplateAd, string>> customNativeTemplateClickHandler in CustomNativeTemplateClickHandlers)
			{
				dictionary[customNativeTemplateClickHandler.Key] = true;
			}
			AdLoaderClientArgs args2 = new AdLoaderClientArgs
			{
				AdUnitId = AdUnitId,
				AdTypes = AdTypes,
				TemplateIds = dictionary
			};
			adLoaderClient = GoogleMobileAdsClientFactory.BuildAdLoaderClient(args2);
			Utils.CheckInitialization();
			adLoaderClient.OnCustomNativeTemplateAdLoaded += delegate(object sender, CustomNativeClientEventArgs args)
			{
				CustomNativeTemplateAd nativeAd = new CustomNativeTemplateAd(args.nativeAdClient);
				CustomNativeEventArgs e = new CustomNativeEventArgs
				{
					nativeAd = nativeAd
				};
				this.OnCustomNativeTemplateAdLoaded(this, e);
			};
			adLoaderClient.OnCustomNativeTemplateAdClicked += delegate(object sender, CustomNativeClientEventArgs args)
			{
				CustomNativeTemplateAd customNativeTemplateAd = new CustomNativeTemplateAd(args.nativeAdClient);
				if (CustomNativeTemplateClickHandlers.ContainsKey(customNativeTemplateAd.GetCustomTemplateId()))
				{
					CustomNativeTemplateClickHandlers[customNativeTemplateAd.GetCustomTemplateId()](customNativeTemplateAd, args.assetName);
				}
			};
			adLoaderClient.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
		}

		public void LoadAd(AdRequest request)
		{
			adLoaderClient.LoadAd(request);
		}
	}
}
