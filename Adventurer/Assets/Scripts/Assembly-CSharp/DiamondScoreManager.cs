using UnityEngine;
using UnityEngine.UI;

public class DiamondScoreManager : MonoBehaviour
{
	public Text text;

	private void Update()
	{
		text.text = CharacterController.totalDiamonds.ToString();
	}
}
