using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedAlert : MonoBehaviour {

    public float blinkingSpeed;

    private bool weedAlertOn;
    private bool upDown;
    private Color changeColor;
    private Color originalColor;
    MeshRenderer m_renderer;
    
    // Use this for initialization
    void Start () {
        m_renderer = GetComponent<MeshRenderer>();
        originalColor = m_renderer.material.color;
        weedAlertOn = false;
        upDown = false;
        changeColor = new Color(0f, blinkingSpeed, blinkingSpeed, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(weedAlertOn)
        {
            if(upDown)
            {
                m_renderer.material.color = m_renderer.material.color + changeColor;
                if(m_renderer.material.color.g >= .7)
                {
                    upDown = false;
                }
            } else
            {
                m_renderer.material.color = m_renderer.material.color - changeColor;
                if (m_renderer.material.color.g <= .3)
                {
                    upDown = true;
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weedAlertOn = !weedAlertOn;
            m_renderer.material.color = originalColor;
        }
    }

    public void StartWeedAlarm()
    {
        weedAlertOn = true;
    }

    public void StopWeedAlarm()
    {
        weedAlertOn = false;
        m_renderer.material.color = originalColor;
    }
}
