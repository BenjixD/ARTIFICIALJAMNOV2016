using UnityEngine;
using System.Collections;

public class meteorShower : MonoBehaviour {

	public Camera cam;
	public float spawningProbability;
	public bool ifOn = false;
	//Spawn Boundaries
	public float margin_left;
	public float margin_right;
	public float margin_height;
	//Magnatude Boundaries
	public float lowerbound_mag;
	public float upperboud_mag;
	//Direction Boundaries
	public float lowerbound_angle;
	public float upperbound_angle;
	public GameObject player;
	public GameObject meteor;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float ifSpawning = Random.Range (0, 100);
		if(ifSpawning < spawningProbability && ifOn){
			float left = cam.ScreenToWorldPoint(new Vector2(0, 0)).x - margin_left;
			float right = cam.ScreenToWorldPoint (new Vector2(cam.pixelWidth, 0)).x + margin_right;
			float lowerHeight = cam.ScreenToWorldPoint (new Vector2(0, cam.pixelHeight)).y;
			float upperHeight = cam.ScreenToWorldPoint (new Vector2(0, cam.pixelHeight)).y + margin_height;

			//Spawn Point of the meteor
			Vector2 spawnPoint = new Vector2 (Random.Range(left, right), Random.Range(lowerHeight, upperHeight));
			//Velocity of meteor
			float angle = -1*(Vector2.Angle(((Vector2)player.transform.position - spawnPoint), Vector2.right) + Random.Range(lowerbound_angle*Mathf.Deg2Rad, upperbound_angle*Mathf.Deg2Rad))*Mathf.Deg2Rad;
			Vector2 meteorVelocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(lowerbound_mag, upperboud_mag); 
			//---Just for fun---//
			//float torque = 100000f;

			//Spawn Object and give it velocity and (torque)
			GameObject spawn = (GameObject)Instantiate(meteor, spawnPoint, Quaternion.identity);
			spawn.GetComponent<Rigidbody2D>().velocity = meteorVelocity;
			//spawn.GetComponent<Rigidbody2D>().AddTorque(torque);
		}
	}
}
