using UnityEngine;
using System.Collections;

public class CrowBehaviour : MonoBehaviour {

	private bool selected;
	public Animator animation;
	private Vector2 target;
	private float speed;
	public float levelSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Vector2) transform.position != target) {
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target, step);
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
			speed =0;
		} else {
			animation.speed = 1;
			speed = levelSpeed;
			selected = false;
		}
	}

	void CastMoveRay(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		target = ray.origin;
		animation.speed = 1;
		speed = levelSpeed;
		Debug.Log ("Crow selected now moving to " + target);
	}

}
