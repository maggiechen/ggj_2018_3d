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

    private const string VO_PATH = "Sounds/VO/";
    private const float VOL_DELTA = 0.1f;
    private const int TOTAL_POLICE_CHANNELS = 3;
    private const int TOTAL_RADIO_SFX = 2;
    private const int WEEDMAN_CHANNEL = 3;

    private bool loopWeedmanRants = false;
    private int weedmanLoopIndex = 1; //or 2
    private int weedmanResponseIndex = 3; //to 5

    public bool TalkingToWeedman = false;
 
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();

        totalAudioSources = audioSources.Length;

        RadioStatic = audioSources[0];
        RadioTuning = audioSources[1];

        radioChannels = new AudioSource[totalAudioSources - TOTAL_RADIO_SFX];

        for (int c = 0; c < totalAudioSources - TOTAL_RADIO_SFX; c++)
        {
            radioChannels[c] = audioSources[c + TOTAL_RADIO_SFX];
        }
        

        radioSFX = Resources.LoadAll<AudioClip>("Sounds/SFX");
        weedmanClips = Resources.LoadAll<AudioClip>(VO_PATH + "Weedman");
        policeClips = new AudioClip[TOTAL_POLICE_CHANNELS][];

        for (int p = 0; p < TOTAL_POLICE_CHANNELS; p++)
        {
            string path = VO_PATH + "Channel" + (p + 1);
            policeClips[p] = Resources.LoadAll<AudioClip>(path);

            assignClipToSource(policeClips[p][0], radioChannels[p]);
        }
        assignClipToSource(weedmanClips[0], radioChannels[WEEDMAN_CHANNEL]);

        assignClipToSourceAndPlay(radioSFX[0], RadioStatic);
        assignClipToSourceAndPlay(radioSFX[1], RadioTuning);
    }

    private void assignClipToSource(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.volume = 0;
    }

    private void assignClipToSourceAndPlay(AudioClip clip, AudioSource source)
    {
        assignClipToSource(clip, source);
        source.loop = true;
        source.Play();
    }

    public void TriggerIntro()
    {
        radioChannels[WEEDMAN_CHANNEL].volume = 1;
        radioChannels[WEEDMAN_CHANNEL].Play();
        Invoke("FinishedIntro", weedmanClips[0].length);
    }

    public void FinishedIntro()
    {
        if(GameManager.Instance.gameStateMachine.currentState == StateType.WeedManIntro)
        {
            GameManager.Instance.gameStateMachine.AdvanceState();
        }

        for(int p = 0; p < TOTAL_POLICE_CHANNELS; p++)
        {
            AudioSource police = radioChannels[p];
            //TODO Don't actually loop, just for testing
            police.loop = true;
            police.Play();
        }

        radioChannels[WEEDMAN_CHANNEL].loop = false;
        radioChannels[WEEDMAN_CHANNEL].Stop();
    }

    public void SkipIntro()
    {
        CancelInvoke("FinishedIntro");
        FinishedIntro();
    }

    private void changeWeedmanLoopIndex()
    {
        radioChannels[WEEDMAN_CHANNEL].clip = weedmanClips[weedmanLoopIndex];
        if (weedmanLoopIndex == 1)
        {
            weedmanLoopIndex = 2;
        } else
        {
            weedmanLoopIndex = 1;
        }
        radioChannels[WEEDMAN_CHANNEL].Play();
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
	void Update ()
    {
        bool dialMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) 
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        int currentChannel = ReturnChannel(DialRotation.GetFrequency());

        if (!DialRotation.DialLocked())
        {
            handleTuningSound(dialMoving);
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
            if (currentChannel != WEEDMAN_CHANNEL)
            {
                loopWeedmanRants = true;
                TalkingToWeedman = false;
            }
        }
        else
        {
            //Static, make the channels quieter
            if (RadioStatic.volume < 0.5)
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

        if (currentChannel == WEEDMAN_CHANNEL)
        {
            //TODO: check if a pin was placed/button was pressed and give a stock response.
            radioChannels[WEEDMAN_CHANNEL].volume += VOL_DELTA;
            TalkingToWeedman = true;
        }

        //Check for weedman rant loops
        if (loopWeedmanRants && !radioChannels[WEEDMAN_CHANNEL].isPlaying)
        {
            changeWeedmanLoopIndex();
        }
    }

    public void InterruptWeedman()
    {
        radioChannels[WEEDMAN_CHANNEL].Stop();
        loopWeedmanRants = false;
        radioChannels[WEEDMAN_CHANNEL].clip = weedmanClips[pickWeedmanResponse()];
        radioChannels[WEEDMAN_CHANNEL].Play();
    }

    private int pickWeedmanResponse()
    {
        if(weedmanResponseIndex > 5)
        {
            weedmanResponseIndex = 3;
        }
        return weedmanResponseIndex++;
    }

    private void handleTuningSound(bool dialMoving)
    {
        if (dialMoving)
        {
            if (RadioTuning.volume < 0.5)
            {
                RadioTuning.volume += VOL_DELTA;
            }
        }
        else
        {
            RadioTuning.volume -= VOL_DELTA;
        }
    }
}
