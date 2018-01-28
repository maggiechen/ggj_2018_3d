using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTimer : MonoBehaviour {


    public static RadioTimer instance;
    public TextMesh timerText;
    private float timeElapsed = 0.0f;
    private bool timerRunning = false;
    bool startedTimerFirstTime = false;
	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
	}

    public void StartTimer() {
        timerRunning = true;
    }

    public void StopTimer() {
        timerRunning = false;
    }

    public void ResetTimer() {
        StopTimer();
        timeElapsed = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!startedTimerFirstTime && GameManager.Instance.gameStateMachine.currentState == StateType.Playing)
        {
            startedTimerFirstTime = true;
            StartTimer();
        }

        if (timerRunning) {
            timeElapsed += Time.deltaTime;
            int minutes = (int)Mathf.Floor(timeElapsed / 60f);
            int seconds = Mathf.RoundToInt(timeElapsed % 60f);
            string minutesText = (minutes < 10) ? "0" + minutes.ToString() : minutes.ToString();
            string secondsText = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();
            timerText.text = minutesText + ":" + secondsText;
        }
	}
}
