using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour {
    void Update () {
        if (Input.GetKey(KeyCode.Space) && (GameManager.Instance.gameStateMachine.currentState == StateType.BadEnd ||
                                            GameManager.Instance.gameStateMachine.currentState == StateType.GoodEnd))
        {
            GameManager.Instance.gameStateMachine.AdvanceState();
        }
    }
}
