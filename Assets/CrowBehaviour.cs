using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CrowBehaviour : MonoBehaviour {


	public Animator crowAnimation;
	public int obedience;
	public ShamanBehaviour shaman;
	private bool facingLeft;
	private bool selected;
	private bool moving = false;
	private Vector2 target;
	private float maxSpeed;
	private System.Random rand;

	// Use this for initialization
	void Start () {
		maxSpeed = 3;
		rand = new System.Random ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			// Select bird on Left Click
			if (Input.GetMouseButtonDown (0)) {
				//Debug.Log("Pressed left click, casting ray.");
				CastSelectRay ();
			}

			// Move bird if selected
			if (Input.GetMouseButtonDown (1) && selected) {
				CastMoveRay ();
			}

			checkDarkCollide();

			moveBird();

			adjustDirection();
		}
	}

	void adjustDirection(){
		//Get direction that bird is moving
		float move = transform.position.x - target.x;
		
		//Face the bird right
		if (move < 0) {
			facingLeft = false;
			transform.localRotation = Quaternion.Euler (0, 180, 0);
		}
		
		//Face the bird left
		if (move > 0) {
			facingLeft = true;
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		}
	}

	private bool isCloseToTarget(){
		if (Math.Abs (transform.position.x - target.x) < 0.01f) {
			if(Math.Abs(transform.position.y - target.y) < 0.01f){
				return true;
			}
		}
		return false;
	}

	void moveBird (){
		if (target == null || isCloseToTarget()) {
			target = getRandomPoint ((Vector2)transform.position, obedienceToDistance ());
		} else if (moving) {
			if (isCloseToTarget()){
				target = getRandomPoint ((Vector2)transform.position, obedienceToDistance ());
			} else {
				float step = getCurrentSpeed () * Time.deltaTime;
				transform.position = Vector2.MoveTowards (transform.position, target, step);
			}
		}
	}
	
	void CastSelectRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit.collider.gameObject == this.gameObject) {
			crowAnimation.speed = 0;
			selected = true;
			moving = false;
		} else {
			crowAnimation.speed = 1;
			selected = false;
			moving = true;
		}
	}

	void CastMoveRay(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		target = getRandomPoint(ray.origin, obedienceToDistance());
		crowAnimation.speed = 1;
		moving = true;
		//Debug.Log ("Crow selected now moving to " + target);
	}

	private bool checkCollidesPoint(Vector2 point, List<Point> points, CircleCollider2D collider){
		for (int i = 0; i < points.Count; i++) {
			Point p = points [i];
			double end = p.getRad () * 100;
			
			double p1x = p.getX ();
			double p1y = p.getY ();
			
			double p2x = -Math.Cos (p.getTheta ()) * end;
			double p2y = -Camera.main.orthographicSize + (Math.Sin (p.getTheta ()) * end);
			
			
			double cx = point.x;
			double cy = point.y;
			if (intersects ((float)cx, (float)cy, (float)collider.radius, (float)p1x, (float)p1y, (float)p2x, (float)p2y)) {
				p.setRad (p.getRad () + p.getRad () * 0.1d);
				return true;
			}
			//Instantiate(sun, new Vector3((float)px,(float)p2y, 0), Quaternion.identity );
			//intersection((float)cx,(float) cy, (float)collider.radius,(float) p1x,(float) p1y,(float) p2x,(float) p2y);
		}
		return false;
	}

	void checkDarkCollide() {
		CircleCollider2D collider = GetComponent<CircleCollider2D> ();
		Shadow shadow = GameObject.Find ("Shadow").GetComponent<Shadow> ();
		List<Point> points = shadow.getPoints();
		List<Vector2> boxCheckSquarePoints = new List<Vector2> ();
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x + 1, transform.position.y + 1));
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x - 1, transform.position.y + 1));
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x + 1, transform.position.y - 1));
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x - 1, transform.position.y - 1));
		for (int j = 0; j < 4; j++) {
			checkCollidesPoint(boxCheckSquarePoints[j], points, collider);
		}
		if (checkCollidesPoint((Vector2)transform.position, points, collider) || !shadow.isInScreen((Vector2)transform.position)) {
			target = getRandomPoint(new Vector2(0, -3), 2);
		}
	}

	private bool isInSquare(float cx, float cy, float radius,
	                        float p1x, float p1y, float p2x, float p2y) {
		if (cx > Math.Min (p1x, p2x) && cx < Math.Max (p1x, p2x)) {
			if (cy > Math.Min (p1y, p2y) && cy < Math.Max (p1y, p2y)) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	private bool intersects(float cx, float cy, float radius,
	                                       	float p1x, float p1y, float p2x, float p2y)
	{
		float dx, dy, A, B, C, det, t;
		
		dx = p2x - p1x;
		dy = p2y - p1y;
		
		A = dx * dx + dy * dy;
		B = 2 * (dx * (p1x - cx) + dy * (p1y - cy));
		C = (p1x - cx) * (p1x - cx) + (p1y - cy) * (p1y - cy) - radius * radius;
		
		det = B * B - 4 * A * C;
		if ((A <= 0.0000001) || (det < 0))
		{
			// No real solutions.
			return false;
		}
		else if (det == 0)
		{
			// One solution.
			return isInSquare(cx, cy, radius, p1x, p1y, p2x, p2y);
		}
		else
		{
			// Two solutions
			return isInSquare(cx, cy, radius, p1x, p1y, p2x, p2y);
		}
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
		return 10 - shaman.getCurrentEnergy() * 2;
	}
	
	Vector2 getRandomPoint(Vector2 center,int furthestDistance){
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