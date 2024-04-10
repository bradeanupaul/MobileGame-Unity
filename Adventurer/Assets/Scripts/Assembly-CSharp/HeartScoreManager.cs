using UnityEngine;
using UnityEngine.UI;

public class HeartScoreManager : MonoBehaviour
{
	public Text text;

	private void Update()
	{
		text.text = CharacterController.totalHearts.ToString();
	}
}
