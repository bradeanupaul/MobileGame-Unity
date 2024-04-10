using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class RequestConfigurationClient
	{
		public static AndroidJavaObject BuildRequestConfiguration(RequestConfiguration requestConfiguration)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.android.gms.ads.RequestConfiguration$Builder");
			if (requestConfiguration.MaxAdContentRating != null)
			{
				androidJavaObject = androidJavaObject.Call<AndroidJavaObject>("setMaxAdContentRating", new object[1] { requestConfiguration.MaxAdContentRating.Value });
			}
			if (requestConfiguration.TestDeviceIds.Count > 0)
			{
				AndroidJavaObject javaListObject = Utils.GetJavaListObject(requestConfiguration.TestDeviceIds);
				androidJavaObject = androidJavaObject.Call<AndroidJavaObject>("setTestDeviceIds", new object[1] { javaListObject });
			}
			if (requestConfiguration.TagForUnderAgeOfConsent.HasValue)
			{
				int? num = null;
				switch (requestConfiguration.TagForUnderAgeOfConsent.GetValueOrDefault())
				{
				case TagForUnderAgeOfConsent.False:
					num = new AndroidJavaClass("com.google.android.gms.ads.RequestConfiguration").GetStatic<int>("TAG_FOR_UNDER_AGE_OF_CONSENT_FALSE");
					break;
				case TagForUnderAgeOfConsent.True:
					num = new AndroidJavaClass("com.google.android.gms.ads.RequestConfiguration").GetStatic<int>("TAG_FOR_UNDER_AGE_OF_CONSENT_TRUE");
					break;
				case TagForUnderAgeOfConsent.Unspecified:
					num = new AndroidJavaClass("com.google.android.gms.ads.RequestConfiguration").GetStatic<int>("TAG_FOR_UNDER_AGE_OF_CONSENT_UNSPECIFIED");
					break;
				}
				if (num.HasValue)
				{
					androidJavaObject.Call<AndroidJavaObject>("setTagForUnderAgeOfConsent", new object[1] { num });
				}
			}
			if (requestConfiguration.TagForChildDirectedTreatment.HasValue)
			{
				int? num2 = null;
				switch (requestConfiguration.TagForChildDirectedTreatment.GetValueOrDefault())
				{
				case TagForChildDirectedTreatment.False:
					num2 = new AndroidJavaClass("com.google.android.gms.ads.RequestConfiguration").GetStatic<int>("TAG_FOR_CHILD_DIRECTED_TREATMENT_FALSE");
					break;
				case TagForChildDirectedTreatment.True:
					num2 = new AndroidJavaClass("com.google.android.gms.ads.RequestConfiguration").GetStatic<int>("TAG_FOR_UNDER_AGE_OF_CONSENT_TRUE");
					break;
				case TagForChildDirectedTreatment.Unspecified:
					num2 = new AndroidJavaClass("com.google.android.gms.ads.RequestConfiguration").GetStatic<int>("TAG_FOR_UNDER_AGE_OF_CONSENT_UNSPECIFIED");
					break;
				}
				if (num2.HasValue)
				{
					androidJavaObject.Call<AndroidJavaObject>("setTagForChildDirectedTreatment", new object[1] { num2 });
				}
			}
			return androidJavaObject.Call<AndroidJavaObject>("build", Array.Empty<object>());
		}

		public static RequestConfiguration GetRequestConfiguration(AndroidJavaObject androidRequestConfiguration)
		{
			TagForChildDirectedTreatment value = (TagForChildDirectedTreatment)androidRequestConfiguration.Call<int>("getTagForChildDirectedTreatment", Array.Empty<object>());
			TagForUnderAgeOfConsent value2 = (TagForUnderAgeOfConsent)androidRequestConfiguration.Call<int>("getTagForUnderAgeOfConsent", Array.Empty<object>());
			MaxAdContentRating maxAdContentRating = MaxAdContentRating.ToMaxAdContentRating(androidRequestConfiguration.Call<string>("getMaxAdContentRating", Array.Empty<object>()));
			List<string> csTypeList = Utils.GetCsTypeList(androidRequestConfiguration.Call<AndroidJavaObject>("getTestDeviceIds", Array.Empty<object>()));
			return new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(value).SetTagForUnderAgeOfConsent(value2).SetMaxAdContentRating(maxAdContentRating)
				.SetTestDeviceIds(csTypeList)
				.build();
		}
	}
}
