using UnityEngine;
using System.Collections;

public class dragon : MonoBehaviour {

	public int hp;					//Hp of the dragon
	private weakSpot[] children;	//List of WeakSpots
	private bool dying = false;		//This is just so that we dont call death twice

	// Use this for initialization
	void Start () {
		children = GetComponentsInChildren<weakSpot>();
		hp = children.Length;
		Debug.Log (children.Length);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hp == 0 && !dying) {
			dying = true;
			dragonDeath();
		}

		if (Input.GetKeyDown ("q") && !dying) {
			dying = true;
			dragonDeath();
		}
	}

	public void notifyDestroyedPoint(weakSpot ws){
		for (int i = 0; i < children.Length; i++) {
			if (children [i] == ws) {
				Destroy (ws);
				hp--;
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
