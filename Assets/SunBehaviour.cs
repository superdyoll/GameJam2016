using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour {

	public AudioSource audioSource;

	public AudioClip audioPlay;
	public AudioClip audioLose;

	public GameObject gameOver, gameWin;
	public int level;

	private float yOrigin = -5, maxY = 2;
	private float percentageRisen = 0, maxRiseRate, currentRiseRate = 0;
	private Shadow shadowScript;

	// Use this for initialization
	void Start () {
		audioSource.clip = audioPlay;
		audioSource.Play ();
		shadowScript = GameObject.Find ("Shadow").GetComponent<Shadow> ();
	}

	// Allows other classes to see how much sun has risen
	public float getPercentageRisen(){
		return percentageRisen;
	}

	// Update is called once per frame
	void Update () {
		setMaxRiseRate ();
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
				if (audioSource.clip != audioLose){
					audioSource.clip = audioLose;
					audioSource.Play ();
				}
				percentageRisen = 0f;
				gameOver.SetActive (true);
				Time.timeScale = 0;
			}
			float yPosition = 7f / 100f * percentageRisen;
			transform.position = new Vector3 (transform.position.x, yOrigin + yPosition, 0);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void setMaxRiseRate (){
		switch (level) {
		case 0:
			maxRiseRate = 0.2f;
			break;
		case 1: 
			maxRiseRate = 0.15f;
			break;
		case 2:
			maxRiseRate = 0.1f;
			break;
		default:
			maxRiseRate = 0.2f;
			break;
		}
	}
}
