using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffController : MonoBehaviour {

    private void OnMouseDown()
    {
        if (GameManager.Instance.gameStateMachine.currentState == StateType.Intro)
        {
            return;
        }

        if (GameManager.Instance.gameStateMachine.currentState == StateType.Off)
        {
            GameManager.Instance.gameStateMachine.AdvanceState();
        } else
        {
            Debug.Log("Performing Quit");
            Application.Quit();
        }
    }
}
