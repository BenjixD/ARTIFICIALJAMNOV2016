using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class dragonPath : MonoBehaviour {
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
}
