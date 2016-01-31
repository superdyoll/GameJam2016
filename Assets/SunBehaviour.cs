using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour {

	public AudioSource audioSource;

	public AudioClip audioPlay;
	public AudioClip audioLose;

	public GameObject gameOver, gameWin;
	public int level;

	private float yOrigin = -5, gameWinTimer = -1;
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
		if(gameWinTimer > 6f){
			if(Application.loadedLevel == 2){
				Application.LoadLevel(0);
			} else {
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		} else if (gameWinTimer > -1) {
			gameWinTimer += 0.03f;
		}
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
				if(gameWinTimer < 0){
					gameWinTimer = 0;
				}
			} else if (percentageRisen <= 0f && !gameWin.activeInHierarchy) {
				if (audioSource.clip != audioLose){
					audioSource.clip = audioLose;
					audioSource.Play ();
				}
				percentageRisen = 0f;
				gameOver.SetActive (true);
				gameWinTimer = 0;
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
		maxRiseRate = 0.2f;
	}
}
