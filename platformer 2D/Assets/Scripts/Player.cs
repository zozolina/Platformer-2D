using UnityEngine;
using System.Collections;
using CnControls;

public class Player : MonoBehaviour {


	//floats
	public float maxSpeed = 3;
	public float speed = 50F;
	public float jumpPower = 150F;


	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool wallSliding;
	public bool facingRight = true;
    private bool jumped = false;
   


		//stats

		public int curHealt;

		public int maxHealt = 5;



	//refferences
	private Rigidbody2D rb2d;
	private Animator anim;
	private GameMaster gm;
	public Transform wallCheckPoint;
	public bool wallCheck;
	public LayerMask wallLayerMask;



	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();

		curHealt = maxHealt;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent <GameMaster>();
	}

    public void Jump()
    {
        jumped = true;
    }
	

	void Update () 
	{
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));

        if (Input.GetAxis("Horizontal") < -0.1F || CnInputManager.GetAxis("Horizontal") < -0.1F)
			{
				transform.localScale = new Vector3 (-1,1,1);
				facingRight = false;
			}
        if (Input.GetAxis("Horizontal") > 0.1F || CnInputManager.GetAxis("Horizontal") > 0.1F)
			{
				transform.localScale = new Vector3 (1,1,1);
				facingRight = true;
			}

			if( jumped && !wallSliding ) //Input.GetButtonDown ("Jump")
			{
					if(grounded)
					{
						rb2d.AddForce (Vector2.up * jumpPower);
						canDoubleJump = true;
                        jumped = false;
					}

					else
					{
						if(canDoubleJump)
						{
							canDoubleJump = false;
							rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
							rb2d.AddForce(Vector2.up * jumpPower / 1.75F);
                            jumped = false;
						}
					}
			 }

				if (curHealt > maxHealt)
					{
						curHealt = maxHealt;
					}
				if (curHealt <= 0) 
					{
						curHealt = 0;
						Die ();
					}

		if (!grounded) 
		{
			wallCheck = Physics2D.OverlapCircle (wallCheckPoint.position, 0.1F, wallLayerMask);
            if (facingRight && CnInputManager.GetAxis("Horizontal") > 0.1F || !facingRight && CnInputManager.GetAxis("Horizontal") < 0.1F)
				{
					if (wallCheck)
					{
						HandleWallSliding ();
					}
				}
		}
		if (wallCheck == false || grounded) 
		{
			wallSliding = false;
		}

	}



	void HandleWallSliding ()
	{
		rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7F);

		wallSliding = true;

		if (jumped) 
		{
			if (facingRight) 
			{
				rb2d.AddForce(new Vector2 (-1.5F, 2)* jumpPower);
                jumped = false;
			}
			else
			{
				rb2d.AddForce(new Vector2 (1.5F, 2)* jumpPower);
                jumped = false;
			}
		}
	}

	
	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		float h = CnInputManager.GetAxis("Horizontal");

		//fake friction

		if (grounded) 
			{
				rb2d.velocity = easeVelocity;
			}

		//moving player

				if (grounded) 
				{
					rb2d.AddForce ((Vector2.right * speed) * h);
				} 
				else 
				{
					rb2d.AddForce ((Vector2.right * speed / 2) * h);
				}
	

		//limiting the speed to player
		if (rb2d.velocity.x > maxSpeed) 
			{
				rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
			}
		if (rb2d.velocity.x < -maxSpeed) 
			{
				rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
			}
	}

	 
	void Die()
	{
		if (PlayerPrefs.HasKey ("Highscore"))
		{
			if (PlayerPrefs.GetInt ("Highscore") < gm.score) 
			{
				PlayerPrefs.SetInt ("Highscore", gm.score);
			}
		} 
		else 
		{
			PlayerPrefs.SetInt ("Highscore", gm.score);
		}
		Application.LoadLevel (Application.loadedLevel);
	}
	

	public void Damage(int dmg)
			{
				curHealt -= dmg;
				gameObject.GetComponent<Animation>().Play("Player_RedFlash");
			}

		
	public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
			{
				float timer = 0;
				rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
				while (knockDur > timer)
					{
						timer += Time.deltaTime;
						rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
					}

					yield return 0;
				}


	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Coin")) 
		{
			Destroy(col.gameObject);
			gm.score +=1;
		}
	}

}

