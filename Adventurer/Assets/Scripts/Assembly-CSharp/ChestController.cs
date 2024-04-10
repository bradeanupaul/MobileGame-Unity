using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
	public float msToWait = 50000f;

	public Button chestButton;

	private ulong lastChestOpen;

	public GameObject openedChest;

	public GameObject closedChest;

	public ParticleSystem coinPS;

	public ParticleSystem diamondPS;

	private void Start()
	{
		lastChestOpen = ulong.Parse(PlayerPrefs.GetString("LastChestOpen"));
		if (!IsChestReady())
		{
			chestButton.interactable = false;
			openedChest.SetActive(value: true);
			closedChest.SetActive(value: false);
		}
	}

	public void ChestClick()
	{
		lastChestOpen = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastChestOpen", lastChestOpen.ToString());
		PlayerPrefs.Save();
		CharacterController.totalCoins += 50;
		CharacterController.totalDiamonds += 3;
		PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
		PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
		PlayerPrefs.Save();
		openedChest.SetActive(value: true);
		closedChest.SetActive(value: false);
		coinPS.Play();
		diamondPS.Play();
		chestButton.interactable = false;
		chestButton.GetComponentInChildren<Text>().text = "Opened";
	}

	private void Update()
	{
		if (!chestButton.IsInteractable())
		{
			if (IsChestReady())
			{
				chestButton.interactable = true;
				return;
			}
			ulong num = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen) / 10000uL;
			float num2 = (msToWait - (float)num) / 1000f;
			string text = "";
			text = text + ((int)num2 / 60).ToString("0") + "m ";
			text = text + (num2 % 60f).ToString("0") + "s";
			chestButton.GetComponentInChildren<Text>().text = text;
		}
	}

	private bool IsChestReady()
	{
		ulong num = (ulong)(DateTime.Now.Ticks - (long)lastChestOpen) / 10000uL;
		if ((msToWait - (float)num) / 1000f < 0f)
		{
			chestButton.GetComponentInChildren<Text>().text = "Open";
			openedChest.SetActive(value: false);
			closedChest.SetActive(value: true);
			return true;
		}
		return false;
	}
}
