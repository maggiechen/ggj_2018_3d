using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelController : MonoBehaviour {
    private AudioSource[] radioChannels;
    private AudioSource RadioTuning;
    private AudioSource[] audioSources;

    private AudioClip[] radioSFX;
    private AudioClip[] weedmanClips;

    private const string VO_PATH = "Sounds/VO";
    private const float VOL_DELTA = 0.1f;

    // Use this for initialization
    void Start () {
        audioSources = GetComponents<AudioSource>();
        RadioTuning = audioSources[0];
        radioChannels = new AudioSource[audioSources.Length - 1];
        for (int c = 1; c < audioSources.Length; c++)
        {
            radioChannels[c - 1] = audioSources[c];
        }
        

        radioSFX = Resources.LoadAll<AudioClip>("Sounds/SFX");
        weedmanClips = Resources.LoadAll<AudioClip>(VO_PATH + "Weedman");

        RadioTuning.clip = radioSFX[1];
        RadioTuning.volume = 0;
        RadioTuning.loop = true;
        RadioTuning.Play();
	}
	
    //returns 0 - 2 for police channels
    //        3 for weed
    //       -1 for nothing
    public static int ReturnChannel(float frequency){
        if (frequency >= 369.69 && frequency <= 373.01)
            return 0;
        else if (frequency >= 388.90 && frequency <= 393.10)
            return 1;
        else if (frequency >= 407.63 && frequency <= 410.20)
            return 2;
        else if (frequency == 420.69)
            return 3;
        else
            return -1;
            
    }

	// Update is called once per frame
	void Update () {
        bool dialMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        int currentChannel = ChannelController.ReturnChannel(DialRotation.GetFrequency());

        if (dialMoving)
        {
            if (RadioTuning.volume < 1)
            {
                RadioTuning.volume += VOL_DELTA;
            }
        }else
        {
            if ( currentChannel < 0) {
                
            }
                //TODO: Play staticSound OR Random channel

            RadioTuning.volume -= VOL_DELTA;
        }

        //Dial can hear a radio station
        //TODO THIS IS NOT TESTED
        if (currentChannel >= 0)
        {
            radioChannels[currentChannel].volume += VOL_DELTA;
            if (currentChannel - 1 >= 0)
            {
                radioChannels[currentChannel - 1].volume -= VOL_DELTA;
            }
            if (currentChannel + 1 < radioChannels.Length)
            {
                radioChannels[currentChannel + 1].volume -= VOL_DELTA;
            }
        }
        if (currentChannel == 3)
        {
            //TODO Weed Channel, let Player interact with Map
        }
	}


}
