using UnityEngine;
using System.Collections;

public class dragon : MonoBehaviour {

	public int hp;					//Hp of the dragon
	private weakSpot[] children;	//List of WeakSpots
	private bool dying = false;		//This is just so that we dont call death twice
	public GameObject sound;		//Soundcontroller

	// Use this for initialization
	void Start () {
		children = GetComponentsInChildren<weakSpot>();
		hp = children.Length;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hp == 0 && !dying) {
			dying = true;
			dragonDeath();
			sound.GetComponent<soundEffectScript> ().playDragonDeath();
		}

		if (Input.GetKeyDown ("q") && !dying) {
			dying = true;
			dragonDeath();
			sound.GetComponent<soundEffectScript> ().playDragonDeath();
		}
	}

	public void notifyDestroyedPoint(weakSpot ws){
		for (int i = 0; i < children.Length; i++) {
			if (children [i] == ws) {
				Destroy (ws);
				hp--;
				sound.GetComponent<soundEffectScript> ().playDragonRoar();
				break;
			}
		}
	}

	//DESTRUCTION ZONE!!!!
	public void dragonDeath(){
		//Destroy after 8 seconds
		Destroy (gameObject, 8);
		Transform[] go = GetComponentsInChildren<Transform>();
		for (int i = 1; i < go.Length; i++) {
			go [i].GetComponent<dragonPath> ().killSection ();
		}
	}
}
