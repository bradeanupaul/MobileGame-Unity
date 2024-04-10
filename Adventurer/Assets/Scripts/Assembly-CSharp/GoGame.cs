using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoGame : MonoBehaviour
{
	public Button gameButton;

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

	private void Update()
	{
	}

	public void Game()
	{
		if (PlayerPrefs.HasKey("CharacterType"))
		{
			SceneManager.LoadScene(CharacterController.level);
		}
		else
		{
			SceneManager.LoadScene(14);
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
		SceneManager.LoadScene(CharacterController.level);
	}

	public void Ad()
	{
		if (PlayerPrefs.HasKey("CharacterType"))
		{
			if (adInterstitial.IsLoaded())
			{
				StartCoroutine(LoadAd());
			}
			else
			{
				StartCoroutine(LoadGame());
			}
		}
		else
		{
			StartCoroutine(OpenSelector());
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
		gameButton.interactable = false;
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		ShowInterstitialAd();
	}

	private AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	private IEnumerator OpenGame(int enter)
	{
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(enter);
	}

	private IEnumerator LoadGame()
	{
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(CharacterController.level);
	}

	private IEnumerator OpenSelector()
	{
		UnityEngine.Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(14);
	}
}
