﻿using UnityEngine;
using System;
using System.Collections;

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
		maxSpeed = 5;
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
		if (hit.collider == this.GetComponent<BoxCollider2D>()) {
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
		target = getRandomPoint(ray.origin);
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

	int obedienceToDistance(){
		return 5 - obedience;
	}

    Vector2 getRandomPoint(Vector2 center){
		System.Random rand = new System.Random ();
		int furthestDistance = obedienceToDistance();
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
			//Debug.Log("t = " + t + " u = " + u + " r = " + r);
			//Debug.Log("xAdjust = " + xAdjust + " yAdjust = " + yAdjust);
			center.x = center.x + (float) xAdjust;
			center.y = center.y + (float) yAdjust;
			return center;
		} else {
			return center;
		}
	}

}
