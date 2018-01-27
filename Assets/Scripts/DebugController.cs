using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && GameManager.Instance.gameStateMachine.currentState == StateType.Playing) {
            Debug.Log("Bad End will be initiated");
            GameManager.Instance.gameStateMachine.bad = true;
            GameManager.Instance.gameStateMachine.AdvanceState();
        } else if(Input.GetKey(KeyCode.G) && GameManager.Instance.gameStateMachine.currentState == StateType.Playing) {
            Debug.Log("Good End will be initiated");
            GameManager.Instance.gameStateMachine.bad = false;
            GameManager.Instance.gameStateMachine.AdvanceState();
        }
    }
}