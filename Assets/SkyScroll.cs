using UnityEngine;
using System.Collections;

public class SkyScroll : MonoBehaviour {
	float yOrigin = 10, yFinal = -10;
	float percentageRisen = 0;
	private SunBehaviour sun;
	// Use this for initialization
	void Start () {
		sun = GameObject.Find ("Sun").GetComponent<SunBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			percentageRisen = sun.getPercentageRisen ();
			float yPosition = 20f / 100f * percentageRisen;
			transform.position = new Vector3 (transform.position.x, yOrigin - yPosition, 0);
		}
	}
}
