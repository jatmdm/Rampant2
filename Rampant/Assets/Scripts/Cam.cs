using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position = Vector2.Lerp ((Vector2)transform.position, (Vector2)player.transform.position+(player.GetComponent<Player>().getWeaponDist()*.65f), Time.fixedDeltaTime * 5);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
	}
}
