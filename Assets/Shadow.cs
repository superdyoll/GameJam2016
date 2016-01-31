using UnityEngine;
using System.Collections.Generic;
using System;

public class Shadow : MonoBehaviour {

	public Material m;
	private double size;
	private double centre;

	private int numPoints = 50;
	private double delay = 1000/60;
	public double maxVariation;
	private double wiggle = 0.002d;
	private double wiggleLim = 0.05d;

	private int updateCount = 0;
	private double exagDir;
	private double exagAmnt = .01d;
	private int updateLim = 120;
	private double exagRange = (System.Math.PI / 10) / 2;

	private List<Point> points = new List<Point>();

	private int maxWiggles = 30;

	public void meshInit() {
		Vector2[] vertices = new Vector2[points.Count + 4];
		int j = 0;
		for (; j < points.Count; j++) {
			Point p = points[j];
			double x = centre - Math.Cos (p.getTheta()) * p.getRad();
			double y = -Camera.main.orthographicSize - Camera.main.orthographicSize * 0.05d + (Math.Sin (p.getTheta()) * p.getRad());
			vertices[j] = new Vector2((float)x,(float) y);
		}
		vertices [j++] = new Vector2 ((float)+ size,(float) -size);
		vertices [j++] = new Vector2 ((float)size, (float)size);
		vertices [j++] = new Vector2 ((float)- size, (float)size);
		vertices [j++] = new Vector2 ((float)- size, (float)- size);

		Triangulator t = new Triangulator (vertices);
		int[] indices = t.Triangulate ();
		Vector3[] v3 = new Vector3[vertices.Length];
		for (int i = 0; i < v3.Length; i++) {
			v3[i] = new Vector3(vertices[i].x, vertices[i].y, 0);
		}


		Mesh m = new Mesh ();
		m.vertices = v3;
		m.triangles = indices;
		m.RecalculateNormals ();
		m.RecalculateBounds ();

		
		//Add to stuff
		this.GetComponent<MeshFilter> ().mesh = m;
	}

	public List<Point> getPoints() {
		return points;
	}

	void pointsInit() {
		double lim = System.Math.Max (size, size) / 2;
		double increment = Math.PI / (numPoints - 1);
		for (int i = 0; i < numPoints; i++) {
			points.Add (new Point (increment * i, lim));
		}
	}

	public bool isInScreen(Vector2 v) {
		float height = Camera.main.orthographicSize;
		float width = height * Camera.main.aspect;
		float x = v.x;
		float y = v.y;
		if (x > -width && x < width) {
			if (y > - height && y < height) {
				return true;
			}
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		maxVariation = 0.03d / delay;
		maxVariation = maxVariation + Application.loadedLevel / 30;
		GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
		GetComponent<Renderer> ().material = m;
		GetComponent<Renderer> ().material.color = new Color (0.1f, 0.1f, 0.1f, 1f);

		GetComponent<Renderer> ().sortingLayerID = GetComponent<Transform>().parent.GetComponent<Renderer>().sortingLayerID;

		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		size = Math.Max (width, height);

		centre = 0;

		pointsInit ();
		setExagDir ();
		meshInit ();
	}

	void setExagDir() {
		System.Random rand = new System.Random();
		exagDir = rand.NextDouble () * Math.PI;
	}

	public float getBlackAverage(){
		float totalRadius = 0;
		for(int i = 0; i < points.Count; ++i){
			totalRadius += (float)points[i].getRad();
		}
		totalRadius = totalRadius / points.Count;
		float maxRad = (float)size / 1.35f;
		return 100f / maxRad * totalRadius;
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			System.Random rand = new System.Random ();
			if (updateCount++ > updateLim) {
				updateCount = rand.Next (updateLim - updateLim / 10);
				setExagDir ();
			}
			for (int i = 0; i < points.Count; i++) {
				Point p = points [i];
				double theta = p.getTheta ();
				if (theta < exagDir - exagRange || theta > exagDir + exagRange) {
					p.setRad (p.getRad () - p.getRad () * rand.NextDouble () * maxVariation);
				} else {
					double amnt;
					if (theta < exagDir) {
						amnt = (theta - (exagDir - exagRange)) / exagRange;
					} else {
						amnt = ((exagDir + exagRange) - theta) / exagRange;
					}
					p.setRad (p.getRad () - p.getRad () * rand.NextDouble () * exagAmnt * amnt);
				}


				//Wiggles
				double wiggleAmnt = wiggle * rand.NextDouble ();
				if (System.Math.Abs (p.getDrawOffset ()) + p.getRad () > p.getRad () + p.getRad () * wiggleLim) {
					p.setWiggleCount (0);
					if (p.getDrawOffset () > 0) {
						p.setDrawOffset (p.getDrawOffset () - p.getRad () * wiggleAmnt);
					} else {
						p.setDrawOffset (p.getDrawOffset () + p.getRad () * wiggleAmnt);
					}
					p.setWiggleOutwards (!p.isWiggleOutwards ());
				} else {
					if (p.isWiggleOutwards ()) {
						p.setDrawOffset (p.getDrawOffset () + p.getRad () * wiggleAmnt);
					} else {
						p.setDrawOffset (p.getDrawOffset () - p.getRad () * wiggleAmnt);
					}
				}
				p.setWiggleCount (p.getWiggleCount () + 1);
				if (p.getWiggleCount () > maxWiggles) {
					p.setWiggleOutwards (!p.isWiggleOutwards ());
					p.setWiggleCount (rand.Next (maxWiggles - 5));
				}
			}

			Mesh m = GetComponent<MeshFilter> ().mesh;
			Vector3[] v3 = m.vertices;
			//Update the mesh

			Vector2[] vertices = new Vector2[v3.Length];
			int j = 0;
			for (; j < points.Count; j++) {
				Point p = points [j];
				double x = centre - Math.Cos (p.getTheta ()) * (p.getRad () + p.getDrawOffset ());
				double y = -Camera.main.orthographicSize - Camera.main.orthographicSize * 0.05d + (Math.Sin (p.getTheta ()) * (p.getRad () + p.getDrawOffset ()));
				vertices [j] = new Vector2 ((float)x, (float)y);
			}
			vertices [j++] = new Vector2 ((float)+ size, (float)-size);
			vertices [j++] = new Vector2 ((float)size, (float)size);
			vertices [j++] = new Vector2 ((float)- size, (float)size);
			vertices [j++] = new Vector2 ((float)- size, (float)- size);
		
			Triangulator t = new Triangulator (vertices);
			int[] indices = t.Triangulate ();
			for (int i = 0; i < v3.Length; i++) {
				v3 [i] = new Vector3 (vertices [i].x, vertices [i].y, 0);
			}

			m.vertices = v3;
			m.triangles = indices;
			m.RecalculateNormals ();
			m.RecalculateBounds ();
		}
	}

	public void pushback(){
		for (int i = 0; i < points.Count; i++) {
			double newRad = points[i].getRad () + size * 0.025d;
			if (newRad >= size) {
				newRad = size;
			}
			points[i].setRad(newRad);
		}
	}
}

public class Point { 
	double theta;
	double rad ;
	double drawOffset ;
	int wiggleCount;
	bool wiggleOutwards;
	
	public Point(double theta, double rad) {
		this.theta = theta;
		this.rad = rad;
		this.drawOffset = 0;
		System.Random rand = new System.Random ();
		wiggleOutwards = rand.Next (2) == 1;
		wiggleCount = 0;
	}

	public double getX() {
		return -Math.Cos (getTheta()) * (getRad () + getDrawOffset());
	}

	public double getY() {
		return -Camera.main.orthographicSize + (Math.Sin (getTheta()) * (getRad() + getDrawOffset()));
	}

	public double getTheta() {
		return theta;
	}
	
	public void setTheta(double theta) {
		this.theta = theta;
	}
	
	public double getRad() {
		return rad;
	}
	
	public void setRad(double rad) {
		this.rad = rad;
	}
	
	public bool isWiggleOutwards() {
		return wiggleOutwards;
	}
	
	public void setWiggleOutwards(bool wiggleOutwards) {
		this.wiggleOutwards = wiggleOutwards;
	}
	
	public int getWiggleCount() {
		return wiggleCount;
	}
	
	public void setWiggleCount(int wiggleCount) {
		this.wiggleCount = wiggleCount;
	}
	
	public double getDrawOffset() {
		return drawOffset;
	}
	
	public void setDrawOffset(double drawOffset) {
		this.drawOffset = drawOffset;
	}
	
}
