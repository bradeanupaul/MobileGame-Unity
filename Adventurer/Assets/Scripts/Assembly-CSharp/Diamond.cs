using UnityEngine;

public class Diamond : MonoBehaviour
{
	public GameObject diamondPS;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
			{
				Object.FindObjectOfType<AudioManager>().Play("DiamondSound");
				Object.Instantiate(diamondPS, base.transform.position, Quaternion.identity);
				CharacterController.gatheredDiamonds++;
				CharacterController.totalDiamonds++;
				Object.Destroy(base.gameObject);
			}
			else
			{
				Object.Instantiate(diamondPS, base.transform.position, Quaternion.identity);
				CharacterController.gatheredDiamonds++;
				CharacterController.totalDiamonds++;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
