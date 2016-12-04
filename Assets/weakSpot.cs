using UnityEngine;
using System.Collections;

public class weakSpot : MonoBehaviour {

	public int hp;				//This is my own hp
	public Sprite deathSprite;	//this is the death sprite

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Check if the scale is dead
		if(hp <= 0){
			gameObject.GetComponent<SpriteRenderer> ().sprite = deathSprite;
			gameObject.transform.parent.GetComponent<Camera_Shake> ().shakeCamera (1f, 0.1f);
			gameObject.transform.parent.GetComponent<dragon> ().notifyDestroyedPoint (this);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("THIS TAG NEEDS TO BE CHANGED TO WEAPON")) {
			//----Call Invincibility Frame and take damage----//
			hp--;
		}
	}
}
