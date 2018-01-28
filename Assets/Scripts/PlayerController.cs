using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CopController cops;

    private string playerPosition;

	// Use this for initialization
	void Start () {
        playerPosition = "Area1";
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetKey(KeyCode.Q))
        {
            MovePlayer("Area1");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            MovePlayer("Area2");
        }
        else if (Input.GetKey(KeyCode.E))
        {
            MovePlayer("Area3");
        }
        else if (Input.GetKey(KeyCode.R))
        {
            MovePlayer("Area4");
        }
        if (cops.checkForCop(playerPosition))
        {
            GameManager.Instance.gameStateMachine.bad = true;
            GameManager.Instance.gameStateMachine.AdvanceState();
            if (RadioTimer.instance.GetTime() >= 300)
            {
                GameManager.Instance.gameStateMachine.bad = false;
                GameManager.Instance.gameStateMachine.AdvanceState();
            }
        }
	}

    public void MovePlayer(string areaName)
    {
        playerPosition = areaName;
    }
}
