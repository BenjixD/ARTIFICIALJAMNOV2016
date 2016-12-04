using UnityEngine;
using System.Collections;

public class meteorSoundScript : MonoBehaviour {

    GameObject soundController;
    soundEffectScript soundscript;

    AudioClip meteorExplosion;
    AudioSource sounds;
    public float meteorExpVol = 0.2f;

    void Start()
    {
        sounds = GetComponentInChildren<AudioSource>();

        soundController = GameObject.Find("SoundController");
        soundscript = soundController.GetComponent<soundEffectScript>();

        meteorExplosion = soundscript.meteorExplosion;
    }

    public void playMeteorExp()
    {
        sounds.PlayOneShot(meteorExplosion, meteorExpVol);
    }
}
