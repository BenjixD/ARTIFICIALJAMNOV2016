using UnityEngine;
using System.Collections;

public class meteor : MonoBehaviour {

    meteorSoundScript fx;

	private Animator anim;
	private bool touched = false;
	// Use this for initialization
	void Awake(){
		fx = this.gameObject.GetComponent<meteorSoundScript>();
	}

	void Start () {
		StartCoroutine(destroyObjectIfNotAlready());
		StartCoroutine (delayEnableTrigger ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
        anim = GetComponent<Animator> ();
		if (!touched) {
			touched = true;
			transform.rotation = Quaternion.Euler (0,0,0);
            //StartCoroutine(playExplosion());
            fx.playMeteorExp();
            Destroy (gameObject.GetComponent<CircleCollider2D>());
			anim.SetTrigger ("destroy");
			Destroy (gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
			//Set Velocity to be 0
			GetComponent<Rigidbody2D>().velocity = Vector2.zero	;
			//Destroy all children
			foreach(Transform child in transform){
                if (!(child.gameObject.name == "Meteor AudioSource"))
				GameObject.Destroy (child.gameObject);
			}
            this.enabled = false;
		}
	}

	IEnumerator destroyObjectIfNotAlready(){
		yield return new WaitForSeconds (60f);
		Destroy (gameObject);
	}

    IEnumerator playExplosion()
    {
        fx.playMeteorExp();
        yield return null;
    }

	IEnumerator delayEnableTrigger(){
		//Freeze Velocity
		Vector2 oldVelocity = GetComponent<Rigidbody2D> ().velocity; 
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		GetComponent<CircleCollider2D> ().enabled = false;

		yield return new WaitForSeconds (1f);

		GetComponent<Rigidbody2D> ().velocity = oldVelocity;
		GetComponent<CircleCollider2D> ().enabled = true;
	}
}
