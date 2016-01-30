﻿using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour {
	private float yOrigin = -5, maxY = 2;
	private float percentageRisen = 0, maxRiseRate = 0.2f, currentRiseRate = 0;
	private Shadow shadowScript;
	// Use this for initialization
	void Start () {
		shadowScript = GameObject.Find ("Shadow").GetComponent<Shadow> ();
	}

	public float getPercentageRisen(){
		return percentageRisen;
	}
	// Update is called once per frame
	void Update () {
		currentRiseRate = ((shadowScript.getBlackAverage() * 2) - 100f) / 100f * maxRiseRate;
		if (currentRiseRate > maxRiseRate) {
			currentRiseRate = maxRiseRate;
		} else if (currentRiseRate < -maxRiseRate) {
			currentRiseRate = -maxRiseRate;
		}
		percentageRisen += currentRiseRate;
		if (percentageRisen >= 100) {
			percentageRisen = 100f;
		} else if (percentageRisen <= 0f) {
			percentageRisen = 0f;
		}
		float yPosition = 7f / 100f * percentageRisen;
		transform.position = new Vector3 (transform.position.x, yOrigin + yPosition, 0);
	}
}
