using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdController : MonoBehaviour
{
	public float msToWait1;

	public float msToWait2;

	public float msToWait3;

	private bool interact1 = true;

	private bool interact2 = true;

	private bool interact3 = true;

	private ulong lastChestOpen1;

	private ulong lastChestOpen2;

	private ulong lastChestOpen3;

	public Button coinsButton;

	public ParticleSystem coinPS;

	public Button diamondsButton;

	public ParticleSystem diamondPS;

	public Button heartsButton;

	public ParticleSystem heartPS;

	public bool banner;

	private BannerView adBanner;

	private InterstitialAd adInterstitial;

	private RewardBasedVideoAd adReward;

	private string idApp;

	private string idBanner;

	private string idInterstitial;

	private string idReward;

	private void Start()
	{
		idApp = "ca-app-pub-2406139663072835~4444489077";
		idBanner = "ca-app-pub-3940256099942544/6300978111";
		idInterstitial = "ca-app-pub-2406139663072835/3869774000";
		idReward = "ca-app-pub-2406139663072835/2556692337";
		adReward = RewardBasedVideoAd.Instance;
		MobileAds.Initialize(idApp);
		RequestBannerAd();
		RequestInterstitialAd();
		lastChestOpen1 = ulong.Parse(PlayerPrefs.GetString("LastChestOpen1"));
		if (!IsChestReady1())
		{
			coinsButton.interactable = false;
			interact1 = false;
		}
		lastChestOpen2 = ulong.Parse(PlayerPrefs.GetString("LastChestOpen2"));
		if (!IsChestReady2())
		{
			diamondsButton.interactable = false;
			interact2 = false;
		}
		lastChestOpen3 = ulong.Parse(PlayerPrefs.GetString("LastChestOpen3"));
		if (!IsChestReady3())
		{
			heartsButton.interactable = false;
			interact3 = false;
		}
	}

	public void ChestClick1()
	{
		lastChestOpen1 = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen1", lastChestOpen1.ToString());
		PlayerPrefs.Save();
		interact1 = true;
		coinsButton.interactable = false;
		diamondsButton.interactable = false;
		heartsButton.interactable = false;
		coinsButton.GetComponentInChildren<Text>().text = "Loading";
		RequestRewardAd1();
	}

	public void ChestClick2()
	{
		lastChestOpen2 = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen2", lastChestOpen2.ToString());
		PlayerPrefs.Save();
		interact2 = true;
		coinsButton.interactable = false;
		diamondsButton.interactable = false;
		heartsButton.interactable = false;
		diamondsButton.GetComponentInChildren<Text>().text = "Loading";
		RequestRewardAd2();
	}

	public void ChestClick3()
	{
		lastChestOpen3 = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen3", lastChestOpen3.ToString());
		PlayerPrefs.Save();
		interact3 = true;
		coinsButton.interactable = false;
		diamondsButton.interactable = false;
		heartsButton.interactable = false;
		heartsButton.GetComponentInChildren<Text>().text = "Loading";
		RequestRewardAd3();
	}

	private void Update()
	{
		if (!interact1)
		{
			if (IsChestReady1())
			{
				coinsButton.interactable = true;
				return;
			}
			ulong num = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen1) / 10000uL;
			float num2 = (msToWait1 - (float)num) / 1000f;
			string text = "";
			text = text + ((int)num2 / 60).ToString("0") + "m ";
			text = text + (num2 % 60f).ToString("0") + "s";
			coinsButton.GetComponentInChildren<Text>().text = text;
		}
		if (!interact2)
		{
			if (IsChestReady2())
			{
				diamondsButton.interactable = true;
				return;
			}
			ulong num3 = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen2) / 10000uL;
			float num4 = (msToWait2 - (float)num3) / 1000f;
			string text2 = "";
			text2 = text2 + ((int)num4 / 60).ToString("0") + "m ";
			text2 = text2 + (num4 % 60f).ToString("0") + "s";
			diamondsButton.GetComponentInChildren<Text>().text = text2;
		}
		if (!interact3)
		{
			if (IsChestReady3())
			{
				heartsButton.interactable = true;
				return;
			}
			ulong num5 = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen3) / 10000uL;
			float num6 = (msToWait3 - (float)num5) / 1000f;
			string text3 = "";
			text3 = text3 + ((int)num6 / 60).ToString("0") + "m ";
			text3 = text3 + (num6 % 60f).ToString("0") + "s";
			heartsButton.GetComponentInChildren<Text>().text = text3;
		}
	}

	private bool IsChestReady1()
	{
		ulong num = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen1) / 10000uL;
		if ((msToWait1 - (float)num) / 1000f < 0f)
		{
			coinsButton.GetComponentInChildren<Text>().text = "Watch";
			return true;
		}
		return false;
	}

	private bool IsChestReady2()
	{
		ulong num = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen2) / 10000uL;
		if ((msToWait2 - (float)num) / 1000f < 0f)
		{
			diamondsButton.GetComponentInChildren<Text>().text = "Watch";
			return true;
		}
		return false;
	}

	private bool IsChestReady3()
	{
		ulong num = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen3) / 10000uL;
		if ((msToWait3 - (float)num) / 1000f < 0f)
		{
			heartsButton.GetComponentInChildren<Text>().text = "Watch";
			return true;
		}
		return false;
	}

	public void RequestBannerAd()
	{
		adBanner = new BannerView(idBanner, AdSize.Banner, AdPosition.Bottom);
		AdRequest request = AdRequestBuild();
		if (banner)
		{
			adBanner.LoadAd(request);
		}
	}

	public void RequestInterstitialAd()
	{
		adInterstitial = new InterstitialAd(idInterstitial);
		AdRequest request = AdRequestBuild();
		adInterstitial.LoadAd(request);
		adInterstitial.OnAdLoaded += HandleOnAdLoaded;
		adInterstitial.OnAdOpening += HandleOnAdOpening;
		adInterstitial.OnAdClosed += HandleOnAdClosed;
	}

	public void RequestRewardAd1()
	{
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request, idReward);
		adReward.OnAdLoaded += HandleOnRewardedAdLoaded1;
		adReward.OnAdOpening += HandleOnRewardedAdOpening1;
		adReward.OnAdRewarded += HandleOnAdRewarded1;
		adReward.OnAdClosed += HandleOnRewardedAdClosed1;
	}

	public void ShowRewardAd1()
	{
		if (adReward.IsLoaded())
		{
			adReward.Show();
		}
	}

	public void RequestRewardAd2()
	{
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request, idReward);
		adReward.OnAdLoaded += HandleOnRewardedAdLoaded2;
		adReward.OnAdOpening += HandleOnRewardedAdOpening2;
		adReward.OnAdRewarded += HandleOnAdRewarded2;
		adReward.OnAdClosed += HandleOnRewardedAdClosed2;
	}

	public void ShowRewardAd2()
	{
		if (adReward.IsLoaded())
		{
			adReward.Show();
		}
	}

	public void RequestRewardAd3()
	{
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request, idReward);
		adReward.OnAdLoaded += HandleOnRewardedAdLoaded3;
		adReward.OnAdOpening += HandleOnRewardedAdOpening3;
		adReward.OnAdRewarded += HandleOnAdRewarded3;
		adReward.OnAdClosed += HandleOnRewardedAdClosed3;
	}

	public void ShowRewardAd3()
	{
		if (adReward.IsLoaded())
		{
			adReward.Show();
		}
	}

	public void ShowInterstitialAd()
	{
		adInterstitial.Show();
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
	}

	public void HandleOnAdOpening(object sender, EventArgs args)
	{
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		adInterstitial.OnAdLoaded -= HandleOnAdLoaded;
		adInterstitial.OnAdOpening -= HandleOnAdOpening;
		adInterstitial.OnAdClosed -= HandleOnAdClosed;
		RequestInterstitialAd();
	}

	public void HandleOnRewardedAdLoaded1(object sender, EventArgs args)
	{
		ShowRewardAd1();
	}

	public void HandleOnRewardedAdOpening1(object sender, EventArgs args)
	{
	}

	public void HandleOnAdRewarded1(object sender, EventArgs args)
	{
		CharacterController.totalCoins += 50;
		coinPS.Play();
		PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
		PlayerPrefs.Save();
	}

	public void HandleOnRewardedAdClosed1(object sender, EventArgs args)
	{
		coinsButton.interactable = false;
		interact1 = false;
		if (IsChestReady2())
		{
			diamondsButton.interactable = true;
		}
		if (IsChestReady3())
		{
			heartsButton.interactable = true;
		}
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded1;
		adReward.OnAdOpening -= HandleOnRewardedAdOpening1;
		adReward.OnAdRewarded -= HandleOnAdRewarded1;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed1;
		lastChestOpen1 = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen1", lastChestOpen1.ToString());
		PlayerPrefs.Save();
	}

	public void HandleOnRewardedAdLoaded2(object sender, EventArgs args)
	{
		ShowRewardAd2();
	}

	public void HandleOnRewardedAdOpening2(object sender, EventArgs args)
	{
	}

	public void HandleOnAdRewarded2(object sender, EventArgs args)
	{
		CharacterController.totalDiamonds += 3;
		diamondPS.Play();
		PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
		PlayerPrefs.Save();
	}

	public void HandleOnRewardedAdClosed2(object sender, EventArgs args)
	{
		diamondsButton.interactable = false;
		interact2 = false;
		if (IsChestReady1())
		{
			coinsButton.interactable = true;
		}
		if (IsChestReady3())
		{
			heartsButton.interactable = true;
		}
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded2;
		adReward.OnAdOpening -= HandleOnRewardedAdOpening2;
		adReward.OnAdRewarded -= HandleOnAdRewarded2;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed2;
		lastChestOpen2 = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen2", lastChestOpen2.ToString());
		PlayerPrefs.Save();
	}

	public void HandleOnRewardedAdLoaded3(object sender, EventArgs args)
	{
		ShowRewardAd3();
	}

	public void HandleOnRewardedAdOpening3(object sender, EventArgs args)
	{
	}

	public void HandleOnAdRewarded3(object sender, EventArgs args)
	{
		CharacterController.totalHearts += 5;
		heartPS.Play();
		PlayerPrefs.SetInt("TotalHearts", CharacterController.totalHearts);
		PlayerPrefs.Save();
	}

	public void HandleOnRewardedAdClosed3(object sender, EventArgs args)
	{
		heartsButton.interactable = false;
		interact3 = false;
		if (IsChestReady1())
		{
			coinsButton.interactable = true;
		}
		if (IsChestReady2())
		{
			diamondsButton.interactable = true;
		}
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded3;
		adReward.OnAdOpening -= HandleOnRewardedAdOpening3;
		adReward.OnAdRewarded -= HandleOnAdRewarded3;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed3;
		lastChestOpen3 = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen3", lastChestOpen3.ToString());
		PlayerPrefs.Save();
	}

	public void GetMoreCoins()
	{
		coinsButton.interactable = false;
		coinsButton.GetComponentInChildren<Text>().text = "Loading";
		RequestRewardAd1();
	}

	public void GetMoreDiamonds()
	{
		diamondsButton.interactable = false;
		diamondsButton.GetComponentInChildren<Text>().text = "Loading";
		RequestRewardAd2();
	}

	public void GetMoreHearts()
	{
		heartsButton.interactable = false;
		heartsButton.GetComponentInChildren<Text>().text = "Loading";
		RequestRewardAd3();
	}

	private void OnDestroy()
	{
		DestroyBannerAd();
		DestroyInterstitialAd();
		adInterstitial.OnAdLoaded -= HandleOnAdLoaded;
		adInterstitial.OnAdOpening -= HandleOnAdOpening;
		adInterstitial.OnAdClosed -= HandleOnAdClosed;
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded1;
		adReward.OnAdOpening -= HandleOnRewardedAdOpening1;
		adReward.OnAdRewarded -= HandleOnAdRewarded1;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed1;
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded2;
		adReward.OnAdOpening -= HandleOnRewardedAdOpening2;
		adReward.OnAdRewarded -= HandleOnAdRewarded2;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed2;
		adReward.OnAdLoaded -= HandleOnRewardedAdLoaded3;
		adReward.OnAdOpening -= HandleOnRewardedAdOpening3;
		adReward.OnAdRewarded -= HandleOnAdRewarded3;
		adReward.OnAdClosed -= HandleOnRewardedAdClosed3;
	}

	public void DestroyBannerAd()
	{
		if (adBanner != null)
		{
			adBanner.Destroy();
		}
		Debug.Log("reclama stearsa");
	}

	public void DestroyInterstitialAd()
	{
		adInterstitial.Destroy();
	}

	private AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}
}
