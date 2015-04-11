using UnityEngine;
using System.Collections;

public class RevanentAI : MonoBehaviour {

	public Vector2 targetPosition;
	public Vector2 dir = Vector2.zero;
	public Vector2 knock;

	public float speed;

	public float followDistance;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		knock = Vector2.Lerp(knock, Vector2.zero, Time.fixedDeltaTime*5);

		if(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.gameObject.transform.position) < followDistance){
			targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
		}
		else{
			targetPosition = this.gameObject.transform.position;
		}

		if(targetPosition == null){
			dir = Vector2.zero;
		}
		else{
			dir = Vector2.Lerp(dir, this.GetComponent<Agent>().pathing(targetPosition, 1f), Time.fixedDeltaTime*10);
		}

		if(knock.magnitude < .1f) GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + dir * speed * Time.fixedDeltaTime);
		else GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + knock * 20 * Time.fixedDeltaTime);
	}
}
