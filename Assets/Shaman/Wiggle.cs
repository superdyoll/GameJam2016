using UnityEngine;
using System.Collections;

public class Wiggle : MonoBehaviour {
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			float speed = 6.0f; //how fast it shakes
			Vector3 rotation = transform.rotation.eulerAngles;
			float yRot = rotation.y;
			yRot = Mathf.Cos (Time.time * speed);
			transform.rotation = Quaternion.Euler (transform.rotation.x + yRot, transform.rotation.y, transform.rotation.z + yRot * 10);
		}
	}
}
