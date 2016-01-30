using UnityEngine;
using System.Collections;

public class ShamanBehaviour : MonoBehaviour {
	private float lastInspirationRequest = 0f;
	private int inspirationTimer;
	private float originalX, originalY, thisx = 0, thisy, ySpeed;

	// Use this for initialization
	void Start () {
		originalX = transform.position.x;
		originalY = transform.position.y;
		thisy = Random.Range (0.0f, 1.0f);
		ySpeed = Random.Range (2, 5);
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
		thisx = Mathf.Sin(Time.time * speed / ySpeed) / 3;
		thisy = Mathf.Cos (Time.time * speed);
		transform.position = new Vector3(originalX + thisx, originalY + thisy / 3, transform.position.z);
	}

	private void requestInspiration(){
	}
}
