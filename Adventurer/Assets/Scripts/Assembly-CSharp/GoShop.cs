using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoShop : MonoBehaviour
{
	public Button shopButton;

	private InterstitialAd adInterstitial;

	private string idApp;

	private string idInterstitial;

	public Animator sceneClose;

	private void Start()
	{
		idApp = "ca-app-pub-2406139663072835~4444489077";
		idInterstitial = "ca-app-pub-2406139663072835/3869774000";
		MobileAds.Initialize(idApp);
		RequestInterstitialAd();
	}

	public void Shop()
	{
		StartCoroutine(OpenShop());
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
		SceneManager.LoadScene(12);
	}

	public void Ad()
	{
		if (adInterstitial.IsLoaded())
		{
			StartCoroutine(LoadAd());
		}
		else
		{
			StartCoroutine(OpenShop());
		}
	}

	private void OnDestroy()
	{
		DestroyInterstitialAd();
		adInterstitial.OnAdLoaded -= HandleOnAdLoaded;
		adInterstitial.OnAdOpening -= HandleOnAdOpening;
		adInterstitial.OnAdClosed -= HandleOnAdClosed;
	}

	public void DestroyInterstitialAd()
	{
		adInterstitial.Destroy();
	}

	private IEnumerator LoadAd()
	{
		shopButton.interactable = false;
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		ShowInterstitialAd();
	}

	private AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	private IEnumerator OpenShop()
	{
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(12);
	}
}
