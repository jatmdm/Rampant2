using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {
	public float countdown;
	public bool count = false;
	public void OnTriggerEnter2D(Collider2D c){
		if(c.tag == "Player"){
			c.GetComponent<AdventurerStats>().healthPotions++;
			count = true;
		}
	}

	void Update(){
		if(count){
			countdown-=Time.deltaTime;
			transform.position = Vector2.Lerp(transform.position, GameObject.Find ("Player").transform.position, Time.deltaTime);
			transform.localScale *= 0.91f;
		}
		if(countdown < 0){
			Destroy(this.gameObject);
		}
	}
}
