using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameStateMachine
{
    private Dictionary<StateType, GameState> states = new Dictionary<StateType, GameState>()
    {
        {StateType.Intro, new IntroState()},
        {StateType.Off, new OffState()},
        {StateType.WeedManIntro, new WeedManIntroState()},
        {StateType.Playing, new PlayingState()},
        {StateType.Paused, new PausedState()},
        {StateType.BadEnd, new BadEndState()},
        {StateType.GoodEnd, new GoodEndState()}
    };

    public StateType currentState = StateType.Intro;
    public StateType previousState = StateType.Intro;

    public bool pauseRequested;
    public bool bad;

    List<int> locations = new List<int> { 0, 0, 0, 0};
    int weedVanLocation;
    int weedVanPrevLocation;

    public void MoveWeedVan(int newLocation)
    {
        weedVanPrevLocation = weedVanLocation;
        weedVanLocation = newLocation;
        if(locations[newLocation] > 0)
        {
            bad = true;
            AdvanceState();
        }
    }

    public void InsertAtLocation(int location)
    {
        locations[location]++;
        if (weedVanLocation == location)
        {
            bad = true;
            AdvanceState();
        }
    }

    public void DeleteCop(int from)
    {
        locations[from]--;
        if (locations[from] < 0)
        {
            throw new Exception("what the cyber can you count?!!");
        }
    }

    public GameStateMachine()
    {
        Debug.Log("State is now: Intro");
    }

    public void Pause()
    {
        pauseRequested = true;
        AdvanceState();
    }

    public void AdvanceState()
    {
        previousState = currentState;
        currentState = states[currentState].GetNextState(this);
        Debug.Log("State is now: " + Enum.GetName(typeof(StateType), currentState));
        if (currentState == StateType.Intro)
        {
            SceneManager.LoadSceneAsync("MainGame", LoadSceneMode.Single);
        }
        else if (currentState == StateType.BadEnd)
        {
            SceneManager.LoadSceneAsync("BadEnd", LoadSceneMode.Single);
        } else if (currentState == StateType.GoodEnd)
        {
            SceneManager.LoadSceneAsync("GoodEnd", LoadSceneMode.Single);
        }
    }
}

public enum StateType
{
    Intro,
    Off,
    Paused,
    BadEnd,
    GoodEnd,
    WeedManIntro,
    Playing
}

public interface GameState
{
    StateType GetNextState(GameStateMachine sm);
}

public class IntroState : GameState
{

    public StateType GetNextState(GameStateMachine sm)
    {
        return StateType.Off;
    }
}

public class OffState : GameState
{
    public StateType GetNextState(GameStateMachine sm)
    {
        return StateType.WeedManIntro;
    }
}

public class WeedManIntroState : GameState
{
    public StateType GetNextState(GameStateMachine sm)
    {
        return StateType.Playing;
    }
}

public class PlayingState : GameState
{
    public StateType GetNextState(GameStateMachine sm)
    {
        if (sm.pauseRequested)
        {
            return StateType.Paused;
        }
        else if (sm.bad)
        {
            return StateType.BadEnd;
        }
        else
        {
            return StateType.GoodEnd;
        }
    }
}

public class PausedState : GameState
{
    public StateType GetNextState(GameStateMachine sm)
    {
        sm.pauseRequested = false;
        return sm.previousState;
    }
}

public class BadEndState : GameState
{
    public StateType GetNextState(GameStateMachine sm)
    {
        return StateType.Intro;
    }
}

public class GoodEndState : GameState
{
    public StateType GetNextState(GameStateMachine sm)
    {
        return StateType.Intro;
    }
}
