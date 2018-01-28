using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFlickerController : MonoBehaviour {
    public TextMesh textMesh;
    public float[] keyFrames;
    bool displayed = false;
    private void Start()
    {
        Color textColor = textMesh.color;
        textColor.a = 0;
        textMesh.color = textColor;
    }

    void Update()
    {
        if (!displayed && GameManager.Instance.gameStateMachine.currentState == StateType.WeedManIntro)
        {
            displayed = true;
            FlickerOn();
        }
    }

    void FlickerOn()
    {
        StartCoroutine(FlickerToOpaque());
    }

    IEnumerator FlickerToOpaque()
    {
        for (int i = 0; i < keyFrames.Length; i++)
        {
            float current = textMesh.color.a;
            if (keyFrames[i] < current)
            {
                while (textMesh.color.a > keyFrames[i])
                {
                    Color textColor = textMesh.color;
                    textColor.a = textColor.a - 0.06f;
                    textMesh.color = textColor;
                    yield return null;
                }
            } else
            {
                while (textMesh.color.a < keyFrames[i])
                {
                    Color textColor = textMesh.color;
                    textColor.a = textColor.a + 0.07f;
                    textMesh.color = textColor;
                    yield return null;
                }
            }
        }
        while (textMesh.color.a < 1)
        {
            Color textColor = textMesh.color;
            textColor.a = textColor.a + 0.04f;
            textMesh.color = textColor;
            yield return null;
        }
    }
}
