using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class moveDragon : dragonPath {

	public Vector2 startingVelocity;	//Starting velocity	
	public GameObject gameObj;			//referencing piece
	private dragonPath reference;		//referencing move
	private Rigidbody2D rb;				//me

	//Called at the beginning
	void Start(){
		prevMoves = new List<move>();					//Instantiate instance
		reference = gameObj.GetComponent<dragonPath>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = gameObj.GetComponent<Rigidbody2D>().velocity;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (reference.prevMoves.Count == 0) {
			transform.rotation = reference.transform.rotation;
			rb.velocity = gameObj.GetComponent<Rigidbody2D>().velocity;
		} 
		else {
			if (Mathf.Abs(transform.position.x - reference.prevMoves [0].position.x) <= 0.020 &&
				Mathf.Abs(transform.position.y - reference.prevMoves [0].position.y) <= 0.020) {
				//Update the change in direction and rotation
				transform.position = reference.prevMoves[0].position;
				rb.velocity = reference.prevMoves[0].direction * reference.GetComponent<Rigidbody2D> ().velocity.magnitude;
				transform.rotation = Quaternion.Slerp (transform.rotation, 
					Quaternion.Euler (0.0f, 0.0f, reference.prevMoves[0].rotAngle), 
					reference.prevMoves[0].rotSpeed);

				//Add the change to our own queue
				prevMoves.Add (new move (reference.prevMoves [0].direction,
					reference.prevMoves [0].position,
					reference.prevMoves [0].rotAngle,
					reference.prevMoves [0].rotSpeed));

				//Pop from our reference's queue
				reference.prevMoves.RemoveAt (0);
			} 
			else {
				//Move towards our target location (we are aleady facing the right direction)
				rb.velocity = gameObj.GetComponent<Rigidbody2D>().velocity.magnitude * 
					(reference.prevMoves[0].position - transform.position).normalized;
			}
		}
	}

/*
	IEnumerator rotateCell(object[] parameters){
		Quaternion start = (Quaternion)parameters [0];
		float rotAngle = (float)parameters [1]; 
		float rotSpeed = (float)parameters [2];
		while (transform.rotation != Quaternion.Euler (0.0f, 0.0f, rotAngle)) {
			transform.rotation = Quaternion.Slerp (start, 
				Quaternion.Euler (0.0f, 0.0f, rotAngle), 
				rotSpeed);
			yield return new WaitForFixedUpdate();
		}
	}
*/
}
