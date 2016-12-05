using UnityEngine;
using System.Collections;

public class bobble : MonoBehaviour {

	private Vector3 upperBound;
	private Vector3 lowerBound;

	private bool moveUp = false;

	// Use this for initialization
	void Start () {
		upperBound = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, transform.localPosition.z);
		lowerBound = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 reff = Vector3.zero;
		if (moveUp) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, upperBound, 0.002f);
		} else {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, lowerBound, 0.002f);
		}

		if (transform.localPosition == upperBound)
			moveUp = false;
		else if (transform.localPosition == lowerBound)
			moveUp = true;
	}
}
