using UnityEngine;
using System.Collections;

public class soundEffectScript : MonoBehaviour {

    public AudioSource jumpSource;
    public AudioSource attackSource;
    public AudioSource atkHitSource;
    public AudioSource meteorSource;
    public AudioSource dragonRoarSource;
    public AudioSource dragonDeathSource;

    public AudioClip jumpSound;
    public AudioClip swordSwing;
    public AudioClip SwordSlash;
    public AudioClip meteorExplosion;
    public AudioClip dragonRoar;
    public AudioClip dragonDeath;

    public float jumpSoundVol = 0.1f;
    public float swordSwingVol = 0.1f;
    public float swordHitVol = 0.1f;
    public float meteorExpVol = 0.2f;
    public float dragonRoarVol = 0.2f;
    public float dragonDeathVol = 0.4f;

    public void playJumpSound()
    {
        jumpSource.PlayOneShot(jumpSound, jumpSoundVol);
    }

    public void playSwordSwing()
    {
        attackSource.PlayOneShot(swordSwing, swordSwingVol);
    }

    public void playSwordHit()
    {
        atkHitSource.PlayOneShot(SwordSlash, swordHitVol);
    }

    public void playMeteorExp()
    {
        meteorSource.PlayOneShot(meteorExplosion, meteorExpVol);
    }

    public void playDragonRoar()
    {
        dragonRoarSource.PlayOneShot(dragonRoar, dragonRoarVol);
    }

    public void playDragonDeath()
    {
        dragonDeathSource.PlayOneShot(dragonDeath, dragonDeathVol);
    }

}
