using UnityEngine;
using System.Collections.Generic;

public class SkyColouring : MonoBehaviour {
	private Color purple = new Color (72, 61, 139);
	private Color crimson = new Color(178, 34, 34);
	private Color orange = new Color(255, 69, 0);
	private Color yellow = new Color(255, 215, 0);
	private Color lightYellow = new Color(255, 235, 205);
	private Color currentColor;
	private Color colorA, colorB;
	private Camera mainCamera;
	private float percentage = 0, lerpVal = 0;

	// Use this for initialization
	void Start () {
		mainCamera = gameObject.GetComponent<Camera> ();
		currentColor = purple;
	}
	
	// Update is called once per frame
	void Update () {
		if (percentage >= 0 && percentage < 20) {
			colorB = crimson;
		}else if (percentage >= 20 && percentage < 40) {
			colorB = orange;
		}else if (percentage >= 40 && percentage < 60) {
			colorB = yellow;
		}else if (percentage >= 60 && percentage < 80) {
			colorB = lightYellow;
		}else if (percentage >= 80 && percentage <= 100) {
			colorB = lightYellow;
		}
		Debug.Log (currentColor + " ~ " + colorB + " ///// " + lerpVal);
		currentColor = Color.Lerp (currentColor, colorB, lerpVal);
		mainCamera.backgroundColor = currentColor;
		percentage += 0.2f;
		lerpVal += 0.01f;
		if (lerpVal >= 1f) {
			lerpVal = 0f;
		}
		if (percentage > 100) {
			percentage = 100;
		} else if (percentage < 0) {
			percentage = 0;
		}
	}
}
