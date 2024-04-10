namespace Unity.Notifications.Android
{
	public class AndroidNotificationIntentData
	{
		internal int id;

		internal string channel;

		internal AndroidNotification notification;

		public int Id => id;

		public string Channel => channel;

		public AndroidNotification Notification => notification;
	}
}
