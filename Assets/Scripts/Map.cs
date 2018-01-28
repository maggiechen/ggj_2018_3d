using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    Color originalColor;
    MeshRenderer m_renderer;

    ChannelController channels;

    // Use this for initialization
    void Start () {
        m_renderer = GetComponent<MeshRenderer>();
        originalColor = m_renderer.material.color;
        channels = FindObjectOfType<ChannelController>();
    }

    void OnMouseOver()
    {
        if (!DialRotation.DialLocked() && channels.TalkingToWeedman)
        {
            m_renderer.material.color = Color.blue;
        }
    }

    void OnMouseDown()
    {
        Debug.Log(this.name + " clicked");
        if (!DialRotation.DialLocked() && channels.TalkingToWeedman)
        {
            channels.InterruptWeedman();
            //TODO move Van here, if it's not here already
        }
    }

    void OnMouseExit()
    {
        m_renderer.material.color = originalColor;
    }
}
