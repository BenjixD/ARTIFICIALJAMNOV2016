using UnityEngine;
using System.Collections;

public class blinkBody : MonoBehaviour {

	public bool blinkOn = true;

	private int frames = 10;
	private int counter = 0;
	private Transform[] children; 
	// Use this for initialization
	void Start () {
		children = GetComponentsInChildren<Transform>();	//note children[0] is ourself (which we dont want)
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(counter == 0)
			StartCoroutine(blink(frames, children));
		counter = (counter + 1) % (frames*frames);
	}

	IEnumerator blink(int frames, Transform[] children){
		for (int i = 1; i < children.Length; i++) {
			if(children [i].GetComponent<blinkPiece> ().ifBlink)
				children [i].GetComponent<SpriteRenderer>().sprite = children [i].GetComponent<blinkPiece> ().blinkOn;
			for(int j = 0; j < frames; j++)
				yield return new WaitForFixedUpdate ();
			if(children [i].GetComponent<blinkPiece> ().ifBlink)
				children [i].GetComponent<SpriteRenderer>().sprite = children [i].GetComponent<blinkPiece> ().blinkOff;
			for(int j = 0; j < frames; j++)
				yield return new WaitForFixedUpdate (); 
		}
		yield return null;
	}
}
