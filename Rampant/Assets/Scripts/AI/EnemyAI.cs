using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	
	public Vector2 targetPosition;
	public Vector2 dir = Vector2.zero;
	public Vector2 knock;
	public float speed;

	public Vector2 minimumGenderTolerance;
	public Vector2 maximumGenderTolerance;


	public bool canSeeObject(GameObject desiredObject){
		float angle = Vector2.Angle(transform.position, desiredObject.transform.position);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos (angle), Mathf.Sin(angle)));
		if(hit.collider.CompareTag("Wall")){
			return false;
		}
		else return true;
	}
}
