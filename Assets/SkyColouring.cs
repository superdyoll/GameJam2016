using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkyColouring : MonoBehaviour {
	private GameObject introText;
	private float currentTime = 0;
	public bool ready = false;
	// Use this for initialization
	void Start () {
		introText = GameObject.Find ("Panel");
		introText.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime > 5) {
			introText.GetComponent<Image>().CrossFadeAlpha(0f, 2.0f, false);
			introText.transform.Find("Text").GetComponent<Text>().CrossFadeAlpha(0f, 2.0f, false);
			ready = true;
		}
	}
}
