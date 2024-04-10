using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			float x = base.transform.position.x;
			float y = base.transform.position.y;
			PlayerPrefs.SetFloat("CheckpointX", x);
			PlayerPrefs.SetFloat("CheckpointY", y);
			PlayerPrefs.SetInt("CheckpointLevel", CharacterController.level);
			PlayerPrefs.Save();
		}
	}
}
