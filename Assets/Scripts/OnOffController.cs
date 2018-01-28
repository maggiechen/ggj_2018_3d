using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffController : MonoBehaviour {
    ChannelController channels;

    void Start()
    {
        channels = FindObjectOfType<ChannelController>();
    }

    private void OnMouseDown()
    {
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
