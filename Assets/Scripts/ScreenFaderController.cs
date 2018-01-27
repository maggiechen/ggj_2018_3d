using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFaderController : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public float fadeRate;
	// Use this for initialization
	void Start () {
        GameManager gameInstance = GameManager.Instance;
        if (gameInstance.gameStateMachine.currentState == StateType.Intro)
        {
            canvasGroup.alpha = 1;
            StartCoroutine(FadeToClear());
        }
	}
	
    IEnumerator FadeToClear()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeRate;
            yield return null;
        }
    }
}
