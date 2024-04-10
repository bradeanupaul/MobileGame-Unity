using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float xMax;

	public float yMax;

	public float xMin;

	public float yMin;

	private Transform target;

	private void Start()
	{
		target = GameObject.Find("Character").transform;
	}

	private void LateUpdate()
	{
		base.transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), base.transform.position.z);
	}
}
