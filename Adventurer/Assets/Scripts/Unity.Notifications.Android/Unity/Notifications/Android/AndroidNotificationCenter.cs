using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Notifications.Android
{
	public class AndroidNotificationCenter
	{
		public delegate void NotificationReceivedCallback(AndroidNotificationIntentData data);

		private const int ANDROID_OREO = 26;

		private const int ANDROID_M = 23;

		private const string DEFAULT_APP_ICON_ADAPTIVE = "ic_launcher_foreground";

		private const string DEFAULT_APP_ICON_LEGACY = "app_icon";

		private static AndroidJavaObject notificationManager;

		private static int AndroidSDK;

		private static bool initialized;

		private GameObject receivedNotificationDispatcher;

		public static event NotificationReceivedCallback OnNotificationReceived;

		public static bool Initialize()
		{
			if (initialized)
			{
				return true;
			}
			if (AndroidReceivedNotificationMainThreadDispatcher.GetInstance() == null)
			{
				new GameObject("AndroidReceivedNotificationMainThreadDispatcher").AddComponent<AndroidReceivedNotificationMainThreadDispatcher>();
			}
			AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getApplicationContext", Array.Empty<object>());
			notificationManager = new AndroidJavaClass("com.unity.androidnotifications.UnityNotificationManager").CallStatic<AndroidJavaObject>("getNotificationManagerImpl", new object[2] { androidJavaObject, @static });
			notificationManager.Call("setNotificationCallback", new NotificationCallback());
			AndroidSDK = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
			return initialized = true;
		}

		public static AndroidNotificationIntentData GetLastNotificationIntent()
		{
			if (!Initialize())
			{
				return null;
			}
			return ParseNotificationIntentData(new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getIntent", Array.Empty<object>()));
		}

		public static void RegisterNotificationChannel(AndroidNotificationChannel channel)
		{
			if (Initialize())
			{
				if (string.IsNullOrEmpty(channel.id))
				{
					throw new Exception("Cannot register notification channel, the channel ID is not specified.");
				}
				if (string.IsNullOrEmpty(channel.id))
				{
					throw new Exception(string.Format("Cannot register notification channel: {} , the channel Name is not set.", channel.id));
				}
				if (string.IsNullOrEmpty(channel.description))
				{
					throw new Exception(string.Format("Cannot register notification channel: {} , the channel Description is not set.", channel.id));
				}
				notificationManager.Call("registerNotificationChannel", channel.id, channel.title, Enum.IsDefined(typeof(Importance), channel.importance) ? channel.importance : 3, channel.description, channel.enableLights, channel.enableVibration, channel.canBypassDnd, channel.canShowBadge, channel.vibrationPattern, (!Enum.IsDefined(typeof(LockScreenVisibility), channel.lockscreenVisibility)) ? 1 : channel.lockscreenVisibility);
			}
		}

		public static void CancelNotification(int id)
		{
			if (Initialize())
			{
				CancelScheduledNotification(id);
				CancelDisplayedNotification(id);
			}
		}

		public static void CancelScheduledNotification(int id)
		{
			if (Initialize())
			{
				notificationManager.Call("cancelPendingNotificationIntent", id);
			}
		}

		public static void CancelDisplayedNotification(int id)
		{
			if (Initialize())
			{
				notificationManager.Call<AndroidJavaObject>("getNotificationManager", Array.Empty<object>()).Call("cancel", id);
			}
		}

		public static void CancelAllNotifications()
		{
			CancelAllScheduledNotifications();
			CancelAllDisplayedNotifications();
		}

		public static void CancelAllScheduledNotifications()
		{
			if (Initialize())
			{
				notificationManager.Call("cancelAllPendingNotificationIntents");
			}
		}

		public static void CancelAllDisplayedNotifications()
		{
			if (Initialize())
			{
				notificationManager.Call("cancelAllNotifications");
			}
		}

		public static AndroidNotificationChannel[] GetNotificationChannels()
		{
			if (!Initialize())
			{
				return new AndroidNotificationChannel[0];
			}
			List<AndroidNotificationChannel> list = new List<AndroidNotificationChannel>();
			AndroidJavaObject[] array = notificationManager.Call<AndroidJavaObject[]>("getNotificationChannels", Array.Empty<object>());
			foreach (AndroidJavaObject androidJavaObject in array)
			{
				AndroidNotificationChannel item = default(AndroidNotificationChannel);
				item.id = androidJavaObject.Get<string>("id");
				item.title = androidJavaObject.Get<string>("name");
				item.importance = androidJavaObject.Get<int>("importance");
				item.description = androidJavaObject.Get<string>("description");
				item.enableLights = androidJavaObject.Get<bool>("enableLights");
				item.enableVibration = androidJavaObject.Get<bool>("enableVibration");
				item.canBypassDnd = androidJavaObject.Get<bool>("canBypassDnd");
				item.canShowBadge = androidJavaObject.Get<bool>("canShowBadge");
				long[] array2 = androidJavaObject.Get<long[]>("vibrationPattern");
				if (array2 != null)
				{
					item.vibrationPattern = array2.Select((long i) => (int)i).ToArray();
				}
				item.lockscreenVisibility = androidJavaObject.Get<int>("lockscreenVisibility");
				list.Add(item);
			}
			return list.ToArray();
		}

		public static AndroidNotificationChannel GetNotificationChannel(string id)
		{
			return GetNotificationChannels().SingleOrDefault((AndroidNotificationChannel c) => c.Id == id);
		}

		public static void UpdateScheduledNotification(int id, AndroidNotification notification, string channel)
		{
			if (Initialize() && notificationManager.CallStatic<bool>("checkIfPendingNotificationIsRegistered", new object[1] { id }))
			{
				SendNotification(id, notification, channel);
			}
		}

		public static void SendNotificationWithExplicitID(AndroidNotification notification, string channel, int id)
		{
			if (Initialize())
			{
				SendNotification(id, notification, channel);
			}
		}

		public static int SendNotification(AndroidNotification notification, string channel)
		{
			if (!Initialize())
			{
				return -1;
			}
			int num = Math.Abs(DateTime.Now.ToString("yyMMddHHmmssffffff").GetHashCode()) + new System.Random().Next(10000);
			SendNotification(num, notification, channel);
			return num;
		}

		public static NotificationStatus CheckScheduledNotificationStatus(int id)
		{
			return (NotificationStatus)notificationManager.Call<int>("checkNotificationStatus", new object[1] { id });
		}

		internal static void SendNotification(int id, AndroidNotification notification, string channel)
		{
			if (notification.fireTime < 0)
			{
				Debug.LogError("Failed to schedule notification, it did not contain a valid FireTime");
			}
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity.androidnotifications.UnityNotificationManager");
			AndroidJavaObject androidJavaObject = notificationManager.Get<AndroidJavaObject>("mContext");
			AndroidJavaObject androidJavaObject2 = notificationManager.Get<AndroidJavaObject>("mActivity");
			AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("android.content.Intent", androidJavaObject, androidJavaClass);
			AndroidJavaObject androidJavaObject4 = notificationManager.Get<AndroidJavaObject>("mContext");
			int num = notificationManager.CallStatic<int>("findResourceidInContextByName", new object[3] { notification.smallIcon, androidJavaObject4, androidJavaObject2 });
			int num2 = notificationManager.CallStatic<int>("findResourceidInContextByName", new object[3] { notification.largeIcon, androidJavaObject4, androidJavaObject2 });
			if (num == 0)
			{
				num = notificationManager.CallStatic<int>("findResourceidInContextByName", new object[3] { "ic_launcher_foreground", androidJavaObject4, androidJavaObject2 });
				if (num == 0)
				{
					num = notificationManager.CallStatic<int>("findResourceidInContextByName", new object[3] { "app_icon", androidJavaObject4, androidJavaObject2 });
				}
			}
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "id", id });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "channelID", channel });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "textTitle", notification.title });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "textContent", notification.text });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "smallIcon", num });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "autoCancel", notification.shouldAutoCancel });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "usesChronometer", notification.usesStopwatch });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "fireTime", notification.fireTime });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "repeatInterval", notification.repeatInterval });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "largeIcon", num2 });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "style", notification.style });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "color", notification.color });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "number", notification.number });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "data", notification.intentData });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "group", notification.group });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "groupSummary", notification.groupSummary });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "sortKey", notification.sortKey });
			androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[2] { "groupAlertBehaviour", notification.groupAlertBehaviour });
			notificationManager.Call("scheduleNotificationIntent", androidJavaObject3);
		}

		public static void DeleteNotificationChannel(string id)
		{
			if (Initialize())
			{
				notificationManager.Call("deleteNotificationChannel", id);
			}
		}

		internal static AndroidNotificationIntentData ParseNotificationIntentData(AndroidJavaObject notificationIntent)
		{
			int num = notificationIntent.Call<int>("getIntExtra", new object[2] { "id", -1 });
			string channel = notificationIntent.Call<string>("getStringExtra", new object[1] { "channelID" });
			if (num == -1)
			{
				return null;
			}
			AndroidNotification notification = default(AndroidNotification);
			notification.title = notificationIntent.Call<string>("getStringExtra", new object[1] { "textTitle" });
			notification.text = notificationIntent.Call<string>("getStringExtra", new object[1] { "textContent" });
			notification.shouldAutoCancel = notificationIntent.Call<bool>("getBooleanExtra", new object[2] { "autoCancel", false });
			notification.usesStopwatch = notificationIntent.Call<bool>("getBooleanExtra", new object[2] { "usesChronometer", false });
			notification.fireTime = notificationIntent.Call<long>("getLongExtra", new object[2] { "fireTime", -1L });
			notification.repeatInterval = notificationIntent.Call<long>("getLongExtra", new object[2] { "repeatInterval", -1L });
			notification.style = notificationIntent.Call<int>("getIntExtra", new object[2] { "style", -1 });
			notification.color = notificationIntent.Call<int>("getIntExtra", new object[2] { "color", 0 });
			notification.number = notificationIntent.Call<int>("getIntExtra", new object[2] { "number", -1 });
			notification.intentData = notificationIntent.Call<string>("getStringExtra", new object[1] { "data" });
			notification.group = notificationIntent.Call<string>("getStringExtra", new object[1] { "group" });
			notification.groupSummary = notificationIntent.Call<bool>("getBooleanExtra", new object[2] { "groupSummary", false });
			notification.sortKey = notificationIntent.Call<string>("getStringExtra", new object[1] { "sortKey" });
			notification.groupAlertBehaviour = notificationIntent.Call<int>("getIntExtra", new object[2] { "groupAlertBehaviour", -1 });
			return new AndroidNotificationIntentData
			{
				id = num,
				channel = channel,
				notification = notification
			};
		}

		internal static void ReceivedNotificationCallback(AndroidJavaObject intent)
		{
			AndroidNotificationIntentData data = ParseNotificationIntentData(intent);
			AndroidNotificationCenter.OnNotificationReceived(data);
		}

		static AndroidNotificationCenter()
		{
			AndroidNotificationCenter.OnNotificationReceived = delegate
			{
			};
		}
	}
}
