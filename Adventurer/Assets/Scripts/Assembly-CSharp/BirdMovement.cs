using UnityEngine;

public class BirdMovement : MonoBehaviour
{
	public float speed;

	private bool movingRight = true;

	public Transform groundCheck;

	public float checkRadius;

	public LayerMask whatIsGround;

	private bool isGrounded;

	private CameraShake shake;

	private void Update()
	{
		base.transform.Translate(Vector2.right * speed * Time.deltaTime);
		if (isGrounded)
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
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
	}
}
