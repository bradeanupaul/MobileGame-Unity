using UnityEngine;

public class JoystickSettings : MonoBehaviour
{
	public GameObject leftButton;

	public GameObject rightButton;

	private void Start()
	{
		if (PlayerPrefs.HasKey("JoystickType"))
		{
			if (PlayerPrefs.GetInt("JoystickType") == 1)
			{
				leftButton.SetActive(value: true);
				rightButton.SetActive(value: false);
			}
			else
			{
				leftButton.SetActive(value: false);
				rightButton.SetActive(value: true);
			}
		}
		else
		{
			leftButton.SetActive(value: true);
			rightButton.SetActive(value: false);
		}
	}

	public void Left()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		leftButton.SetActive(value: false);
		rightButton.SetActive(value: true);
		PlayerPrefs.SetInt("JoystickType", 2);
		PlayerPrefs.Save();
	}

	public void Right()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		leftButton.SetActive(value: true);
		rightButton.SetActive(value: false);
		PlayerPrefs.SetInt("JoystickType", 1);
		PlayerPrefs.Save();
	}
}
