﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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

	public List<GameObject> currentGems = new List<GameObject>(3);
	public List<GameObject> gemInventory = new List<GameObject>();
	public List<GameObject> weapons = new List<GameObject>(3);
	public bool dead;

	private float deathTime = 0;
	public GameObject gameOver;

	public bool exausted; 

	public int healthPotions;

	// Use this for initialization
	void Start () {
		initStats();
		updateStats();
		dead = false;
	}

	public void useHealthPotion(){
		if(healthPotions > 0){
			health += dVit * 3;
			if(health > maxHealth)
				health = maxHealth;
			healthPotions--;
			Camera.main.GetComponent<Cam>().shakeCam(0.2f, 0.25f);
			GameObject.Find("HealthPotionAnimation").GetComponent<ParticleSystem>().Play();
		}
	}

	public void removeGem(int index){
		currentGems[index] = Resources.Load("Gems/NoGem") as GameObject;
	}

	public void removeGem2(int index){
		gemInventory[index] = Resources.Load("Gems/NoGem") as GameObject;
	}
	public bool equipGem(GameObject gem){
		bool switched = false;
		for(int i = 0; i < gemInventory.Count; i++){
			if(gemInventory[i].GetComponent<Gem>().gemName == "No Gem"){
				gemInventory[i] = gem;
				return true;
			}
		}
		return false;
	}
	public bool equipGem2(GameObject gem){
		bool switched = false;
		for(int i = 0; i < currentGems.Count; i++){
			if(currentGems[i].GetComponent<Gem>().gemName == "No Gem"){
				currentGems[i] = gem;
				return true;
			}
		}
		return false;
	}

	public void initStats(){
		basePower = Random.Range(3,6);
		baseWit = Random.Range (3,6);
		baseVit = Random.Range (3,6);

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
		for(int i = 0; i < currentGems.Count; i++){
			dPower += currentGems[i].GetComponent<Gem>().powerMod;
			dWit += currentGems[i].GetComponent<Gem>().witMod;
			dVit += currentGems[i].GetComponent<Gem>().vitMod;
			
			dPhysicalDefense += currentGems[i].GetComponent<Gem>().physicalDefenseMod;
			dMagicDefense += currentGems[i].GetComponent<Gem>().magicDefenseMod;	
			dDamage += (currentGems[i].GetComponent<Gem>().weaponDamageMod*currentGems[i].GetComponent<Gem>().weaponDamageMultiplierMod);
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
			fakeStamina += staminaRegen * 90 * Time.deltaTime;
		else
			fakeStamina += staminaRegen * 40 * Time.deltaTime;

		stamina = Mathf.MoveTowards(stamina, fakeStamina, 20*Time.deltaTime);

		//fakeStamina = Mathf.Clamp (fakeStamina, maxHealth, health);

		maxStamina = health;
		if(health <= maxHealth*.2f)
		{
			maxStamina = maxHealth*.2f;
		}

		if(dead)
		{
			deathTime += Time.deltaTime;
		}

		if(deathTime > 2.85f)
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if(health <= 0){
			if(!dead)
			{
				GameObject tmp = Instantiate(gameOver, Vector2.zero, Quaternion.identity) as GameObject;
				tmp.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			dead = true;
		}
		if(fakeStamina >= maxStamina+staminaRegen)
		{
			fakeStamina = maxStamina;
			exausted = false;
		}

		if(stamina < 0)
		{
			exausted = true;
			stamina = 0;
		}

		GameObject.Find ("potions").transform.GetChild (0).GetComponent<Text> ().text = healthPotions.ToString ();

		if(Input.GetKeyDown ("q")){
			GameObject.Find("potions").GetComponent<Animator>().SetTrigger("use");
			useHealthPotion();
		}
	}
}
