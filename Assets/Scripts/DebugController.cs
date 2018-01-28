using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour
{
    ChannelController channels;
    void Start()
    {
        channels = FindObjectOfType<ChannelController>();
    }

    void Update()
    {
        //GameManager.Instance.gameStateMachine.currentState = StateType.Off;
        if (Input.GetKey(KeyCode.B)) {
            Debug.Log("Bad End will be initiated");
            GameManager.Instance.gameStateMachine.bad = true;
            GameManager.Instance.gameStateMachine.AdvanceState();
        } else if(Input.GetKey(KeyCode.G)) {
            Debug.Log("Good End will be initiated");
            GameManager.Instance.gameStateMachine.bad = false;
            GameManager.Instance.gameStateMachine.AdvanceState();
        } else if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("Skip Weedman's Intro!");
            channels.SkipIntro();
        }
    }
}