﻿using UnityEngine;
using System.Collections;

public class Player_Attack : MonoBehaviour {

	private bool attacking = false;

	private float attackTimer = 0;
	private float attackCd = 0.3F;
    private bool attack = false;

	public Collider2D attackTrigger;

	private Animator anim;

	void Awake ()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

    public void Attackbutton()
    {
        attack = true;
    }


	void Update ()
	{
        if (attack && !attacking) 
		{
			attacking = true;
			attackTimer = attackCd;

			attackTrigger.enabled = true;
            attack = false;

		}

		if (attacking) 
		{
			if (attackTimer > 0)
			{
				attackTimer -= Time.deltaTime;
			}
			else
			{
				attacking = false;
				attackTrigger.enabled = false;
			}
		}
		anim.SetBool ("Attacking", attacking);
        attack = false;
	}
}


