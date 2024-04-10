using System;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class DisplayMetrics
	{
		public float Density { get; protected set; }

		public int HeightPixels { get; protected set; }

		public int WidthPixels { get; protected set; }

		public DisplayMetrics()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (new AndroidJavaClass("android.util.DisplayMetrics"))
				{
					using (AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("android.util.DisplayMetrics"))
					{
						using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
						{
							using (AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getWindowManager", Array.Empty<object>()))
							{
								using (AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getDefaultDisplay", Array.Empty<object>()))
								{
									androidJavaObject3.Call("getMetrics", androidJavaObject4);
									Density = androidJavaObject4.Get<float>("density");
									HeightPixels = androidJavaObject4.Get<int>("heightPixels");
									WidthPixels = androidJavaObject4.Get<int>("widthPixels");
								}
							}
						}
					}
				}
			}
		}
	}
}
