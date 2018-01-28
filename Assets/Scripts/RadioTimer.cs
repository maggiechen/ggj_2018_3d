using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTimer : MonoBehaviour {


    public static RadioTimer instance;
    public TextMesh timerText;
    private float timeElapsed = 30.0f;
    private bool timerRunning = false;
    bool startedTimerFirstTime = false;
    bool resetFirstTime = false;
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
        timeElapsed = 30.0f;
    }

    private int ConvertToHour(int minutes) {
        minutes += 11;
        if (minutes > 12) {
            minutes -= 12;
        }
        return minutes;
    }

    int prevSecondsDisplayed = 30;
	// Update is called once per frame
	void Update () {
        if (!resetFirstTime && GameManager.Instance.gameStateMachine.currentState == StateType.Off)
        {
            resetFirstTime = true;
            ResetTimer();
            prevSecondsDisplayed = 30;
            GameManager.Instance.ResetCops();
        }
        if (!startedTimerFirstTime && GameManager.Instance.gameStateMachine.currentState == StateType.WeedManIntro)
        {
            startedTimerFirstTime = true;
            StartTimer();
        }

        if (timerRunning) {
            timeElapsed += Time.deltaTime;
            int minutes = (int)Mathf.Floor(timeElapsed / 60f);
            int seconds = (int)Mathf.Floor(timeElapsed % 60f);
            string minutesText = (ConvertToHour(minutes) < 10) ? "0" + ConvertToHour(minutes).ToString() : ConvertToHour(minutes).ToString();
            string secondsText = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();
            timerText.text = minutesText + ":" + secondsText;

            if (prevSecondsDisplayed != seconds && seconds % 30 == 0)
            {
                GameManager.Instance.AdvanceCopMovements();
            }

            prevSecondsDisplayed = seconds;
        }
    }
}
