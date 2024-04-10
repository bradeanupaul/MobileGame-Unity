using UnityEngine;

public class Flag : MonoBehaviour
{
	public GameObject PS1;

	public GameObject PS2;

	public GameObject PS3;

	public GameObject PS4;

	public GameObject PS5;

	public GameObject PS6;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			Object.Instantiate(PS1, base.transform.position, Quaternion.identity);
			Object.Instantiate(PS2, base.transform.position, Quaternion.identity);
			Object.Instantiate(PS3, base.transform.position, Quaternion.identity);
			Object.Instantiate(PS4, base.transform.position, Quaternion.identity);
			Object.Instantiate(PS5, base.transform.position, Quaternion.identity);
			Object.Instantiate(PS6, base.transform.position, Quaternion.identity);
		}
	}
}
