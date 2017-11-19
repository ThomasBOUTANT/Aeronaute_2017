using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool blockMusic;

    private AudioSource audiosource;

	// Use this for initialization
	void Start () {
        audiosource = player.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (blockMusic)
        {
            audiosource.mute = true;
        }
        else
        {
            audiosource.mute = false;
        }
		
	}
}
