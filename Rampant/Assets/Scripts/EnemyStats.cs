using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	public float health;
	public float maxHealth;
	
	public float physicalDamage;
	public float physicalDamageMultiplier;
	public float magicDamage;
	public float magicDamageMuliplier;

	
	public float physicalDefense; //Damage Reduction (Physical)
	public float magicDefense; //Damage Reduction (Magic)

	public bool dead;

	void Start(){
		health = maxHealth;
		dead = false;
	}

	void Update(){
		if(health <= 0){
			dead = true;
		}
	}

	public void takeDamage(float physicalDmg, float magicDmg){ 
		float magicTaken = magicDmg-magicDefense;
		float physTaken = physicalDmg-physicalDefense;
		if(magicTaken > 0){
			health-= magicTaken;
		}
		if(physTaken > 0){
			health-= physTaken;
		}
	}
	
	public float dealtPhysicalDamage(){
		return physicalDamage * physicalDamageMultiplier;
	}
	public float dealtMagicDamage(){
		return magicDamage * magicDamageMuliplier;
	}
}
