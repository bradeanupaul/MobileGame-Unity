using System.Collections.Generic;
using UnityEngine;

namespace Unity.Notifications.Android
{
	public class AndroidReceivedNotificationMainThreadDispatcher : MonoBehaviour
	{
		private static AndroidReceivedNotificationMainThreadDispatcher instance = null;

		private static Queue<AndroidJavaObject> receivedNotificationQueue = new Queue<AndroidJavaObject>();

		internal static void EnqueueReceivedNotification(AndroidJavaObject intent)
		{
			lock (receivedNotificationQueue)
			{
				receivedNotificationQueue.Enqueue(intent);
			}
		}

		internal static AndroidReceivedNotificationMainThreadDispatcher GetInstance()
		{
			return instance;
		}

		public void Update()
		{
			lock (receivedNotificationQueue)
			{
				while (receivedNotificationQueue.Count > 0)
				{
					AndroidNotificationCenter.ReceivedNotificationCallback(receivedNotificationQueue.Dequeue());
				}
			}
		}

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		private void OnDestroy()
		{
			instance = null;
		}
	}
}
