using UnityEngine;
using System.Collections;

public class weaponPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) &&  Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 1f)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().weapon = this.gameObject;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().weaponInv.Add(this.gameObject);

			Camera.main.GetComponent<Cam> ().shakeCam ();
			Destroy (this);
		}
		transform.rotation = Quaternion.Euler (0, 0, -90);
	}
}
