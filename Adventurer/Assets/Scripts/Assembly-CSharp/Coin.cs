using UnityEngine;

public class Coin : MonoBehaviour
{
	public GameObject coinPS;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
			{
				Object.FindObjectOfType<AudioManager>().Play("CoinSound");
				Object.Instantiate(coinPS, base.transform.position, Quaternion.identity);
				CharacterController.gatheredCoins++;
				CharacterController.totalCoins++;
				Object.Destroy(base.gameObject);
			}
			else
			{
				Object.Instantiate(coinPS, base.transform.position, Quaternion.identity);
				CharacterController.gatheredCoins++;
				CharacterController.totalCoins++;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
