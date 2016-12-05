using UnityEngine;
using System.Collections;

public class PlayerMeteor : MonoBehaviour {

    playerStats playerStatScript;

    void Start()
    {
        playerStatScript = this.gameObject.GetComponent<playerStats>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Meteor")
        {
            playerStatScript.playerTakeDamage();
        }
    }
}
