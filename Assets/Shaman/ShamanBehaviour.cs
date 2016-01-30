using UnityEngine;
using System.Collections.Generic;

public class ShamanBehaviour : MonoBehaviour {
	private float lastInspirationRequest = 0f;
	private int inspirationTimer, positionInString = 0;
	private float originalX, originalY, thisx = 0, thisy, ySpeed;
	public List<Sprite> desires = new List<Sprite> ();
	public List<Sprite> energyBars = new List<Sprite> ();
	private string desireString = null;
	public ShamanBehaviour otherShaman;
	public Sprite currentDesire = null;
	private int energy = 5;
	private float energyCooldown = 0;

	// Use this for initialization
	void Start () {
		originalX = transform.position.x;
		originalY = transform.position.y;
		thisy = Random.Range (0.0f, 1.0f);
		ySpeed = Random.Range (2, 5);
	}

	public int getCurrentEnergy(){
		return energy;
	}
	// Update is called once per frame
	void Update () {
		transform.Find ("Energy Bar").GetComponent<SpriteRenderer> ().sprite = energyBars [energy];
		if (energyCooldown <= 0) {
			if(energy == 0){
				energy = 5;
			}
			lastInspirationRequest += Time.deltaTime;
			if (inspirationTimer == 0) {
				inspirationTimer = Random.Range (7, 14);
			} else if (inspirationTimer < lastInspirationRequest) {
				if (currentDesire != null) {
					--energy;
					if (energy <= 0) {
						energy = 0;
						energyCooldown = 10;
					}
					setDesire (null);
					desireString = null;
				} else {	
					requestInspiration ();
				}
				lastInspirationRequest = 0;
				inspirationTimer = Random.Range(5, 10);
			}

		} else {
			energyCooldown -= Time.deltaTime;
		}
		checkInput ();
		wiggle ();
	}

	private void setDesire(Sprite desire){
		currentDesire = desire;
		transform.Find ("Desire").GetComponent<SpriteRenderer> ().sprite = desire;
	}

	private void checkInput(){
		foreach (char c in Input.inputString) {
			Debug.Log (Input.inputString + "/" + desireString[positionInString]);
			if(c == desireString[positionInString]){
				++positionInString;
				if(positionInString == desireString.Length){
					setDesire(null);
					positionInString = 0;
					break;
				}
			} else {
				positionInString = 0;
			}
		}
	}

	private void wiggle(){
		float speed = 3.0f; //how fast it shakes
		thisx = Mathf.Sin(Time.time * speed / ySpeed) / 3;
		thisy = Mathf.Cos (Time.time * speed);
		transform.position = new Vector3(originalX + thisx, originalY + thisy / 3, transform.position.z);
	}

	private void requestInspiration(){
		List<Sprite> possibleDesires = new List<Sprite> ();
		possibleDesires.AddRange (desires);
		if (otherShaman.currentDesire != null) {
			possibleDesires.Remove(otherShaman.currentDesire);
		}
		currentDesire = possibleDesires[Random.Range(0, possibleDesires.Count)]; 
		setDesire (currentDesire);
		desireString = currentDesire.name.ToLower();
	}
}
