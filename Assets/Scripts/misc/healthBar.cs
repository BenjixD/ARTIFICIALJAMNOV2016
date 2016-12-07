using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class healthBar : MonoBehaviour {

	public GameObject player; //reference the player
	public GameObject lifeUI; //The lifebars
	public float spawnOffset; //Offset spawning of hp bar to the left

	public Sprite fullSprite; //Full sprite
	public Sprite emptySprite; //Empty sprite

	private playerStats pstats; 		//stats of the player
	private IList<GameObject> health = new List<GameObject>(); // health
	private int currHealth;	//current health number

	// Use this for initialization
	void Start () {
		pstats = player.GetComponent<playerStats> ();
		//Generate the HP bars
		for (int i = 0; i < pstats.playerLife; i++) {
			GameObject life = Instantiate (lifeUI, transform.position + new Vector3(i*60f+spawnOffset,0,0), Quaternion.identity) as GameObject;
			life.transform.SetParent(transform, false);	//Set parent to be me
			health.Add(life);
		}
		//Fill in the hp bars if there are enough hp
		for(int i = 0; i < pstats.playerLife; i++){
			health [i].GetComponent<Image> ().sprite = fullSprite;
		}

		currHealth = pstats.playerLife;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Update health bar every frame (only if health changes)
		if(currHealth != pstats.playerLife){
			currHealth = pstats.playerLife;
			for (int i = 0; i < health.Count; i++) {
				if (i + 1 <= pstats.playerLife) {
					health [i].GetComponent<Image> ().sprite = fullSprite;
				} 
				else {
					health [i].GetComponent<Image> ().sprite = emptySprite;
				}
			}
		}
	}
}
