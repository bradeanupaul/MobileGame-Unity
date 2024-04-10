namespace GoogleMobileAds.Api
{
	public class MaxAdContentRating
	{
		public string Value { get; set; }

		public static MaxAdContentRating G => new MaxAdContentRating("G");

		public static MaxAdContentRating MA => new MaxAdContentRating("MA");

		public static MaxAdContentRating PG => new MaxAdContentRating("PG");

		public static MaxAdContentRating T => new MaxAdContentRating("T");

		public static MaxAdContentRating Unspecified => new MaxAdContentRating("");

		private MaxAdContentRating(string value)
		{
			Value = value;
		}

		public static MaxAdContentRating ToMaxAdContentRating(string value)
		{
			return new MaxAdContentRating(value);
		}
	}
}
