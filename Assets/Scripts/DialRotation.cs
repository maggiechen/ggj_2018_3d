using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotation : MonoBehaviour {

    public float RotationSpeed = 100f;
    public Color inactiveTextColor = new Color(0.6F, 0.2F, 0.6F, 1F);
    public Color activeTextColor = new Color(1F, 0.8F, 1F, 1F);
    public GameObject slider;
    public TextMesh frequency;

    private static float currentFrequency;
    private static bool dialLocked = false;

    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private void MoveSlider(float angle) {
        if (slider == null)
        {
            Debug.Log("Slider is unassigned");
        }
        else
        {
            //slider is clamped to (-0.045 to 0.045)
            slider.transform.localPosition = new Vector3(-angle / 200f, 0f, -0.07f);
        }
    }

    private void UpdateFrequency(float angle) {
        float angle2 = angle + 90f;
        float myFrequency = (float)Math.Round(Mathf.Clamp(420.69f - 0.28333f * angle2, 369.69f, 420.69f), 2);
        if (ChannelController.ReturnChannel (myFrequency) >= 0) {
            GameObject.Find("FreqText").GetComponent<TextMesh>().color= activeTextColor;
        }
        else
            GameObject.Find("FreqText").GetComponent<TextMesh>().color= inactiveTextColor;
        frequency.text = (myFrequency).ToString ();
        currentFrequency = myFrequency;
    }

    public static float GetFrequency() {
        return currentFrequency;
    }

    public static bool DialLocked()
    {
        return dialLocked;
    }

	// Update is called once per frame
	void Update () {
        float angle = WrapAngle(gameObject.transform.localRotation.eulerAngles.z);
        MoveSlider(angle);
        UpdateFrequency(angle);

        dialLocked = GameManager.Instance.gameStateMachine.currentState == StateType.Off 
            || GameManager.Instance.gameStateMachine.currentState == StateType.Intro 
            || GameManager.Instance.gameStateMachine.currentState == StateType.WeedManIntro;

        if (!dialLocked)
        {
            if ( (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && angle < 90f)
            {
                gameObject.transform.Rotate(0f, 0f, RotationSpeed);
            }
            else if ((Input.GetKey(KeyCode.RightArrow ) || Input.GetKey(KeyCode.D)) && angle > -90f)
            {
                gameObject.transform.Rotate(0f, 0f, -RotationSpeed);
            }
        }
	}
        
}
