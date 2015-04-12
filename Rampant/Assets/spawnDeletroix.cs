using UnityEngine;
using System.Collections;

public class spawnDeletroix : MonoBehaviour {

	public GameObject boss;

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Player")
		{
			Destroy(this.gameObject);
			Instantiate(boss, new Vector2(-55.3f, -15.1f), Quaternion.identity);
		}
	}
}
