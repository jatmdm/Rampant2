using UnityEngine;
using System.Collections;

public class Deletroix : MonoBehaviour {

	public float followDistance;
	public float chargeTime = 0;
	public GameObject player;

	public GameObject[] enemies = new GameObject[2];
	public int spawnTotal;
	public float spawnAttempt;
	public float damageReaction;

	// Use this for initialization
	void Start () {
		GameObject[] en = GameObject.FindGameObjectsWithTag ("Enemy");
		for(int i=0; i < en.Length; i++)
		{
			if(en[i] != this.gameObject)
				Destroy(en[i]);
		}

		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spawnAttempt -= Time.fixedDeltaTime;

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
			spawnTotal = (int)(10.0f*Mathf.Abs(.5f-(GetComponent<EnemyStats> ().health/GetComponent<EnemyStats> ().maxHealth)));
			spawnTotal = Mathf.Clamp(spawnTotal, 2, 14);

			if (Vector2.Distance (GameObject.FindGameObjectWithTag("Player").transform.position, this.gameObject.transform.position) < 2)
			{
				damageReaction += Time.fixedDeltaTime;
			}

			if(damageReaction > 10)
			{	
				transform.position = new Vector2(Random.Range(-63, -48), Random.Range(-10, -20));
				damageReaction = 0;
			}

			if(spawnAttempt < 0)
			{
				spawnAttempt = 10;

				GameObject[] en = GameObject.FindGameObjectsWithTag("Enemy");
				if(en.Length < spawnTotal)
				{
					for(int i=0; i < Mathf.Abs(spawnTotal-en.Length); i++)
					{
						Instantiate(enemies[Random.Range(0, enemies.Length)], this.gameObject.transform.position, Quaternion.identity);
					}
				}
			}
		}
	}

	void Update(){
//		if (!GetComponent<EnemyStats> ().dead) 
//		{			
//			if (GetComponent<EnemyAI> ().targetPosition == null) {
//				GetComponent<EnemyAI> ().dir = Vector2.zero;
//			} else {
//				GetComponent<EnemyAI> ().dir = Vector2.Lerp (GetComponent<EnemyAI> ().dir, this.GetComponent<Agent> ().pathing (GetComponent<EnemyAI> ().targetPosition, 1f), Time.fixedDeltaTime * 5);
//			}
//		}
	}
}
