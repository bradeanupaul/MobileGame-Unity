using UnityEngine;

public class ZombieController : MonoBehaviour
{
	public float speed;

	private bool movingRight = true;

	public Transform pathCheck;

	public Transform headCheck;

	public LayerMask whatIsPlayer;

	public LayerMask whatIsPath;

	public float checkRadius;

	private bool onHead;

	private bool onPath;

	public GameObject zombiePs;

	public GameObject deadZombie;

	private GameObject playerObject;

	private CameraShake shake;

	private void Start()
	{
		playerObject = GameObject.Find("Player");
		shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<CameraShake>();
	}

	private void Update()
	{
		if (onHead)
		{
			Hurt();
		}
		base.transform.Translate(Vector2.right * speed * Time.deltaTime);
		if (!onPath)
		{
			if (movingRight)
			{
				base.transform.eulerAngles = new Vector3(0f, -180f, 0f);
				movingRight = false;
			}
			else
			{
				base.transform.eulerAngles = new Vector3(0f, 0f, 0f);
				movingRight = true;
			}
		}
	}

	private void FixedUpdate()
	{
		onPath = Physics2D.OverlapCircle(pathCheck.position, checkRadius, whatIsPath);
		onHead = Physics2D.OverlapCircle(headCheck.position, checkRadius, whatIsPlayer);
	}

	public void Hurt()
	{
		Object.Destroy(base.gameObject);
		Object.Instantiate(deadZombie, base.transform.position, Quaternion.identity);
		Object.Instantiate(zombiePs, base.transform.position, Quaternion.identity);
		shake.ShakeCam();
	}
}
