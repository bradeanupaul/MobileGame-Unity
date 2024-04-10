using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
	public Animator sceneClose;

	public GameObject boyButton;

	public GameObject girlButton;

	private void Start()
	{
		if (PlayerPrefs.HasKey("CharacterType"))
		{
			if (PlayerPrefs.GetInt("CharacterType") == 1)
			{
				boyButton.SetActive(value: true);
				girlButton.SetActive(value: false);
			}
			else
			{
				boyButton.SetActive(value: false);
				girlButton.SetActive(value: true);
			}
		}
		else
		{
			boyButton.SetActive(value: true);
			girlButton.SetActive(value: false);
		}
	}

	private void Update()
	{
	}

	public void BoyCharacter()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		boyButton.SetActive(value: true);
		girlButton.SetActive(value: false);
		PlayerPrefs.SetInt("CharacterType", 1);
		PlayerPrefs.Save();
	}

	public void GirlCharacter()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		boyButton.SetActive(value: false);
		girlButton.SetActive(value: true);
		PlayerPrefs.SetInt("CharacterType", 2);
		PlayerPrefs.Save();
	}
}
