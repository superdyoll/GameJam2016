using UnityEngine;
using System.Collections.Generic;

public class SkyColouring : MonoBehaviour {
	private Color purple = new Color (0.282f, 0.239f, 0.545f);
	private Color crimson = new Color(0.698f, 0.133f, 0.133f);
	private Color orange = new Color(1f, 0.271f, 0f);
	private Color yellow = new Color(1f, 0.843f, 0f);
	private Color lightYellow = new Color(1f, 0.922f, 0.804f);
	private Color currentColor;
	private Color colorA, colorB;
	private Camera mainCamera;
	private float percentage = 0, lerpVal = 0;

	// Use this for initialization
	void Start () {
		mainCamera = gameObject.GetComponent<Camera> ();
		mainCamera.clearFlags = CameraClearFlags.Color;
		currentColor = purple;
	}
	
	// Update is called once per frame
	void Update () {
		if (percentage >= 0 && percentage < 20) {
			if(colorB != crimson){
				colorB = crimson;
				lerpVal = 0f;
			}
		}else if (percentage >= 20 && percentage < 40) {
			if(colorB != orange){
				colorB = orange;
				lerpVal = 0f;
			}
		}else if (percentage >= 40 && percentage < 60) {
			if(colorB != yellow){
				colorB = yellow;
				lerpVal = 0f;
			}
		}else if (percentage >= 60 && percentage < 80) {
			if(colorB != lightYellow){
				colorB = lightYellow;
				lerpVal = 0f;
			};
		}else if (percentage >= 80 && percentage <= 100) {
			if(colorB != lightYellow){
				colorB = lightYellow;
				lerpVal = 0f;
			}
		}
		currentColor = Color.Lerp (currentColor, colorB, lerpVal / 4f);
		mainCamera.backgroundColor = currentColor;
		percentage += 0.2f;
		lerpVal += 0.003f;
		if (lerpVal >= 0.4f) {
			lerpVal = 0f;
		}
		if (percentage > 100) {
			percentage = 100;
		} else if (percentage < 0) {
			percentage = 0;
		}
	}
}
