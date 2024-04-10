using UnityEngine;
using UnityEngine.UI;

public class LevelScoreManager : MonoBehaviour
{
	public Text text1;

	public Text text2;

	private void Update()
	{
		text1.text = CharacterController.level.ToString();
		text2.text = CharacterController.level.ToString();
	}
}
