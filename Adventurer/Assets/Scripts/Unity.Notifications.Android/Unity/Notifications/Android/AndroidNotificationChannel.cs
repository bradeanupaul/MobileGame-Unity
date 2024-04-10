using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Notifications.Android
{
	public struct AndroidNotificationChannel
	{
		internal string id;

		internal string title;

		internal string description;

		internal int importance;

		internal bool canBypassDnd;

		internal bool canShowBadge;

		internal bool enableLights;

		internal bool enableVibration;

		internal int lockscreenVisibility;

		internal int[] vibrationPattern;

		public string Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public string Name
		{
			get
			{
				return title;
			}
			set
			{
				title = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public Importance Importance
		{
			get
			{
				return (Importance)importance;
			}
			set
			{
				importance = (int)value;
			}
		}

		public bool CanBypassDnd
		{
			get
			{
				return canBypassDnd;
			}
			set
			{
				canBypassDnd = value;
			}
		}

		public bool CanShowBadge
		{
			get
			{
				return canShowBadge;
			}
			set
			{
				canShowBadge = value;
			}
		}

		public bool EnableLights
		{
			get
			{
				return enableLights;
			}
			set
			{
				enableLights = value;
			}
		}

		public bool EnableVibration
		{
			get
			{
				return enableVibration;
			}
			set
			{
				enableVibration = value;
			}
		}

		public LockScreenVisibility LockScreenVisibility
		{
			get
			{
				return (LockScreenVisibility)lockscreenVisibility;
			}
			set
			{
				lockscreenVisibility = (int)value;
			}
		}

		public long[] VibrationPattern
		{
			get
			{
				return ((IEnumerable<int>)vibrationPattern).Select((Func<int, long>)((int i) => i)).ToArray();
			}
			set
			{
				vibrationPattern = value.Select((long i) => (int)i).ToArray();
			}
		}

		public bool Enabled => Importance != Importance.None;

		public AndroidNotificationChannel(string id, string title, string description, Importance importance)
		{
			this.id = id;
			this.title = title;
			this.description = description;
			this.importance = (int)importance;
			canBypassDnd = false;
			canShowBadge = true;
			enableLights = false;
			enableVibration = true;
			lockscreenVisibility = 1;
			vibrationPattern = null;
		}
	}
}
