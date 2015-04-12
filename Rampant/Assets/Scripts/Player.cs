using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
	private float weaponOrient;
	
	private float dashTime;
	private float dashDelay;
	public float dashSpeed;

	public GameObject HP;
	public GameObject SP;

	public GameObject Can;
	public bool pause = false;

	public List<GameObject> weaponInv = new List<GameObject>();
	public GameObject[] weaponUI = new GameObject[3];

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (10, 9);
		Physics2D.IgnoreLayerCollision (10, 8);
	}

	public Vector2 getWeaponDist()
	{
		return weaponVecDist.normalized;
	}

	public void wepMenu()
	{
		for(int i=0; i < weaponUI.Length; i++)
		{
			weaponUI[i].GetComponent<Animator>().SetBool("in", false);
		}
		pause = false;
	}

	void Update()
	{
		int count = 0;
		foreach (GameObject g in weaponInv) 
		{
			weaponUI[count].GetComponent<weaponInfo>().obj = g;
			weaponUI[count].transform.FindChild("Image").GetComponent<Image>().sprite = g.GetComponent<SpriteRenderer>().sprite;
			if(!pause)weaponUI[count].transform.FindChild("Image").GetComponent<Image>().color = new Color(1, 1, 1, 0);
			count++;	
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pause = !pause;
			if(pause)
			{
				Can.transform.FindChild("weapons").gameObject.SetActive(false);

				Can.transform.FindChild("pause").FindChild("Stats").GetComponent<Animator>().SetBool("in", true);
				Can.transform.FindChild("pause").FindChild("lace").GetComponent<Animator>().SetBool("in", true);
				Can.transform.FindChild("pause").FindChild("GemScroll").GetComponent<Animator>().SetBool("in", true);
			}
			if(!pause)
			{
				Can.transform.FindChild("weapons").gameObject.SetActive(true);

				Can.transform.FindChild("pause").FindChild("Stats").GetComponent<Animator>().SetBool("in", false);
				Can.transform.FindChild("pause").FindChild("lace").GetComponent<Animator>().SetBool("in", false);
				Can.transform.FindChild("pause").FindChild("GemScroll").GetComponent<Animator>().SetBool("in", false);
			}
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			pause = !pause;
			if(pause)
			{
				for(int i=0; i < weaponUI.Length; i++)
				{
					weaponUI[i].transform.FindChild("Image").GetComponent<Image>().SetNativeSize();
					weaponUI[i].GetComponent<Animator>().SetBool("in", true);
					if(weaponUI[i].transform.FindChild("Image").GetComponent<Image>().sprite) weaponUI[i].transform.FindChild("Image").GetComponent<Image>().color = Color.white;
					if(!weaponUI[i].transform.FindChild("Image").GetComponent<Image>().sprite) weaponUI[i].transform.FindChild("Image").GetComponent<Image>().color = new Color(1, 1, 1, 0);
				}
			}
			if(!pause)
			{
				for(int i=0; i < weaponUI.Length; i++)
				{
					weaponUI[i].GetComponent<Animator>().SetBool("in", false);
				 	weaponUI[i].transform.FindChild("Image").GetComponent<Image>().color = new Color(1, 1, 1, 0);
				}
			}
		}
		
		if(pause)
		{
			Can.GetComponent<statScreen>().UpdateUI();
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
		else
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if(GetComponent<AdventurerStats>().dead)
		{
			transform.rotation = Quaternion.Euler(0, 0, 90);
		}
		if(!GetComponent<AdventurerStats>().dead && !pause)
		{
			SP.GetComponent<RectTransform> ().localScale = new Vector2((GetComponent<AdventurerStats> ().stamina/GetComponent<AdventurerStats> ().maxHealth), 1);
			HP.GetComponent<RectTransform> ().localScale = new Vector2((GetComponent<AdventurerStats> ().health/GetComponent<AdventurerStats> ().maxHealth), 1);

			SP.GetComponent<RectTransform> ().localScale = new Vector2(Mathf.Clamp(SP.GetComponent<RectTransform> ().localScale.x, 0, GetComponent<AdventurerStats>().maxHealth),
			                                                                       Mathf.Clamp(SP.GetComponent<RectTransform>().localScale.y, 0, GetComponent<AdventurerStats>().maxHealth));
			HP.GetComponent<RectTransform> ().localScale = new Vector2(Mathf.Clamp(HP.GetComponent<RectTransform> ().localScale.x, 0, GetComponent<AdventurerStats>().maxHealth),
			                                                                      Mathf.Clamp(HP.GetComponent<RectTransform>().localScale.y, 0, GetComponent<AdventurerStats>().maxHealth));


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
			if((Mathf.Abs(vel.x) > .1f || Mathf.Abs(vel.y) > .1f) && !GetComponent<AdventurerStats>().exausted && Input.GetKeyDown(KeyCode.LeftShift) && dashDelay < 0)
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
				if ((GetComponent<AdventurerStats> ().exausted && Unsheathed) || (!GetComponent<AdventurerStats> ().exausted && Input.GetMouseButton (1) && (sheathCoolDown < 0))) {
					weaponOrient = Mathf.Lerp(weaponOrient, 90, Time.fixedDeltaTime*10);
				}
				else
				{
					weaponOrient = Mathf.Lerp(weaponOrient, 0, Time.fixedDeltaTime*10);
				}

				weapon.GetComponent<Rigidbody2D> ().MovePosition ((Vector2)this.transform.position + dist);

				weapon.transform.rotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (diffY, diffX) - 90 - sBounce - weaponOrient);
			}
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

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "E")
		{
			Camera.main.GetComponent<Cam> ().shakeCam ();
			GetComponent<AdventurerStats>().takeDamage(c.gameObject.GetComponent<EnemyStats>().magicDamage, c.gameObject.GetComponent<EnemyStats>().physicalDamage);
		}
	}
}
