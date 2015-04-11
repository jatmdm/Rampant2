using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(player.GetComponent<Player>()) transform.position = Vector2.Lerp ((Vector2)transform.position, (Vector2)player.transform.position+(player.GetComponent<Player>().getWeaponDist()*.65f), Time.fixedDeltaTime * 5);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
	}

	IEnumerator shakeT(object[] parms)
	{
		float tmpF = 0;
		while(tmpF < (float)parms[0])
		{
			tmpF += Time.deltaTime;
			
			Vector2 tmp = Random.insideUnitCircle;
			transform.position = new Vector2(transform.position.x+tmp.x*(float)parms[1], transform.position.y+tmp.y*(float)parms[1]);
			transform.position = new Vector3(transform.position.x, transform.position.y, -10);
			yield return null;
		}
	}
	
	public void shakeCam(float tim = .25f, float power = .25f)
	{
		object[] parms = new object[2]{tim, power};
		StartCoroutine(shakeT(parms));
	}
}
