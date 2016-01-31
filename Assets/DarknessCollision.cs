using UnityEngine;
using System.Collections.Generic;
using System;

public class DarknessCollision : MonoBehaviour {
	public bool checkCollidesPoint(Vector2 point, List<Point> points, CircleCollider2D collider){
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
		float boxSize = 1f;
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x + boxSize, transform.position.y + boxSize));
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x - boxSize, transform.position.y + boxSize));
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x + boxSize, transform.position.y - boxSize));
		boxCheckSquarePoints.Add (new Vector2 (transform.position.x - boxSize, transform.position.y - boxSize));
		for (int j = 0; j < 4; j++) {
			checkCollidesPoint(boxCheckSquarePoints[j], points, collider);
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
		float dx, dy, A, B, C, det;
		
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
	// Update is called once per frame
	void Update () {
		checkDarkCollide ();
	}
}
