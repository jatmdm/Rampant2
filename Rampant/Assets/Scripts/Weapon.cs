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
	
	public float dealtPhysicalDamage(){
		
		float baseDmg = GameObject.FindGameObjectWithTag("Player").GetComponent<AdventurerStats>().baseDamage;
		float power = GameObject.FindGameObjectWithTag("Player").GetComponent<AdventurerStats>().dPower;
		
		float totalDamage = (physicalDamage+baseDmg+power)*physicalDamageMultiplier;
		
		return totalDamage;
	}

	public float dealtMagicDamage(){
		float wit = GameObject.FindGameObjectWithTag("Player").GetComponent<AdventurerStats>().dWit;
		
		float totalDamage = (physicalDamage+(wit*8))*physicalDamageMultiplier;
		
		return totalDamage;
	}

}
