using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyDrop : MonoBehaviour {

	public List<GameObject> itemDrops;
	public float dropChance;

	public void tryDropItems(){
		float temp;
		temp = Random.Range (0f,101f);
		if(dropChance <= temp){
			GameObject.Instantiate(itemDrops[Random.Range(0,itemDrops.Count)], transform.position, Quaternion.identity);
		}
	}

}
