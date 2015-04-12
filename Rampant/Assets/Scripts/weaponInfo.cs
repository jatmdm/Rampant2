using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class weaponInfo : MonoBehaviour {

	public int index;
	public GameObject obj;

	public void KillConrad()
	{
		if (obj && GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().pause) 
		{
			GameObject tmp = Instantiate (obj, GameObject.FindGameObjectWithTag ("Player").transform.position, Quaternion.identity) as GameObject;
			tmp.SetActive(true);
			tmp.AddComponent<weaponPickUp> ();
			if (obj == GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().weapon)
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().weapon = null;

			Destroy (obj);
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().weaponInv.Remove (obj);

			this.transform.FindChild("Image").GetComponent<Image>().color = new Color(1, 1, 1, 0);
		}
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().wepMenu ();
	}

	public void UseConrad()
	{
		if (obj && GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().pause)
		{
			if(GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().weapon.activeSelf) GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().weapon.SetActive(false);
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().weapon = obj;
		}

		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().wepMenu ();
	}
}
