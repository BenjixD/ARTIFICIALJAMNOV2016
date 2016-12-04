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
			GetComponent<playerStats> ().playerTakeDamage ();
			StartCoroutine(sendPlayerUp(targetPosition, cam));
		}
	}

	IEnumerator sendPlayerUp(Vector2 targetPosition, cameraBehaviour cam){
		Vector2 dir = (targetPosition - (Vector2)transform.position).normalized;
		Vector3 refVelocity = Vector3.zero;
		//screen.transform.parent = transform;
		cam.enabled = false;
		//Disable Hitboxes
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<playerController> ().ifOn = false;

		while (transform.position.y < targetPosition.y + 0.5f) {
			screen.transform.position = Vector3.SmoothDamp(screen.transform.position, 
				new Vector3(transform.position.x, transform.position.y, -10), ref refVelocity, 0.3f);
			rb.velocity =  dir * returnMag;
			yield return new WaitForFixedUpdate ();
		}

		for(float f = 90f; f >= 0f; f -= 1f){
			//Enable hitboxes
			if (Mathf.Abs(transform.GetComponent<Rigidbody2D> ().velocity.y) <= 0.25f) {
				GetComponent<playerController> ().ifOn = true;
				GetComponent<BoxCollider2D>().enabled = true;
				GetComponent<CircleCollider2D>().enabled = true;
			}
			screen.transform.position = Vector3.SmoothDamp(screen.transform.position, 
				new Vector3(transform.position.x, transform.position.y, -10), ref refVelocity, 0.3f);
			yield return new WaitForFixedUpdate();	
		};

		//screen.transform.parent = null;
		cam.enabled = true;
		//Just in case
		GetComponent<playerController> ().ifOn = true;
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<CircleCollider2D>().enabled = true;
		yield return null;
	}
}
