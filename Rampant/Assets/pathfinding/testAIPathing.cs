using UnityEngine;
using System.Collections;

public class testAIPathing : MonoBehaviour {

	Vector2 dir = Vector2.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		dir = Vector2.Lerp (dir, this.GetComponent<Agent> ().pathing ((Vector2)GameObject.Find ("follow").transform.position, GetComponent<Collider2D>().bounds.size.magnitude), Time.fixedDeltaTime * 10); ;

		GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + dir * 5 * Time.fixedDeltaTime);
	}
}
