using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
	private GameObject playerObject;

	private InterstitialAd adInterstitial;

	private string idApp;

	private string idInterstitial;

	public Animator sceneClose;

	private void Start()
	{
		playerObject = GameObject.Find("Character");
		idApp = "ca-app-pub-2406139663072835~4444489077";
		idInterstitial = "ca-app-pub-3940256099942544/1033173712";
		MobileAds.Initialize(idApp);
		RequestInterstitialAd();
	}

	private void Update()
	{
		if (playerObject == null)
		{
			if (adInterstitial.IsLoaded())
			{
				StartCoroutine(LoadAd());
			}
			else
			{
				StartCoroutine(ReloadScene());
			}
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

	private AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	private IEnumerator LoadAd()
	{
		yield return new WaitForSeconds(1f);
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		ShowInterstitialAd();
	}

	private IEnumerator ReloadScene()
	{
		yield return new WaitForSeconds(1f);
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
