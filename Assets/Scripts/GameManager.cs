using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager {
    private static GameManager instance;
    public GameStateMachine gameStateMachine = new GameStateMachine();
    public List<Dictionary<int, int>> copMovementsByInterval = new List<Dictionary<int, int>>();
    private int copMovementIndex;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
                instance.copMovementIndex = 0;
                // populate cop movements
                // 00:30
                Dictionary<int, int> copMoves = new Dictionary<int, int>
                {
                    { 3, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 01:00
                copMoves = new Dictionary<int, int>
                {
                    {3, -1 },
                    {1, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 1:30
                copMoves = new Dictionary<int, int>{};
                instance.copMovementsByInterval.Add(copMoves);

                // 2:00
                copMoves = new Dictionary<int, int>
                {
                    {1, -1 },
                    {0, 1 },
                    {2, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 2:30
                copMoves = new Dictionary<int, int>
                {
                    {3, 1 },
                    {2, -1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 3:00
                copMoves = new Dictionary<int, int>
                {
                    {0, -1 },
                    {2, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 3:30
                copMoves = new Dictionary<int, int>
                {
                    {3, -1 },
                    {1, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 4:00
                copMoves = new Dictionary<int, int>
                {
                    {2, -1 },
                    {3, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);

                // 4:30
                copMoves = new Dictionary<int, int>
                {
                    {3, -1 },
                    {0, 1 },
                    {1, 1 }
                };
                instance.copMovementsByInterval.Add(copMoves);
                copMoves = new Dictionary<int, int>
                {
                    {3, 0 }, // 0 will represent grabbing the user's previous position
                };
                instance.copMovementsByInterval.Add(copMoves);
            }

            return instance;
        }
    }

    public void AdvanceCopMovements()
    {
        if (copMovementIndex == copMovementsByInterval.Count)
        {
            if (gameStateMachine.currentState == StateType.Playing)
            {
                gameStateMachine.AdvanceState();
            }
        } else
        {
            Dictionary<int, int> movements = copMovementsByInterval[copMovementIndex];
            
            foreach (KeyValuePair<int, int> entry in movements)
            {
                gameStateMachine.UpdateCops(entry.Key, entry.Value);
            }
            
            copMovementIndex++;
        }
    }
}
