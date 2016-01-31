﻿using UnityEngine;
using System.Collections.Generic;

public class ShamanBehaviour : MonoBehaviour {
	private float lastInspirationRequest = 0f, inspirationTimer;
	private int positionInString = 0;
	private float originalX, originalY, thisx = 0, thisy, ySpeed;
	public List<Sprite> desires = new List<Sprite> ();
	public List<Sprite> energyBars = new List<Sprite> ();
	private string desireString = null;
	public ShamanBehaviour otherShaman;
	public Sprite currentDesire = null;
	private GameObject desireObject;
	private int energy = 5;
	private float energyCooldown = 0;

	// Use this for initialization
	void Start () {
		originalX = transform.position.x;
		originalY = transform.position.y;
		thisy = Random.Range (0.0f, 1.0f);
		ySpeed = Random.Range (2, 5);
		desireObject = transform.Find ("Desire").gameObject;
	}

	public int getCurrentEnergy(){
		return energy;
	}
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			transform.Find ("Energy Bar").GetComponent<SpriteRenderer> ().sprite = energyBars [energy];
			if (energyCooldown <= 0) {
				if (energy == 0) {
					energy = 5;
					desireObject.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
					setDesire (null);
				}
				lastInspirationRequest += Time.deltaTime;
				if (inspirationTimer == 0) {
					inspirationTimer = Random.Range (2, 5);
				} else if (inspirationTimer < lastInspirationRequest) {
					if (currentDesire != null) {
						--energy;
						if (energy <= 0) {
							energy = 0;
							energyCooldown = 10;
							setDesire (desires [4]);
						} else {
							setDesire (null);
						}
						desireString = null;
					} else {	
						requestInspiration ();
					}
					lastInspirationRequest = 0;
					inspirationTimer = Random.Range (1.5f, 3f);
				}
			} else {
				Vector3 rot = desireObject.transform.rotation.eulerAngles;
				rot = new Vector3 (rot.x, rot.y, rot.z - 5);
				desireObject.transform.rotation = Quaternion.Euler (rot);
				energyCooldown -= Time.deltaTime;
			}
			checkInput ();
			wiggle ();
		}
	}

	private void setDesire(Sprite desire){
		currentDesire = desire;
		transform.Find ("Desire").GetComponent<SpriteRenderer> ().sprite = desire;
	}

	private void checkInput(){
		if (desireString != null) {
			foreach (char c in Input.inputString) {
				if (c == desireString [positionInString]) {
					++positionInString;
					if (positionInString >= desireString.Length) {
						setDesire (null);
						positionInString = 0;
						break;
					}
				} else {
					positionInString = 0;
				}
			}
		}
	}

	private void wiggle(){
		float speed = 3.0f; //how fast it shakes
		thisx = Mathf.Sin(Time.time * speed / ySpeed) / 3;
		thisy = Mathf.Cos (Time.time * speed);
		transform.position = new Vector3(originalX + thisx, originalY + thisy / 3, transform.position.z);
	}

	private void requestInspiration(){
		List<Sprite> possibleDesires = new List<Sprite> ();
		possibleDesires.AddRange (desires);
		possibleDesires.RemoveAt (4);
		if (otherShaman.currentDesire != null) {
			possibleDesires.Remove(otherShaman.currentDesire);
		}
		currentDesire = possibleDesires[Random.Range(0, possibleDesires.Count)]; 
		setDesire (currentDesire);
		desireString = currentDesire.name.ToLower();
	}
}
