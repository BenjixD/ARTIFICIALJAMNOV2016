using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnCloud : MonoBehaviour {

	public float spawnRate; //cloud spawn rate 
	public Sprite[] clouds;	//List of clouds

	private Camera camera;	//Camera
	private Vector2 cameraSize;	//size of camera

	// Use this for initialization
	void Start () {
		camera = transform.parent.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float ifDo = Random.Range (1f, 100f);
		if (ifDo <= spawnRate) {
			cameraSize.x = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, 0))) + 10f;
			cameraSize.y = Vector2.Distance (camera.ScreenToWorldPoint(new Vector2(0,0)),camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight)));

			GameObject cloud = new GameObject ("Cloud");
			cloud.transform.position = new Vector3 (0, 0, 0);
			cloud.transform.parent = transform;
			cloud.transform.localPosition = new Vector3 (cameraSize.x/2, Random.Range(-cameraSize.y/2, cameraSize.y/2), 10);
			cloud.transform.parent = null;
			//Add the sprite
			cloud.AddComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length - 1)];
			cloud.AddComponent<Rigidbody2D> ().isKinematic = true;
			cloud.GetComponent<Rigidbody2D> ().velocity = new Vector2(Random.Range (-1f, -5f), 0);
			Destroy (cloud, 30f);
			//StartCoroutine (moveCloud(cloud))
		}
	}
}
