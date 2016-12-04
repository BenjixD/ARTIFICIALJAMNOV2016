using UnityEngine;
using System.Collections;

public class meteor : MonoBehaviour {

	private Animator anim;
	private bool touched = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(destroyObjectIfNotAlready());
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		anim = GetComponent<Animator> ();
		if (!touched) {
			touched = true;
			Destroy (gameObject.GetComponent<CircleCollider2D>());
			anim.SetTrigger ("destroy");
			Destroy (gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
			//Set Velocity to be 0
			GetComponent<Rigidbody2D>().velocity = Vector2.zero	;
			//Destroy all children
			foreach(Transform child in transform){
				GameObject.Destroy (child.gameObject);
			}
			this.enabled = false;
		}
	}

	IEnumerator destroyObjectIfNotAlready(){
		yield return new WaitForSeconds (60f);
		Destroy (gameObject);
	}
}
