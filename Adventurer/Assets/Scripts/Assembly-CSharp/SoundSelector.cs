using UnityEngine;

public class SoundSelector : MonoBehaviour
{
	public GameObject onButton;

	public GameObject offButton;

	private void Start()
	{
		if (PlayerPrefs.HasKey("GameSound"))
		{
			if (PlayerPrefs.GetInt("GameSound") == 1)
			{
				onButton.SetActive(value: true);
				offButton.SetActive(value: false);
			}
			else
			{
				onButton.SetActive(value: false);
				offButton.SetActive(value: true);
			}
		}
		else
		{
			onButton.SetActive(value: true);
			offButton.SetActive(value: false);
		}
	}

	public void Off()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		onButton.SetActive(value: false);
		offButton.SetActive(value: true);
		PlayerPrefs.SetInt("GameSound", 2);
		PlayerPrefs.Save();
	}

	public void On()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		onButton.SetActive(value: true);
		offButton.SetActive(value: false);
		PlayerPrefs.SetInt("GameSound", 1);
		PlayerPrefs.Save();
	}
}
