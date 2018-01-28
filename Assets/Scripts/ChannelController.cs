using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelController : MonoBehaviour {
    private AudioSource[] radioChannels;
    private AudioSource RadioTuning;
    private AudioSource RadioStatic;
    private AudioSource[] audioSources;
    private int totalAudioSources;

    private AudioClip[] radioSFX;
    private AudioClip[][] policeClips;
    private AudioClip[] weedmanClips;

    private const string VO_PATH = "Sounds/VO";
    private const float VOL_DELTA = 0.1f;
    private const int POLICE_CHANNELS = 3;
    private const int RADIO_SFX_SOURCES = 2;

    private int lastPlayingStation = 0;


    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        totalAudioSources = audioSources.Length;

        RadioStatic = audioSources[0];
        RadioTuning = audioSources[1];

        radioChannels = new AudioSource[totalAudioSources - RADIO_SFX_SOURCES];

        for (int c = 0; c < totalAudioSources - RADIO_SFX_SOURCES; c++)
        {
            radioChannels[c] = audioSources[c + RADIO_SFX_SOURCES];
        }
        

        radioSFX = Resources.LoadAll<AudioClip>("Sounds/SFX");
        weedmanClips = Resources.LoadAll<AudioClip>(VO_PATH + "Weedman");
        policeClips = new AudioClip[3][];
        for (int p = 0; p < POLICE_CHANNELS; p++)
        {
            string path = VO_PATH + "/Channel" + (p + 1);
            policeClips[p] = Resources.LoadAll<AudioClip>(path);

            assignClipToSourceAndPlay(policeClips[p][0], radioChannels[p]);
        }

        assignClipToSourceAndPlay(radioSFX[0], RadioStatic);
        assignClipToSourceAndPlay(radioSFX[1], RadioTuning);
    }

    private void assignClipToSourceAndPlay(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.volume = 0;
        source.loop = true;
        source.Play();
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
        else if (frequency >= 417.37 && frequency <= 422)
            return 3;
        else
            return -1;
            
    }

	// Update is called once per frame
	void Update () {
        bool dialMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        int currentChannel = ReturnChannel(DialRotation.GetFrequency());

        if (dialMoving)
        {
            if (RadioTuning.volume < 1)
            {
                RadioTuning.volume += VOL_DELTA;
            }
        }else
        {
            RadioTuning.volume -= VOL_DELTA;
        }

        
        if (currentChannel >= 0)
        {
            //Dial can hear a radio station
            radioChannels[currentChannel].volume += VOL_DELTA;
            if (currentChannel - 1 >= 0)
            {
                radioChannels[currentChannel - 1].volume -= VOL_DELTA;
            }
            if (currentChannel + 1 < radioChannels.Length)
            {
                radioChannels[currentChannel + 1].volume -= VOL_DELTA;
            }
            if (RadioStatic.volume > 0)
            {
                RadioStatic.volume -= VOL_DELTA;
            }
        }
        else
        {
            //Static, make the channels quieter
            if(RadioStatic.volume < 1)
            {
                RadioStatic.volume += VOL_DELTA;
            }
            foreach (AudioSource radioChannel in radioChannels)
            {
                if (radioChannel.volume > 0)
                {
                    radioChannel.volume -= VOL_DELTA;
                }
            }
        }

        if (currentChannel == 3)
        {
           //TODO Weed Channel, let Player interact with Map
        }
	}


}
