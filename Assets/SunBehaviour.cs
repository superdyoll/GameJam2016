using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour {
	private float yOrigin = -5, maxY = 2;
	private float percentageRisen = 0;
	// Use this for initialization
	void Start () {
	
	}

	public float getPercentageRisen(){
		return percentageRisen;
	}
	// Update is called once per frame
	void Update () {
		percentageRisen += 0.2f;
		if (percentageRisen >= 100) {
			percentageRisen = 100f;
		} else if (percentageRisen <= 0f) {
			percentageRisen = 0f;
		}
		float yPosition = 7f / 100f * percentageRisen;
		transform.position = new Vector3 (transform.position.x, yOrigin + yPosition, 0);
	}
}
