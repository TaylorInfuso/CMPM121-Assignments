using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScipt : MonoBehaviour {
    public AudioClip desertSoundClip;
    public AudioSource desertSoundSource;
    // Use this for initialization
    void Start () {
        desertSoundSource.clip = desertSoundClip;
	}
	
	// Update is called once per frame
	void Update () {
        desertSoundSource.Play();

    }
}
