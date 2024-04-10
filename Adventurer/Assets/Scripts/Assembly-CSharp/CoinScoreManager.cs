using UnityEngine;
using UnityEngine.UI;

public class CoinScoreManager : MonoBehaviour
{
	public Text text;

	private void Update()
	{
		text.text = CharacterController.totalCoins.ToString();
	}
}
