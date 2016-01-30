using UnityEngine;
using System.Collections;

public class CrowBehaviour : MonoBehaviour {

	bool selected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			if ( Input.GetMouseButtonDown(0))
			{
				hit = RaycastHit;
				Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (Physics.Raycast (ray, hit, 100.0))
				{  
					Destroy(GameObject.Find("targetArea"));
				}
			}
}
}
