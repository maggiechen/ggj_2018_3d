﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelController : MonoBehaviour {
    public AudioSource channel_1;
    public AudioSource channel_2;
    public AudioSource channel_3;
    public AudioSource channel_weed;
    public AudioSource RadioTuning;

    private AudioClip[] radioSFX;
    private AudioClip[] weedmanClips;

    private const string VO_PATH = "Sounds/VO";

    // Use this for initialization
    void Start () {
        radioSFX = Resources.LoadAll<AudioClip>("Sounds/SFX");
        weedmanClips = Resources.LoadAll<AudioClip>(VO_PATH + "Weedman");

        RadioTuning.clip = radioSFX[1];
        RadioTuning.volume = 0;
        RadioTuning.loop = true;
        RadioTuning.Play();
	}
	
    //returns 1 for channel 1
    //        2 for channel 2
    //        3 for channel 3
    //        4 for weed
    //       -1 for nothing
    int ReturnChannel(float frequency){
        if (frequency >= 369.69 && frequency <= 373.01)
            return 1;
        else if (frequency >= 388.90 && frequency <= 393.10)
            return 2;
        else if (frequency >= 407.63 && frequency <= 410.20)
            return 3;
        else if (frequency == 420.69)
            return 4;
        else
            return -1;
            
    }

	// Update is called once per frame
	void Update () {
        bool dialMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        if (dialMoving)
        {
            if (RadioTuning.volume < 1)
            {
                RadioTuning.volume += 0.1f;
            }
        }else
        {
            if(ReturnChannel(DialRotation.GetFrequency()) < 0)
                //TODO: Play staticSound OR Random channel

            RadioTuning.volume -= 0.1f;
        }
	}


}
