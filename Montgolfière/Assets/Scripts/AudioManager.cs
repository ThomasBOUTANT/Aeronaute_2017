using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool blockMusic;

    private AudioSource audiosourcePlayer;
    private AudioSource audiosource;

    // Use this for initialization
    void OnEnable () {
        audiosourcePlayer = player.GetComponent<AudioSource>();
        audiosource = GetComponent<AudioSource>();
        audiosource.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {

        audiosource.volume += 0.1f * Time.deltaTime ;

        if (blockMusic)
        {
            audiosourcePlayer.mute = true;
        }
        else
        {
            audiosourcePlayer.mute = false;
        }

	}
}
