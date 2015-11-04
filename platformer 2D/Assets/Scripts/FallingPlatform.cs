﻿using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;

	public float fallDelay;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag("Player")) 
		{ print ("YES");
		StartCoroutine(Fall());
		}
	}

	IEnumerator Fall()
	{ print ("merge!");
		yield return new WaitForSeconds (fallDelay);
		rb2d.isKinematic = false;
		GetComponent<Collider2D> ().isTrigger = true;

		yield return 0;
	}
}
