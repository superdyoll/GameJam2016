using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour {
	private float yOrigin = -5;
	private float percentageRisen = 0, maxRiseRate = 0.1f, currentRiseRate = 0;
	private Shadow shadowScript;
	public GameObject gameOver, gameWin;
	// Use this for initialization
	void Start () {
		shadowScript = GameObject.Find ("Shadow").GetComponent<Shadow> ();
	}

	public float getPercentageRisen(){
		return percentageRisen;
	}
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Main Camera").GetComponent<SkyColouring> ().ready) {
			currentRiseRate = ((shadowScript.getBlackAverage () * 2) - 100f) / 100f * maxRiseRate;
			if (currentRiseRate > maxRiseRate) {
				currentRiseRate = maxRiseRate;
			} else if (currentRiseRate < -maxRiseRate) {
				currentRiseRate = -maxRiseRate;
			}
			percentageRisen += currentRiseRate;
			if (percentageRisen >= 100) {
				percentageRisen = 100f;
				gameWin.SetActive (true);
				Time.timeScale = 0;
			} else if (percentageRisen <= 0f) {
				percentageRisen = 0f;
				gameOver.SetActive (true);
				Time.timeScale = 0;
			}
			float yPosition = 7f / 100f * percentageRisen;
			transform.position = new Vector3 (transform.position.x, yOrigin + yPosition, 0);
		}
	}
}
