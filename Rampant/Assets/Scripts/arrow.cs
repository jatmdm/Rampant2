using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour {

	public bool alive;
	public Vector2 dir;
	public float life = 1;

	// Use this for initialization
	void Start () {
		life = 5;
		transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (dir.y, dir.x)*Mathf.Rad2Deg-90);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		life -= Time.fixedDeltaTime;

		GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + dir * 4 * Time.fixedDeltaTime);

		if (life < 0)
		{
			alive = false;
			GetComponent<Collider2D>().enabled = false;
		}

		if(!alive)
		{
			dir = Vector2.Lerp(dir, Vector2.zero, Time.fixedDeltaTime*3);
		}
		if (dir.magnitude < .1f)
			Destroy (this.gameObject);
	}
}
