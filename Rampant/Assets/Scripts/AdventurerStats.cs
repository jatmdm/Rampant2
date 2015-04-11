using UnityEngine;
using System.Collections;

public class AdventurerStats : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float maxStamina;
	public float stamina;
	public float staminaRegen;

	public int basePower; //Strength & Dexterity (Phys Damage)
	public int baseWit; //Intelligence & Wisdom (Magic Damage)
	public int baseVit; //Constitution  & Charisma (Health and Defense)

	public int dPower;
	public int dWit;
	public int dVit;

	public Vector2 gender; //x axis is feminitity, y axis is masculinity

	public float baseDamage; //Base damage done
	
	public float physicalDefense; //Damage Reduction (Physical)
	public float magicDefense; //Damage Reduction (Magic)

	public bool dead;


	// Use this for initialization
	void Start () {
		initStats();
		updateStats();
		dead = false;
	}

	public void initStats(){
		basePower = Random.Range(1,4);
		baseWit = Random.Range (1,4);
		baseVit = Random.Range (1,4);

		maxHealth = baseVit*10f;
		stamina = health = maxStamina =  maxHealth;
		staminaRegen = (baseVit/2f)*Time.deltaTime;

		gender = new Vector2(Random.Range(0f, 6f), Random.Range(0f, 6f));

		baseDamage = (basePower*10f)*1.257f;
		physicalDefense = (basePower/2f) + (10f * baseVit);
		magicDefense = (baseWit/2f) + (8f * baseVit);
	}

	public void updateStats(){

	}



	public void takeDamage(float magicDmg, float physicalDmg){ //Phys is whether or not damage is physical or not
		float magicTaken = magicDmg-magicDefense;
		float physTaken = physicalDmg-physicalDefense;
		if(magicTaken > 0){
			health-= magicTaken;
		}
		if(physTaken > 0){
			health-= physTaken;
		}
	}

	// Update is called once per frame
	void Update () {
		if(health <= 0){
			dead = true;
		}
	}
}
