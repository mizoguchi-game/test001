using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource audio;

    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;

    // Use this for initialization
    void Start () {
        audio = gameObject.AddComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Orange") {
            audio.PlayOneShot(sound01);
        } else if (other.gameObject.tag == "Block"){
            audio.PlayOneShot(sound02);
        }
        else {
            audio.PlayOneShot(sound03);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
