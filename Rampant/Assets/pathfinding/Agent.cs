using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	public List<NODE> path = new List<NODE> ();
	private int pathCount = 0;
	private float updateTime = 0;
	
	private Vector2 avoidanceForce;

	private Vector2 dir;
	private Vector2 dirAhead;
	private Vector2 dirAhead2;
	
	public float LOOKDIST;
	
	private Vector2 prevPos;
	private Vector2 velocity;


	public float completionDist = .9f;
	public float updateFreq = 1.25f;

	private Vector2 avoidanceCheck()
	{
		RaycastHit2D hit;
		Vector2 ret = Vector2.zero;
		
		if (hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y)+dirAhead2, dirAhead, LOOKDIST))
		{
			ret = (new Vector2(transform.position.x, transform.position.y)+dir)-((Vector2)hit.collider.bounds.center);
			ret.Normalize();
			ret *= 1.1f;
		}
		
		return ret;
	}

	public Vector2 pathing(Vector2 target, float size)
	{
		updateTime -= Time.fixedDeltaTime;
		
		if(updateTime < 0)
		{
			updateTime = updateFreq;
			List<NODE> tmp = Camera.main.GetComponent<Grid>().getPathToPos((Vector2)transform.position, target);
			if(tmp != path)
			{
				path =  Camera.main.GetComponent<Grid>().getPathToPos((Vector2)transform.position, target);
				pathCount = path.Count-1;
			}
		}
		if (path.Count <= 1) {
			return Vector2.zero;
		}

		if(Vector2.Distance(path[pathCount].pos, (Vector2)transform.position) < .2f)
		{
			pathCount--;
			if(pathCount < 0)
				pathCount = 0;
		}

		dir = (path[pathCount].pos - (Vector2)transform.position); dir.Normalize ();
		dir += Random.insideUnitCircle;
		
		velocity = GetComponent<Rigidbody2D> ().position - prevPos; velocity.Normalize ();
		float velClamp = Mathf.Clamp (velocity.magnitude, .5f, Mathf.Infinity);
		
		dirAhead2 = dir*velClamp*LOOKDIST;
		dirAhead = (dirAhead2) * LOOKDIST;

		Debug.DrawRay (transform.position, dirAhead2);
		Debug.DrawRay ((Vector2)(transform.position)+dirAhead2, dirAhead, Color.blue);
		Debug.DrawRay ((Vector2)(transform.position), avoidanceForce, Color.red);
		
		avoidanceForce = Vector2.Lerp (avoidanceForce, avoidanceCheck (), Time.fixedDeltaTime * 2);

		return  (path [pathCount].pos - (Vector2)transform.position).normalized;//+(avoidanceForce);
	}
}
