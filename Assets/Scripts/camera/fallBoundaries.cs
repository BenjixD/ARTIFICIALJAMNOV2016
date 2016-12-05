using UnityEngine;
using System.Collections;

public class fallBoundaries : MonoBehaviour {
	public float boundaries = 1f; //boundaries
	public float colliderMargin = 2f; //This is collider offset from camera view

	private Camera camera;
	private Vector2 cameraPos;
	private Vector2 cameraSize;

	private GameObject bounceZone;	//The area where you'll get pushed back (and take damage)


	// Use this for initialization
	void Start () {
		//Get camera size and position
		camera = GetComponent<Camera>();
		cameraPos = transform.position;
		cameraSize.x = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, 0))) - boundaries;
		cameraSize.y = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight))) - boundaries;

		//Set the position of the bounce zone
		bounceZone = new GameObject("bounceZone");
		bounceZone.transform.parent = gameObject.transform; 

		//Spawn the collider as trigger
		BoxCollider2D bounceBottom = bounceZone.AddComponent<BoxCollider2D>();;	//Outer Bottom collider (set to camera view size)
		bounceBottom.isTrigger = true;
		bounceBottom.gameObject.layer = LayerMask.NameToLayer("BounceZone");

		//Create the size of the collider
		bounceBottom.offset = new Vector2(0, -1 * cameraSize.y * 0.5f - colliderMargin * 0.5f) + cameraPos;
		bounceBottom.size = new Vector2 (cameraSize.x, colliderMargin);
	}
}
