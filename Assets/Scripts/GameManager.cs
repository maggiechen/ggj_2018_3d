using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager {
    private static GameManager instance;
    public GameStateMachine gameStateMachine;
    private GameManager()
    {
        // hooboy a game manager
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }
}
