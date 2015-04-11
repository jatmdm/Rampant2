using UnityEngine;
using System.Collections;

public class arrogantBomberAI : MonoBehaviour {

	public float followDistance;
	public float chargeTime = 0;
	public GameObject explosion;
	public float explodeAmount = 1;
	public bool explode = false;
	public float explodeT = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(explode)
		{
			explodeT -= Time.fixedDeltaTime;
			transform.localScale += Vector3.one*.001f;
			transform.localScale = new Vector2(Mathf.Clamp(transform.localScale.x, 1, 1.6f), transform.localScale.y);
		}
		if(explodeT < 0)
		{
			GetComponent<EnemyStats>().dead = true;
			transform.localScale = Vector2.zero;
		}

		if(!GetComponent<EnemyStats>().dead && explodeT < .15f && explodeT > .1f)
		{
			Camera.main.GetComponent<Cam> ().shakeCam (.2f, .01f);
			Destroy (Instantiate(explosion, transform.position+((Vector3)Random.insideUnitCircle*Random.Range(1.0f, 3.0f)), Quaternion.identity), .35f);
		}

		if(GetComponent<EnemyStats>().dead)
		{
			explodeAmount -= Time.fixedDeltaTime;

			if(transform.eulerAngles.z != 90) 
			{
				//Destroy (Instantiate(explosion, transform.position, Quaternion.identity), .35f);
				//GetComponent<EnemyAI>().knock = ((Vector2)transform.position-(Vector2)GameObject.FindGameObjectWithTag ("Player").transform.position).normalized*.05f;
			}
			GetComponent<EnemyAI> ().dir = Vector2.zero;
			GetComponent<EnemyAI>().speed = 0;
			Destroy(GetComponent<Agent>());
			transform.rotation = Quaternion.Euler(0, 0, 90);
		}

		GetComponent<EnemyAI>().knock = Vector2.Lerp(GetComponent<EnemyAI>().knock, Vector2.zero, Time.fixedDeltaTime*5);

		GetComponent<EnemyAI>().speed = Mathf.Lerp(GetComponent<EnemyAI>().speed, 0, Time.fixedDeltaTime*3);

		if (!GetComponent<EnemyStats> ().dead) 
		{

			if (Vector2.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, this.gameObject.transform.position) < followDistance) {
				GetComponent<EnemyAI> ().targetPosition = (GameObject.FindGameObjectWithTag ("Player").transform.position-this.transform.position).normalized*5;
				GetComponent<EnemyAI> ().speed = Mathf.Lerp (GetComponent<EnemyAI> ().speed, 7, Time.fixedDeltaTime * 5);
			}
			if (Vector2.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, this.gameObject.transform.position) < 1f) 
			{
				explode = true;
			}

			if (GetComponent<EnemyAI> ().targetPosition == null) {
				GetComponent<EnemyAI> ().dir = Vector2.zero;
			} else {
				GetComponent<EnemyAI> ().dir = Vector2.Lerp (GetComponent<EnemyAI> ().dir, this.GetComponent<Agent> ().pathing (GetComponent<EnemyAI> ().targetPosition, 1f), Time.fixedDeltaTime * 5);
			}
		}

		if(!explode)
		{	
			if(GetComponent<EnemyAI>().knock.magnitude < .1f) GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + GetComponent<EnemyAI>().dir * GetComponent<EnemyAI>().speed * Time.fixedDeltaTime);
			else GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + GetComponent<EnemyAI>().knock * 20 * Time.fixedDeltaTime);
		}
	}
}
