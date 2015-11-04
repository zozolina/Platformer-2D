using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour 
{

	//bool spiked = false;

	private Player cc;

	void Start ()
	{
		cc = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();


	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
		
			cc.Damage(1);
			//spiked = true;
			StartCoroutine (cc.Knockback(0.02F, 200,cc.transform.position));
		}
	}
}
