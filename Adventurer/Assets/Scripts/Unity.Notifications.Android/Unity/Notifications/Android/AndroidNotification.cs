using System;
using UnityEngine;

namespace Unity.Notifications.Android
{
	public struct AndroidNotification
	{
		internal string title;

		internal string text;

		internal string smallIcon;

		internal long fireTime;

		internal bool shouldAutoCancel;

		internal string largeIcon;

		internal int style;

		internal int color;

		internal int number;

		internal bool usesStopwatch;

		internal long repeatInterval;

		internal string intentData;

		internal string group;

		internal bool groupSummary;

		internal string sortKey;

		internal int groupAlertBehaviour;

		public string Title
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

		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}

		public string SmallIcon
		{
			get
			{
				return smallIcon;
			}
			set
			{
				smallIcon = value;
			}
		}

		public DateTime FireTime
		{
			get
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(fireTime).ToLocalTime();
			}
			set
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				fireTime = (long)Math.Floor((value.ToUniversalTime() - dateTime).TotalMilliseconds);
			}
		}

		public TimeSpan? RepeatInterval
		{
			get
			{
				return TimeSpan.FromMilliseconds(repeatInterval);
			}
			set
			{
				if (value.HasValue)
				{
					repeatInterval = (long)value.Value.TotalMilliseconds;
				}
				else
				{
					repeatInterval = -1L;
				}
			}
		}

		public string LargeIcon
		{
			get
			{
				return largeIcon;
			}
			set
			{
				largeIcon = value;
			}
		}

		public NotificationStyle Style
		{
			get
			{
				return (NotificationStyle)style;
			}
			set
			{
				style = (int)value;
			}
		}

		public Color? Color
		{
			get
			{
				if (color == 0)
				{
					return null;
				}
				int num = (color >> 24) & 0xFF;
				int num2 = (color >> 16) & 0xFF;
				int num3 = (color >> 8) & 0xFF;
				int num4 = color & 0xFF;
				return new Color32((byte)num, (byte)num2, (byte)num3, (byte)num4);
			}
			set
			{
				if (!value.HasValue)
				{
					this.color = 0;
					return;
				}
				Color32 color = value.Value;
				this.color = ((color.a & 0xFF) << 24) | ((color.r & 0xFF) << 16) | ((color.g & 0xFF) << 8) | (color.b & 0xFF);
			}
		}

		public int Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}

		public bool ShouldAutoCancel
		{
			get
			{
				return shouldAutoCancel;
			}
			set
			{
				shouldAutoCancel = value;
			}
		}

		public bool UsesStopwatch
		{
			get
			{
				return usesStopwatch;
			}
			set
			{
				usesStopwatch = value;
			}
		}

		public string Group
		{
			get
			{
				return group;
			}
			set
			{
				group = value;
			}
		}

		public bool GroupSummary
		{
			get
			{
				return groupSummary;
			}
			set
			{
				groupSummary = value;
			}
		}

		public GroupAlertBehaviours GroupAlertBehaviour
		{
			get
			{
				return (GroupAlertBehaviours)groupAlertBehaviour;
			}
			set
			{
				groupAlertBehaviour = (int)value;
			}
		}

		public string SortKey
		{
			get
			{
				return sortKey;
			}
			set
			{
				sortKey = value;
			}
		}

		public string IntentData
		{
			get
			{
				return intentData;
			}
			set
			{
				intentData = value;
			}
		}

		public AndroidNotification(string title, string text, DateTime fireTime)
		{
			this.title = title;
			this.text = text;
			repeatInterval = -1L;
			smallIcon = "";
			shouldAutoCancel = false;
			largeIcon = "";
			style = 0;
			color = 0;
			number = -1;
			usesStopwatch = false;
			intentData = "";
			this.fireTime = -1L;
			group = "";
			groupSummary = false;
			sortKey = "";
			groupAlertBehaviour = -1;
			FireTime = fireTime;
		}

		public AndroidNotification(string title, string text, DateTime fireTime, TimeSpan repeatInterval)
			: this(title, text, fireTime)
		{
			RepeatInterval = repeatInterval;
		}

		public AndroidNotification(string title, string text, DateTime fireTime, TimeSpan repeatInterval, string smallIcon)
			: this(title, text, fireTime, repeatInterval)
		{
			SmallIcon = smallIcon;
		}
	}
}
