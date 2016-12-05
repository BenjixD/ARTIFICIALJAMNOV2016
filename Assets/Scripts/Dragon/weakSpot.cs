using UnityEngine;
using System.Collections;

public class weakSpot : MonoBehaviour {

	public int hp;				//This is my own hp
	public Sprite deathSprite;	//this is the death sprite
	public GameObject camera;	//this is camera
	bool canTakeDamage = true;	//Blocks spanning of damage

	public int numberOfFlicker = 5; // numberOfFlicker * 0.1 = time invulnerable when it takes damage
	public float timeOnFlicker = 0.05f;
	public float timeOffFlicker = 0.05f;

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
		if (LayerMask.LayerToName(other.gameObject.layer) == "Weapon" && canTakeDamage) {
			//----Call Invincibility Frame and take damage----//
			hp--;
			canTakeDamage = false;
			if(hp > 0)
				StartCoroutine(damageAnim (numberOfFlicker, timeOnFlicker, timeOffFlicker));
		}
	}


	//Coroutine for invicibility
	IEnumerator damageAnim(int nTimes, float timeOn, float timeOff)
	{
		SpriteRenderer mySprite = GetComponent<SpriteRenderer> ();
		//old color
		Color oldColor = mySprite.color;

		while (nTimes > 0)
		{
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
			yield return new WaitForSeconds(timeOn);
			this.gameObject.GetComponent<SpriteRenderer>().color = oldColor;
			yield return new WaitForSeconds(timeOff);
			nTimes--;
		}
		this.gameObject.GetComponent<SpriteRenderer>().color = oldColor;
		canTakeDamage = true;
		yield return null;
	}
}
