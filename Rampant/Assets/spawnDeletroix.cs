using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class spawnDeletroix : MonoBehaviour {

	public GameObject boss;
	public bool grow;
	public List<GameObject> bossrocks;
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player")
		{
			Destroy(this.gameObject);
			Instantiate(boss, new Vector2(-55.3f, -15.1f), Quaternion.identity);
			Camera.main.GetComponent<Cam>().shakeCam(0.2f, 0.2f);
			foreach(GameObject rock in bossrocks){
				rock.SetActive(true);
				grow = true;
			}
		}
	}
}
