using UnityEngine;
using System.Collections;

public class playerFalling : MonoBehaviour {

	public GameObject screen;	 //camera
	public GameObject reference; //Where should I bounce to
	public Vector2 offset;		 //how far above you want it
	public float returnMag;		 //magnitude of return

	private Rigidbody2D rb;
	private cameraBehaviour cam;


	void Start(){
		rb = GetComponent<Rigidbody2D> ();
		cam = screen.GetComponent<cameraBehaviour> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (LayerMask.LayerToName (other.gameObject.layer) == "BounceZone") {
			Vector2 targetPosition = (Vector2)reference.transform.position + offset;
			StartCoroutine(sendPlayerUp(targetPosition, cam));
		}
	}

	IEnumerator sendPlayerUp(Vector2 targetPosition, cameraBehaviour cam){
		Vector2 dir = (targetPosition - (Vector2)transform.position).normalized;
		Vector3 refVelocity = Vector3.zero;
		screen.transform.parent = transform;
		cam.enabled = false;

		while (transform.position.y < targetPosition.y) {
			screen.transform.localPosition = Vector3.SmoothDamp(screen.transform.localPosition, 
				new Vector3(0, 0, -10), ref refVelocity, 0.1f);
			rb.velocity =  dir * returnMag;
			yield return new WaitForFixedUpdate ();
		}

		yield return new WaitForSeconds (1.5f);
		screen.transform.parent = null;
		cam.enabled = true;

		yield return null;
	}
}
