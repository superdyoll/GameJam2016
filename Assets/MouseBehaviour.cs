using UnityEngine;
using System.Collections;

public class MouseBehaviour : MonoBehaviour {
	private Vector3 mousePosition;
	private Vector2 target = new Vector2(0, 0);
	private float radius, x, y;
	private CrowBehaviour crowSelected;
	// Use this for initialization
	void Start () {
	
	}

	void deselectCrow(){
		if(crowSelected != null){
			crowSelected.crowAnimation.speed = 1;
			crowSelected.setSelected(false);
			crowSelected = null;
		}
	}

	void CastSelectRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit.collider != null && hit.collider.gameObject.tag == "Crow") {
			deselectCrow();
			crowSelected = hit.collider.gameObject.GetComponent<CrowBehaviour>();
			crowSelected.crowAnimation.speed = 0;
			crowSelected.setSelected(true);
		} else {
			deselectCrow();
		}
	}

	void CastMoveRay(){
		crowSelected.target = transform.position;
		crowSelected.crowAnimation.speed = 1;
		crowSelected.moving = true;
		//Debug.Log ("Crow selected now moving to " + target);
	}


	private bool isCloseToTarget(Vector2 current, Vector2 aim){
		if (Mathf.Abs (aim.x - current.x) < 0.2f) {
			if(Mathf.Abs(aim.y - current.y) < 0.2f){
				return true;
			}
		}
		return false;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			CastSelectRay ();
		}
		
		if (Input.GetMouseButtonDown (1) && crowSelected != null) {
			CastMoveRay ();
		}
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		if (crowSelected != null) {
			radius = (6 - crowSelected.shaman.getCurrentEnergy ()) / 2;
			if (target == new Vector2(0, 0)) {
				float angle = Random.Range (0.001f, 1.000f) * Mathf.PI * 2;
				x = Mathf.Cos (angle) * radius;
				y = Mathf.Sin (angle) * radius;
				target = new Vector3 (mousePosition.x + x, mousePosition.y + y, 0);
			} else {
				target = new Vector3 (mousePosition.x + x, mousePosition.y + y, 0);
				transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime * 7f);
				if(isCloseToTarget((Vector2)transform.position, target)) {
					target = new Vector2(0, 0);
				}
			}
		} else {
			transform.position = mousePosition;
		}
	}
}
