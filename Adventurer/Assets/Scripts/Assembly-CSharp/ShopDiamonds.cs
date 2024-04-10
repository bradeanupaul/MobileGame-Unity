using UnityEngine;
using UnityEngine.UI;

public class ShopDiamonds : MonoBehaviour
{
	public Text text;

	private void Update()
	{
		text.text = PlayerPrefs.GetInt("TotalDiamonds").ToString();
	}
}
