using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Sprite [] HeartSprites;

	public Image HeartUI;

	private Player cc;

	void Start ()
	{

		cc = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> (); 

	}


	void Update ()
	{

		HeartUI.sprite = HeartSprites [cc.curHealt]; 


	}
}