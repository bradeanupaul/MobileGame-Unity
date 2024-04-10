namespace GoogleMobileAds.Api
{
	public class AdSize
	{
		public enum Type
		{
			Standard = 0,
			SmartBanner = 1,
			AnchoredAdaptive = 2
		}

		private Type type;

		private Orientation orientation;

		private int width;

		private int height;

		public static readonly AdSize Banner = new AdSize(320, 50);

		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		public static readonly AdSize IABBanner = new AdSize(468, 60);

		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		public static readonly AdSize SmartBanner = new AdSize(0, 0, Type.SmartBanner);

		public static readonly int FullWidth = -1;

		public int Width => width;

		public int Height => height;

		public Type AdType => type;

		internal Orientation Orientation => orientation;

		public AdSize(int width, int height)
		{
			type = Type.Standard;
			this.width = width;
			this.height = height;
			orientation = Orientation.Current;
		}

		private AdSize(int width, int height, Type type)
			: this(width, height)
		{
			this.type = type;
		}

		private static AdSize CreateAnchoredAdaptiveAdSize(int width, Orientation orientation)
		{
			return new AdSize(width, 0, Type.AnchoredAdaptive)
			{
				orientation = orientation
			};
		}

		public static AdSize GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(int width)
		{
			return CreateAnchoredAdaptiveAdSize(width, Orientation.Landscape);
		}

		public static AdSize GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(int width)
		{
			return CreateAnchoredAdaptiveAdSize(width, Orientation.Portrait);
		}

		public static AdSize GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(int width)
		{
			return CreateAnchoredAdaptiveAdSize(width, Orientation.Current);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			AdSize adSize = (AdSize)obj;
			if (width == adSize.width && height == adSize.height && type == adSize.type)
			{
				return orientation == adSize.orientation;
			}
			return false;
		}

		public static bool operator ==(AdSize a, AdSize b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(AdSize a, AdSize b)
		{
			return !a.Equals(b);
		}

		public override int GetHashCode()
		{
			int num = 11;
			return (((((((71 * num) ^ width.GetHashCode()) * num) ^ height.GetHashCode()) * num) ^ type.GetHashCode()) * num) ^ orientation.GetHashCode();
		}
	}
}
