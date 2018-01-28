using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffController : MonoBehaviour {
    ChannelController channels;
    bool isOn;
    MeshRenderer spriteCan;
    public Material mat;
    void Start()
    {
        channels = FindObjectOfType<ChannelController>();
        isOn = false;
        spriteCan = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        if (!isOn) {
            Material[] mats = spriteCan.materials;
            mats[0] = mat;
            spriteCan.materials = mats;
        }

        if (GameManager.Instance.gameStateMachine.currentState == StateType.Intro)
        {
            return;
        }

        if (GameManager.Instance.gameStateMachine.currentState == StateType.Off)
        {
            GameManager.Instance.gameStateMachine.AdvanceState();
            channels.TriggerIntro();
        } else
        {
            Debug.Log("Performing Quit");
            Application.Quit();
        }
    }
}
