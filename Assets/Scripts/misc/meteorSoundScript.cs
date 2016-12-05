using UnityEngine;
using System.Collections;

public class meteorSoundScript : MonoBehaviour {

    GameObject soundController;
    soundEffectScript soundscript;

    AudioClip meteorExplosion;
    AudioSource sounds;
    public float meteorExpVol = 0.2f;

	void Awake(){
		sounds = GetComponentInChildren<AudioSource>();
		soundController = GameObject.Find("SoundController");
		soundscript = soundController.GetComponent<soundEffectScript>();

		meteorExplosion = soundscript.meteorExplosion;
	}

    void Start()
    {
       
    }

    public void playMeteorExp()
    {
        sounds.PlayOneShot(meteorExplosion, meteorExpVol);
    }
}
