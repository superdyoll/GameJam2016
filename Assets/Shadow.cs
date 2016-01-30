<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections.Generic;
using System;
=======
﻿/*using UnityEngine;
using System.Collections;
>>>>>>> 46639bf7d38986e5b8516d0fd914601e6f51e5f8

public class Shadow : MonoBehaviour {

	private double shadowWidth = 40;
	private double shadowHeight = 40;
	private double centre;

	private int numPoints = 100;
	private double delay = 1000/60;
	private double maxVariation;
	private double wiggle = 0.05d;
	private double wiggleLim = 0.1d;

	private int updateCount = 0;
	private int numSegments = 20;
	private double exagDir;
	private double exagAmnt = .005d;
	private int updateLim = 120;
	private double exagRange = (System.Math.PI / 10) / 2;

	private List<Point> points = new List<Point>();

	private int maxWiggles = 30;

	void meshInit() {
		Vector2[] vertices = new Vector2[points.Count];
		for (int i = 0; i < points.Count; i++) {
			Point p = points[i];
			double x = centre - Math.Cos (p.getTheta()) * p.getRad();
			double y = shadowHeight - Math.Sin (p.getTheta()) * p.getRad();
			vertices[i] = new Vector2((float)x,(float) y);
		}

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
		GameObject shadow = new GameObject ("Shadow");
		shadow.AddComponent<MeshFilter> ().mesh = m;
	}

	// Use this for initialization
	void Start () {
		maxVariation = 0.02d / delay;
		centre = shadowWidth / 2;

		double lim = Camera.main.orthographicSize;
		double increment = Math.PI / (numPoints - 1);
		for (int i = 0; i < numPoints; i++) {
			points.Add(new Point(increment * i, lim));
		}

		setExagDir ();
		meshInit ();
	}

	void setExagDir() {
		System.Random rand = new System.Random();
		exagDir = rand.Next(numSegments) * Math.PI / numSegments;
	}

	// Update is called once per frame
	void Update () {
		System.Random rand = new System.Random();
		if (updateCount++ > updateLim) {
			updateCount = rand.Next(updateLim - updateLim / 10);
			setExagDir();
		}
		for (int i = 0; i < points.Count; i++) {
			Point p = points[i];
			double theta = p.getTheta();
			if (theta < exagDir - exagRange || theta > exagDir + exagRange) {
				p.setRad (p.getRad() - p.getRad () * rand.NextDouble() * maxVariation);
			} else {
				double amnt;
				if (theta < exagDir) {
					amnt = (theta - (exagDir - exagRange)) / exagRange;
				} else {
					amnt = ((exagDir + exagRange) - theta) / exagRange;
				}
				p.setRad (p.getRad() - p.getRad () * rand.NextDouble () * exagAmnt * amnt);
			}


			//Wiggles
			double wiggleAmnt = wiggle * rand.NextDouble();
			if (System.Math.Abs(p.getDrawOffset())+p.getRad() > p.getRad() + p.getRad() * wiggleLim) {
				p.setWiggleCount(0);
				if (p.getDrawOffset() > 0) {
					p.setDrawOffset(p.getDrawOffset() - p.getRad () * wiggleAmnt);
				} else {
					p.setDrawOffset(p.getDrawOffset() + p.getRad() * wiggleAmnt);
				}
				p.wiggleOutwards = !p.wiggleOutwards;
			} else {
				if (p.wiggleOutwards) {
					p.drawOffset += p.rad * wiggleAmnt;
				} else {
					p.drawOffset -= p.rad * wiggleAmnt;
				}
			}
			p.wiggleCount++;
			if (p.wiggleCount > maxWiggles) {
				p.wiggleOutwards = !p.wiggleOutwards;
				p.wiggleCount = rand.Next(maxWiggles - 5);
			}
		}

		Mesh m = GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = m.vertices;
		Vector3[] normals = m.normals;
		//Update the mesh
		for (int i = 0; i < points.Count; i++) {
			double x = centre - Math.Cos (p.theta) * p.rad;
			double y = height - Math.Sin (p.theta) * p.rad;
			vertices[i] = new Vector2[] {x, y};
		}
		
		Triangulator t = new Triangulator (vertices);
		int[] indices = t.Triangulate ();
		Vector3[] v3 = new Vector3[vertices.Length];
		for (int i = 0; i < v3.Length; i++) {
			v3[i] = new Vector3(vertices[i].x, vertices[i].y, 0);
		}

		m.vertices = vertices;
		m.triangles = indices;
		m.RecalculateNormals ();
		m.RecalculateBounds ();
	}

	class Point { 
		double theta;
		double rad ;
		double drawRad ;
		int wiggleCount;
		bool wiggleOutwards;
		
		public Point(double theta, double rad) {
			this.theta = theta;
			this.rad = rad;
			this.drawRad = rad;
			System.Random rand = new System.Random ();
			wiggleOutwards = rand.Next (2) == 1;
			wiggleCount = 0;
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
<<<<<<< HEAD

}


=======
}*/
>>>>>>> 46639bf7d38986e5b8516d0fd914601e6f51e5f8