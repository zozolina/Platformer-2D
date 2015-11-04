using UnityEngine;
using System.Collections;

public class GroundCheck: MonoBehaviour {


	private Player player;

	void Start ()
	{

		player = gameObject.GetComponentInParent<Player> ();


	}

	void OnTriggerEnter2D(Collider2D col) //fail
	{

		player.grounded = true;

	}

	void OnTriggerStay2D(Collider2D col) //fail
	{


		player.grounded = true;
	}

	void OnTriggerExit2D (Collider2D col) //fail o mers pt ca tre sa fie setupu exact

	{

		player.grounded = false;

	}




}
