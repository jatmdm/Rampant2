using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D c){
		if(c.tag == "Player"){
			c.GetComponent<AdventurerStats>().healthPotions++;
			Destroy(this.gameObject);
		}
	}
}
