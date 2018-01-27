using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelController : MonoBehaviour {
    public AudioSource channel_1;
    public AudioSource channel_2;
    public AudioSource channel_3;
    public AudioSource channel_weed;
    public AudioSource Static;

    private AudioClip[] radioSFX;

	// Use this for initialization
	void Start () {
        radioSFX = Resources.LoadAll<AudioClip>("Sounds/SFX");

        Static.clip = radioSFX[0];
	}
	
	// Update is called once per frame
	void Update () {
        bool dialMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        if (dialMoving)
        {
            if (!Static.isPlaying)
            {
                Static.Play();
            }
        }else
        {
            //TODO check if on a station
            Static.Stop();
        }
	}


}
