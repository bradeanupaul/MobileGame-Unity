using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCharacter : MonoBehaviour
{
	public Animator sceneClose;

	private void Update()
	{
	}

	public void BoyCharacter()
	{
		StartCoroutine(BoySelect());
	}

	public void GirlCharacter()
	{
		StartCoroutine(GirlSelect());
	}

	private IEnumerator BoySelect()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		PlayerPrefs.SetInt("CharacterType", 1);
		PlayerPrefs.Save();
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(1);
	}

	private IEnumerator GirlSelect()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		PlayerPrefs.SetInt("CharacterType", 2);
		PlayerPrefs.Save();
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(1);
	}
}
