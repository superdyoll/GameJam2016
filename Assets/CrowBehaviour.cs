using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CrowBehaviour : MonoBehaviour {


	public Animator crowAnimation;
	public ShamanBehaviour shaman;
	public bool moving = false;
	public Vector2 target;
	public float maxSpeed;
	public System.Random rand;
	public GameObject selected;
	public AudioSource soundEffects;
	public AudioClip crowLevel5,crowLevel3,crowLevel1,crowLevel0;


	// Use this for initialization
	void Start () {
		maxSpeed = 5 - Application.loadedLevel;
		rand = new System.Random ();
		selected.SetActive (true);
	}

	public void setSelected(bool b){
		moving = !b;
		selected.SetActive (b);
		switch (shaman.getCurrentEnergy ()) {
		case 5:
			soundEffects.clip = crowLevel5;
			break;
		case 4:
		case 3:
			soundEffects.clip = crowLevel3;
			break;
		case 2:
		case 1:
			soundEffects.clip = crowLevel1;
			break;
		case 0:
			soundEffects.clip = crowLevel0;
			break;
		default:
			soundEffects.clip = crowLevel3;
			break;
		}
		soundEffects.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			// Select bird on Left Click
			moveBird();

			adjustDirection();

			if(selected.activeInHierarchy){
				Vector3 currentRot = selected.transform.rotation.eulerAngles;
				selected.transform.rotation = Quaternion.Euler(new Vector3(currentRot.x, currentRot.y, currentRot.z + 7));
			}

			Shadow shadow = GameObject.Find ("Shadow").GetComponent<Shadow> ();
			List<Point> points = shadow.getPoints();
			if (gameObject.GetComponent<DarknessCollision>().checkCollidesPoint((Vector2)transform.position, points, GetComponent<CircleCollider2D> ()) || !shadow.isInScreen((Vector2)transform.position)) {
				target = getRandomPoint(new Vector2(0, -3), 2);
			}
		}
	}

	void adjustDirection(){
		//Get direction that bird is moving
		float move = transform.position.x - target.x;
		
		//Face the bird right
		if (move < 0) {
			transform.localRotation = Quaternion.Euler (0, 180, 0);
		}
		
		//Face the bird left
		if (move > 0) {
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
		if (isCloseToTarget()) {
			target = getRandomPoint ((Vector2)transform.position, obedienceToDistance ());
		} else if (moving) {
			if (isCloseToTarget()){
				target = getRandomPoint ((Vector2)transform.position, obedienceToDistance ());
			} else {
				float step = maxSpeed * Time.deltaTime;
				transform.position = Vector2.MoveTowards (transform.position, target, step);
			}
		}
	}

	int obedienceToDistance(){
		return 20 - shaman.getCurrentEnergy() * 4;
	}
	
	public Vector2 getRandomPoint(Vector2 center,int furthestDistance){
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