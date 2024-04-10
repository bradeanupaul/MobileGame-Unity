using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextShop : MonoBehaviour
{
	public Animator sceneClose;

	public void Next()
	{
		StartCoroutine(GoNext());
	}

	private IEnumerator GoNext()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
