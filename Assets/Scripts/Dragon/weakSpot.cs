using UnityEngine;
using System.Collections;

public class weakSpot : MonoBehaviour {

	public int hp;				//This is my own hp
	public Sprite deathSprite;	//this is the death sprite
	public GameObject camera;	//this is camera

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Check if the scale is dead
		if(hp <= 0){
			gameObject.GetComponent<blinkPiece> ().ifBlink = false;
			gameObject.GetComponent<SpriteRenderer> ().sprite = deathSprite;
			camera.GetComponent<Camera_Shake> ().shakeCamera (1f, 0.3f);
			gameObject.transform.parent.GetComponent<dragon> ().notifyDestroyedPoint (this);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (LayerMask.LayerToName(other.gameObject.layer) == "Weapon") {
			//----Call Invincibility Frame and take damage----//
			hp--;
		}
	}
}
