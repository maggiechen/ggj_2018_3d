using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroller : MonoBehaviour {
    public ScrollRect scrollRect;
    public CanvasGroup canvasGroup;
    public ScreenFaderController screenFaderController;
    public float scrollRate;
    public float fadeRate;
	void Start () {
        if (GameManager.Instance.gameStateMachine.currentState == StateType.Intro)
        {
            scrollRect.verticalNormalizedPosition = 1;
            StartCoroutine(ScrollDown());
        }
    }

    IEnumerator ScrollDown () {
        while (scrollRect.verticalNormalizedPosition > 0)
        {
            scrollRect.verticalNormalizedPosition -= scrollRate;
            yield return null;
        }

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeRate;
            yield return null;
        }

        if (GameManager.Instance.gameStateMachine.currentState == StateType.Intro)
        {
            screenFaderController.StartFadingToClear();
        }
    }
}
