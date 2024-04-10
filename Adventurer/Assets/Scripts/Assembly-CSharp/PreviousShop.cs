using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousShop : MonoBehaviour
{
	public Animator sceneClose;

	public void Back()
	{
		StartCoroutine(GoBack());
	}

	private IEnumerator GoBack()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
