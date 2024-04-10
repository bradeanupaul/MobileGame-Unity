using UnityEngine;

public class Heart : MonoBehaviour
{
	public GameObject heartPS;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
			{
				Object.FindObjectOfType<AudioManager>().Play("HeartSound");
				Object.Instantiate(heartPS, base.transform.position, Quaternion.identity);
				CharacterController.gatheredHearts++;
				CharacterController.totalHearts++;
				Object.Destroy(base.gameObject);
			}
			else
			{
				Object.Instantiate(heartPS, base.transform.position, Quaternion.identity);
				CharacterController.gatheredHearts++;
				CharacterController.totalHearts++;
				Object.Destroy(base.gameObject);
			}
		}
	}
}
