using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
	public Animator sceneClose;

	public void Quit()
	{
		StartCoroutine(ExitGame());
	}

	private IEnumerator ExitGame()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		Application.Quit();
	}
}
