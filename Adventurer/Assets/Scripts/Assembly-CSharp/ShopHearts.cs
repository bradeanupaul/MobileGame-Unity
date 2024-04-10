using UnityEngine;
using UnityEngine.UI;

public class ShopHearts : MonoBehaviour
{
	public Text text;

	private void Update()
	{
		text.text = PlayerPrefs.GetInt("TotalHearts").ToString();
	}
}
