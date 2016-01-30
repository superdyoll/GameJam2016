using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	private double width = 30;
	private double height = 20;
	private double centre = width / 2;
	private int numPoints = 100;
	private int delay = 1000/60;
	private double maxVariation = 0.02d / delay;
	private double wiggle = 0.05d;
	private double wiggleLimt = 0.1d;

	private int updateCount = 0;
	private int numSegments = 20;
	private double exagDir;
	private double exagAmnt = .005d;
	private int updatelim = 120;
	private double exagRange = (Math.PI / 10) / 2;

	private List<Point> points = new List<Point>();


	// Use this for initialization
	void Start () {
		double lim = Camera.main.orthographicSize;
		double increment = Math.PI / (numPoints - 1);
		for (int i = 0; i < numPoints; i++) {
			points.add(new Point(increment * i, lim));
		}

		setExagDir ();
	}

	void setExagDir() {

	}

	// Update is called once per frame
	void Update () {
		
	}
}


class Point {
	private double theta;
	private double rad;
	private double drawRad;

	public Point(double theta, double rad) {
		this.theta = theta;
		this.rad = rad;
		this.drawRad = rad;
	}
}