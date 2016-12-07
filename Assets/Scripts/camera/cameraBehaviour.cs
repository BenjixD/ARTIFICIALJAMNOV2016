using UnityEngine;
using System.Collections;

public class cameraBehaviour : MonoBehaviour {

	public GameObject refObj; 
	public GameObject velocityRef;	//Referenced velocity of the object
	public float colliderMargin = 2f; //This is collider offset from camera view
	public float cameraSpeedModifier = 2f; //Camera Speed
	public float boundaries = 1f; //boundaries
	public float viewMargin = 1f; //See see more below the character

	private Camera camera;
	private Vector2 cameraPos;
	private Vector2 cameraSize;

	private BoxCollider2D limitColliderTop;	//Outer top collider (set to camera view size)
	private BoxCollider2D limitColliderRight;	//Outer right collider (set to camera view size)
	private BoxCollider2D limitColliderBottom;	//Outer Bottom collider (set to camera view size)
	private BoxCollider2D limitColliderLeft;	//Outer Left collider (set to camera view size)

	//private GameObject margin;	//Margin for camera moving
	private Vector2 lastNotableChange;	//Last notable change

	private bool moveCamera = true;
	// Use this for initialization
	void Start () {
		//Get camera size and position
		camera = GetComponent<Camera>();
		cameraPos = transform.position;
		cameraSize.x = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, 0))) - boundaries;
		cameraSize.y = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight))) - boundaries;

		//Set the position to move to
		lastNotableChange = refObj.transform.position;

		//Start my coroutines
		StartCoroutine(disableScript());
		//StartCoroutine(moveY());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*
		if (moveCamera) {
			if (Mathf.Abs (lastNotableChange.x - refObj.transform.position.x) > 0.5 ||
			   Mathf.Abs (lastNotableChange.y - refObj.transform.position.y) > 0.5) {
				lastNotableChange = refObj.transform.position;
			}
			Vector3 refVelocity = Vector3.zero;
			transform.position = Vector3.SmoothDamp(transform.position, 
				new Vector3(lastNotableChange.x, lastNotableChange.y - viewMargin, transform.position.z),ref refVelocity, cameraSpeedModifier);
		}*/

		if (moveCamera) {
			if (Vector2.Distance (lastNotableChange, refObj.transform.position) > 0.1) {
				lastNotableChange = refObj.transform.position;
			}

			if (Vector2.Distance (lastNotableChange, transform.position) > 0.1) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, (lastNotableChange - (Vector2)transform.position).normalized * cameraSpeedModifier, 0.1f);
			} 
			else {
				//Damp the velocity to be 0
				GetComponent<Rigidbody2D> ().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, 0.1f);
				//GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			}
		}
	}
		
	IEnumerator moveY(){  
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		while (true) {
			if (refObj.transform.position.y - transform.position.y > 0.1f) {
				rb.velocity = new Vector2 (rb.velocity.x, Mathf.Lerp (rb.velocity.y, cameraSpeedModifier, 1f));
			} else if (refObj.transform.position.y - transform.position.y < -0.1f) {
				rb.velocity = new Vector2 (rb.velocity.x, Mathf.Lerp (rb.velocity.y, -1 * cameraSpeedModifier, 1f));
			} else {
				rb.velocity = new Vector2 (rb.velocity.x, Mathf.Lerp (rb.velocity.y, 0, 1f));
			}

			yield return new WaitForFixedUpdate ();
		}

	}
		
	/*
	IEnumerator moveX(){
		
	}*/

	IEnumerator disableScript(){
		while (refObj != null) {
			yield return new WaitForFixedUpdate ();
		}

		moveCamera = false;
		this.enabled = false;
		yield return null;
	}
}
