using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

    public int playerLife = 3;
    public float playerShield;
    public float maxShield = 300f;
    public bool shieldActive = false;
    public bool canRegen = false;

    void Start()
    {
        playerShield = maxShield;
    }

    void Update()
    {
        if (shieldActive)
        {
            playerShield -= 1;
        }
        else
        {
            if(playerShield < maxShield && canRegen)
            {
                playerShield += 1;
            }
        }
    }

    void setRegenTrue()
    {
        canRegen = true;
    }
}
