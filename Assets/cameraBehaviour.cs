﻿using UnityEngine;
using System.Collections;

public class cameraBehaviour : MonoBehaviour {

	public GameObject refObj; 
	public float colliderMargin = 2f; //This is collider offset from camera view
	public float cameraSpeedModifier = 2f; //Camera Speed
	public float boundaries = 1f; //boundaries

	private Camera camera;
	private Vector2 cameraPos;
	private Vector2 cameraSize;

	private BoxCollider2D limitColliderTop;	//Outer top collider (set to camera view size)
	private BoxCollider2D limitColliderRight;	//Outer right collider (set to camera view size)
	private BoxCollider2D limitColliderBottom;	//Outer Bottom collider (set to camera view size)
	private BoxCollider2D limitColliderLeft;	//Outer Left collider (set to camera view size)

	private GameObject margin;	//Margin for camera moving

	private bool moveCamera = true;
	// Use this for initialization
	void Start () {
		//Get camera size and position
		camera = GetComponent<Camera>();
		cameraPos = transform.position;
		cameraSize.x = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, 0))) - boundaries;
		cameraSize.y = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight))) - boundaries;
		Debug.Log (cameraSize);
	

		//Init colliders
		limitColliderTop = gameObject.AddComponent<BoxCollider2D>();
		limitColliderRight = gameObject.AddComponent<BoxCollider2D>();
		limitColliderBottom = gameObject.AddComponent<BoxCollider2D>();
		limitColliderLeft = gameObject.AddComponent<BoxCollider2D>();

		margin = new GameObject("margin");
		margin.transform.parent = gameObject.transform;  


		//Set the collider positions
		limitColliderTop.offset = new Vector2(0, cameraSize.y * 0.5f+ colliderMargin * 0.5f) + cameraPos;
		limitColliderTop.size = new Vector2 (cameraSize.x, colliderMargin);

		limitColliderRight.offset = new Vector2(cameraSize.x * 0.5f +  colliderMargin * 0.5f, 0) + cameraPos;
		limitColliderRight.size = new Vector2 (colliderMargin, cameraSize.y);

		limitColliderBottom.offset = new Vector2(0, -1 * cameraSize.y * 0.5f - colliderMargin * 0.5f) + cameraPos;
		limitColliderBottom.size = new Vector2 (cameraSize.x, colliderMargin);

		limitColliderLeft.offset = new Vector2(-1 * cameraSize.x * 0.5f - colliderMargin * 0.5f, 0) + cameraPos;
		limitColliderLeft.size = new Vector2 (colliderMargin, cameraSize.y);

		BoxCollider2D marginCollider = margin.gameObject.AddComponent<BoxCollider2D>();
		marginCollider.isTrigger = true;
		marginCollider.size = new Vector2 (cameraSize.x * 0.5f, cameraSize.y * 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (moveCamera) {
			Vector3 refVelocity = refObj.transform.position - transform.position;
			//GetComponent<Rigidbody2D>().velocity = new Vector2(refVelocity.x * cameraSpeed, refVelocity.y * cameraSpeed);  
			GetComponent<Rigidbody2D>().velocity = refVelocity * cameraSpeedModifier;
		}
	}

	/*
	void OnTriggerExit2D(Collider2D col){
		GameObject reference = col.gameObject;
		if (reference == refObj) {
			moveCamera = true;
		}
		Debug.Log ("Not Colliding");
	}


	void OnTriggerStay2D(Collider2D col){
		moveCamera = false;
		Debug.Log ("Colliding");
	}*/
}