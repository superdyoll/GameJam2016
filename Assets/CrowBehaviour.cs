using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CrowBehaviour : MonoBehaviour {

	private bool selected;
	private bool moving;
	public Animator animation;
	private Vector2 target;
	private float maxSpeed;
	public int obedience;
	public bool facingLeft;

	// Use this for initialization
	void Start () {
		maxSpeed = 3;
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {

		// Select bird on Left Click
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click, casting ray.");
			CastSelectRay();
		}
		
		// Move bird if selected
		if (Input.GetMouseButtonDown (1) && selected) {
			CastMoveRay();
		}

		// See if object colliding with dark matter
		checkDarkCollide ();

		//Get direction that bird is moving
		float move = transform.position.x - target.x;
		
		// Move bird towards target
		if ((Vector2)transform.position != target) {
			float step = getCurrentSpeed () * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, target, step);
			moving = true;
		} else {
			moving = false;
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
		
		// Wobble
		if (!moving && !selected) {
			target = getRandomPoint((Vector2)transform.position, obedienceToDistance());
		}
	}

	void CastSelectRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit.collider == this.GetComponent<BoxCollider2D>()) {
			Debug.Log("Bird Selected");
			animation.speed = 0;
			selected = true;
			moving = false;
		} else {
			animation.speed = 1;
			selected = false;
			moving = true;
		}
	}

	void checkDarkCollide() {
		CircleCollider2D collider = this.GetComponent<CircleCollider2D> ();
		Shadow shadow = GameObject.Find ("Shadow").GetComponent<Shadow> ();
		List<Point> points = shadow.getPoints();
		for (int i = 0; i < points.Count; i++) {
			Point p = points[i];
			double end = p.getRad () * 100;

			double top;
			double bottom;
			bool check;

		}
	}

	void CastMoveRay(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		target = getRandomPoint(ray.origin, obedienceToDistance());
		animation.speed = 1;
		moving = true;
		Debug.Log ("Crow selected now moving to " + target);
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

	int obedienceToDistance(){
		return 5 - obedience;
	}

    Vector2 getRandomPoint(Vector2 center,int furthestDistance){
		System.Random rand = new System.Random ();
		if (furthestDistance > 0) {
			double t = 2 * Math.PI * rand.NextDouble();
			double u = 0;
			for (int i = furthestDistance; i > 0; i--) {
				u = u + rand.NextDouble();
			}
			double r = u;
			if (u > furthestDistance) {
				r = 2 - u;
			}
			double xAdjust = r * Math.Cos (t);
			double yAdjust = r * Math.Sin (t);
			center.x = center.x + (float) xAdjust;
			center.y = center.y + (float) yAdjust;
			return center;
		} else {
			return center;
		}
	}

}
