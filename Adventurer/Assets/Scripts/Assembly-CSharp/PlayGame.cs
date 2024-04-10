using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
	public Animator sceneClose;

	public void Play()
	{
		StartCoroutine(OpenLevel(CharacterController.level));
	}

	private IEnumerator OpenLevel(int enter)
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(enter);
	}
}
