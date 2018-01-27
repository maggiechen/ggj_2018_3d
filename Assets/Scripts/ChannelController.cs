using System.Collections;
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
            RadioTuning.volume -= 0.1f;
        }
	}


}
