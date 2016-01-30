using UnityEngine;
using System.Collections;

public class CrowBehaviour : MonoBehaviour {

	private bool selected;
	public Animator animation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.name == this.name){
					animation.speed = 0;
				}
			}else{
				if (hit.transform.name == this.name){
					animation.speed = 0;
				}else{
					animation.speed = 1;
				}
			}
		}
	}
}
