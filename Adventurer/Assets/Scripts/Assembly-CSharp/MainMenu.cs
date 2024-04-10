using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Button menuButton;

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
		SceneManager.LoadScene(0);
	}

	public void Menu()
	{
		StartCoroutine(LoadMenu());
	}

	public void Ad()
	{
		if (adInterstitial.IsLoaded())
		{
			StartCoroutine(LoadAd());
		}
		else
		{
			StartCoroutine(LoadMenu());
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

	private IEnumerator LoadMenu()
	{
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(0);
	}

	private IEnumerator LoadAd()
	{
		menuButton.interactable = false;
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		ShowInterstitialAd();
	}

	private AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}
}
