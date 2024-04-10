namespace GoogleMobileAds.Api
{
	public class AdValue
	{
		public enum PrecisionType
		{
			Unknown = 0,
			Estimated = 1,
			PublisherProvided = 2,
			Precise = 3
		}

		public PrecisionType Precision { get; set; }

		public long Value { get; set; }

		public string CurrencyCode { get; set; }
	}
}
