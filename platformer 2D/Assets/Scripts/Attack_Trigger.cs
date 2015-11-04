using UnityEngine;
using System.Collections;

public class Attack_Trigger : MonoBehaviour {

	public int  dmg = 1 ;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag ("Enemy")) 
		{
			col.SendMessageUpwards ("Damage", dmg);
		}
	}
}