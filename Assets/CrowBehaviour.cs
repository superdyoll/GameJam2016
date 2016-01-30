using UnityEngine;
using System.Collections;

public class CrowBehaviour : MonoBehaviour {

	private bool selected;
	public Animator animation;
	private Transform target;
	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target.position, step);
		}

		if (Input.GetMouseButtonDown(0)) {
			//Debug.Log("Pressed left click, casting ray.");
			CastSelectRay();
		}
		if (Input.GetMouseButtonDown (1)) {
			if (selected){
				CastMoveRay();
			}
		}
	}

	void CastSelectRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit) {
			animation.speed = 0;
			selected = true;
		} else {
			animation.speed = 1;
			selected = false;
		}
	}

	void CastMoveRay(){
		Debug.Log ("Crow selected now moving");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		target.position = hit.point;
	}

}
