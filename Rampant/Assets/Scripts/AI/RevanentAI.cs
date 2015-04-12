using UnityEngine;
using System.Collections;

public class RevanentAI : MonoBehaviour {

	public float followDistance;
	public float chargeTime = 0;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(GetComponent<EnemyStats>().dead)
		{
			if(transform.eulerAngles.z != 90) GetComponent<EnemyAI>().knock = ((Vector2)transform.position-(Vector2)player.transform.position).normalized*.05f;
			GetComponent<EnemyAI> ().dir = Vector2.zero;
			GetComponent<EnemyAI>().speed = 0;
			Destroy(GetComponent<Agent>());
			transform.rotation = Quaternion.Euler(0, 0, 90);
		}

		GetComponent<EnemyAI>().knock = Vector2.Lerp(GetComponent<EnemyAI>().knock, Vector2.zero, Time.fixedDeltaTime*5);

		GetComponent<EnemyAI>().speed = Mathf.Lerp(GetComponent<EnemyAI>().speed, 0, Time.fixedDeltaTime*3);

		if (!GetComponent<EnemyStats> ().dead) 
		{
			if (Vector2.Distance (player.transform.position, this.gameObject.transform.position) < followDistance) {
				GetComponent<EnemyAI> ().targetPosition = player.transform.position;
				GetComponent<EnemyAI> ().speed = Mathf.Lerp (GetComponent<EnemyAI> ().speed, 5, Time.fixedDeltaTime * 5);
			}

			if (Vector2.Distance (player.transform.position, this.gameObject.transform.position) < followDistance / 3) {
				chargeTime += Time.fixedDeltaTime;
			}

			if (chargeTime > 2) {
				GetComponent<EnemyAI> ().dir += ((Vector2)player.transform.position - (Vector2)transform.position).normalized * 5;
				chargeTime = 0;
			}


			if (GetComponent<EnemyAI> ().targetPosition == null) {
				GetComponent<EnemyAI> ().dir = Vector2.zero;
			} else {
				//Debug.Log("shit");
				GetComponent<EnemyAI> ().dir = Vector2.Lerp (GetComponent<EnemyAI> ().dir, this.GetComponent<Agent> ().pathing (GetComponent<EnemyAI> ().targetPosition, 1f), Time.fixedDeltaTime * 5);
			}
		}

		if(GetComponent<EnemyAI>().knock.magnitude < .1f) GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + GetComponent<EnemyAI>().dir * GetComponent<EnemyAI>().speed * Time.fixedDeltaTime);
		else GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + GetComponent<EnemyAI>().knock * 20 * Time.fixedDeltaTime);
	}

	void Update(){
		if (!GetComponent<EnemyStats> ().dead) 
		{			
			if (GetComponent<EnemyAI> ().targetPosition == null) {
				GetComponent<EnemyAI> ().dir = Vector2.zero;
			} else {
				//Debug.Log("shit");
				GetComponent<EnemyAI> ().dir = Vector2.Lerp (GetComponent<EnemyAI> ().dir, this.GetComponent<Agent> ().pathing (GetComponent<EnemyAI> ().targetPosition, 1f), Time.fixedDeltaTime * 5);
			}
		}
	}
}
