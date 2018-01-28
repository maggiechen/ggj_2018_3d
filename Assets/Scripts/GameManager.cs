using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager {
    private static GameManager instance;
    public GameStateMachine gameStateMachine = new GameStateMachine();

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
