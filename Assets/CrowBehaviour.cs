using UnityEngine;
using System.Collections;

public class CrowBehaviour : MonoBehaviour {

	private bool selected;
	private bool moving;
	public Animator animation;
	private Vector2 target;
	public float maxSpeed;
	public int obedience;
	public bool facingLeft;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float move = transform.position.x - target.x;
	
		if ((Vector2)transform.position != target) {
			float step = getCurrentSpeed () * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target, step);
		} else {
			moving = false;
		}

		// Select bird on Left Click
		if (Input.GetMouseButtonDown(0)) {
			//Debug.Log("Pressed left click, casting ray.");
			CastSelectRay();
		}

		// Move bird if selected
		if (Input.GetMouseButtonDown (1) && selected) {
			CastMoveRay();
		}

		//Face the bird right
		if (move < 0) {
			facingLeft = false;
			transform.localRotation = Quaternion.Euler (0, 180, 0);
		}

		//Face the bird left
		if (move > 0){
			facingLeft = true;
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		}
	}

	void CastSelectRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit) {
			animation.speed = 0;
			selected = true;
			moving = false;
		} else {
			animation.speed = 1;
			selected = false;
		}
	}

	void CastMoveRay(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		target = ray.origin;
		animation.speed = 1;
		moving = true;
		//Debug.Log ("Crow selected now moving to " + target);
	}

	float getCurrentSpeed(){
		if (moving) {
			if (obedience < maxSpeed) {
				return obedience + 1;
			} else {
				return maxSpeed;
			}
		} else {
			return 0;
		}
	}

}
