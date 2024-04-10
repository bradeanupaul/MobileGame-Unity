using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	public interface IRewardedAdClient
	{
		event EventHandler<EventArgs> OnAdLoaded;

		event EventHandler<AdErrorEventArgs> OnAdFailedToLoad;

		event EventHandler<AdErrorEventArgs> OnAdFailedToShow;

		event EventHandler<EventArgs> OnAdOpening;

		event EventHandler<Reward> OnUserEarnedReward;

		event EventHandler<EventArgs> OnAdClosed;

		event EventHandler<AdValueEventArgs> OnPaidEvent;

		void CreateRewardedAd(string adUnitId);

		void LoadAd(AdRequest request);

		bool IsLoaded();

		string MediationAdapterClassName();

		Reward GetRewardItem();

		void Show();

		void SetServerSideVerificationOptions(ServerSideVerificationOptions serverSideVerificationOptions);
	}
}
