using UnityEngine;
using System.Collections;

public class resonantArcherAI : MonoBehaviour {

	public float followDistance;
	public float chargeTime = 0;

	public GameObject bow;
	public GameObject arrow;
	private float weaponDist;
	private float shoot;

	public GameObject player;

	// Use this for initialization
	void Start () {
		bow = Instantiate (bow, transform.position, Quaternion.identity) as GameObject;
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
			GetComponent<EnemyAI> ().dir = Vector2.zero;
			weaponDist = .65f;
			Vector2 diff = (player.transform.position-this.transform.position).normalized;
			Vector2 dist = new Vector2 (Mathf.Cos (Mathf.Atan2 (diff.y, diff.x)) * weaponDist, Mathf.Sin (Mathf.Atan2 (diff.y, diff.x)) * weaponDist);
			bow.GetComponent<Rigidbody2D> ().MovePosition ((Vector2)this.transform.position + dist/2);
			bow.transform.rotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (diff.y, diff.x) - 90);

			shoot -= Time.fixedDeltaTime;

			if (Vector2.Distance (player.transform.position, this.gameObject.transform.position) < followDistance/3) {
				GetComponent<EnemyAI> ().targetPosition = (this.transform.position-player.transform.position).normalized*5;
				GetComponent<EnemyAI> ().speed = Mathf.Lerp (GetComponent<EnemyAI> ().speed, 6, Time.fixedDeltaTime * 5);
			}
			else
			{
				if(shoot < 0)
				{
					shoot = Random.Range(1.0f, 3.5f);
					GameObject tmp = Instantiate(arrow, (Vector2)transform.position+dist, Quaternion.identity) as GameObject;
					tmp.GetComponent<arrow>().dir = diff*8;
				}
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
				GetComponent<EnemyAI> ().dir = Vector2.Lerp (GetComponent<EnemyAI> ().dir, this.GetComponent<Agent> ().pathing (GetComponent<EnemyAI> ().targetPosition, 1f), Time.fixedDeltaTime * 5);
			}
		}
	}
}
