using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float physicalDamage;
	public float physicalDamageMultiplier;
	public float magicDamage;
	public float magicDamageMultiplier;

	public float defense;

	public Vector2 genderMinimum;
	public Vector2 genderMaximum;

	public float maxRange;

	public bool phys;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//Hella
	public float dealtPhysicalDamage(){
		
		float baseDmg = GameObject.FindGameObjectWithTag("Player").GetComponent<AdventurerStats>().baseDamage;
		float power = GameObject.FindGameObjectWithTag("Player").GetComponent<AdventurerStats>().dPower;
		
		float totalDamage = (physicalDamage + (power*physicalDamageMultiplier))*physicalDamageMultiplier;
		
		return totalDamage;
	}

	public float dealtMagicDamage(){
		float wit = GameObject.FindGameObjectWithTag("Player").GetComponent<AdventurerStats>().dWit;
		
		float totalDamage = (magicDamage+(wit*8))*magicDamageMultiplier;
		
		return totalDamage;
	}

	public void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Enemy" && !this.GetComponent<weaponPickUp>())
		{
			//Debug.Log(dealtPhysicalDamage() + " " + dealtMagicDamage());
			c.gameObject.GetComponent<EnemyStats>().takeDamage(dealtPhysicalDamage(), dealtMagicDamage());
			c.gameObject.GetComponent<EnemyAI>().knock = (c.gameObject.transform.position-this.gameObject.transform.position).normalized;
			Camera.main.GetComponent<Cam> ().shakeCam ();
		}
	}
}
