using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
	public static int characterType = 3;

	public static bool butterfly = false;

	public GameObject boyCharacter;

	public GameObject girlCharacter;

	public GameObject bearCharacter;

	public GameObject koalaCharacter;

	public GameObject pandaCharacter;

	public GameObject RedButterflyObject;

	public GameObject BlueButterflyObject;

	public GameObject YellowButterflyObject;

	public Animator boyAnim;

	public Animator girlAnim;

	public Animator bearAnim;

	public Animator koalaAnim;

	public Animator pandaAnim;

	public GameObject deadBoy;

	public GameObject deadGirl;

	public GameObject deadBear;

	public GameObject deadKoala;

	public GameObject deadPanda;

	public Joystick leftJoystick;

	public Joystick rightJoystick;

	public GameObject leftObject;

	public GameObject rightObject;

	public GameObject leftLevel;

	public GameObject rightLevel;

	private float horizontal;

	private float vertical;

	private Rigidbody2D characterRB;

	public float movementSpeed;

	public float climbSpeed;

	private bool facingRight;

	public Transform groundPoint;

	public float groundRadius;

	private bool onGround;

	private bool onDirt;

	private bool onLadder;

	public LayerMask whatIsDirt;

	public LayerMask whatIsGround;

	public float jumpForce;

	public static int gatheredCoins;

	public static int gatheredDiamonds;

	public static int totalCoins = 0;

	public static int totalDiamonds = 0;

	public static int totalHearts = 0;

	public static int gatheredHearts;

	public GameObject bloodPS;

	public GameObject coinPS;

	public GameObject diamondPS;

	public GameObject heartPS;

	public static int level = 1;

	private int checkpointLevel;

	private CameraShake shake;

	public Animator sceneClose;

	public ParticleSystem walkPS;

	private double posX;

	private double posY;

	public ParticleSystem particle1;

	public ParticleSystem particle2;

	public ParticleSystem particle3;

	public ParticleSystem particle4;

	public ParticleSystem particle5;

	public ParticleSystem particle6;

	public ParticleSystem particle7;

	public ParticleSystem particle8;

	public ParticleSystem particle9;

	public ParticleSystem particle10;

	public ParticleSystem particle11;

	public ParticleSystem particle12;

	public ParticleSystem particle13;

	public ParticleSystem particle14;

	public ParticleSystem particle15;

	public ParticleSystem particle16;

	public ParticleSystem particle17;

	public ParticleSystem particle18;

	public ParticleSystem particle19;

	public ParticleSystem particle20;

	public ParticleSystem particle21;

	public static bool sparkle1 = false;

	public static bool sparkle2 = false;

	public static bool sparkle3 = false;

	public static bool sparkle4 = false;

	public static bool sparkle5 = false;

	public static bool sparkle6 = false;

	public static bool sparkle7 = false;

	public static bool sparkle8 = false;

	public static bool sparkle9 = false;

	public static bool sparkle10 = false;

	public static bool sparkle11 = false;

	public static bool sparkle12 = false;

	public static bool sparkle13 = false;

	public static bool sparkle14 = false;

	public static bool sparkle15 = false;

	public static bool sparkle16 = false;

	public static bool sparkle17 = false;

	public static bool sparkle18 = false;

	public static bool sparkle19 = false;

	public static bool sparkle20 = false;

	public static bool sparkle21 = false;

	private void Start()
	{
		if (PlayerPrefs.GetInt("ButterflyEquipShop1") == 1)
		{
			RedButterflyObject.SetActive(value: true);
			BlueButterflyObject.SetActive(value: false);
			YellowButterflyObject.SetActive(value: false);
		}
		else if (PlayerPrefs.GetInt("ButterflyEquipShop2") == 1)
		{
			RedButterflyObject.SetActive(value: false);
			BlueButterflyObject.SetActive(value: true);
			YellowButterflyObject.SetActive(value: false);
		}
		else if (PlayerPrefs.GetInt("ButterflyEquipShop3") == 1)
		{
			RedButterflyObject.SetActive(value: false);
			BlueButterflyObject.SetActive(value: false);
			YellowButterflyObject.SetActive(value: true);
		}
		else
		{
			RedButterflyObject.SetActive(value: false);
			BlueButterflyObject.SetActive(value: false);
			YellowButterflyObject.SetActive(value: false);
		}
		if (PlayerPrefs.HasKey("CharacterType"))
		{
			characterType = PlayerPrefs.GetInt("CharacterType");
		}
		if (PlayerPrefs.HasKey("TotalCoins"))
		{
			totalCoins = PlayerPrefs.GetInt("TotalCoins");
		}
		else
		{
			PlayerPrefs.SetInt("TotalCoins", totalCoins);
		}
		if (PlayerPrefs.HasKey("TotalDiamonds"))
		{
			totalDiamonds = PlayerPrefs.GetInt("TotalDiamonds");
		}
		else
		{
			PlayerPrefs.SetInt("TotalDiamonds", totalDiamonds);
		}
		if (PlayerPrefs.HasKey("TotalHearts"))
		{
			totalHearts = PlayerPrefs.GetInt("TotalHearts");
		}
		else
		{
			PlayerPrefs.SetInt("TotalHearts", totalHearts);
		}
		if (PlayerPrefs.HasKey("PlayerLevel"))
		{
			level = PlayerPrefs.GetInt("PlayerLevel");
		}
		if (PlayerPrefs.HasKey("JoystickType"))
		{
			if (PlayerPrefs.GetInt("JoystickType") == 1)
			{
				leftObject.SetActive(value: true);
				rightObject.SetActive(value: false);
				leftLevel.SetActive(value: false);
				rightLevel.SetActive(value: true);
			}
			else
			{
				leftObject.SetActive(value: false);
				rightObject.SetActive(value: true);
				leftLevel.SetActive(value: true);
				rightLevel.SetActive(value: false);
			}
		}
		else
		{
			PlayerPrefs.SetInt("JoystickType", 1);
			leftObject.SetActive(value: true);
			rightObject.SetActive(value: false);
			leftLevel.SetActive(value: false);
			rightLevel.SetActive(value: true);
		}
		if (PlayerPrefs.HasKey("EquipShop1") && PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			sparkle1 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop2") && PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			sparkle2 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop3") && PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			sparkle3 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop4") && PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			sparkle4 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop5") && PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			sparkle5 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop6") && PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			sparkle6 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop7") && PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			sparkle7 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop8") && PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			sparkle8 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop9") && PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			sparkle9 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop10") && PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			sparkle10 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop11") && PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			sparkle11 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop12") && PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			sparkle12 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop13") && PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			sparkle13 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop14") && PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			sparkle14 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop15") && PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			sparkle15 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop16") && PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			sparkle16 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop17") && PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			sparkle17 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop18") && PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			sparkle18 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop19") && PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			sparkle19 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop20") && PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			sparkle20 = true;
		}
		if (PlayerPrefs.HasKey("EquipShop21") && PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			sparkle21 = true;
		}
		facingRight = true;
		onLadder = false;
		characterRB = GetComponent<Rigidbody2D>();
		gatheredCoins = 0;
		gatheredDiamonds = 0;
		gatheredHearts = 0;
		shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<CameraShake>();
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			boyCharacter.SetActive(value: false);
			girlCharacter.SetActive(value: false);
			bearCharacter.SetActive(value: true);
			koalaCharacter.SetActive(value: false);
			pandaCharacter.SetActive(value: false);
		}
		else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			boyCharacter.SetActive(value: false);
			girlCharacter.SetActive(value: false);
			bearCharacter.SetActive(value: false);
			koalaCharacter.SetActive(value: true);
			pandaCharacter.SetActive(value: false);
		}
		else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			boyCharacter.SetActive(value: false);
			girlCharacter.SetActive(value: false);
			bearCharacter.SetActive(value: false);
			koalaCharacter.SetActive(value: false);
			pandaCharacter.SetActive(value: true);
		}
		else if (characterType == 1)
		{
			boyCharacter.SetActive(value: true);
			girlCharacter.SetActive(value: false);
			bearCharacter.SetActive(value: false);
			koalaCharacter.SetActive(value: false);
			pandaCharacter.SetActive(value: false);
		}
		else
		{
			boyCharacter.SetActive(value: false);
			girlCharacter.SetActive(value: true);
			bearCharacter.SetActive(value: false);
			koalaCharacter.SetActive(value: false);
			pandaCharacter.SetActive(value: false);
		}
		if (sparkle1)
		{
			particle1.Play();
		}
		else
		{
			particle1.Stop();
		}
		if (sparkle2)
		{
			particle2.Play();
		}
		else
		{
			particle2.Stop();
		}
		if (sparkle3)
		{
			particle3.Play();
		}
		else
		{
			particle3.Stop();
		}
		if (sparkle4)
		{
			particle4.Play();
		}
		else
		{
			particle4.Stop();
		}
		if (sparkle5)
		{
			particle5.Play();
		}
		else
		{
			particle5.Stop();
		}
		if (sparkle6)
		{
			particle6.Play();
		}
		else
		{
			particle6.Stop();
		}
		if (sparkle7)
		{
			particle7.Play();
		}
		else
		{
			particle7.Stop();
		}
		if (sparkle8)
		{
			particle8.Play();
		}
		else
		{
			particle8.Stop();
		}
		if (sparkle9)
		{
			particle9.Play();
		}
		else
		{
			particle9.Stop();
		}
		if (sparkle10)
		{
			particle10.Play();
		}
		else
		{
			particle10.Stop();
		}
		if (sparkle11)
		{
			particle11.Play();
		}
		else
		{
			particle11.Stop();
		}
		if (sparkle12)
		{
			particle12.Play();
		}
		else
		{
			particle12.Stop();
		}
		if (sparkle13)
		{
			particle13.Play();
		}
		else
		{
			particle13.Stop();
		}
		if (sparkle14)
		{
			particle14.Play();
		}
		else
		{
			particle14.Stop();
		}
		if (sparkle15)
		{
			particle15.Play();
		}
		else
		{
			particle15.Stop();
		}
		if (sparkle16)
		{
			particle16.Play();
		}
		else
		{
			particle16.Stop();
		}
		if (sparkle17)
		{
			particle17.Play();
		}
		else
		{
			particle17.Stop();
		}
		if (sparkle18)
		{
			particle18.Play();
		}
		else
		{
			particle18.Stop();
		}
		if (sparkle19)
		{
			particle19.Play();
		}
		else
		{
			particle19.Stop();
		}
		if (sparkle20)
		{
			particle20.Play();
		}
		else
		{
			particle20.Stop();
		}
		if (sparkle21)
		{
			particle21.Play();
		}
		else
		{
			particle21.Stop();
		}
		if (PlayerPrefs.GetInt("GameMusic") == 1 || !PlayerPrefs.HasKey("GameMusic"))
		{
			Invoke("BackgroundMusic", 3f);
		}
	}

	private void Update()
	{
	}

	private void OnLadder()
	{
		if (!onLadder)
		{
			characterRB.velocity = new Vector2(horizontal * movementSpeed, characterRB.velocity.y);
			characterRB.gravityScale = 2f;
			if (onGround)
			{
				if (Mathf.Abs(horizontal) > 0f)
				{
					walkPS.Play();
				}
				else
				{
					walkPS.Stop();
				}
			}
			if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
			{
				bearAnim.speed = 1f;
				bearAnim.SetLayerWeight(2, 0f);
				bearAnim.SetBool("OnGround", value: false);
			}
			else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
			{
				koalaAnim.speed = 1f;
				koalaAnim.SetLayerWeight(2, 0f);
				koalaAnim.SetBool("OnGround", value: false);
			}
			else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
			{
				pandaAnim.speed = 1f;
				pandaAnim.SetLayerWeight(2, 0f);
				pandaAnim.SetBool("OnGround", value: false);
			}
			else if (characterType == 1)
			{
				boyAnim.speed = 1f;
				boyAnim.SetLayerWeight(2, 0f);
				boyAnim.SetBool("OnGround", value: false);
			}
			else
			{
				girlAnim.speed = 1f;
				girlAnim.SetLayerWeight(2, 0f);
				girlAnim.SetBool("OnGround", value: false);
			}
			return;
		}
		characterRB.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
		characterRB.gravityScale = 0f;
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			bearAnim.SetLayerWeight(2, 1f);
			if (vertical == 0f)
			{
				bearAnim.speed = 0f;
			}
			else
			{
				bearAnim.speed = 1f;
			}
		}
		else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			koalaAnim.SetLayerWeight(2, 1f);
			if (vertical == 0f)
			{
				koalaAnim.speed = 0f;
			}
			else
			{
				koalaAnim.speed = 1f;
			}
		}
		else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			pandaAnim.SetLayerWeight(2, 1f);
			if (vertical == 0f)
			{
				pandaAnim.speed = 0f;
			}
			else
			{
				pandaAnim.speed = 1f;
			}
		}
		else if (characterType == 1)
		{
			boyAnim.SetLayerWeight(2, 1f);
			if (vertical == 0f)
			{
				boyAnim.speed = 0f;
			}
			else
			{
				boyAnim.speed = 1f;
			}
		}
		else
		{
			girlAnim.SetLayerWeight(2, 1f);
			if (vertical == 0f)
			{
				girlAnim.speed = 0f;
			}
			else
			{
				girlAnim.speed = 1f;
			}
		}
	}

	private void FixedUpdate()
	{
		onGround = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
		onDirt = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsDirt);
		if (onGround || onDirt)
		{
			if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
			{
				bearAnim.SetLayerWeight(1, 0f);
			}
			else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
			{
				koalaAnim.SetLayerWeight(1, 0f);
			}
			else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
			{
				pandaAnim.SetLayerWeight(1, 0f);
			}
			else if (characterType == 1)
			{
				boyAnim.SetLayerWeight(1, 0f);
			}
			else
			{
				girlAnim.SetLayerWeight(1, 0f);
			}
		}
		else if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			bearAnim.SetLayerWeight(1, 1f);
			bearAnim.SetBool("OnGround", value: false);
			bearAnim.SetFloat("VerticalSpeed", characterRB.velocity.y);
		}
		else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			koalaAnim.SetLayerWeight(1, 1f);
			koalaAnim.SetBool("OnGround", value: false);
			koalaAnim.SetFloat("VerticalSpeed", characterRB.velocity.y);
		}
		else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			pandaAnim.SetLayerWeight(1, 1f);
			pandaAnim.SetBool("OnGround", value: false);
			pandaAnim.SetFloat("VerticalSpeed", characterRB.velocity.y);
		}
		else if (characterType == 1)
		{
			boyAnim.SetLayerWeight(1, 1f);
			boyAnim.SetBool("OnGround", value: false);
			boyAnim.SetFloat("VerticalSpeed", characterRB.velocity.y);
		}
		else
		{
			girlAnim.SetLayerWeight(1, 1f);
			girlAnim.SetBool("OnGround", value: false);
			girlAnim.SetFloat("VerticalSpeed", characterRB.velocity.y);
		}
		if (PlayerPrefs.GetInt("JoystickType") == 1)
		{
			if (leftJoystick.Horizontal >= 0.5f)
			{
				horizontal = 1f;
			}
			else if (leftJoystick.Horizontal <= -0.5f)
			{
				horizontal = -1f;
			}
			else
			{
				horizontal = 0f;
			}
			if (leftJoystick.Vertical >= 0.5f)
			{
				vertical = 1f;
				if (onGround || onDirt)
				{
					characterRB.velocity = Vector2.up * jumpForce;
				}
			}
			else if (leftJoystick.Vertical <= -0.5f)
			{
				vertical = -1f;
			}
			else
			{
				vertical = 0f;
			}
		}
		else
		{
			if (rightJoystick.Horizontal >= 0.5f)
			{
				horizontal = 1f;
			}
			else if (rightJoystick.Horizontal <= -0.5f)
			{
				horizontal = -1f;
			}
			else
			{
				horizontal = 0f;
			}
			if (rightJoystick.Vertical >= 0.5f)
			{
				vertical = 1f;
				if (onGround || onDirt)
				{
					characterRB.velocity = Vector2.up * jumpForce;
				}
			}
			else if (rightJoystick.Vertical <= -0.5f)
			{
				vertical = -1f;
			}
			else
			{
				vertical = 0f;
			}
		}
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			bearAnim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
		}
		else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			koalaAnim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
		}
		else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			pandaAnim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
		}
		else if (characterType == 1)
		{
			boyAnim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
		}
		else
		{
			girlAnim.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
		}
		OnLadder();
		Flip();
	}

	private void BackgroundMusic()
	{
		Object.FindObjectOfType<AudioManager>().Play("GameMusic");
	}

	private void StopMusic()
	{
		Object.FindObjectOfType<AudioManager>().Stop("GameMusic");
	}

	private void Flip()
	{
		if ((horizontal > 0f && !facingRight) || (horizontal < 0f && facingRight))
		{
			facingRight = !facingRight;
			Vector3 localScale = base.transform.localScale;
			localScale.x *= -1f;
			base.transform.localScale = localScale;
		}
	}

	public void Die()
	{
		if (totalHearts == 0)
		{
			StopMusic();
			if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
			{
				Object.FindObjectOfType<AudioManager>().Play("DeathSound");
				Object.Destroy(base.gameObject);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
			if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
			{
				Object.Instantiate(deadBear, base.transform.position, Quaternion.identity);
			}
			else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
			{
				Object.Instantiate(deadKoala, base.transform.position, Quaternion.identity);
			}
			else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
			{
				Object.Instantiate(deadPanda, base.transform.position, Quaternion.identity);
			}
			else if (characterType == 1)
			{
				Object.Instantiate(deadBoy, base.transform.position, Quaternion.identity);
			}
			else
			{
				Object.Instantiate(deadGirl, base.transform.position, Quaternion.identity);
			}
			Object.Instantiate(bloodPS, base.transform.position, Quaternion.identity);
			shake.ShakeCam();
			totalCoins -= gatheredCoins;
			totalDiamonds -= gatheredDiamonds;
			totalHearts -= gatheredHearts;
			if (gatheredCoins > 0)
			{
				Object.Instantiate(coinPS, base.transform.position, Quaternion.identity);
			}
			if (gatheredDiamonds > 0)
			{
				Object.Instantiate(diamondPS, base.transform.position, Quaternion.identity);
			}
			return;
		}
		if (gatheredHearts > 0)
		{
			float @float = PlayerPrefs.GetFloat("CheckpointX");
			float float2 = PlayerPrefs.GetFloat("CheckpointY");
			checkpointLevel = PlayerPrefs.GetInt("CheckpointLevel");
			if (checkpointLevel == level)
			{
				if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
				{
					gatheredHearts--;
					totalHearts -= gatheredHearts;
					PlayerPrefs.SetInt("TotalHearts", totalHearts);
					characterRB.position = new Vector2(@float, float2);
					Object.Instantiate(heartPS, new Vector2(@float, float2), Quaternion.identity);
					Object.FindObjectOfType<AudioManager>().Play("HeartSound");
				}
				else
				{
					gatheredHearts--;
					totalHearts -= gatheredHearts;
					PlayerPrefs.SetInt("TotalHearts", totalHearts);
					characterRB.position = new Vector2(@float, float2);
					Object.Instantiate(heartPS, new Vector2(@float, float2), Quaternion.identity);
				}
			}
			else
			{
				if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
				{
					StopMusic();
					Object.FindObjectOfType<AudioManager>().Play("DeathSound");
					Object.Destroy(base.gameObject);
				}
				else
				{
					StopMusic();
					Object.Destroy(base.gameObject);
				}
				if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
				{
					Object.Instantiate(deadBear, base.transform.position, Quaternion.identity);
				}
				else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
				{
					Object.Instantiate(deadKoala, base.transform.position, Quaternion.identity);
				}
				else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
				{
					Object.Instantiate(deadPanda, base.transform.position, Quaternion.identity);
				}
				else if (characterType == 1)
				{
					Object.Instantiate(deadBoy, base.transform.position, Quaternion.identity);
				}
				else
				{
					Object.Instantiate(deadGirl, base.transform.position, Quaternion.identity);
				}
				Object.Instantiate(bloodPS, base.transform.position, Quaternion.identity);
				shake.ShakeCam();
				totalCoins -= gatheredCoins;
				totalDiamonds -= gatheredDiamonds;
				totalHearts -= gatheredHearts;
				if (gatheredCoins > 0)
				{
					Object.Instantiate(coinPS, base.transform.position, Quaternion.identity);
				}
				if (gatheredDiamonds > 0)
				{
					Object.Instantiate(diamondPS, base.transform.position, Quaternion.identity);
				}
			}
		}
		if (gatheredHearts != 0)
		{
			return;
		}
		float float3 = PlayerPrefs.GetFloat("CheckpointX");
		float float4 = PlayerPrefs.GetFloat("CheckpointY");
		checkpointLevel = PlayerPrefs.GetInt("CheckpointLevel");
		if (checkpointLevel == level)
		{
			if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
			{
				totalHearts--;
				PlayerPrefs.SetInt("TotalHearts", totalHearts);
				characterRB.position = new Vector2(float3, float4);
				Object.Instantiate(heartPS, new Vector2(float3, float4), Quaternion.identity);
				Object.FindObjectOfType<AudioManager>().Play("HeartSound");
			}
			else
			{
				totalHearts--;
				PlayerPrefs.SetInt("TotalHearts", totalHearts);
				characterRB.position = new Vector2(float3, float4);
				Object.Instantiate(heartPS, new Vector2(float3, float4), Quaternion.identity);
			}
			return;
		}
		if (PlayerPrefs.GetInt("GameSound") == 1 || !PlayerPrefs.HasKey("GameMusic"))
		{
			StopMusic();
			Object.FindObjectOfType<AudioManager>().Play("DeathSound");
			Object.Destroy(base.gameObject);
		}
		else
		{
			StopMusic();
			Object.Destroy(base.gameObject);
		}
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			Object.Instantiate(deadBear, base.transform.position, Quaternion.identity);
		}
		else if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			Object.Instantiate(deadKoala, base.transform.position, Quaternion.identity);
		}
		else if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			Object.Instantiate(deadPanda, base.transform.position, Quaternion.identity);
		}
		else if (characterType == 1)
		{
			Object.Instantiate(deadBoy, base.transform.position, Quaternion.identity);
		}
		else
		{
			Object.Instantiate(deadGirl, base.transform.position, Quaternion.identity);
		}
		Object.Instantiate(bloodPS, base.transform.position, Quaternion.identity);
		shake.ShakeCam();
		totalCoins -= gatheredCoins;
		totalDiamonds -= gatheredDiamonds;
		totalHearts -= gatheredHearts;
		if (gatheredCoins > 0)
		{
			Object.Instantiate(coinPS, base.transform.position, Quaternion.identity);
		}
		if (gatheredDiamonds > 0)
		{
			Object.Instantiate(diamondPS, base.transform.position, Quaternion.identity);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Ladder")
		{
			onLadder = true;
		}
		if (collider.gameObject.tag == "Zombie" || collider.gameObject.tag == "Skeleton" || collider.gameObject.tag == "Spike" || collider.gameObject.tag == "Kill")
		{
			Die();
		}
		if (collider.gameObject.tag == "Flag")
		{
			StartCoroutine(LoadNextLevel());
			Debug.Log("next level");
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Ladder")
		{
			onLadder = false;
		}
	}

	private IEnumerator LoadNextLevel()
	{
		yield return new WaitForSeconds(2f);
		sceneClose.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		level++;
		PlayerPrefs.SetInt("TotalCoins", totalCoins);
		PlayerPrefs.SetInt("TotalDiamonds", totalDiamonds);
		PlayerPrefs.SetInt("TotalHearts", totalHearts);
		PlayerPrefs.SetInt("PlayerLevel", level);
		PlayerPrefs.Save();
		SceneManager.LoadScene(level);
	}
}
