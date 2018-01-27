using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFaderController : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public float fadeRate;
	// Use this for initialization
	void Start () {
        if (GameManager.Instance.gameStateMachine.currentState == StateType.Intro ||
            GameManager.Instance.gameStateMachine.currentState == StateType.BadEnd ||
            GameManager.Instance.gameStateMachine.currentState == StateType.GoodEnd)
        {
            canvasGroup.alpha = 1;
        }
	}

    public void StartFadingToClear()
    {
        StartCoroutine(FadeToClear());
    }

    IEnumerator FadeToClear()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeRate;
            yield return null;
        }
        if (GameManager.Instance.gameStateMachine.currentState == StateType.Intro)
        {
            GameManager.Instance.gameStateMachine.AdvanceState();
        }
    }
}
