using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandleActors : MonoBehaviour {
	public GameObject Player;
	public List<GameObject> enemies;
	public float cooldown = 2f;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
		getEnemies();
	}

	void getEnemies(){
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < temp.Length; i++){
			enemies.Add(temp[i]);
		}
	}

	void HandleEnemies(){
		for(int i = 0; i < enemies.Count; i++){
			if(Vector2.Distance(enemies[i].transform.position, Player.transform.position) > 10){
				enemies[i].active = false;
			}
			else{
				enemies[i].active = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		cooldown-=Time.deltaTime;
		if(cooldown <= 0){
			getEnemies();
			HandleEnemies();
			cooldown = 2f;
		}
	}
}
