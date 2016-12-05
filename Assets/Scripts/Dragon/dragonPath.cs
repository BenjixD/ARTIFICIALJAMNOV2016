using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class dragonPath : MonoBehaviour {
	public GameObject explosionAnim;	//Animation for explosion

	public class path_entry{
		public float duration;
		public Vector2 velocity;
		public int index;

		//Constructor
		public path_entry(float dur, Vector2 spe){
			duration = dur;
			velocity = spe;
			index = 0;
		}

		public path_entry(float dur, Vector2 spe, int ind){
			duration = dur;
			velocity = spe;
			index = ind;
		}

		public path_entry(path_entry p){
			duration = p.duration;
			velocity = p.velocity;
			index = p.index;
		}
	}

	public class path{
		public IList<path_entry> travelPath;
		public path_entry currentStep;

		//Constructor
		public path(IList<path_entry> tp){
			travelPath = tp;
			currentStep = new path_entry(travelPath[0]);
		}

		//Get the current Velocity of the current step
		public Vector2 getVelocity(){
			return currentStep.velocity;
		}

		//Set the next action for next frame
		public void setNextStep(){
			//Lessen the duration
			currentStep.duration -= 1;
			//Change steps if we're done with this one
			if (currentStep.duration <= 0) {
				//update data
				int newIndex = (currentStep.index + 1) % travelPath.Count;
				currentStep = new path_entry(travelPath [newIndex]);
			}
		}
	}
		
	public IList<move> prevMoves;			//My Previous change in direction
	public class move{
		public Vector2 direction;
		public Vector3 position;
		public float rotAngle;
		public float rotSpeed;

		public move (Vector2 dir, Vector3 pos, float angle, float speed){
			direction = dir;
			position = pos;
			rotAngle = angle;
			rotSpeed = speed;
		}
	}

	//DESTROY SECTION!!
	public void killSection(){
		//Stop all Scripts
		MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour script in scripts)
		{
			script.enabled = false;
		}

		//Freeze section
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		//Rotate piece randomly
		Quaternion rot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.y + Random.Range(-30, 30));
		transform.rotation = rot;

		//Explosion for 3 seconds!
		StartCoroutine(explosion(transform.position));
		StartCoroutine ("dropPieces");
	}

	IEnumerator explosion(Vector2 pos){
		float interval = 0.1f;
		for (float i = 2f; i >= 0f; i -= interval) {
			float ifDo = Random.Range (1f, 100f);
			if (ifDo <= 15) {
				Vector2 setExplosion = new Vector2 (pos.x + Random.Range (-0.5f, 0.5f), pos.y + Random.Range (-0.5f, 0.5f));
				//Debug.Log ("Explosion at: " + setExplosion);

				//spawn an explosion as a child of this!
				GameObject child = (GameObject) Instantiate(explosionAnim, setExplosion, Quaternion.identity);
				child.transform.parent = transform;
				Destroy(child, child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
			}
			yield return new WaitForSeconds(interval);
		}

		yield return null;
	}

	IEnumerator dropPieces(){
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		yield return new WaitForSeconds (2.5f);

		rb.isKinematic = false;
		rb.gravityScale = 2;
		rb.velocity = (new Vector2(Random.Range(-3f,3f),Random.Range(5f,30f)));

		yield return null;
	}
}
