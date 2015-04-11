using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AdventurerStats : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float maxStamina;
	public float stamina;
	public float fakeStamina;
	public float staminaRegen;

	public int basePower; //Strength & Dexterity (Phys Damage)
	public int baseWit; //Intelligence & Wisdom (Magic Damage)
	public int baseVit; //Constitution  & Charisma (Health and Defense)

	public int dPower;
	public int dWit;
	public int dVit;

	public Vector2 gender; //x axis is feminitity, y axis is masculinity

	public float baseDamage; //Base damage done
	public float dDamage;
	
	public float physicalDefense; //Damage Reduction (Physical)
	public float magicDefense; //Damage Reduction (Magic)
	public float dPhysicalDefense; //Damage Reduction (Physical)
	public float dMagicDefense; //Damage Reduction (Magic)

	public List<GameObject> gems = new List<GameObject>(3);
	public List<GameObject> weapons = new List<GameObject>(3);
	public bool dead;

	public bool exausted; 

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
		fakeStamina = stamina;
		staminaRegen = (baseVit/2f)*Time.deltaTime;

		gender = new Vector2(Random.Range(0f, 6f), Random.Range(0f, 6f));

		baseDamage = (basePower*10f)*1.257f;
		physicalDefense = (basePower/2f) + (10f * baseVit);
		magicDefense = (baseWit/2f) + (8f * baseVit);
	}

	public void updateStats(){
		dPower = basePower;
		dWit = baseWit;
		dVit = baseVit;
		dPhysicalDefense = physicalDefense;
		dMagicDefense = magicDefense;
		dDamage = baseDamage;
		for(int i = 0; i < gems.Count; i++){
			dPower += gems[i].GetComponent<Gem>().powerMod;
			dWit += gems[i].GetComponent<Gem>().witMod;
			dVit += gems[i].GetComponent<Gem>().vitMod;
			
			dPhysicalDefense += gems[i].GetComponent<Gem>().physicalDefenseMod;
			dMagicDefense += gems[i].GetComponent<Gem>().magicDefenseMod;	
			dDamage += (gems[i].GetComponent<Gem>().weaponDamageMod*gems[i].GetComponent<Gem>().weaponDamageMultiplierMod);
		}

		maxHealth = (dVit*10f);
		stamina = fakeStamina = health = maxStamina =  maxHealth;
		staminaRegen = (dVit/2f)*Time.deltaTime;

	}



	public void takeDamage(float magicDmg, float physicalDmg){ //Phys is whether or not damage is physical or not
		float magicTaken = magicDmg-dMagicDefense;
		float physTaken = physicalDmg-dPhysicalDefense;
		if(magicTaken > 0){
			health-= magicTaken;
		}
		if(physTaken > 0){
			health-= physTaken;
		}
	}

	// Update is called once per frame
	void Update () {
		if(!GetComponent<Player>().Unsheathed)
			fakeStamina += staminaRegen * 80 * Time.deltaTime;
		else
			fakeStamina += staminaRegen * 40 * Time.deltaTime;

		stamina = Mathf.MoveTowards(stamina, fakeStamina, 50*Time.deltaTime);

		fakeStamina = Mathf.Clamp (fakeStamina, -10, health);

		if(health <= 0){
			dead = true;
			Application.LoadLevel(Application.loadedLevel);
		}
		if(stamina >= health){
			stamina = health;
			exausted = false;
		}

		if(stamina < 0)
		{
			exausted = true;
			stamina = 0;
		}
	}
}
