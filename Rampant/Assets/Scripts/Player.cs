using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject weapon;
	public float sBounce;
	public float weaponDist = 1;
	public GameObject spark;
	private Vector2 weaponVecDist;
	private float sheathCoolDown;
	public bool Unsheathed;
	public Vector2 vel;
	private float speed;
	private float fakeMax;
	public float maxSpeed;
	private Vector2 knock;
	
	private float dashTime;
	private float dashDelay;
	public float dashSpeed;

	public GameObject HP;
	public GameObject SP;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (10, 9);
		Physics2D.IgnoreLayerCollision (10, 8);
	}

	public Vector2 getWeaponDist()
	{
		return weaponVecDist.normalized;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		SP.GetComponent<RectTransform> ().localScale = new Vector2((GetComponent<AdventurerStats> ().stamina/GetComponent<AdventurerStats> ().maxHealth), 1);
		HP.GetComponent<RectTransform> ().localScale = new Vector2((GetComponent<AdventurerStats> ().health/GetComponent<AdventurerStats> ().maxHealth), 1);

		knock = Vector2.Lerp (knock, Vector2.zero, Time.fixedDeltaTime*5);

		sheathCoolDown -= Time.fixedDeltaTime;
		dashTime -= Time.fixedDeltaTime;
		dashDelay -= Time.fixedDeltaTime;

		sBounce = Mathf.Lerp (sBounce, 0, Time.fixedDeltaTime * 5);

		if(dashTime < 0) vel = Vector2.Lerp (vel, Vector2.zero, Time.fixedDeltaTime * 3);

		if(dashTime < 0)
		{
			if(Input.GetKey(KeyCode.W))
			{
				vel.y = Mathf.Lerp(vel.y, 1, Time.fixedDeltaTime*5);
			}
			if(Input.GetKey(KeyCode.S))
			{
				vel.y = Mathf.Lerp(vel.y, -1, Time.fixedDeltaTime*5);
			}
			if(Input.GetKey(KeyCode.D))
			{
				vel.x = Mathf.Lerp(vel.x, 1, Time.fixedDeltaTime*5);
			}
			if(Input.GetKey(KeyCode.A))
			{
				vel.x = Mathf.Lerp(vel.x, -1, Time.fixedDeltaTime*5);
			}
		}

		if(dashTime < 0) GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + knock + (vel * (fakeMax) * Time.fixedDeltaTime));
		else GetComponent<Rigidbody2D> ().MovePosition (GetComponent<Rigidbody2D> ().position + (vel * Time.fixedDeltaTime));

		if(Unsheathed)
		{
			if(!weapon.activeSelf) 
			{
				sBounce = 360;
			}
			if(weapon) weapon.SetActive(true);
			fakeMax = Mathf.Lerp(fakeMax, maxSpeed*.65f, Time.fixedDeltaTime*5);
		}
		else
		{
			if(GetComponent<AdventurerStats>().exausted) 
				fakeMax = Mathf.Lerp(fakeMax, .65f*maxSpeed, Time.fixedDeltaTime*5);
			if(!GetComponent<AdventurerStats>().exausted) 
				fakeMax = Mathf.Lerp(fakeMax, maxSpeed, Time.fixedDeltaTime*5);
			if(weapon) weapon.SetActive(false);
		}
		if(!GetComponent<AdventurerStats>().exausted && Input.GetKeyDown(KeyCode.LeftShift) && dashDelay < 0)
		{
			dashDelay = .25f;
			dashTime = .18f;
			vel.Normalize();
			vel *= dashSpeed;
			GetComponent<AdventurerStats>().fakeStamina -= 14;
		}

		if (weapon) 
		{
			float cameraDif = Camera.main.transform.position.y - transform.position.y;
			float mouseX = Input.mousePosition.x;
			float mouseY = Input.mousePosition.y;
			Vector2 mWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (mouseX, mouseY, cameraDif));
			Vector2 mainPos = transform.position;
		
			float diffX = mWorldPos.x - mainPos.x;
			float diffY = mWorldPos.y - mainPos.y;

			weaponDist = Mathf.Lerp (weaponDist, Mathf.Clamp (Vector2.Distance (mWorldPos, transform.position), .5f, weapon.GetComponent<Weapon> ().maxRange), Time.fixedDeltaTime * 5);

			Vector2 dist = new Vector2 (Mathf.Cos (Mathf.Atan2 (diffY, diffX) - (sBounce) * Mathf.Deg2Rad) * weaponDist, Mathf.Sin (Mathf.Atan2 (diffY, diffX) - (sBounce) * Mathf.Deg2Rad) * weaponDist);
			weaponVecDist = dist;

			if ((GetComponent<AdventurerStats> ().exausted && Unsheathed) || (!GetComponent<AdventurerStats> ().exausted && Input.GetMouseButtonDown (0) && (sheathCoolDown < 0))) {
				Destroy (Instantiate (spark, (Vector2)this.transform.position + dist, spark.transform.rotation) as GameObject, .167f);
				Unsheathed = !Unsheathed;
				sheathCoolDown = .7f;
				if (Unsheathed)
					GetComponent<AdventurerStats> ().fakeStamina -= 10;
				Camera.main.GetComponent<Cam> ().shakeCam ();
			}

			weapon.GetComponent<Rigidbody2D> ().MovePosition ((Vector2)this.transform.position + dist);

			weapon.transform.rotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (diffY, diffX) - 90 - sBounce);
		}
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Enemy" && !c.gameObject.GetComponent<EnemyStats>().dead)
		{
			knock = (this.gameObject.transform.position-c.gameObject.transform.position).normalized*.02f;
			Camera.main.GetComponent<Cam> ().shakeCam ();
			GetComponent<AdventurerStats>().takeDamage(c.gameObject.GetComponent<EnemyStats>().magicDamage, c.gameObject.GetComponent<EnemyStats>().physicalDamage);
		}
	}
}
