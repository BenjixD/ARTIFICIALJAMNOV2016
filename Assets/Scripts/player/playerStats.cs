using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {


    public int playerLife = 3;
    bool canTakeDamage = true;
    public float playerShield;
    public float maxShield = 300f; // maxShield / 60 = time player can continuously use shield
    public bool shieldActive = false;
    public bool canRegen = false;

    public int numberOfFlicker = 20; // numberOfFlicker * 0.1 = time player is invulnerable when it takes damage
    public float timeOnFlicker = 0.05f;
    public float timeOffFlicker = 0.05f;

    void Start()
    {
        playerShield = maxShield;
    }

    void Update()
    {
        if (shieldActive)
        {
            canRegen = false;
            playerShield -= 1;
        }
        else
        {
            if (!(playerShield == maxShield))
            {
                Invoke("setRegenTrue", 3f);
                if (playerShield < maxShield && canRegen)
                {
                    playerShield += 1;
                }
            }
        }

        if(Input.GetKeyDown("h"))
        {
            playerTakeDamage();
        }
    }



    void setRegenTrue()
    {
        canRegen = true;
    }

    public void playerTakeDamage()
    {
        if (playerLife > 1 && canTakeDamage)
        {
            playerLife -= 1;
            StartCoroutine(playerInvul(numberOfFlicker, timeOnFlicker, timeOffFlicker));
        }
        else if (playerLife == 1 && canTakeDamage)
        {
            canTakeDamage = false;
            StartCoroutine(killPlayer(4f));
        }
    }

    IEnumerator killPlayer(float time)
    {
        Vector3 scale = this.gameObject.GetComponent<Transform>().localScale;
        scale.y *= -1;
        this.gameObject.GetComponent<Transform>().localScale = scale;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<playerController>().enabled = false;
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    IEnumerator playerInvul(int nTimes, float timeOn, float timeOff)
    {
        canTakeDamage = false;
        while (nTimes > 0)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(timeOn);
            this.gameObject.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(timeOff);
            nTimes--;
        }
        this.gameObject.GetComponent<Renderer>().enabled = true;
        canTakeDamage = true;
    }
}
