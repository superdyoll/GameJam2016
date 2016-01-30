using UnityEngine;
using System.Collections;

public class ShamanBehaviour : MonoBehaviour {
	private float lastInspirationRequest = 0f;
	private int inspirationTimer;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		lastInspirationRequest += Time.deltaTime;
		if (inspirationTimer == 0) {
			inspirationTimer = Random.Range (10, 20);
		} else if (inspirationTimer < lastInspirationRequest) {
			requestInspiration();
			lastInspirationRequest = 0;
			inspirationTimer = 0;
		}
		wiggle ();
	}

	private void wiggle(){
		float speed = 3.0f; //how fast it shakes
		float thisx = transform.position.x;
		thisx = Mathf.Sin(Time.time * speed / 3);
		float thisy = transform.position.y;
		thisy = Mathf.Cos (Time.time * speed);
		transform.position = new Vector3(thisx, thisy / 3, transform.position.z);
	}

	private void requestInspiration(){
	}
}
