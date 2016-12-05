using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testDragon : dragonPath {
	public float rotSpeed;					//Head rotational speed
	public float rotAngle;					//rotation angle
	public int numMoves;					//Number of different moves in order
	public path myPath;						//The path object

	private Rigidbody2D rb;					//my Rigidbody

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		prevMoves = new List<move>();
		IList<path_entry> listOfMoves = new List<path_entry>();	//List of moves
		float[] durations = {180, 90, 180, 90, 180, 90, 180, 90};	//Frames
		float[] xMoves = {2f, 0f, 2f, 0f, -2f, 0f, -2f, 0f};
		float[] yMoves = {0f, 2f, 0f, -2f, 0f, 2f, 0f, -2f};

		for (int i = 0; i < numMoves; i++) {
			path_entry entry = new path_entry (durations [i], new Vector2 (xMoves [i], yMoves [i]), i);
			listOfMoves.Add (entry);
		}

		myPath = new path (listOfMoves);
		rb.velocity = myPath.getVelocity ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Get the old velocity
		Vector2 oldVelocity = rb.velocity;
		Vector2 newVelocity = myPath.getVelocity ();

		//Set the current step and prepare for next step
		rb.velocity = newVelocity;
		myPath.setNextStep();

		//Change the rotation
		if(oldVelocity.normalized != newVelocity.normalized && oldVelocity!=Vector2.zero){
			Vector2 difference = (newVelocity).normalized;
			rotAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

			//Set the stack for previous moves
			move curMove = new move(newVelocity.normalized, transform.position, rotAngle, rotSpeed);
			prevMoves.Add (curMove);
		}

		//Apply the rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotAngle), rotSpeed);
	}
}
