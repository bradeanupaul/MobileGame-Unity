using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
	public float speed;

	private Transform target;

	private void Start()
	{
		target = GameObject.Find("Character").GetComponent<Transform>();
	}

	private void Update()
	{
		if ((double)Vector2.Distance(base.transform.position, target.position) > 0.5)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, target.position, speed * Time.deltaTime);
		}
	}
}
