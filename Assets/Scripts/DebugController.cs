using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour
{
    void Update()
    {
        GameManager.Instance.gameStateMachine.currentState = StateType.Off;
        if (Input.GetKey(KeyCode.B)) {
            Debug.Log("Bad End will be initiated");
            GameManager.Instance.gameStateMachine.bad = true;
            GameManager.Instance.gameStateMachine.AdvanceState();
        } else if(Input.GetKey(KeyCode.G)) {
            Debug.Log("Good End will be initiated");
            GameManager.Instance.gameStateMachine.bad = false;
            GameManager.Instance.gameStateMachine.AdvanceState();
        }
    }
}