using System.Collections.Generic;

namespace GoogleMobileAds.Api
{
	public class RequestConfiguration
	{
		public class Builder
		{
			internal MaxAdContentRating MaxAdContentRating { get; private set; }

			internal TagForChildDirectedTreatment? TagForChildDirectedTreatment { get; private set; }

			internal TagForUnderAgeOfConsent? TagForUnderAgeOfConsent { get; private set; }

			internal List<string> TestDeviceIds { get; private set; }

			public Builder()
			{
				MaxAdContentRating = null;
				TagForChildDirectedTreatment = null;
				TagForUnderAgeOfConsent = null;
				TestDeviceIds = new List<string>();
			}

			public Builder SetMaxAdContentRating(MaxAdContentRating maxAdContentRating)
			{
				MaxAdContentRating = maxAdContentRating;
				return this;
			}

			public Builder SetTagForChildDirectedTreatment(TagForChildDirectedTreatment? tagForChildDirectedTreatment)
			{
				TagForChildDirectedTreatment = tagForChildDirectedTreatment;
				return this;
			}

			public Builder SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent? tagForUnderAgeOfConsent)
			{
				TagForUnderAgeOfConsent = tagForUnderAgeOfConsent;
				return this;
			}

			public Builder SetTestDeviceIds(List<string> testDeviceIds)
			{
				TestDeviceIds = testDeviceIds;
				return this;
			}

			public RequestConfiguration build()
			{
				return new RequestConfiguration(this);
			}
		}

		public MaxAdContentRating MaxAdContentRating { get; private set; }

		public TagForChildDirectedTreatment? TagForChildDirectedTreatment { get; private set; }

		public TagForUnderAgeOfConsent? TagForUnderAgeOfConsent { get; private set; }

		public List<string> TestDeviceIds { get; private set; }

		private RequestConfiguration(Builder builder)
		{
			MaxAdContentRating = builder.MaxAdContentRating;
			TagForChildDirectedTreatment = builder.TagForChildDirectedTreatment;
			TagForUnderAgeOfConsent = builder.TagForUnderAgeOfConsent;
			TestDeviceIds = builder.TestDeviceIds;
		}

		public Builder ToBuilder()
		{
			return new Builder().SetMaxAdContentRating(MaxAdContentRating).SetTagForChildDirectedTreatment(TagForChildDirectedTreatment).SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent)
				.SetTestDeviceIds(TestDeviceIds);
		}
	}
}
