namespace GoogleMobileAds.Api
{
	public class ServerSideVerificationOptions
	{
		public class Builder
		{
			internal string UserId { get; private set; }

			internal string CustomData { get; private set; }

			public Builder SetUserId(string userId)
			{
				UserId = userId;
				return this;
			}

			public Builder SetCustomData(string customData)
			{
				CustomData = customData;
				return this;
			}

			public ServerSideVerificationOptions Build()
			{
				return new ServerSideVerificationOptions(this);
			}
		}

		public string UserId { get; private set; }

		public string CustomData { get; private set; }

		private ServerSideVerificationOptions(Builder builder)
		{
			UserId = builder.UserId;
			CustomData = builder.CustomData;
		}
	}
}
