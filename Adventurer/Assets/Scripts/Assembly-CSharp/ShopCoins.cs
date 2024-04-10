using UnityEngine;
using UnityEngine.UI;

public class ShopCoins : MonoBehaviour
{
	public Text text;

	private void Update()
	{
		text.text = PlayerPrefs.GetInt("TotalCoins").ToString();
	}
}
