using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class InitializationStatusClient : IInitializationStatusClient
	{
		private AndroidJavaObject status;

		private AndroidJavaObject statusMap;

		public InitializationStatusClient(AndroidJavaObject status)
		{
			this.status = status;
			statusMap = status.Call<AndroidJavaObject>("getAdapterStatusMap", Array.Empty<object>());
		}

		public AdapterStatus getAdapterStatusForClassName(string className)
		{
			AndroidJavaObject androidJavaObject = statusMap.Call<AndroidJavaObject>("get", new object[1] { className });
			if (androidJavaObject == null)
			{
				return null;
			}
			string description = androidJavaObject.Call<string>("getDescription", Array.Empty<object>());
			int latency = androidJavaObject.Call<int>("getLatency", Array.Empty<object>());
			AndroidJavaObject @static = new AndroidJavaClass("com.google.android.gms.ads.initialization.AdapterStatus$State").GetStatic<AndroidJavaObject>("READY");
			return new AdapterStatus(androidJavaObject.Call<AndroidJavaObject>("getInitializationState", Array.Empty<object>()).Call<bool>("equals", new object[1] { @static }) ? AdapterState.Ready : AdapterState.NotReady, description, latency);
		}

		public Dictionary<string, AdapterStatus> getAdapterStatusMap()
		{
			Dictionary<string, AdapterStatus> dictionary = new Dictionary<string, AdapterStatus>();
			string[] keys = getKeys();
			foreach (string text in keys)
			{
				dictionary.Add(text, getAdapterStatusForClassName(text));
			}
			return dictionary;
		}

		private string[] getKeys()
		{
			AndroidJavaObject androidJavaObject = statusMap;
			AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("keySet", Array.Empty<object>());
			AndroidJavaObject androidJavaObject3 = new AndroidJavaClass("java.lang.reflect.Array").CallStatic<AndroidJavaObject>("newInstance", new object[2]
			{
				new AndroidJavaClass("java.lang.String"),
				androidJavaObject.Call<int>("size", Array.Empty<object>())
			});
			return androidJavaObject2.Call<string[]>("toArray", new object[1] { androidJavaObject3 });
		}
	}
}
